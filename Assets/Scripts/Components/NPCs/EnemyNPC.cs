using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNPC : BaseNPC
{
    [Header("Cached Links")]
    public DetectorTarget DetectorTarget;

    public PlayerTriggerObserver PlayerTrigger;

    [Header("Enemy Settings")]
    public Material DetectedEnemyMaterial;
    public Material ActiveEnemyMaterial;

    public float MinSleepTime,
                 MaxSleepTime;

    public bool IsKilled { get; set; }
    public bool IsAwoke { get; set; }

    private List<MeshRenderer> _enemyRenderers;

    private float _currentSleepTime;

    private Vector3 _startPosition;

    private bool _isPlayerCaught;

    private void Awake()
    {
        GameManager.Instance.RegisteredEnemy(this);

        DetectorTarget.OnFullDetected += OnFullDetectedCallback;

        PlayerTrigger.OnPlayerEntered += OnPlayerTriggerEnteredCallback;
        PlayerTrigger.OnPlayerExited += OnPlayerTriggerExitedCallback;
    }

    private void Start()
    {
        _currentSleepTime = Random.Range(MinSleepTime, MaxSleepTime);

        _startPosition = transform.position;

        _enemyRenderers = GetComponentsInChildren<MeshRenderer>().ToList();
    }

    private void OnDestroy()
    {
        DetectorTarget.OnFullDetected -= OnFullDetectedCallback;

        PlayerTrigger.OnPlayerEntered -= OnPlayerTriggerEnteredCallback;
        PlayerTrigger.OnPlayerExited -= OnPlayerTriggerExitedCallback;
    }

    protected override void Update()
    {
        if (DetectorTarget.FullDetected || IsKilled)
            return;

        base.Update();

        if (_currentSleepTime > 0)
        {
            _currentSleepTime -= Time.deltaTime;

            if (_currentSleepTime <= 0)
                WakeUp();
        }
    }

    public void Kill()
    {
        PlayerTrigger.TriggerCollider.enabled = false;
        NavAgent.isStopped = true;
        IsKilled = true;
    }

    private void WakeUp()
    {
        var playerController = GameManager.Instance.GetPlayerController();
        SetTarget(playerController.transform);

        foreach (var render in _enemyRenderers)
            render.material = ActiveEnemyMaterial;

        IsAwoke = true;

        if (_isPlayerCaught)
            GameManager.Instance.PlayerCaught();
    }

    private void OnFullDetectedCallback()
    {
        NavAgent.destination = IsAwoke
             ? _startPosition
             : transform.position + GameManager.Instance.GetPlayerController().transform.forward;

        foreach (var renderer in _enemyRenderers)
            renderer.material = DetectedEnemyMaterial;
    }

    private void OnPlayerTriggerEnteredCallback(PlayerController playerController)
    {
        _isPlayerCaught = true;

        if (IsAwoke)
            GameManager.Instance.PlayerCaught();
    }

    private void OnPlayerTriggerExitedCallback() =>
        _isPlayerCaught = false;
}

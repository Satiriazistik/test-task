using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Cached Links")]
    public EnemyDetectorTarget DetectorTarget;

    public NavMeshAgent NavAgent;

    public EnemyPlayerTrigger PlayerTrigger;

    [Header("Enemy Settings")]
    public Material DetectedEnemyMaterial;

    public float MinSleepTime,
                 MaxSleepTime;


    private List<MeshRenderer> _enemyRenderers;

    private float _currentSleepTime;

    private Vector3 _startPosition;

    private bool _isKilled;

    private void Awake()
    {
        GameManager.Instance.RegisteredEnemy(this);

        DetectorTarget.OnFullDetected += OnFullDetectedCallback;
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
    }

    private void Update()
    {
        if (DetectorTarget.FullDetected || _isKilled)
            return;

        if (_currentSleepTime > 0)
            _currentSleepTime -= Time.deltaTime;

        if (_currentSleepTime <= 0)
        {
            NavAgent.destination = GameManager.Instance.GetPlayerPosition();
            PlayerTrigger.IsActive = true;
        }
    }

    public void Kill()
    {
        PlayerTrigger.IsActive = false;

        var triggerCollider = PlayerTrigger.GetComponent<Collider>();
        triggerCollider.enabled = false;

        NavAgent.speed = 0f;
        NavAgent.angularSpeed = 0f;

        _isKilled = true;
    }

    private void OnFullDetectedCallback()
    {
        NavAgent.destination = PlayerTrigger.IsActive
             ? _startPosition
             : transform.position + GameManager.Instance.GetPlayerController().transform.forward;
        foreach (var renderer in _enemyRenderers)
            renderer.material = DetectedEnemyMaterial;

        PlayerTrigger.IsActive = false;
    }

}

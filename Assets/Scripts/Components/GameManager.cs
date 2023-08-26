using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private List<EnemyNPC> _enemies = new List<EnemyNPC>();

    private PlayerController _playerController;

    private void Awake()
    {
        Instance = this;
    }

    public void RegisteredPlayer(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public void UnregisteredPlayer()
    {
        _playerController = null;
    }

    public void RegisteredEnemy(EnemyNPC enemy)
    {
        _enemies.Add(enemy);
    }

    public void UnregisteredEnemy(EnemyNPC enemy)
    {
        _enemies.Remove(enemy);
    }

    public void RegisteredFinishPoint(FinishPoint finishPoint)
    {
        finishPoint.OnPlayerEntered += OnPlayerEntererFinishPointCallback;
    }

    public void UnregisteredFinishPoint(FinishPoint finishPoint)
    {
        finishPoint.OnPlayerEntered -= OnPlayerEntererFinishPointCallback;
    }

    public PlayerController GetPlayerController() => _playerController;

    public Vector3 GetPlayerPosition() => _playerController.transform.position;

    public void PlayerCaught()
    {
        RaiseEndGameLogic();
        UIManager.Instance.ShowEndGameInfo(false, _enemies);
    }

    private void OnPlayerEntererFinishPointCallback(PlayerController playerController)
    {
        RaiseEndGameLogic();

        var isWin = true;
        foreach (var enemy in _enemies)
            if (!enemy.DetectorTarget.FullDetected)
                isWin = false;

        UIManager.Instance.ShowEndGameInfo(isWin, _enemies);
    }

    private void RaiseEndGameLogic()
    {
        DisableEnemies();

        _playerController.FreezePlayerMovement();
        _playerController.ScreenPointer.SetActive(false);
    }

    private void DisableEnemies()
    {
        foreach (var enemy in _enemies)
            enemy.Kill();
    }
}

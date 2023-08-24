using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private List<Enemy> _enemies = new List<Enemy>();

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

    public void RegisteredEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    public void UnregisteredEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }

    public void RegisteredFinishPoint(FinishPoint finishPoint)
    {
        finishPoint.OnPlayerEnterer += OnPlayerEntererFinishPointCallback;
    }

    public void UnregisteredFinishPoint(FinishPoint finishPoint)
    {
        finishPoint.OnPlayerEnterer -= OnPlayerEntererFinishPointCallback;
    }

    public PlayerController GetPlayerController() => _playerController;

    public Vector3 GetPlayerPosition() => _playerController.transform.position;

    public void PlayerCaught()
    {
        RaiseEndGameLogic();
        UIManager.Instance.ShowEndGameInfo(false, _enemies);
    }

    private void OnPlayerEntererFinishPointCallback()
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

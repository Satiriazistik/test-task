using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerTrigger : MonoBehaviour
{
    public bool IsActive { get; set; }

    public LayerMask PlayerLayer;

    private PlayerController _enteredPlayer;

    private bool _isPlayerCaught;

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & PlayerLayer.value) == 0)
            return;

        _enteredPlayer = other.GetComponentInParent<PlayerController>();

        if (IsActive)
        {
            GameManager.Instance.PlayerCaught();
            _isPlayerCaught = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & PlayerLayer.value) == 0)
            return;

        _enteredPlayer = null;
    }

    private void Update()
    {
        if (_isPlayerCaught)
            return;

        if (IsActive && _enteredPlayer != null)
            GameManager.Instance.PlayerCaught();
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerObserver : MonoBehaviour
{
    public Action<PlayerController> OnPlayerEntered;
    public Action OnPlayerExited;

    [Header("Cached Links")]
    public Collider TriggerCollider;

    [Header("Observer Settings")]
    public LayerMask PlayerLayer;

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & PlayerLayer.value) == 0)
            return;

        var enteredPlayer = other.GetComponentInParent<PlayerController>();

        OnPlayerEntered?.Invoke(enteredPlayer);
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & PlayerLayer.value) == 0)
            return;

        OnPlayerExited?.Invoke();
    }
}

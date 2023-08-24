using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public Action OnPlayerEnterer;

    public LayerMask PlayerLayer;

    private void Awake()
    {
        GameManager.Instance.RegisteredFinishPoint(this);
    }

    private void OnDestroy()
    {
        GameManager.Instance.UnregisteredFinishPoint(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & PlayerLayer.value) == 0)
            return;

        OnPlayerEnterer?.Invoke();
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : PlayerTriggerObserver
{
    private void Awake()
    {
        GameManager.Instance.RegisteredFinishPoint(this);
    }

    private void OnDestroy()
    {
        GameManager.Instance.UnregisteredFinishPoint(this);
    }
}

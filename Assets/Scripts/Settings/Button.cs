using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Button
{
    public Action OnButtonDown;
    public Action OnButtonUp;

    public KeyCode Key;

    public void UpdateButton()
    {
        if (Input.GetKeyDown(Key))
            OnButtonDown?.Invoke();

        if (Input.GetKeyUp(Key))
            OnButtonUp?.Invoke();
    }
}

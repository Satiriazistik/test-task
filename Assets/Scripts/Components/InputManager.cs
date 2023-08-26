using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [Header("Player Movement Settings")]
    public BaseKeyboardButton MoveForwardButton;
    public BaseKeyboardButton MoveBackButton;
    public BaseKeyboardButton MoveRightButton;
    public BaseKeyboardButton MoveLeftButton;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        //Input update
        MoveForwardButton.UpdateButton();
        MoveBackButton.UpdateButton();
        MoveRightButton.UpdateButton();
        MoveLeftButton.UpdateButton();
    }
}

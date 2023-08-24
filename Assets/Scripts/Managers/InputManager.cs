using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [Header("Player Movement Settings")]
    public Button MoveForwardButton;
    public Button MoveBackButton;
    public Button MoveRightButton;
    public Button MoveLeftButton;

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

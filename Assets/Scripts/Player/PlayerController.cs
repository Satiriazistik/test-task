using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Cached Links")]
    public CharacterController CharacterController;
    public GameObject ScreenPointer;

    [Header("Movement Settings")]
    public float MovementSpeed;
    public float MouseSensivity;

    [Header("Camera Settings")]
    public Transform HeadTransform;
    public float VerticalNearAngleTreshold;
    public float VerticalHighAngleTreshold;

    private Vector3 _currentMovement;

    private bool _moveForward,
                 _moveBack,
                 _moveRight,
                 _moveLeft;

    private bool _freezePlayerMovement;

    private void Awake()
    {
        InputManager.Instance.MoveForwardButton.OnButtonDown += MoveForwardOnButtonDownCallback;
        InputManager.Instance.MoveForwardButton.OnButtonUp += MoveForwardOnButtonUpCallback;

        InputManager.Instance.MoveBackButton.OnButtonDown += MoveBackOnButtonDownCallback;
        InputManager.Instance.MoveBackButton.OnButtonUp += MoveBackOnButtonUpCallback;

        InputManager.Instance.MoveRightButton.OnButtonDown += MoveRightOnButtonDownCallback;
        InputManager.Instance.MoveRightButton.OnButtonUp += MoveRightOnButtonUpCallback;

        InputManager.Instance.MoveLeftButton.OnButtonDown += MoveLeftOnButtonDownCallback;
        InputManager.Instance.MoveLeftButton.OnButtonUp += MoveLeftOnButtonUpCallback;

        if (HeadTransform == null)
            HeadTransform = Camera.main.transform;

        if (CharacterController == null)
            CharacterController = GetComponent<CharacterController>();

        GameManager.Instance.RegisteredPlayer(this);

        ScreenPointer.SetActive(true);
    }

    private void OnDestroy()
    {
        InputManager.Instance.MoveForwardButton.OnButtonDown -= MoveForwardOnButtonDownCallback;
        InputManager.Instance.MoveForwardButton.OnButtonUp -= MoveForwardOnButtonUpCallback;

        InputManager.Instance.MoveBackButton.OnButtonDown -= MoveBackOnButtonDownCallback;
        InputManager.Instance.MoveBackButton.OnButtonUp -= MoveBackOnButtonUpCallback;

        InputManager.Instance.MoveRightButton.OnButtonDown -= MoveRightOnButtonDownCallback;
        InputManager.Instance.MoveRightButton.OnButtonUp -= MoveRightOnButtonUpCallback;

        InputManager.Instance.MoveLeftButton.OnButtonDown -= MoveLeftOnButtonDownCallback;
        InputManager.Instance.MoveLeftButton.OnButtonUp -= MoveLeftOnButtonUpCallback;

        if (GameManager.Instance != null)
            GameManager.Instance.UnregisteredPlayer();
    }

    void Update()
    {
        if (_freezePlayerMovement)
            return;

        UpdatePlayerMovement();
        UpdatePlayerRotation();
    }

    void UpdatePlayerMovement()
    {
        _currentMovement = Vector3.zero;

        //Calculate movement
        if (_moveForward)
            _currentMovement += transform.forward;
        if (_moveBack)
            _currentMovement += -transform.forward;
        if (_moveRight)
            _currentMovement += transform.right;
        if (_moveLeft)
            _currentMovement += -transform.right;

        if (_currentMovement != Vector3.zero)
            CharacterController.Move(_currentMovement * MovementSpeed * Time.deltaTime);
    }

    private void UpdatePlayerRotation()
    {
        transform.rotation = transform.rotation * Quaternion.AngleAxis(Input.GetAxis("Mouse X") * MouseSensivity, Vector3.up);

        var cameraVerticalAngle = Vector3.Angle(transform.forward, HeadTransform.forward);
        var cross = Vector3.Cross(transform.forward, HeadTransform.forward);
        var dot = Vector3.Dot(cross, transform.right);
        if (dot > 0)
            cameraVerticalAngle *= -1f;

        var mouseYDrive = Input.GetAxis("Mouse Y");

        var canCameraVerticalRotate =
            cameraVerticalAngle <= VerticalNearAngleTreshold && mouseYDrive > 0
            || cameraVerticalAngle >= VerticalHighAngleTreshold && mouseYDrive < 0
            || cameraVerticalAngle >= VerticalNearAngleTreshold && cameraVerticalAngle <= VerticalHighAngleTreshold;

        if (!canCameraVerticalRotate)
            return;

        HeadTransform.rotation = HeadTransform.rotation * Quaternion.AngleAxis(mouseYDrive * MouseSensivity, -Vector3.right);
    }

    public void FreezePlayerMovement()
    {
        _freezePlayerMovement = true;
    }

    #region Input Callbacks

    void MoveForwardOnButtonDownCallback() => _moveForward = true;

    void MoveForwardOnButtonUpCallback() => _moveForward = false;

    void MoveBackOnButtonDownCallback() => _moveBack = true;

    void MoveBackOnButtonUpCallback() => _moveBack = false;

    void MoveRightOnButtonDownCallback() => _moveRight = true;

    void MoveRightOnButtonUpCallback() => _moveRight = false;

    void MoveLeftOnButtonDownCallback() => _moveLeft = true;

    void MoveLeftOnButtonUpCallback() => _moveLeft = false;

    #endregion

}

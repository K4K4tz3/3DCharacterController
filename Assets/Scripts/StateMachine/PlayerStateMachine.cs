using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("Controls:")]
    [SerializeField] private bool _look;

    [Header("Player Values:")]
    [SerializeField] private float _speedValue = 5f;
    [SerializeField] private float _sprintValue = 3f;

    // Object's/Component's
    private Rigidbody _rb;
    private GameObject _cam;


    private Vector2 _moveValue;
    private Vector2 _lookValue;

    // Player movement values
    private bool _isMovePressed = false;
    private bool _isSprintPressed = false;
    private bool _isJumpPressed;

    // State variables
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    #region Getters & Setters
    // State variables
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }

    // Object's/Component's
    public Rigidbody RB { get { return _rb; } }

    // Movement Value's
    public Vector2 MoveValue { get { return _moveValue; } }
    public float SpeedValue { get { return _speedValue; } }
    public float SprintValue { get { return _sprintValue; } }

    // Bool Value's
    public bool IsMovePressed { get { return _isMovePressed; }}
    public bool IsSprintPressed { get { return _isSprintPressed; }}
    public bool IsJumpPressed { get { return _isJumpPressed; }}
    #endregion

    public void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _cam = GameObject.Find("Camera");

        // setup state
        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();
    }
    public void Update()
    {
        _currentState.UpdateStates();
        if (_look) Look();

        if (_moveValue != new Vector2(0,0)) _isMovePressed = true;
        else _isMovePressed = false;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveValue = context.ReadValue<Vector2>();
        _isMovePressed = _moveValue == Vector2.zero ? false : true;
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        _lookValue = context.ReadValue<Vector2>();
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        _isSprintPressed = context.ReadValueAsButton();
        //if (context.performed)
        //{
        //    _sprintValue = _sprintMaxValue;
        //}
        //else if (context.canceled)
        //{
        //    _sprintValue = 1;
        //}
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
    }
    private void Look()
    {
        transform.Rotate(new Vector3(0, _lookValue.x, 0));
        _cam.transform.Rotate(new Vector3(-_lookValue.y, 0, 0));
    }
    private void LimitCamRotation()
    {
        //limit camera rotation
        Vector3 cameraEulerAngles = _cam.transform.rotation.eulerAngles;

        cameraEulerAngles.x = (cameraEulerAngles.y > 180) ? cameraEulerAngles.x - 360 : cameraEulerAngles.x;
        cameraEulerAngles.x = Mathf.Clamp(cameraEulerAngles.x, -60, 60);

        _cam.transform.rotation = Quaternion.Euler(cameraEulerAngles);
        //change the camera so the camera moves -> cube/player is always in center
    }
}

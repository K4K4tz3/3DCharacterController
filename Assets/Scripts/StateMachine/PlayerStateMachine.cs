using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("Controls:")]
    [SerializeField] private bool _debugStateSwitch;
    [SerializeField] private bool _look;
    [SerializeField] private bool _sprintAdvanced; // false -> you can sprint forward, backwards, left and right; true -> you can only sprint forward
    [SerializeField] private bool _canMoveWhileInAir;

    [Header("Player Values:")]
    [SerializeField] private float _speedValue = 5f;
    [SerializeField] private float _sprintValue = 3f;
    [SerializeField] private float _jumpForce = 10f;

    // Object's/Component's
    private Rigidbody _rb;
    private GameObject _cam;
    private Gun _currentWeapon;

    // Player movement value's
    private Vector2 _moveValue;
    private Vector2 _lookValue;
    private float _facingDirection;

    // Player movement bools
    private bool _isMovePressed = false;
    private bool _isSprintPressed = false;
    private bool _isJumpPressed;
    private bool _isOnGround;

    // State variables
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    #region Getters & Setters
    public bool DebugStateSwitch { get { return _debugStateSwitch; } }
    // State variables
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }

    // Object's/Component's
    public Rigidbody RB { get { return _rb; } }

    // Movement Value's
    public Vector2 MoveValue { get { return _moveValue; } }
    public float SpeedValue { get { return _speedValue; } }
    public float SprintValue { get { return _sprintValue; } }
    public float JumpForce { get { return _jumpForce; } }
    public float FacingDirection { get { return _facingDirection; } }

    // Bool Value's
    public bool IsMovePressed { get { return _isMovePressed; }}
    public bool IsSprintPressed { get { return _isSprintPressed; }}
    public bool IsJumpPressed { get { return _isJumpPressed; }}
    public bool IsOnGround { get { return _isOnGround; } set { _isOnGround = value; } }
    public bool CanMoveInAir { get { return _canMoveWhileInAir; } }
    #endregion
    public void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _cam = GameObject.Find("Camera");
        _currentWeapon = GameObject.Find("Gun").GetComponent<Gun>();

        // setup state
        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();
    }
    public void Update()
    {
        _currentState.UpdateStates();

        //Look();
        if(_look) LimitCamRotation();

        if (_moveValue != new Vector2(0,0)) _isMovePressed = true;
        else _isMovePressed = false;
    }
    #region InputSystem
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveValue = -context.ReadValue<Vector2>();
        _isMovePressed = _moveValue == Vector2.zero ? false : true;
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        _lookValue = context.ReadValue<Vector2>();
        transform.Rotate(new Vector3(0, _lookValue.x, 0));
        if (_look)
        {
            _cam.transform.Rotate(new Vector3(-_lookValue.y, 0, 0));
            //_weapon.transform.Rotate(new Vector3(-_lookValue.y, 0, 0));
        }
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (!_sprintAdvanced) _isSprintPressed = context.ReadValueAsButton();
        else _isSprintPressed = _moveValue == new Vector2(1, 0) ? true : false;
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
    }
    public void OnUseLeft(InputAction.CallbackContext context)
    {
        if (context.started) _currentWeapon.CanShoot = true;
        else if (context.performed || context.canceled) _currentWeapon.CanShoot = false;
    }
    public void OnUseRight(InputAction.CallbackContext context)
    {

    }
    #endregion
    private void LimitCamRotation()
    {
        //limit camera rotation
        Vector3 cameraEulerAngles = _cam.transform.rotation.eulerAngles;

        cameraEulerAngles.x = (cameraEulerAngles.x > 180) ? cameraEulerAngles.x - 360 : cameraEulerAngles.x;
        cameraEulerAngles.x = Mathf.Clamp(cameraEulerAngles.x, -60, 60);
        cameraEulerAngles.z = 0;

        _cam.transform.rotation = Quaternion.Euler(cameraEulerAngles);
    }
    public void GetColliderMessage(bool state)
    {
        _isOnGround = state;
    }
    public IEnumerator Wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}

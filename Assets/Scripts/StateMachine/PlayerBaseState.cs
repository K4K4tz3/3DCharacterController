using UnityEngine;
public abstract class PlayerBaseState
{
    private bool _isRootState = false;
    private PlayerStateMachine _ctx;
    private PlayerStateFactory _factory;
    private PlayerBaseState _currentSuperState;
    private PlayerBaseState _currentSubState;
    public string name;

    protected bool IsRootState { set { _isRootState = value; } }
    protected PlayerStateMachine CTX { get { return _ctx; } }
    protected PlayerStateFactory Factory { get { return _factory; } }
    protected PlayerBaseState CurrentSubState { get { return _currentSubState; } set { _currentSubState = value; } }
    protected PlayerBaseState CurrentSuperState { get { return _currentSuperState; } }

    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {
        _ctx = currentContext;
        _factory = playerStateFactory;
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchState();
    public abstract void InitializeSubstate();
    public void UpdateStates()
    {
        UpdateState();
        if(_currentSubState != null) _currentSubState.UpdateStates();
    }
    void ExitStates()
    {
        ExitState();
        if (_currentSubState != null) _currentSubState.ExitStates();
    }
    protected void SwitchState(PlayerBaseState newState)
    {
        // current state exits state
        ExitState();

        // new state enters state
        newState.EnterState();

        // switch current state of context
        if (_isRootState) _ctx.CurrentState = newState;
        //else if (_ctx.CurrentState.CurrentSubState.name != newState.name) _ctx.CurrentState.CurrentSubState = newState; // i hate this line, this sole line fixed a problem which took roughly 8h to fix
        else if (_currentSuperState != null) _currentSuperState.SetSubState(newState); // why is the line above a comment? cuz i read smth wrong and i made my own problem ;-; instead of SetSubState i wrote SetSuperState, that was the issue
    }
    protected void SetSuperState(PlayerBaseState newSuperState)
    {
        _currentSuperState = newSuperState;
    }
    protected void SetSubState(PlayerBaseState newSubState)
    {
        _currentSubState = newSubState;
        _currentSubState.SetSuperState(this);
        _currentSubState.EnterState();
    }
}
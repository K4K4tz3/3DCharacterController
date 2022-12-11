using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintState : PlayerBaseState
{
    public PlayerSprintState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }
    public override void EnterState()
    {
        if (CTX.DebugStateSwitch) Debug.Log("Entered Substate Sprint");
    }
    public override void UpdateState()
    {
        CheckSwitchState();
        HandleSprint();
    }
    public override void ExitState()
    {
        if (CTX.DebugStateSwitch) Debug.Log("Left Sprint State");
    }
    public override void CheckSwitchState()
    {
        if (!CTX.IsMovePressed) SwitchState(Factory.Idle());
        else if (CTX.IsMovePressed && !CTX.IsSprintPressed) SwitchState(Factory.Move());
    }
    public override void InitializeSubstate()
    {
        
    }
    private void HandleSprint()
    {
        CTX.RB.velocity = CTX.transform.forward * CTX.MoveValue.y * CTX.SpeedValue * CTX.SprintValue;
    }
}

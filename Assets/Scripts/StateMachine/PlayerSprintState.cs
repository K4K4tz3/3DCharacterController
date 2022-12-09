using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintState : PlayerBaseState
{
    public PlayerSprintState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }
    public override void EnterState()
    {
        Debug.Log("Entered Substate Sprint");
    }
    public override void UpdateState()
    {
        CheckSwitchState();
        //HandleMove();
    }
    public override void ExitState() { }
    public override void CheckSwitchState()
    {
        if (!CTX.IsMovePressed) SwitchState(Factory.Idle());
        else if (CTX.IsMovePressed && !CTX.IsSprintPressed) SwitchState(Factory.Move());
    }
    public override void InitializeSubstate()
    {
        
    }
    private void HandleMove()
    {
        CTX.RB.velocity = CTX.transform.forward * CTX.MoveValue.y * CTX.SpeedValue * CTX.SprintValue;
    }
}

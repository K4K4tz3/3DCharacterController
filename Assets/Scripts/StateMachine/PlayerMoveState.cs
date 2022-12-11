using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { name = "move"; }
    public override void EnterState()
    {
        if (CTX.DebugStateSwitch) Debug.Log("Entered Substate Move");
    }
    public override void UpdateState()
    {
        CheckSwitchState();
        HandleMove();
    }
    public override void ExitState()
    {
        if (CTX.DebugStateSwitch) Debug.Log("Left Move State");
    }
    public override void CheckSwitchState()
    {
        if (CTX.IsMovePressed && CTX.IsSprintPressed) SwitchState(Factory.Sprint());
        else if (!CTX.IsMovePressed) SwitchState(Factory.Idle());
    }
    public override void InitializeSubstate()
    {
        
    }
    private void HandleMove()
    {
        CTX.RB.velocity = (CTX.transform.forward * CTX.MoveValue.y + CTX.transform.right * CTX.MoveValue.x).normalized * CTX.SpeedValue;
    }
}

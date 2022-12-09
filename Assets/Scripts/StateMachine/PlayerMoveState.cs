using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { name = "move"; }
    public override void EnterState()
    {
        Debug.Log("Entered Substate Move");
    }
    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void ExitState()
    {
        Debug.Log("Left Move State");
    }
    public override void CheckSwitchState()
    {
        if (!CTX.IsMovePressed)
        {
            SwitchState(Factory.Idle());
            //Debug.Log("Switch state. From: MOve To: Idle");
        }
    }
    public override void InitializeSubstate()
    {
        
    }
    private void HandleMove()
    {
        CTX.RB.velocity = CTX.transform.forward * CTX.MoveValue.y * CTX.SpeedValue;
    }
}

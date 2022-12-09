using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { name = "idle"; }
    public override void EnterState()
    {
        Debug.Log("Entered Substate Idle");
    }
    public override void UpdateState()
    {
        CheckSwitchState();
        //Debug.Log("Idle");
        //Debug.Log($"Idle SuperState: {CurrentSuperState.name}");
    }
    public override void ExitState()
    {
        Debug.Log("Left Substate Idle");
    }
    public override void CheckSwitchState()
    {
        if (CTX.IsMovePressed)
        {
            SwitchState(Factory.Move());
            //Debug.Log("Switch state. From: Idle To: Move");
        }
    }
    public override void InitializeSubstate()
    {

    }
}

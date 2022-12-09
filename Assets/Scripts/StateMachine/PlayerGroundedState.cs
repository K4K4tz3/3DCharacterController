using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitializeSubstate();
        name = "grounded";
    }
    public override void EnterState()
    {
        Debug.Log("Housten we got grounded");
        //CurrentSubState.EnterState();
    }
    public override void UpdateState()
    {
        CheckSwitchState();
        //Debug.Log($"Grounded SubState: {CurrentSubState.name}");
    }
    public override void ExitState() { }
    public override void CheckSwitchState()
    {
        if (CTX.IsJumpPressed)
        {
            SwitchState(Factory.Jump());
            //Debug.Log("Switch state. From: Grounded To: JumP");
        }
    }
    public override void InitializeSubstate()
    {
        Debug.Log("Init Substate");
        if (!CTX.IsMovePressed && !CTX.IsSprintPressed) SetSubState(Factory.Idle());
        else if (CTX.IsMovePressed && !CTX.IsSprintPressed) SetSubState(Factory.Move());
        //else if (CTX.IsMovePressed && CTX.IsSprintPressed) SetSubState(Factory.Sprint());
    }
}
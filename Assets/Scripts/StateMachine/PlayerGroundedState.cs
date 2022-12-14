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
    }
    public override void EnterState()
    {
        if (CTX.DebugStateSwitch) Debug.Log("Enterede Grounded State");
    }
    public override void UpdateState()
    {
        CheckSwitchState();
        //Debug.Log($"Grounded SubState: {CurrentSubState.name}");
    }
    public override void ExitState()
    {
        if (CTX.DebugStateSwitch) Debug.Log("Left Grounded State");
    }
    public override void CheckSwitchState()
    {
        if (CTX.IsJumpPressed) SwitchState(Factory.Jump());
    }
    public override void InitializeSubstate()
    {
        if (!CTX.IsMovePressed && !CTX.IsSprintPressed) SetSubState(Factory.Idle());
        else if (CTX.IsMovePressed && !CTX.IsSprintPressed) SetSubState(Factory.Move());
        else if (CTX.IsMovePressed && CTX.IsSprintPressed) SetSubState(Factory.Sprint());
    }
}

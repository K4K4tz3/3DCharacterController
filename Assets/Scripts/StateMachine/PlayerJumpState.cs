using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory)
    {
        InitializeSubstate();
        IsRootState = true;
    }
    public override void EnterState()
    {
        if (CTX.DebugStateSwitch) Debug.Log("Entered Jump State");
        HandleJump();
    }
    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void ExitState()
    {
        if (CTX.DebugStateSwitch) Debug.Log("Left Jump State");
    }
    public override void CheckSwitchState()
    {
        if (CTX.IsOnGround) SwitchState(Factory.Grounded());
    }
    public override void InitializeSubstate(){}
    private void HandleJump()
    {
        CTX.RB.velocity = Vector3.up * 20;
        CTX.Wait(2);
    }
}

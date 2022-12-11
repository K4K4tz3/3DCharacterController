using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { name = "idle"; }
    public override void EnterState()
    {
        if (CTX.DebugStateSwitch) Debug.Log("Entered Substate Idle");
    }
    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void ExitState()
    {
        if (CTX.DebugStateSwitch) Debug.Log("Left Substate Idle");
    }
    public override void CheckSwitchState()
    {
        if (CTX.IsMovePressed && !CTX.IsSprintPressed) SwitchState(Factory.Move());
        else if (CTX.IsMovePressed && CTX.IsSprintPressed) SwitchState(Factory.Sprint());
    }
    public override void InitializeSubstate()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        //InitializeSubstate();
        name = "jump";
    }
    public override void EnterState()
    {
        Debug.Log("We jumped");
    }
    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void ExitState()
    {
        Debug.Log("Left idle State");
    }
    public override void CheckSwitchState()
    {
        if (!CTX.IsJumpPressed)
        {
            SwitchState(Factory.Grounded());
            Debug.Log("Switch state. From: Jump To: Grounded");
        }
    }
    public override void InitializeSubstate(){}
}

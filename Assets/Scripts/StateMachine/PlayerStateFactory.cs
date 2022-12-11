public class PlayerStateFactory
{
    PlayerStateMachine _context;
    
    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
    }
    // Top Level States
    public PlayerBaseState Grounded()
    {
        return new PlayerGroundedState(_context, this);
    }
    public PlayerBaseState Jump()
    {
        return new PlayerJumpState(_context, this);
    }
    // Second Level States
    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(_context, this);
    }
    public PlayerBaseState Move()
    {
        return new PlayerMoveState(_context, this);
    }
    public PlayerBaseState Sprint()
    {
        return new PlayerSprintState(_context, this);
    }
}

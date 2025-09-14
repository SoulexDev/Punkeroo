using UnityEngine;

public class PlayerIdle : State<PlayerController>
{
    public override void EnterState(PlayerController ctx)
    {
        ctx.rb.linearVelocity = Vector2.zero;
    }
    public override void ExitState(PlayerController ctx)
    {
        
    }
    public override void FixedUpdateState(PlayerController ctx)
    {
        
    }
    public override void UpdateState(PlayerController ctx)
    {
        if (ctx.SwitchByCondition(PlayerState.Fall, !ctx.isGrounded))
            return;
        if (ctx.SwitchByCondition(PlayerState.Run, ctx.usingInput))
            return;
        if (ctx.SwitchByCondition(PlayerState.Jump, Input.GetKeyDown(KeyCode.Space)))
            return;
    }
}
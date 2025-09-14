using UnityEngine;

public class PlayerRun : State<PlayerController>
{
    public override void EnterState(PlayerController ctx)
    {
        
    }
    public override void ExitState(PlayerController ctx)
    {
        
    }
    public override void FixedUpdateState(PlayerController ctx)
    {
        ctx.rb.linearVelocity = ctx.moveVector * 7 - Vector2.up * 2;
    }
    public override void UpdateState(PlayerController ctx)
    {
        if (ctx.SwitchByCondition(PlayerState.Fall, !ctx.isGrounded))
            return;
        if (ctx.SwitchByCondition(PlayerState.Idle, !ctx.usingInput))
            return;
        if (ctx.SwitchByCondition(PlayerState.Jump, Input.GetKeyDown(KeyCode.Space)))
            return;
    }
}
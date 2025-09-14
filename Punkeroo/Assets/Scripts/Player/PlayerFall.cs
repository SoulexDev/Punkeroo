using UnityEngine;

public class PlayerFall : State<PlayerController>
{
    private float m_gravity;
    private float m_xMove;
    public override void EnterState(PlayerController ctx)
    {
        m_gravity = 0;
    }
    public override void ExitState(PlayerController ctx)
    {
        
    }
    public override void FixedUpdateState(PlayerController ctx)
    {
        m_gravity -= 15 * Time.fixedDeltaTime;

        if (ctx.usingInput)
            m_xMove = Mathf.Lerp(m_xMove, ctx.moveVector.x, Time.fixedDeltaTime * 3);
        else
            m_xMove = Mathf.Lerp(m_xMove, 0, Time.fixedDeltaTime * 3);

        ctx.rb.linearVelocity = new Vector2(m_xMove, m_gravity);
    }
    public override void UpdateState(PlayerController ctx)
    {
        if (ctx.SwitchByCondition(PlayerState.Idle, ctx.isGrounded))
            return;
    }
}
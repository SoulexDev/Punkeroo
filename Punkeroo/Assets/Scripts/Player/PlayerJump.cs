using UnityEngine;

public class PlayerJump : State<PlayerController>
{
    private float m_gravity;
    private float m_xMove;
    private int m_jumpCount;
    private float m_enterVelX;
    private float m_jumpTimer;
    public override void EnterState(PlayerController ctx)
    {
        if (ctx.previousState.Equals(PlayerState.Jump))
            m_jumpCount = 0;
        else
            m_jumpCount = 1;

        m_gravity = 16;
        m_enterVelX = ctx.rb.linearVelocity.x;
        m_jumpTimer = 0;
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

        ctx.rb.linearVelocity = new Vector2(Mathf.Lerp(m_enterVelX, m_xMove, Mathf.Clamp01(m_jumpTimer * 0.35f)), m_gravity);
    }
    public override void UpdateState(PlayerController ctx)
    {
        m_jumpTimer += Time.deltaTime;

        if (ctx.SwitchByCondition(PlayerState.Jump, Input.GetKeyDown(KeyCode.Space) && m_jumpCount > 0))
            return;
        if (ctx.SwitchByCondition(PlayerState.Idle, ctx.isGrounded && m_jumpTimer > 0.2f))
            return;
    }
}
using UnityEngine;

public class PlayerController : StateMachine<PlayerController>
{
    public Rigidbody2D rb;

    public bool isGrounded;
    public bool usingInput;
    public bool moving;

    public Vector2 groundNormal;
    public Vector2 inputVector;
    public Vector2 moveVector;

    public Vector2 playerCenter => (Vector2)transform.position + Vector2.up;

    private void Awake()
    {
        stateDictionary.Add(PlayerState.Idle, new PlayerIdle());
        stateDictionary.Add(PlayerState.Run, new PlayerRun());
        stateDictionary.Add(PlayerState.Dash, new PlayerDash());
        stateDictionary.Add(PlayerState.Fall, new PlayerFall());
        stateDictionary.Add(PlayerState.Jump, new PlayerJump());

        SwitchState(PlayerState.Run);
    }
    public override void Update()
    {
        GroundCheck();

        inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        moveVector = Vector2.Perpendicular(groundNormal) * -inputVector.x;

        usingInput = inputVector.x != 0;
        moving = rb.linearVelocity.sqrMagnitude > 0.1f;

        base.Update();
    }
    public void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(playerCenter, Vector2.down, 2, GameManager.playerRayIgnoreMask);
        if (hit.collider)
        {
            if (hit.distance <= 1.2f)
                isGrounded = true;
            else
                isGrounded = false;

            groundNormal = hit.normal;
        }
        else
        {
            isGrounded = false;
            groundNormal = Vector2.up;
        }
    }
}
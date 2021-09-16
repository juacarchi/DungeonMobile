using UnityEngine;


public class TopDownCharacterController : MonoBehaviour
{
    public float speed;
    [SerializeField]
    Joystick joystickMovement;
    Rigidbody2D rb2D;
    Vector2 move;
    SpriteRenderer rendererPlayer;
    [SerializeField]
    GameObject weapon;
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rendererPlayer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        bool isAiming = PlayerManager.instance.GetIsAiming();
        move.x = joystickMovement.Horizontal;
        move.y = joystickMovement.Vertical;
        move.Normalize();
        if (move.x < 0)
        {
            rendererPlayer.flipX = true;
            if (!isAiming)
            {
                weapon.transform.localScale = new Vector2(1, -1);
            }
        }
        else if (move.x > 0)
        {
            rendererPlayer.flipX = false;
            if (!isAiming)
            {
                weapon.transform.localScale = new Vector2(1, 1);
            }
        }
        else
        {
            if (!isAiming)
            {
                if (rendererPlayer.flipX)
                {
                    weapon.transform.localScale = new Vector2(-1, 1);
                }
                else
                {
                    weapon.transform.localScale = new Vector2(1, 1);
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (!PlayerManager.instance.GetIsAiming())
        {
            weapon.transform.right = move;
        }
        rb2D.MovePosition(rb2D.position + move * speed * Time.fixedDeltaTime);
    }
}

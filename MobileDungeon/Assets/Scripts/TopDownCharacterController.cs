using UnityEngine;


public class TopDownCharacterController : MonoBehaviour
{
    public float speed;
    public float speedDash;
    [SerializeField]
    Joystick joystickMovement;
    Rigidbody2D rb2D;
    Vector2 move;
    SpriteRenderer rendererPlayer;
    [SerializeField]
    GameObject weapon;
    bool isDash;
    [SerializeField] float dashtime;
    float timerDash;
    private void Start()
    {

        timerDash = dashtime;
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

        if (isDash)
        {
            timerDash -= Time.deltaTime;
            if (timerDash <= 0)
            {
                isDash = false;
                timerDash = dashtime;
            }
        }
  
    }
    private void FixedUpdate()
    {
        if (!PlayerManager.instance.GetIsAiming())
        {
            weapon.transform.right = move;
        }
        if (PlayerManager.instance.PlayerCanMove)
        {
            
            if (isDash)
            {
                rb2D.MovePosition(rb2D.position + (Vector2)weapon.transform.right * speedDash * Time.deltaTime);
            }
            else
            {
                rb2D.MovePosition(rb2D.position + move * speed * Time.fixedDeltaTime);
            }
        }

    }
    public void Dash()
    {
        Debug.Log("Dash");
        isDash = true;
        
    }
}

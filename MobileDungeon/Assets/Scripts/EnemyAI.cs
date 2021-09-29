using UnityEngine;
using UnityEngine.AI;
public enum EnemyState { PATROL, CHASE, ATTACK, HIT, DEATH }
public class EnemyAI : MonoBehaviour
{
    public Transform player;
    NavMeshAgent agente;
    [SerializeField]
    SpriteRenderer graphics;
    public EnemyState state;
    public LayerMask playerMask;
    public GameObject enemyBullet;
    [SerializeField] float timeBetweenShoots;
    float timerBetweenShoots;
    public float thrust;
    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        agente.updateRotation = false;
        agente.updateUpAxis = false;
        timerBetweenShoots = timeBetweenShoots;
    }
    private void Update()
    {
        bool isChaseRange = Physics2D.OverlapCircle(this.transform.position, 8, playerMask);
        bool isAttackRange = Physics2D.OverlapCircle(this.transform.position, 6, playerMask);

        if (isChaseRange && !isAttackRange)
        {
            state = EnemyState.CHASE;
            Debug.Log("Chase");
        }
        if (isAttackRange)
        {
            state = EnemyState.ATTACK;
            Debug.Log("Attack");
        }
        if(state == EnemyState.CHASE)
        {
            agente.SetDestination(player.position);
            if (this.transform.position.x > player.position.x && !graphics.flipX)
            {
                Flip();
            }
            else if (this.transform.position.x < player.position.x && graphics.flipX)
            {
                Flip();
            }
        }
        else if (state == EnemyState.ATTACK)
        {
            agente.SetDestination(transform.position);
            Shoot();
        }


    }
    public void Flip()
    {
        if (graphics.flipX)
        {
            graphics.flipX = false;
        }
        else
        {
            graphics.flipX = true;
        }
    }
    public void Shoot()
    {
        timerBetweenShoots -= Time.deltaTime;
        if (timerBetweenShoots <= 0)
        {
            GameObject bullet = Instantiate(enemyBullet, this.transform.position, Quaternion.identity); //ROTACION ORIENTACION ENEMY
            var heading = player.position - this.transform.position;
            bullet.GetComponent<Rigidbody2D>().AddForce(heading * thrust, ForceMode2D.Impulse);
            timerBetweenShoots = timeBetweenShoots;
        }
    }
}

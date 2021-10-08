using UnityEngine;
using UnityEngine.AI;
public enum EnemyState { PATROL, CHASE, ATTACK, HIT, DEATH }
public class EnemyAI : MonoBehaviour
{
    public float chaseRange = 8;
    public float attackRange = 6;
    public Transform player;
    NavMeshAgent agente;
    [SerializeField]
    SpriteRenderer graphics;
    public EnemyState state;
    public LayerMask playerMask;
    public GameObject enemyBullet;
    [SerializeField] float timeBetweenShoots;
    float timerBetweenShoots;
    //public float thrust;
    NavMeshData navData;
    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        agente.updateRotation = false;
        agente.updateUpAxis = false;
        timerBetweenShoots = timeBetweenShoots;
        InvokeRepeating("GetRandomLocation", 5, 5);
    }
    private void Update()
    {
        bool isChaseRange = Physics2D.OverlapCircle(this.transform.position, chaseRange, playerMask);
        bool isAttackRange = Physics2D.OverlapCircle(this.transform.position, attackRange, playerMask);

        if (isChaseRange && !isAttackRange)
        {
            state = EnemyState.CHASE;
            //Debug.Log("Chase");
        }
        if (isAttackRange)
        {
            state = EnemyState.ATTACK;
            //Debug.Log("Attack");
        }
        if (state == EnemyState.CHASE)
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
            var heading = player.position - this.transform.position;
            GameObject bullet = Instantiate(enemyBullet, this.transform.position, Quaternion.identity); //ROTACION ORIENTACION ENEMY
            var direction = heading.normalized;
            bullet.GetComponent<BulletBoss>().SetDirection(direction);
            //bullet.GetComponent<Rigidbody2D>().AddForce(heading * thrust, ForceMode2D.Impulse);
            timerBetweenShoots = timeBetweenShoots;
        }
    }
    public Vector2 GetRandomLocation()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
        int t = Random.Range(0, navMeshData.indices.Length - 3);

        Vector2 point = Vector2.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t + 1]], Random.value);
        point = Vector2.Lerp(point, navMeshData.vertices[navMeshData.indices[t + 2]], Random.value);
        //Debug.Log(point);
        Patrol(point);
        return point;

    }
    public void Patrol(Vector2 point)
    {
        agente.SetDestination(point);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    public Transform player;
    NavMeshAgent agente;
    [SerializeField]
    SpriteRenderer graphics;
    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        agente.updateRotation = false;
        agente.updateUpAxis = false;
    }
    private void Update()
    {
        agente.SetDestination(player.position);
        if(this.transform.position.x > player.position.x && !graphics.flipX)
        {
            Flip();
        }
        else if (this.transform.position.x < player.position.x && graphics.flipX)
        {
            Flip();
        }




    }
    public void Flip()
    {
        if(graphics.flipX)
        {
            graphics.flipX = false;
        }
        else
        {
            graphics.flipX = true;
        }
    }
}

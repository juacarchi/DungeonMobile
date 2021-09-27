using UnityEngine;

public class RoomSpawner : MonoBehaviour
{

    RoomTemplates templates;
    //Necesitamos saber que habitación necesitamos
    public int openingDirection; //1--> Need Bottom Door //2--> Need Top//3--> Need Left //4--> Need Right
    int rnd;
    bool spawned = false;
    public float waitTime = 4f;

    private void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }
    private void Spawn()
    {
        if (spawned==false)
        {
            if (openingDirection == 1)
            {
                //Spawn bottomRoom
                rnd = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rnd], transform.position, templates.bottomRooms[rnd].transform.rotation);
            }
            else if (openingDirection == 2)
            {
                //Spawn TopRoom
                rnd = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rnd], transform.position, templates.topRooms[rnd].transform.rotation);
            }
            else if (openingDirection == 3)
            {
                //Spawn LeftRoom
                rnd = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rnd], transform.position, templates.leftRooms[rnd].transform.rotation);
            }
            else if (openingDirection == 4)
            {
                //Spawn RightRoom
                rnd = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rnd], transform.position, templates.rightRooms[rnd].transform.rotation);
            }
            spawned = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>() != null)
            {
                if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
                {
                    //spawn paredes que bloquean cualquier apertura
                    Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
            spawned = true;
        }
    }
}

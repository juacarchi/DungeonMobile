using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRoom;
    public List<GameObject> rooms;

    public float waitTime;
    bool spawnedBoss;
    bool randomNumber;
    public GameObject boss;
    public GameObject shop;
    int rnd;

    private void Update()
    {

        if (waitTime <= 0 && !spawnedBoss)
        {
            if (!randomNumber)
            {
                rnd = Random.Range(0, rooms.Count - 2);
                randomNumber = true;
            }

            for (int i = 0; i < rooms.Count; i++)
            {
                if (i == rnd)
                {
                    Instantiate(shop, rooms[i].transform.position, Quaternion.identity);
                    
                }
                if (i == rooms.Count - 1)
                {
                    Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                    spawnedBoss = true;
                }
            }
        }
        else
        {
            if (!spawnedBoss)
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}

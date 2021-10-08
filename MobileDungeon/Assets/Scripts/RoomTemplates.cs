using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{   [Header ("Normal Rooms")]
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    [Header("Shop Rooms")]
    public GameObject B;
    public GameObject L;
    public GameObject LR;
    public GameObject LT;
    public GameObject R;
    public GameObject RB;
    public GameObject T;
    public GameObject TB;
    public GameObject TR;

    Dictionary<string, GameObject> shopRooms;


    public GameObject closedRoom;
    public List<GameObject> rooms;

    public float waitTime;
    bool spawnedBoss;
    bool randomNumber;
    public GameObject boss;
    public GameObject shop;
    int rnd;
    private void Start()
    {
        shopRooms = new Dictionary<string, GameObject>();
        shopRooms.Add("B", B);
        shopRooms.Add("L", L);
        shopRooms.Add("LR", LR);
        shopRooms.Add("LT", LT);
        shopRooms.Add("R", R);
        shopRooms.Add("RB", RB);
        shopRooms.Add("T", T);
        shopRooms.Add("TB", TB);
        shopRooms.Add("TR", TR);
    }
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
                    string tagName = rooms[i].tag;

                     Instantiate(shopRooms[tagName], rooms[i].transform.position, Quaternion.identity);
                    
                }
                if (i == rooms.Count - 1)
                {
                    GameObject bossInstantiate = Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                    bossInstantiate.transform.SetParent(rooms[i].transform);
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

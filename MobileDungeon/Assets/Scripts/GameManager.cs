using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    float waitTime = 4;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        PlayerManager.instance.PlayerCanMove = false;
        Invoke("StartGame", waitTime);
    }
    
    void StartGame()
    {
        PlayerManager.instance.PlayerCanMove = true;
    }
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GameManager : MonoBehaviour
{
    public NavMeshSurface2d nav;
    void Start()
    {
        nav.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

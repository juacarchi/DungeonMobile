using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        Debug.Log("Colision");
    }
    public void OnTriggerEnter2D(Collider other)
    {
        Debug.Log("Colisionando");
    }
}

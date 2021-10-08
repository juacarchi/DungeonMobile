using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoss : MonoBehaviour
{
    public float bulletSpeed=5;
    Vector2 direction = Vector2.up;
    public GameObject fxExplosion;
    
    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }
    private void Update()
    {
        
        transform.Translate(direction * bulletSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject goFX = Instantiate(fxExplosion, transform.position, Quaternion.identity);
        Destroy(goFX, 0.4f);
        Destroy(this.gameObject);
    }
}

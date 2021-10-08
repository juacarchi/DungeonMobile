using System.Collections;
using UnityEngine;
public enum BossState { SPIRAL, SUPERSPIRAL, CIRCLE }
public class BossAI : MonoBehaviour
{
    [Header("SpiralAttack")]
    public float timeBettweenShootsSpiral;
    [Header("SuperSpiral")]
    public float timeBettweenShootsSuperSpiral;
    [Header("Circle")]
    public float timeBettweenShootsCircle;
    public int numberBullets;
    [Space(10)]
    public float timeChange = 2;
    public GameObject bulletBoss;
    public float increaseAngle = 5;
    [SerializeField] int sizeRoom;
    float angle;
    //public float timeBettwenShoots;
    BossState state;

    [SerializeField] int numberState;
    public LayerMask playerMask;
    int previousState = 4;
    float angleBullet;
    bool stateChanged;
    bool isActive;
    private void Start()
    {
        
        state = BossState.CIRCLE;
        //InvokeRepeating("Shoot", 4, timeBettwenShoots);
    }
    void Shoot()
    {
        if (state == BossState.SPIRAL)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Instantiate(bulletBoss, transform.position, rotation);
            angle += increaseAngle;
        }
        else if (state == BossState.SUPERSPIRAL)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Quaternion rotation2 = Quaternion.Euler(0, 0, angle * 2);
            Quaternion rotation3 = Quaternion.Euler(0, 0, angle * 3);
            Instantiate(bulletBoss, transform.position, rotation);
            Instantiate(bulletBoss, transform.position, rotation2);
            Instantiate(bulletBoss, transform.position, rotation3);
            angle += increaseAngle;
        }
        else if (state == BossState.CIRCLE)
        {
            float angleCircle = 360 / numberBullets;
            for (int i = 0; i < numberBullets; i++)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, angleBullet);
                Instantiate(bulletBoss, transform.position, rotation);
                angleBullet += angleCircle;
            }
        }
    }
    IEnumerator SpiralShootCO()
    {
        CancelInvoke();
        yield return new WaitForSecondsRealtime(timeChange);
        InvokeRepeating("Shoot", 0.5f, timeBettweenShootsSpiral);
    }
    IEnumerator SuperSpiralShootCO()
    {
        CancelInvoke();
        yield return new WaitForSecondsRealtime(timeChange);
        InvokeRepeating("Shoot", 0.5f, timeBettweenShootsSuperSpiral);
    }
    IEnumerator CircleShootCO()
    {
        CancelInvoke();
        yield return new WaitForSecondsRealtime(timeChange);
        InvokeRepeating("Shoot", 0.5f, timeBettweenShootsCircle);
    }

    private void Update()
    {
        if (isActive)
        {
            if (numberState == 0 && previousState != 0)
            {
                previousState = numberState;
                state = BossState.SPIRAL;
                StartCoroutine(SpiralShootCO());
                Debug.Log("DISPARA SPIRAL");
            }
            else if (numberState == 1 && previousState != 1)
            {
                previousState = numberState;
                state = BossState.SUPERSPIRAL;
                StartCoroutine(SuperSpiralShootCO());
                Debug.Log("DISPARA SUPERSPIRAL");

            }
            else if (numberState == 2 && previousState != 2)
            {
                previousState = numberState;
                state = BossState.CIRCLE;
                StartCoroutine(CircleShootCO());
                Debug.Log("DISPARA CIRCLE");
            }
        }
        else
        {
            CancelInvoke();
            previousState++;
        }

        bool isChaseRange = Physics2D.OverlapCircle(this.transform.position, 8, playerMask);
        if (isChaseRange)
        {
            Debug.Log("Player en la sala");
            isActive = true;
        }
        else
        {
            Debug.Log("Player no esta en la sala");
            isActive = false;
        }
    }
}

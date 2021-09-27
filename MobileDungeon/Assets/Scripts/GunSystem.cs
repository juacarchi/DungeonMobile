using UnityEngine;

public class GunSystem : MonoBehaviour
{
    Weapon actualWeapon;
    int damage;
    float timeBetweenShooting, range, reloadTime, timeBetweenShots;
    int magazineSize, bulletsPerTap;
    int bulletsLeft, bulletsShot;
    GameObject bullets;

    //boolsGameplay
    bool shooting, readyToShoot, reloading;

    //Reference
    public VariableJoystick joystickAttack;
    public Transform attackPoint;
    public Transform canyon;
    RaycastHit2D rayHit;
    [SerializeField] LayerMask whatIsEnemy;

    public static GunSystem instance;
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
    private void Start()
    {
        actualWeapon = PlayerManager.instance.GetWeapon();

        damage = actualWeapon.damage;
        timeBetweenShooting = actualWeapon.timeBetweenShooting;
        range = actualWeapon.range;
        reloadTime = actualWeapon.reloadTime;
        timeBetweenShots = actualWeapon.timeBetweenShots;
        magazineSize = actualWeapon.magazineSize;
        bulletsPerTap = actualWeapon.bulletsPerTap;
        bullets = actualWeapon.bullets;

        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    //JOYSTICK DETECTION
    public void OnPointerUp()
    {
        shooting = true;
        if (bulletsLeft == 0 && !reloading) Reload();
        //SHOOT
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
        
        
    }
    void Shoot()
    {
        Debug.Log("instancia");
        readyToShoot = false;
        //RAYCAST
        rayHit = Physics2D.Raycast(canyon.transform.position, canyon.transform.right, whatIsEnemy);
        if (rayHit.collider != null)
        {
            if (rayHit.distance < range)
            {
                //ACCEDER AL COMPONENTE DEL ENEMIGO Y TAKE DAMAGE
                Debug.Log("Golpea enemigo");
            }
        }
        //GRAPHICS: Instantiate 
        GameObject bullet = Instantiate(bullets, canyon.transform.position, canyon.transform.rotation);
        if (bullet.GetComponentInChildren<Rigidbody2D>() != null)
        {
            Rigidbody2D[] rb2ds = bullet.GetComponentsInChildren<Rigidbody2D>();
            for (int i = 0; i < rb2ds.Length; i++)
            {
                rb2ds[i].AddForce(rb2ds[i].gameObject.transform.right * 10, ForceMode2D.Impulse);
            }
        }
        else
        {
            bullet.GetComponent<Rigidbody2D>().AddForce(canyon.transform.right * 10, ForceMode2D.Impulse);
        }
        

        bulletsLeft--;
        bulletsShot--;

        if (bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
        Invoke("ResetShoot", timeBetweenShooting);

    }
    void ResetShoot()
    {
        readyToShoot = true;
    }
    void Reload()
    {
        Debug.Log("Reloading");
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
    private void Update()
    {
        if (shooting)
        {
            Debug.Log("DISPARA");
            shooting = false;
        }
        Debug.DrawRay(canyon.transform.position, canyon.transform.right,Color.blue);
    }
    public void ChangeWeapon()
    {
        actualWeapon = PlayerManager.instance.GetWeapon();

        damage = actualWeapon.damage;
        timeBetweenShooting = actualWeapon.timeBetweenShooting;
        range = actualWeapon.range;
        reloadTime = actualWeapon.reloadTime;
        timeBetweenShots = actualWeapon.timeBetweenShots;
        magazineSize = actualWeapon.magazineSize;
        bulletsPerTap = actualWeapon.bulletsPerTap;
        bullets = actualWeapon.bullets;
    }
}

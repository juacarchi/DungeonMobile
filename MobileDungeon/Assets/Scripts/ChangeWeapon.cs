using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    SpriteRenderer spriteWeapon;
    [SerializeField] Weapon weapon;
    private void Awake()
    {
        spriteWeapon = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            Weapon lastWeapon = PlayerManager.instance.GetWeapon();
            Debug.Log("CambiaArma");
            PlayerManager.instance.SetWeapon(weapon);
            GunSystem.instance.ChangeWeapon();
            spriteWeapon.sprite = lastWeapon.spriteWeapon;
            weapon = lastWeapon;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    [SerializeField] Weapon actualWeapon;
    [SerializeField] SpriteRenderer spriteWeapon;
    bool isAiming;
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

    public bool GetIsAiming()
    {
        return isAiming;
    }
    public void SetIsAiming(bool isAiming)
    {
        this.isAiming = isAiming;
    }
    public void SetWeapon(Weapon actualWeapon)
    {
        this.actualWeapon = actualWeapon;
        spriteWeapon.sprite = actualWeapon.spriteWeapon;
    }
    public Weapon GetWeapon()
    {
        return actualWeapon;
    }
}

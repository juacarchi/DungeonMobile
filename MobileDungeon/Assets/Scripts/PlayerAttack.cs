using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    VariableJoystick joystickAttack;
    [SerializeField]
    GameObject pivotTrail;
    [SerializeField]
    GameObject pivotWeapon;
    Vector2 dirAttack;
    float timeShooting = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        dirAttack.x = joystickAttack.Horizontal;
        dirAttack.y = joystickAttack.Vertical;
        dirAttack.Normalize();
        pivotTrail.transform.up = dirAttack;

        if (dirAttack != Vector2.zero)
        {
            pivotTrail.SetActive(true);
            PlayerManager.instance.SetIsAiming(true);
            pivotWeapon.transform.right = dirAttack;
            if (dirAttack.x > 0)
            {
                pivotWeapon.transform.localScale = new Vector2(1, 1);
            }
            else
            {
                pivotWeapon.transform.localScale = new Vector2(1, -1);
            }
            timeShooting = 0.2f;
        }
        else
        {
            timeShooting -= Time.deltaTime;
            if (timeShooting < 0)
            {
                PlayerManager.instance.SetIsAiming(false);
            }
            pivotTrail.SetActive(false);

        }

    }
}

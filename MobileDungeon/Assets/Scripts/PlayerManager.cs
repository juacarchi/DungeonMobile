using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
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
}

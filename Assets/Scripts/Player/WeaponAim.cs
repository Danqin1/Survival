using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    [SerializeField] private Transform weapon;
    [SerializeField] private Transform weaponLook;

    void Update()
    {
        weapon.LookAt(weaponLook);
    }
}

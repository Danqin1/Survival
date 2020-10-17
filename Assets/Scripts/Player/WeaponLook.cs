using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLook : MonoBehaviour
{
    [SerializeField] private Transform lookTarget;
    [SerializeField] private Transform weapon;
    void Update()
    {
        weapon.transform.LookAt(lookTarget);
    }
}

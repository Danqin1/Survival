﻿using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    #region variables

    public UnityEvent TakingDamage;
    public UnityEvent PlayerDied;

    public delegate void MyDelegate();
    public static event MyDelegate OnTakeAgro;

    public static Player instance;

    public float Damage = 20;

    private float Health = 100;

    #endregion

    #region Unity methods

    private void Awake()
    {
        instance = this;
    }
    
    private void Update()
    {
        if(OnTakeAgro != null)
        {
            OnTakeAgro();
        }
    }

    #endregion

    #region public methods

    public void TakeDamage(float dmg)
    {
        Health -= dmg;
        if (Health <= 0) PlayerDied.Invoke();
        TakingDamage.Invoke();
    }

    #endregion
}

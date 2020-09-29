using UnityEngine;

public class Player : MonoBehaviour
{
    #region variables

    public delegate void MyDelegate();
    public static event MyDelegate OnTakeAgro;

    public static Player instance;

    public float Damage = 10;

    private float Health = 100;

    #endregion

    #region Unity methods

    private void Start()
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

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void TakeDamage(float dmg)
    {
        Health -= dmg;
    }

    #endregion
}

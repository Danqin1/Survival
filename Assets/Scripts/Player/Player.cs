using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void MyDelegate();
    public static event MyDelegate OnTakeAgro;
    public static Player instance;
    private float Health = 100;
    public float Damage = 10;
    private void Start()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if(OnTakeAgro != null)
        {
            OnTakeAgro();
        }
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public void TakeDamage(float dmg)
    {
        Health -= dmg;
    }
}

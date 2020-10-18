using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region variables

    [SerializeField] private Animator animator;
    [SerializeField] private Shooting shooting;

    private DynamicJoystick joystick;
    private float horizontal = 0, vertical = 0;

    #endregion

    #region Unity methods

    private void Start()
    {
        joystick = FindObjectOfType<DynamicJoystick>();
    }

    private void FixedUpdate()
    {
        if (joystick != null)
        {
            if (joystick.MoveThreshold > 0)
            {
                horizontal = joystick.Horizontal;
                vertical = joystick.Vertical;
                animator.SetFloat("Y", vertical);
                animator.SetFloat("X", horizontal);
            }
        }
        else Debug.LogError("No Joystick");
    }

    #endregion
}


using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region variables

    [SerializeField] private DynamicJoystick joystick;
    [SerializeField] private Animator animator;
    [SerializeField] private Shooting shooting;

    #endregion

    #region Unity methods

    private void FixedUpdate()
    {
        float horizontal = 0, vertical = 0;
        if (joystick.MoveThreshold > 0)
        {
            horizontal = joystick.Horizontal; //(joystick.Horizontal < -0.1) ? -1 : (joystick.Horizontal > 0.1f) ? 1 : joystick.Horizontal;
            vertical = joystick.Vertical;//(joystick.Vertical < -0.1) ? -1 : (joystick.Vertical > 0.1f) ? 1 : joystick.Vertical;
            animator.SetFloat("Y", vertical);
            animator.SetFloat("X", horizontal);
        }
    }

    #endregion
}

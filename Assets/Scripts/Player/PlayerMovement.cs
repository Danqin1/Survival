
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
        if (joystick.MoveThreshold > 0)
        {
            float horizontal, vertical = 0;
            horizontal = (joystick.Horizontal < -0.2) ? -1 : (joystick.Horizontal > 0.5f) ? 1 : joystick.Horizontal;
            vertical = (joystick.Vertical < -0.2) ? -1 : (joystick.Vertical > 0.5f) ? 1 : joystick.Vertical;
            animator.SetFloat("Y", vertical);
            animator.SetFloat("X", horizontal);
        }
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.A))
            {
                animator.SetFloat("X", -1);
            }
            else animator.SetFloat("X", 1);
        }
        else animator.SetFloat("X", 0);



        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.S))
            {
                animator.SetFloat("Y", -1);
            }
            else animator.SetFloat("Y", 1);
        }
        else animator.SetFloat("Y", 0);
#endif
    }

    #endregion
}

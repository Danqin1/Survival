using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region variables

    [SerializeField] private Animator animator;

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
                if(horizontal > .5f && vertical > .5f)
                {
                    animator.SetFloat("Y", 1);
                    animator.SetFloat("X", 1);
                }
                else if (horizontal < -.5f && vertical < -.5f)
                {
                    animator.SetFloat("Y", -1);
                    animator.SetFloat("X", -1);
                }
                else if (horizontal < -.5f && vertical > .5f)
                {
                    animator.SetFloat("Y", 1);
                    animator.SetFloat("X", -1);
                }
                else if (horizontal > .5f && vertical < -.5f)
                {
                    animator.SetFloat("Y", -1);
                    animator.SetFloat("X", 1);
                }
                else
                {
                    animator.SetFloat("Y", vertical);
                    animator.SetFloat("X", horizontal);
                }
            }
            Debug.Log(horizontal + " " + vertical);
        }
        else Debug.LogError("No Joystick");
    }

    #endregion
}

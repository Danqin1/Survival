
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region variables

    [SerializeField] private DynamicJoystick joystick;
    
    public float accelerationSpeed = 50000.0f;
    public float deaccelerationSpeed = 5f;

    private float maxSpeed;
    private Rigidbody rb;
    private Vector2 horizontalMovement;

    #endregion

    #region Unity methods

    private void Start()
    {
        maxSpeed = 10;
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (joystick.MoveThreshold > 0)
        {
            float horizontal, vertical = 0;
            horizontal = (joystick.Horizontal < -0.2) ? -1 : (joystick.Horizontal > 0.5f) ? 1 : joystick.Horizontal;
            vertical = (joystick.Vertical < -0.2) ? -1 : (joystick.Vertical > 0.5f) ? 1 : joystick.Vertical;

            rb.AddRelativeForce(horizontal * accelerationSpeed * Time.deltaTime, 0, vertical * accelerationSpeed * Time.deltaTime);
            horizontalMovement = new Vector2(rb.velocity.x, rb.velocity.z);
            if (horizontalMovement.magnitude > maxSpeed)
            {
                horizontalMovement = horizontalMovement.normalized;
                horizontalMovement *= maxSpeed;
            }
            rb.velocity = new Vector3(
                horizontalMovement.x,
                rb.velocity.y,
                horizontalMovement.y
            );
        }
    }
    #endregion
}

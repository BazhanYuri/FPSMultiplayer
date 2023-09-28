using UnityEngine;

public class Movement : MonoBehaviour
{
    public float walkSpeed = 4f;
    public float sprintSpeed = 14f;
    public float maxVelocityChange = 10f;

    private Vector2 input;
    private Rigidbody rb;

    private bool _isSprinting;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        input = new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"), UnityEngine.Input.GetAxisRaw("Vertical"));
        input.Normalize();

        _isSprinting = Input.GetButton("Sprint");
    }

    void FixedUpdate()
    {
        rb.AddForce(CalculateMovement(_isSprinting ? sprintSpeed : walkSpeed), ForceMode.VelocityChange);
    }

    Vector3 CalculateMovement(float _speed)
    {
        Vector3 targetVelocity = new Vector3(input.x, 0, input.y);
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= _speed;

        Vector3 velocity = rb.velocity;

        if (input.magnitude > 0.5f)
        {
            Vector3 velocityChange = targetVelocity - velocity;
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0f;

            return velocityChange;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
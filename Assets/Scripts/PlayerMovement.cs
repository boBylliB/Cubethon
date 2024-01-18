using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    public float forwardForce = 2000f;
    public float forwardForceIncrease = 5f;
    public float sideForce = 200f;

    private int moveInput = 0; // 1 for right, -1 for left
    private float sideForceRatio;

    void Start()
    {
        // We're using a ratio for the strafing force because the forward force increases over time
        // This way it's still easy to modify the strafing force via playtesting but the relative strafing power stays the same throughout the level
        sideForceRatio = sideForce / forwardForce;
    }

    void Update()
    {
        if (Input.GetKey("a") && Input.GetKey("d")) // Handle both movement keys being pressed
        {
            moveInput = 0;
        }
        else if (Input.GetKey("d")) // Right movement
        {
            moveInput = 1;
        }
        else if (Input.GetKey("a")) // Left movement
        {
            moveInput = -1;
        }
        else
        {
            moveInput = 0;
        }
    }

    void FixedUpdate()
    {
        // Applying the forward movement force along the z axis.
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        // Applying the strafing force input by the user
        rb.AddForce(moveInput * forwardForce * sideForceRatio * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        // Increase the forward force over time to increase difficulty
        forwardForce += forwardForceIncrease * Time.deltaTime;

        // Check if the player has fallen off
        if (rb.position.y < -1f)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}

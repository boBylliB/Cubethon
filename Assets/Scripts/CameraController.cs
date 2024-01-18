using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    public Vector3 offset;
    public Vector3 rotationAxis;
    public float maxAngle = 45f;
    public float rotationSpeed = 1f;

    private int rotationDir = 0; // 1 for left, -1 for right

    void Update()
    {
        transform.position = player.position + offset;
        if (Input.GetKey("a") && Input.GetKey("d")) // Handle both movement keys being pressed
        {
            rotationDir = 0;
        }
        else if (Input.GetKey("d")) // Right movement
        {
            rotationDir = -1;
        }
        else if (Input.GetKey("a")) // Left movement
        {
            rotationDir = 1;
        }
        else
        {
            rotationDir = 0;
        }
    }
    void FixedUpdate()
    {
        float currentAngle = transform.eulerAngles.z;
        // Recenter the angle measurement to be within -180 to 180
        if (currentAngle > 180)
        {
            currentAngle -= 360;
        }
        // Rotates the camera by smoothly converging on a target angle about the local z axis
        // This target angle is in the direction specified by the player input and is of the magnitude specified by the maximum angle
        transform.eulerAngles += new Vector3(0, 0, rotationSpeed * Time.deltaTime * (rotationDir * maxAngle - currentAngle));
    }
}

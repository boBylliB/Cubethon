using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    // The below variable defines the desired behaviour of the obstacle, so each obstacle can use the same prefab and behave differently
    // -1 is fleeing, 0 is stationary, and 1 is seeking
    public int seekOrFlee = 0;
    public float speed = 1f;
    public float accel = 1f;
    public float seekDistance = 30f;
    public float fleeDistance = 10f;

    public Material obstacleMat;
    public Color seekColor;
    public Color fleeColor;

    private Vector3 velocity;
    private Transform player;

    void Start()
    {
        // Telegraph the behaviour to the player by changing the color
        if (seekOrFlee > 0)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = seekColor;
        }
        else if (seekOrFlee < 0)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = fleeColor;
        }
        player = FindObjectOfType<PlayerMovement>().gameObject.transform;
    }

    void Update()
    {
        // Handle orientation once per frame
        if (velocity.magnitude > 0)
        {
            transform.eulerAngles = new Vector3(0, Mathf.Atan2(velocity.x, velocity.z) * 180 / Mathf.PI, 0);
        }
    }

    void FixedUpdate()
    {
        // Handle velocity on a fixed timestep
        Vector3 difference = player.position - transform.position;
        // We only want to change the velocity if the player is within the seek or flee distance, to keep level layouts organized
        if ((difference.magnitude < seekDistance && seekOrFlee > 0) || (difference.magnitude < fleeDistance && seekOrFlee < 0))
        {
            // The seekOrFlee variable defines the direction along the player-obstacle vector the obstacle moves
            velocity = seekOrFlee * (player.position - transform.position).normalized * speed;
            // Move the obstacle according to the previously calculated velocity
            transform.position += velocity * Time.deltaTime;
        }
        else
        {
            velocity.Set(0, 0, 0);
        }
        // Update the desired speed by the given acceleration (to match the player acceleration)
        speed += accel * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Player")
        {
            // Setting the obstacle to act as if it was stationary does the same thing as forcing the velocity to 0
            seekOrFlee = 0;
        }
    }
}

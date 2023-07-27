using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float thrust = 250f; // Sets the speed of the ball movement.
    [SerializeField] private Rigidbody rb; // Reference to the ball's Rigidbody component.
    [SerializeField] private float minCamDistance = 5f; // Minimum distance between the ball and the camera.

    private Vector2 lastMousePos; // The 2D vector position of the last mouse position.
    private bool gameStarted = false; // Indicates whether the game has started.

    //[SerializeField] Animator cameraShake;


    private void Start()
    {
        //cameraShake = FindObjectOfType<Camera>().GetComponent<Animator>();
    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            //cameraShake.Play("CameraShake");
            GameManager.singleton.EndGame(false); // End the game if the ball collides with the death object.
        }
    }

    private void Update()
    {
        Vector2 deltaPos = Vector2.zero; // The default 2D vector value.

        // If the left mouse button is pressed or the screen is touched.
        if (Input.GetMouseButton(0))
        {
            // If the game has not started yet.
            if (!gameStarted)
            {
                var x = PlayerPrefs.GetInt("Level");
                if (x < 10) GameManager.singleton.StartGame(); // Start the game.
                gameStarted = true;
            }

            Vector2 currentMousePos = Input.mousePosition;

            // Set the last mouse position if it's not set yet.
            if (lastMousePos == Vector2.zero)
            {
                lastMousePos = currentMousePos;
            }

            // Calculate the difference (applied force direction).
            deltaPos = currentMousePos - lastMousePos;

            // Update the last mouse position.
            lastMousePos = currentMousePos;

            // The force vector applied to the ball in 3D space.
            Vector3 force = new Vector3(deltaPos.x, 0, deltaPos.y) * thrust;

            // Apply the force to the ball's Rigidbody.
            rb.AddForce(force);
        }
        else
        {
            lastMousePos = Vector2.zero; // Reset the last mouse position.
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.singleton.GameEnded)
        {
            return; // Do not execute any further if the game has ended.
        }

        if (gameStarted)
        {
            rb.MovePosition(transform.position + Vector3.forward * 5 * Time.fixedDeltaTime);
        }
    }

    private void LateUpdate()
    {
        // Get the current position of the ball.
        Vector3 pos = transform.position;

        // If the ball gets behind the camera, set its z-position to the camera's + minCamDistance.
        // This ensures the ball is always visible to the player.
        if (transform.position.z < Camera.main.transform.position.z + minCamDistance)
        {
            pos.z = Camera.main.transform.position.z + minCamDistance;
        }

        // Apply the updated position to the ball.
        transform.position = pos;
    }
}

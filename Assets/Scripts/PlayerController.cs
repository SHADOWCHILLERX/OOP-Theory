using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject ballPrefab;
    private GameObject currentBall;
    private float speed = 5;
    public float sensitivity = 2f;
    float xRotation = 0f;
    public Transform playerBody;
    [SerializeField] private float throwSpeed = 15f;
    private float spawnDistance = 1f;
    [SerializeField] private float upwardForce = 0.6f;
    private GameManager gameManager;
    private bool canThrow = true;
    private float throwCooldown = 0.5f;


    
    // Start is called before the first frame update
    void Start()
    {

        // Lock the cursor to the center of the screen and hide it
        //Cursor.lockState = CursorLockMode.Locked;
        gameManager = GameObject.FindObjectOfType<GameManager>();  // Find the GameManager object in the scene
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }

        // Initially, lock the cursor when the game starts
        LockOrUnlockCursor();
    }

    // Update is called once per frame
    void Update()
    {
        
        // Get horizontal and vertical input values
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

        // Normalize the movement vector to prevent faster movement diagonally
        //movement.Normalize();

        // Move the player based on input
        MovePlayer(movement);

        // Rotate the player with the mouse
        RotatePlayerWithMouse();

        // Check for user input to throw the ball
        if (Input.GetMouseButtonDown(0) && gameManager.isGameActive && canThrow == true)
        {
            ThrowBall();
            StartCoroutine(ThrowCooldown());
        }

        // Clamp position to stay within specified boundaries
        float clampedX = Mathf.Clamp(transform.position.x, -15f, 15f);
        float clampedZ = Mathf.Clamp(transform.position.z, -15f, -3f);
        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);

        // Check for UI interactions or other conditions to determine if cursor should be unlocked
        if (gameManager != null && !gameManager.isGameActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            // If the game is active, lock the cursor
            LockOrUnlockCursor();
        }
    }

    IEnumerator ThrowCooldown()
    {
        canThrow = false;
        yield return new WaitForSeconds(throwCooldown);
        canThrow = true;
    }
    void ThrowBall()
    {
        // Calculate the offset in front of the player where the ball should be instantiated
        Vector3 spawnPosition = transform.position + transform.forward * spawnDistance;

        // Instantiate the ball prefab at the calculated position and rotation
        GameObject ball = Instantiate(ballPrefab, spawnPosition, transform.rotation);

        // Get the rigidbody component of the instantiated ball
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();

        if (ballRigidbody != null)
        {
            // Set the velocity of the ball with an upward force
            Vector3 throwDirection = transform.forward + transform.up * upwardForce;

            // Normalize the direction to maintain the same overall speed
            throwDirection.Normalize();

            // Set the velocity of the ball based on the player's facing direction
            ballRigidbody.velocity = throwDirection * throwSpeed;
        }
        else
        {
            Debug.LogError("Ball prefab does not have a Rigidbody component!");
        }
    }

    void MovePlayer(Vector3 movement)
    {
        if (gameManager !=null && gameManager.isGameActive)
        {
            // Translate the player's position based on input and speed
            transform.Translate(movement * speed * Time.deltaTime);
        }
        
    }


    void RotatePlayerWithMouse()
    {
        if (gameManager.isGameActive) 
        {
            // Get mouse input for rotation
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Rotate the player around the Y-axis based on mouse input
            transform.Rotate(Vector3.up * mouseX * sensitivity);

            // Rotate the camera around the X-axis based on mouse input
            xRotation -= mouseY * sensitivity;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // Apply the rotation to the camera
            Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }

    void LockOrUnlockCursor()
    {
        // Use this method to determine whether to lock or unlock the cursor
        Cursor.lockState = (gameManager != null && gameManager.isGameActive) ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = (gameManager == null || !gameManager.isGameActive); // Optionally, make the cursor visible when not locked
    }

}
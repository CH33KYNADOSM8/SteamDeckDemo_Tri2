using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static Movement Instance { get; private set; }


    public float movementSpeed = 1000f;                       // Adjust Movement speed in the inspector
    public float mouseSensitivity = 2.0f;                      // Adjust the mouse sensitivity in the inspector
    public Vector2 turn;
    public float turnSpeed = 5;

    //private float verticalRotation = 0;                        // Adjust the Vertical rotation
    private Rigidbody rb;                                      // To check the physics of the character

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /* Step 1: We are grabbing the rigid body  */
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerLookAtMouse();

        WASD();
    }

    void PlayerLookAtMouse()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float hitDistance;

        if (playerPlane.Raycast(ray, out hitDistance))
        {

            Vector3 targetPoint = ray.GetPoint(hitDistance);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

    /* Step 1: Takes the horizontal input from WASD or arrow keys
     * Step 2: Takes the vertical input from WASD
     * Step 3: Control them using MoveSpeed and Time.deltaTime
     * Step 4: make sure the player is facing the right direction
     * Step 5: Set the velocity of the rb to match Y */

    private void WASD()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        float moveVertical = Input.GetAxis("Vertical");


        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical) * movementSpeed;

        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    } 
}

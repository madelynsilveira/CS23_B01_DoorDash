using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private GameController gameController;
    public float moveSpeed = 10f;
    public float rotationSpeed = 90f; 
    public float angularDrag = 2f; 
    public float bounceForce = 50f;
    public int deliveryCount;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularDrag = angularDrag; // Set the angular drag value
    }
    void Update()
    {
        // Get input from arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 moveDirection = new Vector3(0f, 0f, verticalInput).normalized;

        // Calculate rotation based on horizontal input
        float rotation = horizontalInput * rotationSpeed * Time.deltaTime;

        // Rotate the character
        transform.Rotate(Vector3.up * rotation);

        // Move the character forward or backward
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
    // private void OnTriggerEnter(Collider other)
    // {
    //     if(other.gameObject.CompareTag("PickUp") && gameController.deliveries < 3){
    //         other.gameObject.SetActive(false);
    //         deliveryCount++;
    //     }

    //     if(other.gameObject.CompareTag("Trampoline")) {
    //         Debug.Log("Hit a trampoline.");

    //         // Calculate the bounce direction (assuming upward).
    //         Vector3 bounceDirection = Vector3.up;

    //         // Apply the bounce force to the object's Rigidbody.
    //         rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
    //     }
    // }
}




// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;

// public class PlayerController : MonoBehaviour
// {
//     private GameController gameController;
//     private Rigidbody rb;
//     private float movementX;
//     private float movementY;
//     public float bounceForce = 50f;

//     public int deliveryCount;

//     public float speed = 0;
//     // Start is called before the first frame update
//     void Start()
//     {
//         rb = GetComponent<Rigidbody>();
//         deliveryCount = 0;

//         if (GameObject.FindWithTag ("GameController") != null) { 
//             gameController = GameObject.FindWithTag ("GameController").GetComponent<GameController>();
//         }
//     }

//     void OnMove(InputValue movementValue)
//     {
//         Vector2 movementVector = movementValue.Get<Vector2>();
//         movementX = movementVector.x;
//         movementY = movementVector.y;
//         Debug.Log("moving!" + this.gameObject.name);
//     }

//     void FixedUpdate()
//     {
//         Vector3 movement = new Vector3(movementX, movementY, movementY);
//         rb.AddForce(movement * speed * (Time.fixedDeltaTime + 1));

//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         if(other.gameObject.CompareTag("PickUp") && gameController.deliveries < 3){
//             other.gameObject.SetActive(false);
//             deliveryCount++;
//         }

//         if(other.gameObject.CompareTag("Trampoline")) {
//             Debug.Log("Hit a trampoline.");

//             // Calculate the bounce direction (assuming upward).
//             Vector3 bounceDirection = Vector3.up;

//             // Apply the bounce force to the object's Rigidbody.
//             rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
//         }
//     }
// }

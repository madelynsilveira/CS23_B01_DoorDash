using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private GameController gameController;
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    public int deliveryCount;

    public float speed = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularDrag = 40;
        deliveryCount = 0;

        if (GameObject.FindWithTag ("GameController") != null) { gameController = GameObject.FindWithTag ("GameController").GetComponent<GameController>();
         }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed * (Time.fixedDeltaTime + 1));

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp") && gameController.deliveries < 3){
            other.gameObject.SetActive(false);
            deliveryCount++;
        }
    }
}

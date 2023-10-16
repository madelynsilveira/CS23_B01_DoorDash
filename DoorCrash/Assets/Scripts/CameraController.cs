using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float distance = 10f; 
    public float rotationSpeed = 5f; // camera rotation speed
    public float maxCameraSpeed = 10f; // max camera speed
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        offset = offset.normalized * distance; 
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float playerRotation = player.transform.rotation.eulerAngles.y;

        // rotate based on position
        Quaternion rotation = Quaternion.Euler(0, playerRotation, 0);
        Vector3 rotatedOffset = rotation * offset;
        Vector3 desiredPosition = player.transform.position + rotatedOffset;

        // limit camera speed
        float distanceToDesiredPosition = Vector3.Distance(transform.position, desiredPosition);
        float maxMoveDistance = maxCameraSpeed * Time.deltaTime;
        if (distanceToDesiredPosition > maxMoveDistance) {
            transform.position = Vector3.Lerp(transform.position, transform.position + (desiredPosition - transform.position).normalized * maxMoveDistance, 1f);
        } else {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 5f);
        }

        // lerp camera rotation for smooth
        Quaternion currentRotation = transform.rotation;
        Quaternion desiredRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(currentRotation, desiredRotation, rotationSpeed * Time.deltaTime);
        
    }
}

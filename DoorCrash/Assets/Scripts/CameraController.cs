using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float distance = 10f; // how far away camera is
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
        // new camera position with the desired distance
        Vector3 desiredPosition = player.transform.position + offset;

        // this is for smoothing the camera so ball can speed up
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 5f);
    }
}

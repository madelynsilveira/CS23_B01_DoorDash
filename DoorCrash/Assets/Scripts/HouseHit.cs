using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseHit : MonoBehaviour
{
    private GameController gameController;
    private bool isHit = false; // Flag to track if the house has been hit
    // hardcoded to save time rn but should fix later
    private Color housePrefabColor = new Color(0.747f, 0.785f, 0.821f, 1.000f); // you could softcode this
    private Color[] houseColors = new Color[] {
            new Color(1.0f, 0.0f, 0.0f),     // Red
            new Color(1.0f, 0.5f, 0.0f),     // Orange
            new Color(1.0f, 1.0f, 0.0f),     // Yellow
            new Color(0.0f, 1.0f, 0.0f),     // Green
            new Color(0.0f, 0.0f, 1.0f),     // Blue
            new Color(0.3f, 0.0f, 0.5f),     // Indigo
            new Color(0.5f, 0.0f, 0.5f),      // Violet
            new Color(1.0f, 0.7f, 0.7f),     // Light Red
            new Color(1.0f, 0.8f, 0.6f),     // Light Orange
            new Color(1.0f, 1.0f, 0.7f),     // Light Yellow
            new Color(0.7f, 1.0f, 0.7f),     // Light Green
            new Color(0.7f, 0.7f, 1.0f),     // Light Blue
            new Color(0.7f, 0.5f, 0.7f),     // Light Indigo
            new Color(0.8f, 0.6f, 0.8f)      // Light Violet
        };

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (!isHit && other.gameObject.CompareTag("Player")) {
            Debug.Log("Hit");
            // Set the flag to true to prevent further collisions - omitting this because of unintended bug
            // isHit = true; 

            // get house color 
            Renderer houseRenderer = GetComponent<Renderer>();
            Color houseColor = houseRenderer.material.color;

            // Better way to do this later
            bool isInHouseColors = false;
            foreach (Color color in houseColors) {
                if (houseColor == color) {
                    isInHouseColors = true;
                    break; 
                }
            }

            // If this house needed a delivery update back to default color
            if (isInHouseColors) {
                gameController.RemoveDelivery();
                gameController.AddMoney(UnityEngine.Random.Range(10, 31));
                houseRenderer.material.color = housePrefabColor;
            } 

        }
    }
}

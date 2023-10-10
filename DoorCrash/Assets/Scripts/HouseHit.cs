using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseHit : MonoBehaviour
{
    private GameController gameController;
    private bool isHit = false; // Flag to track if the house has been hit
    // hardcoded to save time rn but should fix later
    private Color houseRoofColor; // you could softcode this
    private Color[] houseColors = new Color[] {
        new Color(1.0f, 0.0f, 0.0f),     // Red
        new Color(1.0f, 0.5f, 0.0f),     // Orange
        new Color(1.0f, 1.0f, 0.0f),     // Yellow
        new Color(0.0f, 1.0f, 0.0f),     // Green
        new Color(0.0f, 0.0f, 1.0f),     // Blue
        new Color(0.3f, 0.0f, 0.5f),     // Indigo
        new Color(0.5f, 0.0f, 0.5f),     // Violet
        new Color(0.5f, 0.0f, 0.0f),     // Dark Red
        new Color(0.7f, 0.35f, 0.0f),    // Dark Orange
        new Color(0.7f, 0.7f, 0.0f),     // Dark Yellow
        new Color(0.0f, 0.7f, 0.0f),     // Dark Green
        new Color(0.0f, 0.0f, 0.5f),     // Dark Blue
        new Color(0.15f, 0.0f, 0.25f),   // Dark Indigo
        new Color(0.25f, 0.0f, 0.25f),   // Dark Violet
        new Color(0.7f, 0.0f, 0.0f),     // Darker Red
        new Color(0.9f, 0.45f, 0.0f),    // Darker Orange
        new Color(0.9f, 0.9f, 0.0f),     // Darker Yellow
        new Color(0.0f, 0.9f, 0.0f),     // Darker Green
        new Color(0.0f, 0.0f, 0.7f),     // Darker Blue
        new Color(0.2f, 0.0f, 0.4f),     // Darker Indigo
        new Color(0.4f, 0.0f, 0.4f)      // Darker Violet
        };

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        houseRoofColor = GameController.HexToColor("#686868");
    }

    void OnCollisionEnter(Collision other)
    {
        if (!isHit && other.gameObject.CompareTag("Player")) {
            // Debug.Log("Hit");
            // Set the flag to true to prevent further collisions - omitting this because of unintended bug
            // isHit = true; 

            // get house color 
            MeshRenderer houseRenderer = GetComponent<MeshRenderer>();
            Color houseColor = houseRenderer.materials[2].color;

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
                houseRenderer.materials[2].color = houseRoofColor;
                houseRenderer.materials[2].SetColor("_EmissionColor", houseRoofColor);
                houseRenderer.materials[2].SetFloat("_EmissionIntensity", 0.0f);
            } 

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxScript : MonoBehaviour
{
    // Control the atmosphere thickness over time
    public float minAtmosphereThickness = 0.1f;
    public float maxAtmosphereThickness = 1.0f;
    public float changeSpeed = 0.2f;

    private float currentAtmosphereThickness;

    private void Start()
    {
        // Initialize the current atmosphere thickness
        currentAtmosphereThickness = minAtmosphereThickness;
    }

    private void Update()
    {
        // Change the atmosphere thickness over time
        currentAtmosphereThickness += changeSpeed * Time.deltaTime;

        // Ensure the atmosphere thickness stays within the specified range
        currentAtmosphereThickness = Mathf.Clamp(currentAtmosphereThickness, minAtmosphereThickness, maxAtmosphereThickness);

        // Set the atmosphere thickness property in the skybox material
        RenderSettings.skybox.SetFloat("_AtmosphereThickness", currentAtmosphereThickness);
    }
}

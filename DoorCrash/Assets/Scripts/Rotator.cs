using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

        private GameController gameController; // #1: add variable to hold GameController
        private Color[] cubeColors = new Color[] {
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

       void Start () {                                           // #2: assign actual Game Controller to variable
          if (GameObject.FindWithTag ("GameController") != null) { gameController = GameObject.FindWithTag ("GameController").GetComponent<GameController>();
         }

         SetCubeColors();
      }

       void Update () {
             transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
       }

       void OnTriggerEnter(Collider other){
             if (other.gameObject.tag == "Player") {
                if (gameController.deliveries < 3){
                  // collision detection
                  GetComponent<Collider>().enabled = false;
                  
                  // use color of cube to update delivery
                  Renderer cubeRenderer = GetComponent<Renderer>();
                  Color cubeColor = cubeRenderer.material.color;
                  gameController.AddDelivery (cubeColor);         
                  StartCoroutine(DestroyThis());
                } 
             }
       }

      void SetCubeColors() {
      GameObject[] cubes = GameObject.FindGameObjectsWithTag("PickUp");

      // assign colors from the rainbow array
      for (int i = 0; i < cubes.Length; i++)
      {
            if (i < cubeColors.Length)
            {
                  Renderer cubeRenderer = cubes[i].GetComponent<Renderer>();
                  cubeRenderer.material.color = cubeColors[i];
                  cubeRenderer.material.SetColor("_EmissionColor", cubeColors[i]);
                  cubeRenderer.material.SetFloat("_EmissionIntensity", 1.0f);
            }
            else
            {
                  Debug.LogWarning("Not enough rainbow colors for all cubes.");
                  break;
            }
      }
      }

       IEnumerator DestroyThis(){
             yield return new WaitForSeconds(0.5f);
             Destroy(gameObject);
       }
}
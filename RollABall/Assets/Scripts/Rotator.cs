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
            new Color(0.5f, 0.0f, 0.5f),      // Violet
            new Color(1.0f, 0.7f, 0.7f),     // Light Red
            new Color(1.0f, 0.8f, 0.6f),     // Light Orange
            new Color(1.0f, 1.0f, 0.7f),     // Light Yellow
            new Color(0.7f, 1.0f, 0.7f),     // Light Green
            new Color(0.7f, 0.7f, 1.0f),     // Light Blue
            new Color(0.7f, 0.5f, 0.7f),     // Light Indigo
            new Color(0.8f, 0.6f, 0.8f)      // Light Violet
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
                // collision detection
                GetComponent<Collider>().enabled = false;
                
                // use color of cube to update delivery
                Renderer cubeRenderer = GetComponent<Renderer>();
                Color cubeColor = cubeRenderer.material.color;
                gameController.AddDelivery (cubeColor);         
                StartCoroutine(DestroyThis());
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
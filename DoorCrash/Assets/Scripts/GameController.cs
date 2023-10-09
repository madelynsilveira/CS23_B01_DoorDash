using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
        public GameObject textGameObject;
        public GameObject moneyGameObject;
        Color housePrefabColor = new Color(0.747f, 0.785f, 0.821f, 1.000f);

        private int money;
        public int deliveries;
        private int totalDeliveries = 0;
        private bool winCondition = false;
        private int pickupNumber = 10;

       void Start () {
            // initialize delivery count
            deliveries = 0;
            money = 0;
            UpdateDeliveries();
            UpdateMoney();
        
        }

        void Update(){       
            if (Input.GetKey("escape")){
                Application.Quit();
            }
        }

        public void RestartGame(){
            money = 0;
            deliveries = 0;
            totalDeliveries = 0;
            winCondition = false;
            SceneManager.LoadScene("TaskGame");
        }

        public void QuitGame(){
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }

       public void AddDelivery (Color color) {
             deliveries++;
             totalDeliveries++;
             ChangeRandomHouseColor(color);
             UpdateDeliveries ();
            if (totalDeliveries == pickupNumber) {
                winCondition = true;
                Debug.Log("You have cleared this round!");
            }
            Debug.Log("Total deliveries = " + totalDeliveries);
        }

        public void RemoveDelivery () {
             deliveries--;
             UpdateDeliveries ();
        }

        public void AddMoney (int newMoney) {
            money +=  newMoney;
            UpdateMoney();
        }

       void UpdateDeliveries () {
            Text deliveryTextB = textGameObject.GetComponent<Text>();
            if(deliveries == 3){
                deliveryTextB.text = "Deliveries: 3 (Max)";
            }
            else{
                deliveryTextB.text = "Deliveries: "+deliveries;
            }
        }

        void UpdateMoney () {
            Text moneyText = moneyGameObject.GetComponent<Text>();
            moneyText.text = "Money: " + money;
        }

        bool AreColorsCloseEnough(Color a, Color b, float tolerance = 0.01f) {
            return Mathf.Abs(a.r - b.r) <= tolerance &&
                Mathf.Abs(a.g - b.g) <= tolerance &&
                Mathf.Abs(a.b - b.b) <= tolerance &&
                Mathf.Abs(a.a - b.a) <= tolerance;
        }


        // change color of random house to collor of collided cube
        void ChangeRandomHouseColor (Color color) {
            
            // get list of house prefabs
            GameObject[] houses = GameObject.FindGameObjectsWithTag("House"); 
            if (houses.Length > 0) {
            
                // find houses with starting hosue prefab color
                List<int> housesWithoutDeliveries = new List<int>();
                
                // TODO: this might not be working to not rewrite houses that are already colored
                for (int i = 0; i < houses.Length; i++) {
                    Renderer houseRenderer = houses[i].GetComponent<Renderer>();
                    Debug.Log("Renderer material color is "+ houseRenderer.material.color);
                    if (AreColorsCloseEnough(houseRenderer.material.color,housePrefabColor)) {
                        Debug.Log("Found a house without a delivery, index is "+i);
                        housesWithoutDeliveries.Add(i);
                    }
                }

                // assign undelivered house the current delivery color
                if (houses.Length > 0) {
                        
                        int undeliveredHouseIndex = housesWithoutDeliveries[Random.Range(0, houses.Length)];
                        GameObject randomHouse = houses[undeliveredHouseIndex];
                        Renderer houseRenderer = randomHouse.GetComponent<Renderer>();
                        houseRenderer.material.color = color;
                        houseRenderer.material.SetColor("_EmissionColor", color);
                        houseRenderer.material.SetFloat("_EmissionIntensity", 5.0f);
                }
            }
        }
}
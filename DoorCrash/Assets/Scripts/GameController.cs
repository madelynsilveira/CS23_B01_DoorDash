using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
        public GameObject textGameObject;
        public GameObject moneyGameObject;
        Color housePrefabColor = new Color(0.747f, 0.785f, 0.821f, 1.000f);

        public int deliveries;
        private int money;

       void Start () {
            // initialize delivery count
            deliveries = 0;
            money = 0;
            UpdateDeliveries();
            UpdateMoney();
        
        }

       public void AddDelivery (Color color) {
             deliveries++;
             ChangeRandomHouseColor(color);
             UpdateDeliveries ();
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
                deliveryTextB.text = "Deliveries: 3 (Maximum)";
            }
            else{
                deliveryTextB.text = "Deliveries: "+deliveries;
            }
            
        }

        void UpdateMoney () {
            Text moneyText = moneyGameObject.GetComponent<Text>();
            Debug.Log("in money");
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
                        Debug.Log("Undelivered House Index is "+undeliveredHouseIndex);
                        GameObject randomHouse = houses[undeliveredHouseIndex];
                        Renderer houseRenderer = randomHouse.GetComponent<Renderer>();
                        houseRenderer.material.color = color;
                }
            }
        }
}
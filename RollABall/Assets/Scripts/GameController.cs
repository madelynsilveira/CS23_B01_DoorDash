using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
        public GameObject textGameObject;
        public GameObject moneyGameObject;
        Color housePrefabColor = new Color(0.747f, 0.785f, 0.821f, 1.000f);

        private int deliveries;
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
            deliveryTextB.text = "Deliveries: " + deliveries;
        }

        void UpdateMoney () {
            Text moneyText = moneyGameObject.GetComponent<Text>();
            Debug.Log("in money");
            moneyText.text = "Money: " + money;
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
                    if (houseRenderer.material.color == housePrefabColor) {
                        housesWithoutDeliveries.Add(i);
                    }
                }

                // assign undelivered house the current delivery color
                if (houses.Length > 0) {

                        GameObject randomHouse = houses[Random.Range(0, houses.Length)];
                        Renderer houseRenderer = randomHouse.GetComponent<Renderer>();
                        houseRenderer.material.color = color;
                }
            }
        }
}
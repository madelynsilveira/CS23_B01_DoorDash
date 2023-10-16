using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
        public GameObject textGameObject;
        public GameObject moneyGameObject;

        public DateTime startTime;
        public DateTime endTime;
        public int roundTime;
        
        //TODO do this another way
        Color housePrefabColor = HexToColor("#686868");

        private int money;
        public int deliveries;
        private int totalDeliveries = 0;
        private bool winCondition = false;
        public int pickupNumber = 10;

       void Start () {
            
            if (SceneManager.GetActiveScene().name == "LevelOne"){
                // initialize delivery count
                startTime = DateTime.Now;
                deliveries = 0;
                money = 0;
                UpdateDeliveries();
                UpdateMoney();

                textGameObject = GameObject.FindGameObjectsWithTag("ScoreText")[0];
                moneyGameObject = GameObject.FindGameObjectsWithTag("MoneyText")[0];
            }
            
        }

        void Update(){       
            if (Input.GetKey("escape")){
                Application.Quit();
            }
        }

        public void EndSceneYouLose(){
            SceneManager.LoadScene("YouLose");
        }

        public void EndSceneYouWin(){
            SceneManager.LoadScene("YouWin");
        }

        public void RestartGame(){
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene("LevelOne");
        }

        public void QuitGame(){
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }

        public void winCheck(){
            if (totalDeliveries == pickupNumber && deliveries == 0) {
                winCondition = true;

                Debug.Log("You have cleared this round!" + winCondition);
                endTime = DateTime.Now;
                TimeSpan difference = endTime - startTime;
                roundTime = (int)difference.TotalSeconds;
                Debug.Log("Seconds elapsed: "+roundTime);
                EndSceneYouWin();
            }
        }

        // hitting a pikcup box
       public void AddDelivery (Color color) {
             deliveries++;
             totalDeliveries++;
             ChangeRandomHouseColor(color);
             ChangeCarColor(color);
             UpdateDeliveries ();
            
            Debug.Log("Total deliveries = " + totalDeliveries);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Your code to execute after a new scene is loaded
        Debug.Log("Scene Loaded: " + scene.name);

        if (scene.name == "LevelOne")
        {
            startTime = DateTime.Now;
            money = 0;
            deliveries = 0;
            totalDeliveries = 0;
            winCondition = false;
            

            try{
                textGameObject = GameObject.FindGameObjectsWithTag("ScoreText")[0];
                moneyGameObject = GameObject.FindGameObjectsWithTag("MoneyText")[0];
            }
            catch(Exception ex) {
                Debug.Log("Exception: "+ex.Message);
                textGameObject = null;
                moneyGameObject = null;
            }
        }
        if (scene.name == "YouWin" || scene.name == "YouLose")
        {
            Button button = GameObject.FindGameObjectsWithTag("PlayAgainButton")[0].GetComponent<Button>();
            button.onClick.AddListener(RestartGame);
        }
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

                Debug.Log("found more than one house!");
            
                // find houses with starting hosue prefab color
                List<int> housesWithoutDeliveries = new List<int>();
                
                // TODO: this might not be working to not rewrite houses that are already colored
                for (int i = 0; i < houses.Length; i++) {
                    MeshRenderer houseRenderer = houses[i].GetComponent<MeshRenderer>();
                    if (AreColorsCloseEnough(houseRenderer.materials[2].color,housePrefabColor)) {
                        housesWithoutDeliveries.Add(i);
                    }
                }

                // assign undelivered house the current delivery color
                if (housesWithoutDeliveries.Count > 0) {
                        int undeliveredHouseIndex = housesWithoutDeliveries[UnityEngine.Random.Range(0, housesWithoutDeliveries.Count)];
                        GameObject randomHouse = houses[undeliveredHouseIndex];
                        Debug.Log("Got a house, index is "+undeliveredHouseIndex);
                        Renderer houseRenderer = randomHouse.GetComponent<MeshRenderer>();
                        houseRenderer.materials[2].color = color;
                        houseRenderer.materials[2].SetColor("_EmissionColor", color);
                        houseRenderer.materials[2].SetFloat("_EmissionIntensity", 5.0f);
                }
            }
        }

        public static Color HexToColor(string hex)
        {
            // Ensure the hash symbol is removed
            hex = hex.Replace("#", "");

            byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            byte a = 255;  // Default value for alpha

            // Check if the hex string includes an alpha value
            if (hex.Length == 8)
            {
                a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
            }

            return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
        }

    private void ChangeCarColor(Color color) {
        // grab necessary variables
        GameObject car= GameObject.FindGameObjectsWithTag("Player")[0];
        Renderer carRenderer = car.GetComponent<MeshRenderer>();
        Material carBaseMaterial = carRenderer.materials[0];
        Color originalColor = carBaseMaterial.color;

        // change car base
        carBaseMaterial.color = color; 
        carBaseMaterial.SetColor("_EmissionColor", color);
        carBaseMaterial.SetFloat("_EmissionIntensity", 5.0f);
        StartCoroutine(RevertCarColor(carBaseMaterial, originalColor, 0.5f));
    }

    private IEnumerator RevertCarColor(Material carMaterial, Color originalColor, float duration)
    {
        yield return new WaitForSeconds(duration);
        carMaterial.color = originalColor;
    }


}
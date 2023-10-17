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
        Color housePrefabColor = HexToColor("#686868");

        private int money;
        public int deliveries;
        private int totalDeliveries = 0;
        private bool winCondition = false;
        public int pickupNumber = 10;

        public string nextScene;
        public string currScene;

       void Start () {
            Debug.Log("Running start.");
            
            if (SceneManager.GetActiveScene().name == "LevelOne" || 
                SceneManager.GetActiveScene().name == "LevelZero"||
                SceneManager.GetActiveScene().name == "LevelTwo"){
                // initialize delivery count
                startTime = DateTime.Now;
                deliveries = 0;
                money = 0;
                UpdateDeliveries();
                UpdateMoney();

                textGameObject = GameObject.FindGameObjectsWithTag("ScoreText")[0];
                moneyGameObject = GameObject.FindGameObjectsWithTag("MoneyText")[0];

                SceneManager.sceneLoaded += OnSceneLoaded;
            }
            if(SceneManager.GetActiveScene().name == "LevelZero"){
                nextScene = "LevelOne";
                currScene = "LevelZero";
            }
            if(SceneManager.GetActiveScene().name == "LevelOne"){
                nextScene = "LevelTwo";
                currScene = "LevelOne";
            }
            if(SceneManager.GetActiveScene().name == "LevelTwo"){
                nextScene = "GameEnd";
                currScene = "Level Two";
            }
            Debug.Log("Next scene is "+nextScene);
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
        if(SceneManager.GetActiveScene().name == "LevelZero"){
                nextScene = "LevelOne";
                currScene = "LevelZero";
            }
            if(SceneManager.GetActiveScene().name == "LevelOne"){
                nextScene = "LevelTwo";
                currScene = "LevelOne";
            }
            if(SceneManager.GetActiveScene().name == "LevelTwo"){
                nextScene = "GameEnd";
                currScene = "Level Two";
            }
        // Your code to execute after a new scene is loaded
        Debug.Log("Scene Loaded: " + scene.name);

        if (SceneManager.GetActiveScene().name == "LevelOne" || 
                SceneManager.GetActiveScene().name == "LevelZero"||
                SceneManager.GetActiveScene().name == "LevelTwo"){
                // initialize delivery count
                totalDeliveries = 0;
                startTime = DateTime.Now;
                deliveries = 0;
                money = 0;

                textGameObject = GameObject.FindGameObjectsWithTag("ScoreText")[0];
                moneyGameObject = GameObject.FindGameObjectsWithTag("MoneyText")[0];

                UpdateDeliveries();
                UpdateMoney();

                
            }
            if(SceneManager.GetActiveScene().name == "LevelZero"){
                nextScene = "LevelOne";
            }
            if(SceneManager.GetActiveScene().name == "LevelOne"){
                nextScene = "LevelTwo";
            }
            if(SceneManager.GetActiveScene().name == "LevelTwo"){
                nextScene = "LevelTwo";
            }
            Debug.Log("Next scene is "+nextScene);    
            if (scene.name == "YouWin")
            {
                Button nextButton = GameObject.FindGameObjectsWithTag("NextButton")[0].GetComponent<Button>();
                if(nextScene == "LevelOne" || nextScene == "LevelTwo") {
                    // Access the Text component of the Button and set its text
                    Text buttonText = nextButton.GetComponentInChildren<Text>();
                    if (buttonText != null)
                    {
                        buttonText.text = "Next Level";
                        nextButton.onClick.AddListener(loadNextLevel);
                    }
                    else
                    {
                        Debug.LogWarning("No Text component found in the children of the NextButton.");
                    }
                }
            }
            if (scene.name == "YouLose")
            {
                Button nextButton = GameObject.FindGameObjectsWithTag("NextButton")[0].GetComponent<Button>();
                    // Access the Text component of the Button and set its text
                    Text buttonText = nextButton.GetComponentInChildren<Text>();
                    if (buttonText != null)
                    {
                        buttonText.text = "Try Again";
                        nextButton.onClick.AddListener(loadCurrLevel);
                    }
                    else
                    {
                        Debug.LogWarning("No Text component found in the children of the NextButton.");
                    }
                }
        }
        
        public void loadNextLevel () {
            SceneManager.LoadScene(nextScene);
        }
        
        public void loadCurrLevel() {
            SceneManager.LoadScene(currScene);
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
        Debug.Log(carRenderer.materials[0]);

        // change car base
        carBaseMaterial.color = color; 
        StartCoroutine(RevertCarColor(carBaseMaterial, originalColor, 0.5f));
    }

    private IEnumerator RevertCarColor(Material carBaseMaterial, Color originalColor, float duration)
    {
        yield return new WaitForSeconds(duration);
        carBaseMaterial.color = originalColor;
    }
}
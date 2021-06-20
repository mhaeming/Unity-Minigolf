using System.Collections.Generic;
using Code.CSV;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Code
{
    public class WorldManager : MonoBehaviour
    {
   
        public bool riskPrime = false;
        public KeyCode input;
        [Header("Testing Options")]
        public bool skipTutorial;
        public float primingChance = 0.5f;
        private GameObject _experimentManager;
        [HideInInspector]
        public bool timeout = false;
        public float timeLimit = 120;
    
        // some potential variables to keep track of for CSV
        [HideInInspector]
        public int numberResets = 0;
        [HideInInspector]
        public int numberPits = 0;
        [HideInInspector]
        public int numberObstacles = 0;
        [HideInInspector]
        public int resetObstacle = 0;
        [HideInInspector]
        public int resetFall = 0;
    
        // create lists to store data in a csv file later
        public List<List<string>> csvData = new List<List<string>>();
        private List<string> _csvHeaders = new List<string>();
    
        void Start()
        {
            defineCSVHeader();
            DontDestroyOnLoad(gameObject);
            if (Random.value <= primingChance)
            {
                riskPrime = true;
            }
        }
    
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(input) && SceneManager.GetActiveScene().name == "StartScene")
            {
                if (skipTutorial)
                {
                    SceneManager.LoadScene("Priming");
                }
                else
                {
                    SceneManager.LoadScene("TrainingScene");
                }
            
            }

            if (Input.GetKeyDown(input) && SceneManager.GetActiveScene().name == "Priming")
            {
                SceneManager.LoadScene("SampleScene");
                _experimentManager = GameObject.Find("GameManager");
            }
        
            // use Time.time if this is not wanted
            // only load TimeOut Screen once
            if (Time.timeSinceLevelLoad > timeLimit && SceneManager.GetActiveScene().name != "Timeout")
            {
                Debug.Log("timeout"); // end the game here
                timeout = true;
                SceneManager.LoadScene("Timeout");
                // once Timeout is reached convert collected stats into CSV File 
                List<string> csv = CSVTools.GenerateCsv(csvData, _csvHeaders);
                CSVTools.SaveCsv(csv, Application.dataPath + "/Assets/CSVData/" + GUID.Generate());
            }
        }
    
        void defineCSVHeader()
        {
            // define variables saved in CSV file
            _csvHeaders.Add("Primed for Risk");
            _csvHeaders.Add("Number Obstacles");
            _csvHeaders.Add("Number Pits");
            _csvHeaders.Add("Current z-Pos");
            _csvHeaders.Add("Current Velocity");
            _csvHeaders.Add("Distance to closest Obstacle");
            _csvHeaders.Add("Overall Resets");
            _csvHeaders.Add("Resets due to Obstacles");
            _csvHeaders.Add("Resets due to Fall");
        }
    
    }
}

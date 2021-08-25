using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Code.CSV;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Experiment
{
    public class ExperimentManager : MonoBehaviour
    {
        private int _prob;
        private string _fileAddress;
    
        public Statistics savedData = new Statistics();
    
        // make sure that the data saved here won't be deleted when transitioning between scenes:
        public static ExperimentManager Instance;
        void Awake ()   
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy (gameObject);
            }

            // handle which of the two groups the trial belongs to
            if (FullSceneManager.CurrentScene == FullSceneManager.sceneEnum.Start)
            {
                // randomly choose one of the two groups (experimental or control)
                _prob = Random.Range(0, 2);

                if (_prob == 0)
                {
                    savedData.isDecision = false;
                    Debug.Log("You've landed in the control group.");
                }
                else
                {
                    savedData.isDecision = true;
                    Debug.Log("You've landed in the experimental group.");
                }   
            }
        }

        private void Start()
        {
            _fileAddress = Application.dataPath + "/Assets/CSVData/data.csv";
            _fileAddress.ToString();
        }

        private void OnDisable()
        {
            if (FullSceneManager.CurrentScene == FullSceneManager.sceneEnum.End)
            {
                defineCsvHeader();
            
                // only create the csv line when the data has been collected and saved to savedData Statistics
                if (savedData.dataCollected)
                {
                    CreateCsvLine();
                }
        
                // only create the csv file when the data has been collected and a csv file has not yet been created
                if (savedData.dataCollected && !savedData.csvCreated)
                {
                    /*List<string> csv = CSVTools.CreateCsv(savedData.csvData, savedData.csvHeader);
                CSVTools.SaveCsv(csv, Application.dataPath + "/Assets/CSVData/data" + GUID.Generate());
                Debug.Log("Created CSV file.");
                savedData.csvCreated = true;*/
                    if (File.Exists(_fileAddress))
                    {
                        Debug.Log("UpdateCsv");
                        CSVTools.UpdateCsv(savedData.csvData, _fileAddress);
                    }
                    else
                    {
                        CSVTools.CreateEmptyCsv(savedData.csvHeader, _fileAddress);
                        Debug.Log("UpdateCsv");
                        CSVTools.UpdateCsv(savedData.csvData, _fileAddress);
                    }
                }
            }
        }
    

        private void defineCsvHeader()
        {
            //savedData.csvHeader.Add("subjectNR");
            //savedData.csvHeader.Add("trialNR");
            savedData.csvHeader.Add("isDecision");
            savedData.csvHeader.Add("time");
            savedData.csvHeader.Add("items");
            savedData.csvHeader.Add("interactions");
            savedData.csvHeader.Add("metres");
            savedData.csvHeader.Add("failures");
            savedData.csvHeader.Add("levels");
        }
    
        void CreateCsvLine()
        {
            List<string> csvLine = new List<string>();

            //csvLine.Add(savedData.subjectNr.ToString());
            //csvLine.Add(savedData.trialNr.ToString());
        
            // "Yes" for experimental group, "No" for control group
            if (savedData.isDecision)
            {
                csvLine.Add("Yes");
            }
            else
            {
                csvLine.Add("No");
            }
        
            csvLine.Add(savedData.time.ToString(CultureInfo.InvariantCulture)); // unrounded data

            csvLine.Add(savedData.items.ToString()); 
            csvLine.Add(savedData.interactions.ToString());

            csvLine.Add(savedData.metres.ToString(CultureInfo.InvariantCulture));
            csvLine.Add(savedData.failures.ToString());
            csvLine.Add(savedData.levels.ToString());
        
            // add the csv line to csvData
            savedData.csvData.Add(csvLine);
        }
    }
}

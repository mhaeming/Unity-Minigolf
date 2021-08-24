using System;
using System.Collections;
using System.Collections.Generic;
using Code.CSV;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExperimentManager : MonoBehaviour
{
    private int _prob;
    public bool isDecision;
    public float time;
    public int items;
    public int interactions;
    public float metres;
    public int failures;
    public bool csvCreated;
    public bool dataCollected;

    // create lists to store the experimental data in a csv file
    public List<List<string>> csvData = new List<List<string>>();
    public List<string> csvHeader;
    
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
        
        // randomly choose one of the two groups (experimental or control)
        if (FullSceneManager.CurrentScene == FullSceneManager.sceneEnum.Customize)
        {
            _prob = Random.Range(0, 2);

            if (_prob == 0)
            {
                isDecision = false;
                Debug.Log("You've landed in the control group.");
            }
            else
            {
                isDecision = true;
                Debug.Log("You've landed in the experimental group.");
            }
            
        }
        
    }

    private void OnDisable()
    {
        Debug.Log("data collected: " + dataCollected + "csvCreated: " + csvCreated);
        if (dataCollected && !csvCreated)
        {
            defineCsvHeader();
            Debug.Log("hi");
            Debug.Log("csvHeader: " + csvHeader.Count);
            Debug.Log("csvData: " + csvData.Count);
            List<string> csv = CSVTools.CreateCsv(csvData, csvHeader);
            CSVTools.SaveCsv(csv, Application.dataPath + "/Assets/CSVData/data");
            Debug.Log("Created CSV file.");
            csvCreated = true;
        }
    }

    private void Update()
    {
        //Debug.Log("start coroutine");
        //StartCoroutine(CreateCsvFile());
    }

    private IEnumerator CreateCsvFile()
    {
        Debug.Log("inside coroutine");
        if (dataCollected && !csvCreated)
        {
            Debug.Log("hi");
            Debug.Log("csvHeader: " + csvHeader.Count);
            Debug.Log("csvData: " + csvData.Count);
            List<string> csv = CSVTools.CreateCsv(csvData, csvHeader);
            CSVTools.SaveCsv(csv, Application.dataPath + "/Assets/CSVData/data");
            Debug.Log("Created CSV file.");
            csvCreated = true;
        }
        Debug.Log("at the end of coroutine");
        yield return new WaitForSeconds(3);
    }

    private void defineCsvHeader()
    {
        //csvHeader.Add("subjectNR");
        csvHeader.Add("isDecision");
        csvHeader.Add("time");
        csvHeader.Add("items");
        csvHeader.Add("interactions");
        csvHeader.Add("metres");
        csvHeader.Add("failures");
        //csvHeader.Add("levels");
    }
    

}

using System;
using System.Collections;
using System.Collections.Generic;
using Code.CSV;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExperimentManager : MonoBehaviour
{
    // define whether the game is started in experimental or control group mode
    public bool isDecision;
    private int _prob;
    
    // create lists to store the experimental data in a csv file
    public List<List<string>> csvData = new List<List<string>>();
    private List<string> _csvHeader = new List<string>();

    private bool _csvCreated = false; 

    private void OnEnable()
    {
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
        
        defineCsvHeader();
        
    }

  private void OnDisable()
    {

        if (FullSceneManager.CurrentScene == FullSceneManager.sceneEnum.End && _csvCreated == false)
        {
            List<string> csv = CSVTools.CreateCsv(csvData, _csvHeader);
            CSVTools.SaveCsv(csv, Application.dataPath + "/Assets/CSVData/data");
            Debug.Log("Created CSV file.");
            _csvCreated = true;
        }
        
    }

    private void defineCsvHeader()
    {
        //_csvHeader.Add("subjectNR");
        _csvHeader.Add("isDecision");
        _csvHeader.Add("time");
        _csvHeader.Add("items");
        _csvHeader.Add("interactions");
        _csvHeader.Add("metres");
        _csvHeader.Add("failures");
        //_csvHeader.Add("levels");
    }
    

}

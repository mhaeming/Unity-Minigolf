using System;
using System.Collections;
using System.Collections.Generic;
using Code.Player;
using Code.World;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataCollection : MonoBehaviour
{
    private ExperimentManager _experimentManager;
    
    public bool isDecision;
    public float time;
    public int items;
    public int interactions;
    public float metres;
    public int failures;
    public static bool dataCollected = false;


    void Start()
    {
        Debug.Log("Start Data Collection for scene " + FullSceneManager.CurrentScene);

        if (FullSceneManager.CurrentScene == FullSceneManager.sceneEnum.Customize)
        {
            // Which group are we in?
            _experimentManager = gameObject.GetComponent<ExperimentManager>();
            if (_experimentManager != null)
            {
                isDecision = _experimentManager.GetComponent<ExperimentManager>().isDecision;
            }
            else
            {
                Debug.Log("experiment manager is null!");
            }
        }
    }

    private void OnDisable()
    {
        Debug.Log("Disable Data Collection in scene " + FullSceneManager.CurrentScene);
        if (FullSceneManager.CurrentScene == FullSceneManager.sceneEnum.Tutorial)
        {
            //CharacterEditor:
            // get time spend in character editor from FullSceneManager
            time = FullSceneManager.timeCustomize;
            Debug.Log("time: " + time);

            if (isDecision)
            {
                // get both items and interactions from CharacterCustomization
                //items: different characters that were clicked on
                items = CharacterCustomization.items;
                //interactions: total number of clicks in editor
                interactions = CharacterCustomization.interactions;
                Debug.Log("items: " + items + ", interactions: " + interactions);
            }
            
        }

        if (FullSceneManager.CurrentScene == FullSceneManager.sceneEnum.Feedback)
        {
            //get information on the main scene from PlayerInfo:
            metres = PlayerInfo.DistanceTraveled;
            failures = PlayerInfo.HitObstacles + PlayerInfo.HitPits;
            Debug.Log("metres: " + metres + ", failures: " + failures);
            //levels t.b. implemented in WorldEvents class
        }

        if (FullSceneManager.CurrentScene == FullSceneManager.sceneEnum.End && dataCollected == false)
        {
            CreateCsvLine();
            dataCollected = true;
        }
    }


    void CreateCsvLine()
    {
        Debug.Log("creating a csvline");
        List<string> csvLine = new List<string>();

        // need to handle whether experimental or control group
        if (isDecision)
        {
            csvLine.Add("Yes");
        }
        else
        {
            csvLine.Add("No");
        }
        
        csvLine.Add(time.ToString());
        
        // only add for experimental group:
        if (isDecision)
        {
            csvLine.Add(items.ToString());
            csvLine.Add(interactions.ToString());
        }
        
        csvLine.Add(metres.ToString());
        csvLine.Add(failures.ToString());
        //csvLine.Add(levels.ToString());

        Debug.Log("CSV Line created");
        
        //need to put into csvData (t.b. handled by Experiment Manager)
        _experimentManager = gameObject.GetComponent<ExperimentManager>();
        if (_experimentManager != null)
        {
            _experimentManager.csvData.Add(csvLine);
            Debug.Log("added csvline to csvdata " + _experimentManager.csvData.ToString());
        }
        else
        {
            Debug.Log("experiment manager is null!");
        }
    }
}

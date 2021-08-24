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
    public static bool dataCollected;

    public List<List<string>> csvData;

    void Awake()
    {
        Debug.Log("AWAKE DATA COLLECTION");
        Debug.Log("Start Data Collection for scene " + FullSceneManager.CurrentScene);

        isDecision = ExperimentManager.Instance.isDecision;
        time = ExperimentManager.Instance.time;
        items = ExperimentManager.Instance.items;
        interactions = ExperimentManager.Instance.interactions;
        metres = ExperimentManager.Instance.metres;
        failures = ExperimentManager.Instance.failures;
        dataCollected = ExperimentManager.Instance.dataCollected;
        csvData = ExperimentManager.Instance.csvData;
    }

    private void OnDisable()
    {
        Debug.Log("Disable Data Collection in scene " + FullSceneManager.CurrentScene);
        if (FullSceneManager.CurrentScene == FullSceneManager.sceneEnum.Tutorial)
        {
            //CharacterEditor:
            // get time spend in character editor from FullSceneManager
            time = FullSceneManager.timeCustomize;
            ExperimentManager.Instance.time = time; // save to ExperimentManager
            Debug.Log("time: " + time);

            if (isDecision)
            {
                // get both items and interactions from CharacterCustomization
                //items: different characters that were clicked on
                items = CharacterCustomization.items;
                ExperimentManager.Instance.items = items;
                //interactions: total number of clicks in editor
                interactions = CharacterCustomization.interactions;
                ExperimentManager.Instance.interactions = interactions;
                Debug.Log("items: " + items + ", interactions: " + interactions);
            }
            
        }

        if (FullSceneManager.CurrentScene == FullSceneManager.sceneEnum.Feedback)
        {
            //get information on the main scene from PlayerInfo:
            metres = PlayerInfo.DistanceTraveled;
            ExperimentManager.Instance.metres = metres;
            failures = PlayerInfo.HitObstacles + PlayerInfo.HitPits;
            ExperimentManager.Instance.failures = failures;
            Debug.Log("metres: " + metres + ", failures: " + failures);
            //levels t.b. implemented in WorldEvents class
        }

        if (FullSceneManager.CurrentScene == FullSceneManager.sceneEnum.End && dataCollected == false)
        {
            Debug.Log("data in end scene: items: " + items + "interactions: " + interactions + "time: " + time);
            Debug.Log("data in end scene: failures: " + failures + "metres: " + metres);
            CreateCsvLine();
            ExperimentManager.Instance.dataCollected = true;
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
        ExperimentManager.Instance.csvData.Add(csvLine);
    }
}

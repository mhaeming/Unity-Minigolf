using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Code.Player;
using Code.World;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataCollection : MonoBehaviour
{

    public Statistics localData = new Statistics();

    void Start()
    {
        localData = ExperimentManager.Instance.savedData;
    }

    private void OnDisable()
    {
        if (FullSceneManager.CurrentScene == FullSceneManager.sceneEnum.Tutorial)
        {
            //CharacterEditor:
            // get time spend in character editor from FullSceneManager
            localData.time = FullSceneManager.timeCustomize;
            Debug.Log("time: " + localData.time);

            if (ExperimentManager.Instance.savedData.isDecision)
            {
                // get both items and interactions from CharacterCustomization
                //items: different characters that were clicked on
                localData.items = CharacterCustomization.items;

                //interactions: total number of clicks in editor
                localData.interactions = CharacterCustomization.interactions;
            }
            else
            {
                localData.items = 0;
            }
            Debug.Log("items: " + localData.items + ", interactions: " + localData.interactions);
        }

        if (FullSceneManager.CurrentScene == FullSceneManager.sceneEnum.Feedback)
        {
            //get information on the main scene from PlayerInfo:
            localData.metres = PlayerInfo.DistanceTraveled;
            localData.failures = PlayerInfo.HitObstacles + PlayerInfo.HitPits;
            Debug.Log("metres: " + localData.metres + ", failures: " + localData.failures);
            //levels t.b. implemented in WorldEvents class
        }
        SaveStatistics();
    }

    public void SaveStatistics()
    {
        // handle when which data is saved to make sure that no data is overwritten accidentally:
        // save time, items and interactions after Customize scene
        if (FullSceneManager.CurrentScene == FullSceneManager.sceneEnum.Tutorial)
        {
            ExperimentManager.Instance.savedData.time = localData.time;
            ExperimentManager.Instance.savedData.items = localData.items;
            ExperimentManager.Instance.savedData.interactions = localData.interactions;
        }
        // save metres & failures (& levels) after Play scene
        if (FullSceneManager.CurrentScene == FullSceneManager.sceneEnum.Feedback)
        {
            ExperimentManager.Instance.savedData.metres = localData.metres;
            ExperimentManager.Instance.savedData.failures = localData.failures;
            //ExperimentManager.Instance.savedData.levels = localData.levels;
        }
        // set bool dataCollected = true when all data is collected
        if (FullSceneManager.CurrentScene == FullSceneManager.sceneEnum.End)
        {
            ExperimentManager.Instance.savedData.dataCollected = true;
        }
    }
}
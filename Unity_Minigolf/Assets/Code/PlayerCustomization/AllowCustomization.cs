using System;
using System.Collections;
using System.Collections.Generic;
using Code;
using UnityEngine;
using UnityEngine.SceneManagement;
using ExperimentManager = Code.Experiment.ExperimentManager;

/// <summary>
/// Script that disables or enables the customization Buttons dependent on Trial Group
/// </summary>
public class AllowCustomization : MonoBehaviour
{
    public GameObject controlUI;
    public GameObject experimentUI;
    private FullSceneManager _sceneManager;
    
    private void OnEnable()
    {
        _sceneManager = GameObject.FindGameObjectWithTag("SceneChange").GetComponent<FullSceneManager>();
        if (ExperimentManager.savedData.isDecision)
        {
            experimentUI.SetActive(true);
        }
        else
        {
            controlUI.SetActive(true);
            _sceneManager.playerChoice = 0;
        }
    }

    /// <summary>
    /// OnDone disables the currently active UI
    /// it is called manually to disable the corresponding Game Object before the Scene is changed
    /// </summary>
    public void OnDone()
    {
        Debug.Log("Player Variety " +_sceneManager.playerChoice + " was chosen.");
        if (ExperimentManager.savedData.isDecision)
        {
            experimentUI.SetActive(false);
        }
        else
        {
            controlUI.SetActive(false);
        }
    }
}

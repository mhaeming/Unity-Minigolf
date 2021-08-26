using System;
using System.Collections;
using System.Collections.Generic;
using Code;
using UnityEngine;
using ExperimentManager = Code.Experiment.ExperimentManager;

/// <summary>
/// Script that disables or enables the customization Buttons dependent on Trial Group
/// </summary>
public class AllowCustomization : MonoBehaviour
{
    public GameObject controlUI;
    public GameObject experimentUI;

    private void OnEnable()
    {
        if (ExperimentManager.Instance.savedData.isDecision)
        {
            experimentUI.SetActive(true);
        }
        else
        {
            controlUI.SetActive(true);
        }
    }

    private void OnDisable()
    {
        controlUI.SetActive(false);
        experimentUI.SetActive(false);
    }
}

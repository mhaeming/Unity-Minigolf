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
    public GameObject customizationButtons;

    private void OnEnable()
    {
        if (ExperimentManager.Instance.savedData.isDecision)
        {
            customizationButtons.SetActive(true);
        }
    }

    private void OnDisable()
    {
        customizationButtons.SetActive(false);
    }
}

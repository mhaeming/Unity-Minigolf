using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackManager : MonoBehaviour
{
    private FullSceneManager _fullSceneManager;
    public void OnEnable()
    {
        _fullSceneManager = GameObject.FindGameObjectWithTag("SceneChange").GetComponent<FullSceneManager>();
    }

    public void Submit()
    {
        
    }

    public void NextScene()
    {
        _fullSceneManager.ChangeScene();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EndManager : MonoBehaviour
{
    
    private FullSceneManager _fullSceneManager;

    private void OnEnable()
    {
        _fullSceneManager = GameObject.FindGameObjectWithTag("SceneChange").GetComponent<FullSceneManager>();
    }

    public void NextScene()
    {
        _fullSceneManager.ChangeScene();
    }
    public void EndGame()
    {
        EditorApplication.isPlaying = false;
    }
}

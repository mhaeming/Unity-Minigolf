using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FullSceneManager : ScriptableSingleton<FullSceneManager>
{
    public enum sceneEnum
    {
        //Customize,
        Start,
        Tutorial,
        Play,
        End,
    }

    //private sceneEnum _currentScene = sceneEnum.Customize;
    //private sceneEnum _nextScene = sceneEnum.Start;
    private sceneEnum _currentScene = sceneEnum.Start;
    private sceneEnum _nextScene = sceneEnum.Tutorial;
    
    public bool skipTutorial = false;
    public KeyCode changeSceneKey;
    
    public new static FullSceneManager Instance { get => ScriptableSingleton<FullSceneManager>.instance; }

    public event Action OnSceneChange;

    public sceneEnum CurrentScene
    {
        get => _currentScene;
        set
        {
            switch (_currentScene)
            {
                //case sceneEnum.Customize:
                //    _nextScene = sceneEnum.Start;
                //    break;
                case sceneEnum.Start:
                    if (!skipTutorial) _nextScene = sceneEnum.Tutorial;
                    else
                    {
                        _nextScene = sceneEnum.Play;
                    }
                    break;
                case sceneEnum.Tutorial:
                    _nextScene = sceneEnum.Play;
                    break;
                case sceneEnum.Play:
                    _nextScene = sceneEnum.End;
                    break;
            }
        }
    }

    private void ChangeScene()
    {
        //Loads next Scene according to Build index, which needs to stay consistent with Enum int
        SceneManager.LoadScene((int) _nextScene);
    }

    public void OnEnable()
    {
        OnSceneChange += ChangeScene;
        DontDestroyOnLoad(this);
    }

    public void OnDisable()
    {
        OnSceneChange -= ChangeScene;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class FullSceneManager : MonoBehaviour
{
    public enum sceneEnum
    {
        Customize,
        Start,
        Tutorial,
        Play,
        End,
    }

    private sceneEnum _currentScene = sceneEnum.Customize;
    private sceneEnum _nextScene = sceneEnum.Start;
    
    public bool skipTutorial = false;
    public KeyCode changeSceneKey;
    public static FullSceneManager sceneManager;

    //private GameObject _player;

    public event Action OnSceneChange;

    public sceneEnum CurrentScene
    {
        get => _currentScene;
        set
        {
            _currentScene = value;
            switch (_currentScene)
            {
                case sceneEnum.Customize:
                    _nextScene = sceneEnum.Start;
                    break;
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
        Debug.Log("Load next scene" + _nextScene + (int)_nextScene);
        SceneManager.LoadScene((int) _nextScene);
        CurrentScene = _nextScene;
        Debug.Log(CurrentScene);
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0,2,0);

    }

    public void Awake()
    {
        // Destroy any other existing sceneManagers
        if (sceneManager != null && sceneManager != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            sceneManager = this;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(changeSceneKey))
        {
            OnSceneChange?.Invoke();
        }
    }


    public void OnEnable()
    {
        //keyDown += ChangeScene();
        OnSceneChange += ChangeScene;
    }

    public void OnDisable()
    {
        OnSceneChange -= ChangeScene;
    }
}
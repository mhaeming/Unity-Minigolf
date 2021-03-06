using System;
using System.Collections;
using System.Collections.Generic;
using Code.Experiment;
using Code.Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

/// <summary>
/// A class to handle changing the Scenes in the intended order
/// </summary>
public class FullSceneManager : MonoBehaviour
{
    // Enum representing all playable Scenes
    public enum sceneEnum
    {
        Start,
        Customize,
        Tutorial,
        Play,
        Feedback,
        End,
    }

    // variables to keep track of current status
    private static sceneEnum _currentScene = sceneEnum.Start;
    private static sceneEnum _nextScene = sceneEnum.Customize;
    public int playerChoice;

    public static float timeCustomize;

    public static bool skipTutorial = false;
    public KeyCode changeSceneKey;
    public static FullSceneManager sceneManager;
    public static bool keySceneChange = true;
    public static bool secondRound = false;

    //private GameObject _player;

    public static event Action OnSceneChange;

   // dynamically return the current and corresponding next Scene
    public static sceneEnum CurrentScene
    {
        get => _currentScene;
        set
        {
            _currentScene = value;
            switch (_currentScene)
            {
                case sceneEnum.Start:
                    _nextScene = sceneEnum.Customize;
                    break;
                case sceneEnum.Customize:
                    keySceneChange = false;
                    if (!skipTutorial) _nextScene = sceneEnum.Tutorial;
                    else
                    {
                        _nextScene = sceneEnum.Play;
                    }
                    break;
                case sceneEnum.Tutorial:
                    //allow skipping tutorial before its officially finished
                    keySceneChange = true;
                    _nextScene = sceneEnum.Play;
                    break;
                case sceneEnum.Play:
                    //Play Mode should only end if Time is run out
                    keySceneChange = false; 
                    // only go to Feedback scene in first round:
                    if (!secondRound)
                    {
                        _nextScene = sceneEnum.Feedback;
                        secondRound = true;
                    }
                    else
                    {
                        _nextScene = sceneEnum.End;
                    }
                    break;
                case sceneEnum.Feedback:
                    keySceneChange = true;
                    PlayerBehavior.PlayerFreeze();
                    _nextScene = sceneEnum.End;
                    break;
                case sceneEnum.End:
                    // if the user chooses to, UI interaction should allow them to play another round
                    keySceneChange = false;
                    PlayerBehavior.PlayerFreeze();
                    _nextScene = sceneEnum.Play;
                    break;
            }
        }
    }

    public void ChangeScene()
    {
        // log the time spend in character editor:
        if (CurrentScene == sceneEnum.Customize)
        {
            timeCustomize = Time.timeSinceLevelLoad;
        }

        // Loads next Scene according to Build index, which needs to stay consistent with Enum int
        Debug.Log("Load next scene " + _nextScene + (int)_nextScene);
        SceneManager.LoadScene((int) _nextScene);
        // As same Player is taken through all Scenes, reset the Player to ensure starting each Scene at the correct position
        if (CurrentScene != sceneEnum.Start)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0,2,0);
        }
        CurrentScene = _nextScene;
        Debug.Log("Current scene: " + CurrentScene);
        Debug.Log("Next scene: " + _nextScene);

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
            // SceneManager gameObject is mandatory for all Scenes
            DontDestroyOnLoad(gameObject);
            sceneManager = this;
        }
    }

    // Subscribe and Unsubscribe from the Scene Change Event
    public void OnEnable()
    {
        OnSceneChange += ChangeScene;
    }


    public void Update()
    {
        // if the chosen SceneChangeKey is pressed within any Scene, the Scene Change is initiated
        if (Input.GetKeyDown(changeSceneKey) && keySceneChange)
        {
            OnSceneChange?.Invoke();
        }
    }

    public void OnDisable()
    {
        OnSceneChange -= ChangeScene;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Code.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomizationHandler : MonoBehaviour
{
    private GameObject _player;
    private FullSceneManager _fullSceneManager;
    private AllowCustomization _allowCustomization;
    private void OnEnable()
    {
        _player = GameObject.FindWithTag("Player");
        _fullSceneManager = GameObject.FindWithTag("SceneChange").GetComponent<FullSceneManager>();
        _allowCustomization = gameObject.GetComponent<AllowCustomization>();
        if (_player != null)
        {
            _player.GetComponent<PlayerMovement>().enabled = false;
            _player.GetComponent<Rigidbody>().useGravity = false;
            Debug.Log("DONT RUN OR STEP ON MY CUSTOMIZATION LAWN!");
        }
        else
        {
            Debug.Log("There is no Player in this Scene");
        }
    }

    public void NextScene()
    {
        //disable the UIs manually
        _allowCustomization.OnDone();
        _fullSceneManager.ChangeScene();
    }
}

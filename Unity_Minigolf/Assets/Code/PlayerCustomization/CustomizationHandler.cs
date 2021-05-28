using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomizationHandler : MonoBehaviour
{
    private GameObject _player;
    private void OnEnable()
    {
        _player = GameObject.FindWithTag("Player");
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

    private void OnDisable()
    {
        _player.GetComponent<PlayerMovement>().enabled = true;
    }
}

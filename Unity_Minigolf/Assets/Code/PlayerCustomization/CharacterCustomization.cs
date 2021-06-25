using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
    private int _colorIndex = 0;
    private int _shapeIndex = 0;
    private int _varietyIndex = 0;
    [SerializeField] public Color[] charColor;
    [SerializeField] public GameObject[] players;
    private GameObject _player;
    private GameObject _shell;
    private GameObject _finalChoice;
    private Renderer _playerCol;
    private Transform _playerTransform;
    private Vector3 _startScale;
    public bool confirmed;

    private void OnEnable()
    {
        _player = GameObject.FindWithTag("Player");
        if (_player == null){
            Debug.Log("I have no master");
        }
        _shell = GameObject.FindWithTag("Shell");
        if (_shell == null)
        {
            Debug.Log("I am a naked slug! :(");
        }
        _playerCol = _player.GetComponent<Renderer>();
        _playerTransform = _player.GetComponent<Transform>();

        charColor[charColor.Length-1] = _playerCol.material.color;
        _startScale = _playerTransform.localScale;
        confirmed = false;
        
        DontDestroyOnLoad(_shell);
    }

    public void ChangeColor(){
        _player = GameObject.FindWithTag("Player");
        _playerCol = _player.GetComponent<Renderer>();
        if (_colorIndex < charColor.Length-1)
        {
            _colorIndex++;
        }
        else if (_colorIndex == charColor.Length - 1)
        {
            _colorIndex = 0;
        }
        else
        {
            Debug.Log("Tried to index color not existent in Array");
        }
        
        _playerCol.material.color = charColor[_colorIndex];
    }

    public void ChangeShape()
    { 
        _player = GameObject.FindWithTag("Player");
        _playerTransform = _player.GetComponent<Transform>();
        switch (_shapeIndex)
        {
            case 0:
                Debug.Log("Eggcelent choice!");
                _playerTransform.localScale = new Vector3(0.4f, 0.5f, 0.5f);
                _shapeIndex++;
                break;
            case 1:
                _playerTransform.localScale = new Vector3(0.1f,0.5f, 0.5f);
                _shapeIndex++;
                break;
            case 2:
                _playerTransform.localScale = new Vector3(0.6f, 0.3f, 0.3f);
                _shapeIndex++;
                break;
            case 3:
                _playerTransform.localScale = _startScale;
                _shapeIndex = 0;
                break;
        }
    }

    public void EnableChild()
    {
        players[_varietyIndex].SetActive(false);
        switch (_varietyIndex)
        {
            case 0:
                _varietyIndex++;
                break;
            case 1:
                _varietyIndex = 0;
                break;
        }
        players[_varietyIndex].SetActive(true);
    }

    public void Confirm()
    {
        confirmed = true;
       Debug.Log("Don't you forget about me");
       foreach (var potentialPlayer in players)
       {
           if (!potentialPlayer.activeSelf)
           {
               Destroy(potentialPlayer);
           }
       }
    }
}

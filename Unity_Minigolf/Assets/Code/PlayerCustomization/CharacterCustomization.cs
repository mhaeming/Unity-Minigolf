using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
    // set different Variables representing aspect that can then be customized
    private int _colorIndex = 0;
    private Renderer _playerCol;
    private int _shapeIndex = 0;
    private Transform _playerTransform;
    private Vector3 _startScale;
    
    // Array of potential Player Colours
    //[SerializeField] public Color[] charColor;
    // Array of predefined Player Variants, stored as Children of Shell
    [SerializeField] public GameObject[] players;
    private int _numberPlayers;
    private GameObject _shell;
    private int _varietyIndex = 0;
  
    private GameObject _player;
    
    public bool confirmed;
    
    // for data collection:
    public static int items = 1;
    public static int interactions;

    private void OnEnable()
    {
        // Find currently active Player and Shell
        _shell = GameObject.FindWithTag("Shell");
        if (_shell == null)
        {
            Debug.Log("I am a naked slug! :(");
        }
        _player = GameObject.FindWithTag("Player");
        if (_player == null){
            Debug.Log("I have no master");
        }

        _numberPlayers = players.Length - 1;
        // add initial Color and Shape as options
        //_playerCol = _player.GetComponent<Renderer>();
        //charColor[charColor.Length-1] = _playerCol.material.color;
        //_playerTransform = _player.GetComponent<Transform>();
        //_startScale = _playerTransform.localScale;
        
        confirmed = false;
        
        DontDestroyOnLoad(_shell);
    }

    /// <summary>
    /// hitting the ColorChanging Button selects the next element of the Array of potential Colors
    /// </summary>
    /*public void ChangeColor(){
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
    }*/

    /// <summary>
    /// Change the PLayer's shape by setting its scale to be according to predefined values
    /// </summary>
    /*public void ChangeShape()
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
    }*/

    /// <summary>
    /// Enable Child switches between different Child Objects of the Player Variety Game Object
    /// This allows choosing a pre-designed PLayer variant out of an Array
    /// </summary>
    public void EnableChild()
    {
        players[_varietyIndex].SetActive(false);
        
        // for data collection:
        interactions += 1;
        if (items < players.Length)
        {
            items += 1;
        }
        
        if (_varietyIndex == players.Length-1)
        {
            _varietyIndex = 0;
        }
        else
        {
            _varietyIndex++;
        }
        players[_varietyIndex].SetActive(true);
    }

    public void Confirm()
    {
        // Confirming the player deletes any other potential Player Scaffolding
        // avoids unnecessary GameObject being carried through Scenes
        if (!confirmed)
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
        else
        {
            Debug.Log("The Player has already been decided on!");
        }

    }

    private void OnDisable()
    {
        // if the Confirmation Button was not manually selected, do so on Scene Change nevertheless
        Confirm();
    }
}

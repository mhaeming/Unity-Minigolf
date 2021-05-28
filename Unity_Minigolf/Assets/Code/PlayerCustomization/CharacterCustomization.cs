using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
    private int _colorIndex = 0;
    [SerializeField] public Color[] charColor;
    private GameObject _player;
    private Renderer _playerCol;

    private void OnEnable()
    {
        _player = GameObject.FindWithTag("Player");
        if (_player == null){
        Debug.Log("I have no master");
        }
        _playerCol = _player.GetComponent<Renderer>();

        charColor[charColor.Length-1] = _playerCol.material.color;
    }

    public void ChangeColor(){
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
}

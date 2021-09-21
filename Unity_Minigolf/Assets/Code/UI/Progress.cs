using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Code.Player;
using UnityEngine;
using UnityEngine.UI;

public class Progress : MonoBehaviour
{
    private float _metresSoFar;
    public Text metresText;
    
    void Update()
    {
        _metresSoFar = PlayerInfo.DistanceTraveled;
        DisplayMetres(_metresSoFar);
    }
    
    void DisplayMetres(float metresToDisplay)
    {
        float metres = Mathf.Round(metresToDisplay);
        metresText.text = string.Format("{0} m", metres);
    }
}

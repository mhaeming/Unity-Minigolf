using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class Timer : MonoBehaviour
    {
        public float timeLimit = 60;
        public float remainingTime;
        public Text timerText;
        private FullSceneManager _sceneManager;
        
        private void Start()
        {
            Debug.Log(String.Format("Set time limit to {0}", timeLimit));
            _sceneManager = GameObject.FindGameObjectWithTag("SceneChange").GetComponent<FullSceneManager>();
        }

        void Update()
        {
            remainingTime = timeLimit - Time.timeSinceLevelLoad;
            DisplayTime(remainingTime);
            // start displaying the remaining time in red when there are only 10 secs left
            if (remainingTime <= 11)
            {
                DisplayTimeInRed(remainingTime);
            }
            // don't display any negative values
            if (remainingTime <= 1)
            {
                DisplayTimeInRed(0);
                _sceneManager.ChangeScene();
            }
        }
    
        void DisplayTime(float timeToDisplay)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);  
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        void DisplayTimeInRed(float timeToDisplay)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);  
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerText.color = Color.red;
        }
    
    }
}

using System;
using Code.Player;
using UnityEngine;

namespace Code.Tutorial
{
    public class TutorialManager : MonoBehaviour
    {

        public GameObject[] tutorialTexts;
        private int _tutorialIndex;
        public float waitTime = 2f;

        private KeyCode _moveLeft = KeyCode.A;
        private KeyCode _moveRight = KeyCode.D;
        private KeyCode _jump = KeyCode.Space;

        private GameObject _player;

        private void OnEnable()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _player.GetComponent<PlayerMovement>().enabled = true;
            _player.GetComponent<Rigidbody>().useGravity = true;
        }

        void Update()
        {
            if (_tutorialIndex == 0)
            {
                tutorialTexts[0].SetActive(true);

                if (Input.GetKeyDown(_moveLeft))
                {
                    _tutorialIndex++;
                    tutorialTexts[0].SetActive(false);
                    tutorialTexts[1].SetActive(true);
                }
            }
            //right
            else if (_tutorialIndex == 1)
            {
                if (Input.GetKeyDown(_moveRight))
                {
                    _tutorialIndex++;
                    tutorialTexts[1].SetActive(false);
                    tutorialTexts[2].SetActive(true);
                }
            }
            //jump
            else if (_tutorialIndex == 2)
            {
                if (Input.GetKeyDown(_jump))
                {
                    _tutorialIndex++;
                    tutorialTexts[2].SetActive(false);
                }
            }
            //end of tutorial
            else if (_tutorialIndex == 3)
            {
                if (waitTime <= 0)
                {
                    tutorialTexts[3].SetActive(true); 
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }
}

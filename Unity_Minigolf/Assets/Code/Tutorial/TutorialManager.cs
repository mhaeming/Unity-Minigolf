using Code.Player;
using UnityEngine;

namespace Code.Tutorial
{
    public class TutorialManager : MonoBehaviour
    {

        public GameObject[] tutorialTexts;
        private int tutorialIndex = 0;
        public float waitTime = 2f;

        private PlayerMovement _playerMovement;
        private KeyCode _moveLeft = KeyCode.A;
        private KeyCode _moveRight = KeyCode.D;
        private KeyCode _speedUp = KeyCode.W;
        private KeyCode _slowDown = KeyCode.S;
        private KeyCode _jump = KeyCode.Space;

        void Update()
        {
            //slow
            if (tutorialIndex == 0)
            {
                tutorialTexts[0].SetActive(true);
                if (Input.GetKeyDown(_slowDown))
                {
                    tutorialIndex++;
                    tutorialTexts[0].SetActive(false);
                    tutorialTexts[1].SetActive(true);
                }
            }
            //fast
            else if (tutorialIndex == 1)
            {
                if (Input.GetKeyDown(_speedUp))
                {
                    tutorialIndex++;
                    tutorialTexts[1].SetActive(false);
                    tutorialTexts[2].SetActive(true);
                }
            }
            //left
            else if (tutorialIndex == 2)
            {
                if (Input.GetKeyDown(_moveLeft))
                {
                    tutorialIndex++;
                    tutorialTexts[2].SetActive(false);
                    tutorialTexts[3].SetActive(true);
                }
            }
            //right
            else if (tutorialIndex == 3)
            {
                if (Input.GetKeyDown(_moveRight))
                {
                    tutorialIndex++;
                    tutorialTexts[3].SetActive(false);
                    tutorialTexts[4].SetActive(true);
                }
            }
            //jump
            else if (tutorialIndex == 4)
            {
                if (Input.GetKeyDown(_jump))
                {
                    tutorialIndex++;
                    tutorialTexts[4].SetActive(false);
                }
            }
            //end of tutorial
            else if (tutorialIndex == 5)
            {
                if (waitTime <= 0)
                {
                    tutorialTexts[5].SetActive(true); 
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }
}

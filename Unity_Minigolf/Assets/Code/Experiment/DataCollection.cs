using System;
using Code.Player;
using UnityEngine;

namespace Code.Experiment
{
    public class DataCollection : MonoBehaviour
    {

        public Statistics localData = new Statistics();

        void Start()
        {
            //localData = ExperimentManager.Instance.savedData;
            localData = ExperimentManager.savedData;
        }

        private void OnDisable()
        {
            if (FullSceneManager.CurrentScene == FullSceneManager.sceneEnum.Tutorial)
            {
                //CharacterEditor:
                // get time spend in character editor from FullSceneManager
                localData.time = FullSceneManager.timeCustomize;
                Debug.Log("time: " + localData.time);

                //if (ExperimentManager.Instance.savedData.isDecision)
                if (ExperimentManager.savedData.isDecision)
                {
                    // get both items and interactions from CharacterCustomization
                    //items: different characters that were clicked on
                    localData.items = CharacterCustomization.items;

                    //interactions: total number of clicks in editor
                    localData.interactions = CharacterCustomization.interactions;
                }
                else
                {
                    localData.items = 0;
                }
                Debug.Log("items: " + localData.items + ", interactions: " + localData.interactions);
            }

            if (FullSceneManager.CurrentScene == FullSceneManager.sceneEnum.Feedback)
            {
                //get information on the main scene from PlayerInfo:
                localData.metres = PlayerInfo.DistanceTraveled;
                localData.failures = PlayerInfo.HitObstacles + PlayerInfo.HitPits;
                Debug.Log("metres: " + localData.metres + ", failures: " + localData.failures);
                localData.levels = PlayerBehavior.maxLevel;
                Debug.Log("maximum level reached: " + localData.levels);
            }

            if (FullSceneManager.CurrentScene == FullSceneManager.sceneEnum.End)
            {
                // get information on answers to the questionnaire from FeedbackManager
                localData.answer1 = FeedbackManager.answerDic[0];
                localData.answer2 = FeedbackManager.answerDic[1];
                localData.answer3 = FeedbackManager.answerDic[2];
                Debug.Log("answers saved: " + localData.answer1 + localData.answer2 + localData.answer3);
            }
            
            SaveStatistics();
        }

        public void SaveStatistics()
        {
            // handle when which data is saved to make sure that no data is overwritten accidentally:
            // save time, items and interactions after Customize scene
            if (FullSceneManager.CurrentScene == FullSceneManager.sceneEnum.Tutorial)
            {
                ExperimentManager.savedData.time = localData.time;
                ExperimentManager.savedData.items = localData.items;
                ExperimentManager.savedData.interactions = localData.interactions;
            }
            // save metres, failures & levels after Play scene
            if (FullSceneManager.CurrentScene == FullSceneManager.sceneEnum.Feedback)
            {
                ExperimentManager.savedData.metres = localData.metres;
                ExperimentManager.savedData.failures = localData.failures;
                ExperimentManager.savedData.levels = localData.levels;
            }
            // save answers to questions
            if (FullSceneManager.CurrentScene == FullSceneManager.sceneEnum.End)
            {
                ExperimentManager.savedData.answer1 = localData.answer1;
                ExperimentManager.savedData.answer2 = localData.answer2;
                ExperimentManager.savedData.answer3 = localData.answer3;
                // set bool dataCollected = true when all data is collected
                ExperimentManager.savedData.dataCollected = true;
            }
        }
    }
}
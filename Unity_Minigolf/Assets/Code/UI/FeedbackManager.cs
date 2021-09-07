using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FeedbackManager : MonoBehaviour
{
    private FullSceneManager _fullSceneManager;
    public GameObject canvasConsent;
    public GameObject canvasQuestions;
    public GameObject[] questions;
    public GameObject canvasEnd;
    private int _questionId = 0;
    private int _countMissing;
    public static Dictionary<int, int> answerDic = new Dictionary<int, int>();
    public void OnEnable()
    {
        _fullSceneManager = GameObject.FindGameObjectWithTag("SceneChange").GetComponent<FullSceneManager>();
        //initialize Dictionary containing user answers with as many key as there are questions
        for (int i = 0; i < questions.Length; i++)
        {
            answerDic.Add(i,-1);
        }
    }

    public void StartQuestionnaire(){
        canvasConsent.SetActive(false);
        canvasQuestions.SetActive(true);
        questions[_questionId].SetActive(true);
    }

    // Each possible answer will get an int assigned, which will be passed upon selection
    // assign the currently pressed answer-int to the current active question
    public void Answer(int answer)
    {
       answerDic[_questionId] = answer;
    }
    //Move to the next question
    public void Next()
    {
        Debug.Log("Question "+(_questionId+1)+"= Answer "+answerDic[_questionId]);
        if (_questionId < questions.Length - 1)
        {
            questions[_questionId].SetActive(false);
            _questionId++;
            questions[_questionId].SetActive(true);
        }
        else
        {
            EndQuestionnaire();
        }
        
    }
    //Backtrack a single question
    public void Back()
    {
        questions[_questionId].SetActive(false);
        if (_questionId > 0)
        {
            _questionId--;
        }
        questions[_questionId].SetActive(true);
    }

    /// <summary>
    /// Once the end of the questionnaire is reached, check whether all questions are answered
    /// Only finish the questionnaire if no question is left unanswered
    /// Else not to missing question
    /// </summary>
    private void EndQuestionnaire()
    {
        _countMissing = 0;
        for (int i = 0; i < questions.Length; i++)
        {
            if (answerDic[i] == -1)
            {
                Debug.Log("Please go back and answer question"+(i+1));
                _countMissing++;
            }
        }
        if (_countMissing == 0)
        {
            questions[_questionId].SetActive(false);
            canvasQuestions.SetActive(false);
            canvasEnd.SetActive(true);
        }
    }
    

    public void NextScene()
    {
        _fullSceneManager.ChangeScene();
    }
}

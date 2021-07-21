using System.Collections;
using System.Collections.Generic;
using Code.Player;
using Code.World;
using UnityEngine;

public class DataCollection : MonoBehaviour
{
    private FullSceneManager _fullSceneManager;
    private PlayerInfo _playerInfo;
    private WorldEvents _worldEvents;

    public float time;
    public float metres;
    public int failures;
    public bool timeRunning=true;
    
    // Start is called before the first frame update
    void Start()
    {
        //CharacterEditor:
        // get time spend in character editor from FullSceneManager
        _fullSceneManager = gameObject.GetComponent<FullSceneManager>();
        time = _fullSceneManager.timeCustomize;
        //items: different characters that were clicked on
        //interactions: total number of clicks in editor
        
        //get information on the main scene from PlayerInfo:
        _playerInfo = gameObject.GetComponent<PlayerInfo>();
        metres = _playerInfo.DistanceTraveled;
        failures = _playerInfo.HitObstacles + _playerInfo.HitPits;
        //levels t.b. implemented in WorldEvents class

        _worldEvents = gameObject.GetComponent<WorldEvents>();
        timeRunning = _worldEvents.timer;
        StartCoroutine(CsvCreation());
    }
    
    private IEnumerator CsvCreation()
    {
        while (timeRunning)
        {
            CreateCsvLine();
            yield return new WaitForSeconds(3f);   
        }
    }
    
    void CreateCsvLine()
    {
        List<string> csvLine = new List<string>();

        /* need to handle whether experimental or control group
        if (isDecision)
        {
            csvLine.Add("Yes");
        }
        else
        {
            csvLine.Add("No");
        }
        */
        csvLine.Add(time.ToString());
        
        // only add for experimental group:
        //csvLine.Add(items.ToString());
        //csvLine.Add(interactions.ToString());
        
        csvLine.Add(metres.ToString());
        csvLine.Add(failures.ToString());
        //csvLine.Add(levels.ToString());

        Debug.Log("CSV Line created" + csvLine);
        //need to put into csvData (t.b. handled by Experiment Manager)
        //_worldManager.GetComponent<WorldManager>().csvData.Add(csvLine);
    }
}

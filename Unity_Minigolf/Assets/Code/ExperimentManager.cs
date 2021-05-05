using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExperimentManager : MonoBehaviour
{
    public GameObject player;
    private GameObject _worldManager;
    [HideInInspector]
    public bool isRisk = false;
    [HideInInspector]
    public bool timeout = false;
    [HideInInspector]
    public bool resetplayer = false;
    public int distance = 2;
    private int _behindPlayer;
    
    private GenerateFloorTile _generateFloorTile;
    private ExperimentManager _experimentManager;
    public Dictionary<int, GameObject> activeFloors;
    public Dictionary<int, GameObject> activeObstacles;
    public Dictionary<int, GameObject> activePits;
    
    // Start is called before the first frame update
    void Start()
    {
        _experimentManager = gameObject.GetComponent<ExperimentManager>();
        _generateFloorTile = gameObject.GetComponent<GenerateFloorTile>();
        activeFloors = _generateFloorTile.activeFloors;
        activeObstacles = _generateFloorTile.activeObstacles;
        activePits = _generateFloorTile.activePits;
        
        _worldManager = GameObject.Find("WorldManager");
        isRisk =_worldManager.GetComponent<WorldManager>().riskPrime;
        timeout = _worldManager.GetComponent<WorldManager>().timeout;
        ResetStage();
        StartCoroutine(csvCreation());
    }

    void ResetStage()
    {
        player.transform.position = new Vector3(0,1,0);
        gameObject.GetComponent<NewGameManager>().Start();
        player.GetComponent<PlayerMovement>().Start();
    }
    
    public float GetPlayerVelocitiy()
    {
        return player.GetComponent<Rigidbody>().velocity.z;
    }

    public GameObject GetClosestObstacle()
    {
        int closestObstacle = Int32.MaxValue;
        int closestPit = Int32.MaxValue;
        
        // Get the closest obstacle by finding the closest index to the players z position
        if (activeObstacles.Count > 0)
        {
            closestObstacle = activeObstacles.Keys.OrderBy(item => Mathf.Abs(player.transform.position.z - item)).First();
        }
        // Get the closest pit by finding the closest index to the players z position
        if (activePits.Count > 0)
        {
            closestPit = activePits.Keys.OrderBy(item => Mathf.Abs(player.transform.position.z - item)).First();
        }
        
        // Compare the values and return the corresponding GameObject from the dicts
        if (closestObstacle < closestPit)
        {
            return activeObstacles[closestObstacle];
        }
        if (closestPit < closestObstacle)
        {
            return activePits[closestPit];
        }
        return null;
    }
    
    public float DistanceToClosestObstacle()
    {
        GameObject closest = GetClosestObstacle();
        if (closest)
        {
            return Mathf.Abs(closest.transform.position.z - player.transform.position.z);
        }
        return 0;
    }
    
    // delete all existing objects to restart completely fresh
    void DeleteStage()
    {
        // Destroy all floor elements, Obstacles, pits
        foreach (KeyValuePair<int, GameObject> floorEntry in activeFloors)
        {
            Destroy(floorEntry.Value);
        }
        activeFloors.Clear();
        Debug.Log("Floors destroyed!");

        foreach (KeyValuePair<int, GameObject> obstacleEntry in activeObstacles)
        {
            Destroy(obstacleEntry.Value);
        }
        activeObstacles.Clear();
        Debug.Log("Obstacles destroyed!");
        
        foreach (KeyValuePair<int, GameObject> pitEntry in activePits)
        {
            Destroy(pitEntry.Value);
        }
        activePits.Clear();
        Debug.Log("Pits destroyed!");
        
    }
    private IEnumerator csvCreation()
    {
        while (!timeout)
        {
            createCSVLine();
            yield return new WaitForSeconds(3f);   
        }
    }
    
    void createCSVLine()
    {
        List<string> csvLine = new List<string>();

        if (isRisk)
        {
            csvLine.Add("Yes");
        }
        else
        {
            csvLine.Add("No");
        }
        csvLine.Add(_worldManager.GetComponent<WorldManager>().numberObstacles.ToString());
        csvLine.Add(_worldManager.GetComponent<WorldManager>().numberPits.ToString());
        csvLine.Add(player.GetComponent<Transform>().position.z.ToString("0.00", CultureInfo.InvariantCulture));
        csvLine.Add(GetPlayerVelocitiy().ToString("0.00", CultureInfo.InvariantCulture));
        csvLine.Add(DistanceToClosestObstacle().ToString("0.00", CultureInfo.InvariantCulture));
        csvLine.Add(_worldManager.GetComponent<WorldManager>().numberResets.ToString());
        csvLine.Add(_worldManager.GetComponent<WorldManager>().resetObstacle.ToString());
        csvLine.Add(_worldManager.GetComponent<WorldManager>().resetFall.ToString());

        Debug.Log("CSV Line created" + csvLine);
        _worldManager.GetComponent<WorldManager>().csvData.Add(csvLine);
    }

    // Update is called once per frame
    void Update()
    {
        _behindPlayer = (int)player.transform.position.z - distance;

        // Remove blocks behind player
        if (activeFloors.ContainsKey(_behindPlayer))
        {
            Destroy(activeFloors[_behindPlayer]);
            activeFloors.Remove(_behindPlayer);
        }
        if (activeObstacles.ContainsKey(_behindPlayer))
        {
            Destroy(activeObstacles[_behindPlayer]);
            activeObstacles.Remove(_behindPlayer);
        }
        if (activePits.ContainsKey(_behindPlayer))
        {
            Destroy(activePits[_behindPlayer]);
            activePits.Remove(_behindPlayer);
        }

        // reset Player to starting stage
        if (resetplayer)
        {
            Debug.Log("reset please");
            _worldManager.GetComponent<WorldManager>().numberResets += 1;
            // delete all current objects
            DeleteStage();
            // restart at start stage
            ResetStage();
            resetplayer = false;
        }
        
         gameObject.GetComponent<NewGameManager>().Update();
    }

    
}


using System.Collections.Generic;
using Code.World;
using UnityEngine;

namespace Code
{
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
            isRisk =_worldManager.GetComponent<WorldManager>().riskPrime;
            timeout = _worldManager.GetComponent<WorldManager>().timeout;
            // ResetStage();
            // StartCoroutine(csvCreation());
        }

        // void ResetStage()
        // {
        //     player.transform.position = new Vector3(0,1,0);
        //     gameObject.GetComponent<NewGameManager>().Start();
        //     player.GetComponent<PlayerMovement>().Start();
        // }
        //
        // public float GetPlayerVelocitiy()
        // {
        //     return player.GetComponent<Rigidbody>().velocity.z;
        // }

        // public GameObject GetClosestObstacle()
        // {
        //     int closestObstacle = Int32.MaxValue;
        //     int closestPit = Int32.MaxValue;
        //     
        //     // Get the closest obstacle by finding the closest index to the players z position
        //     if (activeObstacles.Count > 0)
        //     {
        //         closestObstacle = activeObstacles.Keys.OrderBy(item => Mathf.Abs(player.transform.position.z - item)).First();
        //     }
        //     // Get the closest pit by finding the closest index to the players z position
        //     if (activePits.Count > 0)
        //     {
        //         closestPit = activePits.Keys.OrderBy(item => Mathf.Abs(player.transform.position.z - item)).First();
        //     }
        //     
        //     // Compare the values and return the corresponding GameObject from the dicts
        //     if (closestObstacle < closestPit)
        //     {
        //         return activeObstacles[closestObstacle];
        //     }
        //     if (closestPit < closestObstacle)
        //     {
        //         return activePits[closestPit];
        //     }
        //     return null;
        // }
    
        // public float DistanceToClosestObstacle()
        // {
        //     GameObject closest = GetClosestObstacle();
        //     if (closest)
        //     {
        //         return Mathf.Abs(closest.transform.position.z - player.transform.position.z);
        //     }
        //     return 0;
        // }
    
        // private IEnumerator csvCreation()
        // {
        //     while (!timeout)
        //     {
        //         createCSVLine();
        //         yield return new WaitForSeconds(3f);   
        //     }
        // }
    
        // void createCSVLine()
        // {
        //     List<string> csvLine = new List<string>();
        //
        //     if (isRisk)
        //     {
        //         csvLine.Add("Yes");
        //     }
        //     else
        //     {
        //         csvLine.Add("No");
        //     }
        //     csvLine.Add(_worldManager.GetComponent<WorldManager>().numberObstacles.ToString());
        //     csvLine.Add(_worldManager.GetComponent<WorldManager>().numberPits.ToString());
        //     csvLine.Add(player.GetComponent<Transform>().position.z.ToString("0.00", CultureInfo.InvariantCulture));
        //     csvLine.Add(GetPlayerVelocitiy().ToString("0.00", CultureInfo.InvariantCulture));
        //     // csvLine.Add(DistanceToClosestObstacle().ToString("0.00", CultureInfo.InvariantCulture));
        //     csvLine.Add(_worldManager.GetComponent<WorldManager>().numberResets.ToString());
        //     csvLine.Add(_worldManager.GetComponent<WorldManager>().resetObstacle.ToString());
        //     csvLine.Add(_worldManager.GetComponent<WorldManager>().resetFall.ToString());
        //
        //     Debug.Log("CSV Line created" + csvLine);
        //     _worldManager.GetComponent<WorldManager>().csvData.Add(csvLine);
        // }

        // Update is called once per frame
        void Update()
        {
        
        }

    
    }
}


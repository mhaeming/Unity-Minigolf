using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.World
{
    public class NewGameManager : MonoBehaviour
    {
    
        // Player
        public GameObject playerObj;
        private float _zPosPlayer = 0;
        // how far ahead should the scene be generated?
        [SerializeField] private int _lookahead = 10;
        private int _placePos = 0;
    
        // create a queue to store all positions at which floors/obstacles are at
        // private Queue<int> _obstaclesAt = new Queue<int>();
        // public Queue<int> _floorAt = new Queue<int>();
        private GenerateFloorTile _generateFloorTile;
        private ExperimentManager _experimentManager;
        public Dictionary<int, GameObject> activeFloors;
        public Dictionary<int, GameObject> activePits;

        public void Start()
        {
            _experimentManager = gameObject.GetComponent<ExperimentManager>();
            _generateFloorTile = gameObject.GetComponent<GenerateFloorTile>();
            activeFloors = _generateFloorTile.activeFloors;
            activePits = _generateFloorTile.activePits;
        
            for (int i = -3; i < _lookahead; i++)
            {
                _generateFloorTile.PlaceFloorTile(i);
            }
            StartCoroutine(ObstaclePlacement());
        }

        public void Update()
        {
        
            // get positions
            if (playerObj)
            {
                _zPosPlayer = playerObj.transform.position.z;
                _placePos = (int) _zPosPlayer + _lookahead;
            }
        
            // floor tile is placed every frame update
            if(! activeFloors.ContainsKey(_placePos) & ! activePits.ContainsKey(_placePos))
            {
                _generateFloorTile.PlaceFloorTile(_placePos);
            }
        
        }
    
        /// <summary>
        /// coroutine for placing the obstacles
        /// </summary>
        /// <returns></returns>
        private IEnumerator ObstaclePlacement()
        {
            while (! _experimentManager.timeout)
            {
                _generateFloorTile.PlaceObstacles(_placePos);
                yield return new WaitForSeconds(1f);
            }
        
        }
    }
}

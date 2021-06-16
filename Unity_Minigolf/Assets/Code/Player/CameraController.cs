using UnityEngine;

// Link the player object
// Link camera object
// Expose camera settings
    // Distance
    // Height
    // Offset (Left/right) 0 as default

namespace Code.Player
{
    public class CameraController : MonoBehaviour
    {
        public GameObject player;
        public float distance = 3;
        public float height = 0.6f;
        public float offset = 0;

        private Vector3 heading;

        private float _rayLength;
        private Material _currentMat;
    
        // Start is called before the first frame update
        void Start()
        {
            // Calculate ray length to limit it to the player
            _rayLength = Mathf.Sqrt(distance * distance + height * height);
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = player.transform.position + new Vector3(offset, height, -distance);
        }

        void FixedUpdate()
        {
            heading = player.transform.position - transform.position;
            Ray camRay = new Ray(transform.position, heading);
            RaycastHit hit;
        
            // Cast the from camera towards player object
            if (Physics.Raycast(camRay, out hit, _rayLength))
            {
                if (hit.collider.tag == "Obstacle")
                {
                    // Get the object's material
                    _currentMat = hit.collider.GetComponent<MeshRenderer>().material;
                    Color oldCol = _currentMat.color;
                    // Set new color with transparency
                    Color newCol = new Color(oldCol.r, oldCol.g, oldCol.b, 0.5f);
                    hit.collider.GetComponent<MeshRenderer>().material.SetColor("_Color", newCol);
                }
            }
        }
    }
}

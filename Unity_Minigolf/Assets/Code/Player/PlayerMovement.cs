using System;
using System.Collections;
using Code.World;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Player
{
    [RequireComponent(typeof(PlayerInfo))]
    [RequireComponent(typeof(PlayerBehavior))]
    public class PlayerMovement : MonoBehaviour
    {
        // Define KeyControls
        public KeyCode moveLeft = KeyCode.A;
        public KeyCode moveRight = KeyCode.D;
        public KeyCode jump = KeyCode.Space;
    
        public float jumpForce;
        public float speed = 2;
        
        private Rigidbody _rigidbody;
        private PlayerInfo _info;
        private Vector3 _target;

        // Start is called before the first frame update
        public void Start()
        {
            _info = GetComponent<PlayerInfo>();
            _rigidbody = GetComponent<Rigidbody>();
            Physics.gravity = new Vector3(0,-75.0f,0);
        }

        // Update is called once per frame
        void Update()
        {
            // Move left
            if (Input.GetKeyDown(moveLeft) & _info.GetLane() != -1)
            {
                _target += Vector3.left;
                _rigidbody.MovePosition(transform.position + Vector3.left);
            }

            // Move Right
            if (Input.GetKeyDown(moveRight) & _info.GetLane() != 1)
            { 
                _target += Vector3.right;
                _rigidbody.MovePosition(transform.position + Vector3.right);
            }
            
            // Jump
            if (Input.GetKeyDown(jump))
            {
                // TODO: Smoother falling
                _rigidbody.AddForce(new Vector3(0,10) * jumpForce, ForceMode.Impulse);
            }

        }

        private void FixedUpdate()
        {
            // TODO: Smoother line switching
            _rigidbody.velocity = new Vector3(0, 0, speed);
        }
    }
}

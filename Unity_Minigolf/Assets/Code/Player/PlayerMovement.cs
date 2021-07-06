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
        public float speed = 10;

        private float _horizontalVel = 0;
        public float verticalVel;
        private float _upVel = 0;
        private Rigidbody _rigidbody;
        private bool _onGround;
        private float _startJump;
        private PlayerInfo _info;
        
        // Start is called before the first frame update
        public void Start()
        {
            _info = GetComponent<PlayerInfo>();
            verticalVel = speed;
            _rigidbody = GetComponent<Rigidbody>();
            Physics.gravity = new Vector3(0,-100.0f,0);
        }

        // Update is called once per frame
        void Update()
        {
            // Move the object at start speed
            _rigidbody.velocity = new Vector3(_horizontalVel, _upVel, verticalVel);

            // Move left
            if (Input.GetKeyDown(moveLeft) & _info.GetLane() > 1)
            {
                _horizontalVel = -2;
                StartCoroutine(StopSlide());
            }

            // Move Right
            if (Input.GetKeyDown(moveRight) & _info.GetLane() < 3)
            {
                _horizontalVel = 2;
                StartCoroutine(StopSlide());
            }
            
            // Jump
            if (Input.GetKeyDown(jump) & _onGround)
            {
                _onGround = false;
                _upVel = jumpForce;
                _startJump = transform.position.z;
                StartCoroutine(Fall());
            }

        }

        IEnumerator StopSlide()
        {
            yield return new WaitForSeconds(.5f);
            _horizontalVel = 0;
        }

        // player falls as quickly as he jumps up (+ gravity)
        IEnumerator Fall()
        {
            yield return new WaitUntil(ReachedLength);
            _upVel = -jumpForce;
        }

        // is true once player has moved 1 unit in air
        private bool ReachedLength()
        {
            if (transform.position.z >= _startJump + 1.3)
            {
                return true;
            }
            return false;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Floor"))
            {
                _onGround = true;
                _upVel = 0;
            }
        }
    }
}

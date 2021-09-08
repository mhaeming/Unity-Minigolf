using System;
using System.Collections;
using System.Numerics;
using Code.World;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector3 = UnityEngine.Vector3;

namespace Code.Player
{
    [RequireComponent(typeof(PlayerBehavior))]
    public class PlayerMovement : MonoBehaviour
    {
        // Define KeyControls
        public KeyCode moveLeft = KeyCode.A;
        public KeyCode moveRight = KeyCode.D;
        public KeyCode jump = KeyCode.Space;
    
        public float jumpForce;
        public static float speed = 3;
        public float sideSpeed = 0.1f;
        public int Lane { get; private set; }
        public bool OnGround { get; private set; }
        
        private Rigidbody _rigidbody;
        private Vector3 _pos;
        private float _movementFactor;

        private void OnEnable()
        {
            PlayerBehavior.NextLevel += OnNextLevel;
        }

        private void OnDisable()
        {
            PlayerBehavior.NextLevel -= OnNextLevel;
        }


        // Start is called before the first frame update
        public void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            Lane = 0;
            _movementFactor = 1;
        }

        // Update is called once per frame
        private void Update()
        {
            // Debug for ground
            // if (OnGround)
            // {
            //     Debug.DrawRay(transform.position, Vector3.up * 10, Color.white, 0, false);
            // } else
            // {
            //     Debug.DrawRay(transform.position, Vector3.up * 10, Color.red, 0, false);
            // }
            
            // Move left
            if (Input.GetKeyDown(moveLeft) & Lane != -1)
            {
                Lane--;
            }

            // Move Right
            if (Input.GetKeyDown(moveRight) & Lane != 1)
            {
                Lane++;
            }
            
            // Jump
            if (Input.GetKeyDown(jump) & OnGround)
            {
                _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                OnGround = false;
            }

            _pos = transform.position;
            _pos.x = Mathf.Lerp(_pos.x, Lane * _movementFactor , sideSpeed);
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(_pos);
            _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, speed);
        }

        /// <summary>
        /// Set <see cref="OnGround"/> true when on a Floor tile
        /// </summary>
        /// <param name="other">Collider of other objects</param>
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Floor")) OnGround = true;
        }

        private void OnNextLevel()
        {
            speed += 0.1f;
        }
    }
}

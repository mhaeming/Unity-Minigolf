using System;
using System.Collections;
using System.Collections.Generic;
using Code.WorldGeneration;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    // Define KeyControls
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode speedUp;
    public KeyCode slowDown;
    public KeyCode jump;
    
    public float jumpForce;
    public float defaultSpeed = 2;

    public GameObject worldManager;
    
    private float _horizontalVel = 0;
    public float verticalVel;
    private float _upVel = 0;
    private Rigidbody _rigidbody;
    private bool _onGround;
    private float _startJump;

    // Start is called before the first frame update
    public void Start()
    {
        verticalVel = defaultSpeed;
        _rigidbody = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0,-100.0f,0);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object at start speed
        _rigidbody.velocity = new Vector3(_horizontalVel, _upVel, verticalVel);

        // Move left
        if (Input.GetKeyDown(moveLeft) & returnWhichLane() > 1)
        {
            _horizontalVel = -2;
            StartCoroutine(stopSlide());
        }

        // Move Right
        if (Input.GetKeyDown(moveRight) & returnWhichLane() < 3)
        {
            _horizontalVel = 2;
            StartCoroutine(stopSlide());
        }
        
        // Speed up
        if (Input.GetKeyDown(speedUp))
        {
            verticalVel += .5f;
        }
        
        // Slow down
        if (Input.GetKeyDown(slowDown) & verticalVel >= defaultSpeed)
        {
            verticalVel -= .5f;
        }
        
        // Jump
        if (Input.GetKeyDown(jump) & _onGround)
        {
            _onGround = false;
            _upVel = jumpForce;
            _startJump = gameObject.GetComponent<Transform>().position.z;
            StartCoroutine(Fall());
        }
        
        // reset Player if he has fallen off plane
        /*if (gameObject.GetComponent<Transform>().position.y <= -2) 
        {
            if (SceneManager.GetActiveScene().name == "TrainingScene")
            {
                transform.position = new Vector3(transform.position.x,1,transform.position.z - 5);
            }
            else
            {
                GameObject.Find("GameManager").GetComponent<ExperimentManager>().resetplayer = true;
                GameObject.Find("WorldManager").GetComponent<WorldManager>().resetFall += 1;
            }
        }*/
        
    }

    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(.5f);
        _horizontalVel = 0;
    }

    // player falls as quickly as he jumps up (+ gravity)
    IEnumerator Fall()
    {
        yield return new WaitUntil(reachedLength);
        _upVel = -jumpForce;
    }

    // is true once player has moved 1 unit in air
    private bool reachedLength()
    {
        if (gameObject.GetComponent<Transform>().position.z >= _startJump + 1.3)
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

        if (other.gameObject.CompareTag("Obstacle") & SceneManager.GetActiveScene().name != "TrainingScene")
        {
            WorldEvents.ResetEvent();
        }

        if (other.gameObject.CompareTag("TrainingFinish"))
        {
            Debug.Log("Training stage is finished.");
            SceneManager.LoadScene("Priming");
        }

        if (other.gameObject.CompareTag("Pit"))
        {
            _onGround = false;
            // GetComponent<AudioSource>().Play();
            StartCoroutine(WaitAndReset());
        }
    }

    IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(0.5f);
        if (SceneManager.GetActiveScene().name == "TrainingScene")
        {
            transform.position = new Vector3(transform.position.x,1,transform.position.z - 5);
        }
        else
        {
            WorldEvents.ResetEvent();
            // GameObject.Find("WorldManager").GetComponent<WorldManager>().resetFall += 1;
        }
    }

    // returns in which lane the player is at that moment (left=1, middle=2, right=3)
    public int returnWhichLane()
    {
        if (gameObject.GetComponent<Transform>().position.x > 0.5f )
        {
            return 3;
        }
        else if (gameObject.GetComponent<Transform>().position.x < -0.5f)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // public float speed = 3.5f;
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    private float _laser_spawn_offset = 0.8f;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire = 0f;


    // for demo purposes check horizontal input
    public float horizontalInputDemo;

    // Start is called before the first frame update
    void Start()
    {
    
        Vector3 vec3 = new Vector3(0, 0);       // Z is 0 by default
        transform.position = vec3;
        // transform.position = new Vector3(0,0,0);
        // since the script has been dragged to the Player object, and is now a component of same, the transform here is Player's transform

        //Player player = new Player();
        // player.transform
        // take the current position and assign to it a start position - snap to zero 0,0,0
    }

    // Update is called once per frame
    void Update()
    {    
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)        // 5-31 Instantiating 'laser' object & 5-37 'Cooling-down' system
        {
            FireLaser();            // cf Cleanup sections for refactoring
        }
    }

    // 4-26 Code Cleanup i.e. refactoring to a new method
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");        //transform.Translate(Vector3.right); // constant rapid move right-ward
        float verticalInput = Input.GetAxis("Vertical");        //transform.Translate(Vector3.right); // constant rapid move right-ward

        // Vector3.right is the same as new Vector3(1,0,0)
        //transform.Translate(new Vector3(1 * Time.deltaTime,0,0));   // use deltaTime to move at equivalent of 1 metre per second
        //transform.Translate(Vector3.right * 5 * Time.deltaTime);   // use deltaTime to move at equivalent of 5 metres per second
        //transform.Translate(Vector3.forward * _speed * Time.deltaTime);   // use deltaTime to move at equivalent of 5 metres per second
        // transform.Translate(Vector3.forward * _speed * verticalInput * Time.deltaTime);   // use deltaTime to move at equivalent of 5 metres per second

        // 4-22 User Input challenge
        //transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);   // use deltaTime to move at equivalent of 5 metres per second
        //transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);   // use deltaTime to move at equivalent of 5 metres per second

        // 4-23 1-line user input/movement (DIY)
        //transform.Translate(new Vector3(_speed * horizontalInput * Time.deltaTime, _speed * verticalInput * Time.deltaTime, 0));

        // could have been (using scalar multiplication) transform.Translate(new Vector3(horizontalInput,verticalInput, 0)* _speed * Time.deltaTime);

        // 4-23 refactored to use a variable for the vector:
        // Vector3 direction = new Vector3(horizontalInput, verticalInput,0);
        // transform.Translate(direction * _speed * Time.deltaTime);

        // 4-23 refactored to use a variable for the vector:
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
/*
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, transform.position.z);
        }
*/
        // 4-26 use .clamp to set Player bounds (in Code Cleanup) - won't work for wrapping though
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y,-3.8f,0), transform.position.z);


        // 4-24 user bounds/wrap challenge
        // vertical/horizontal bounds in separate clauses else only the 1st may be checked-for (and the 2nd ignored)
        // wrap from left side (approx -8.5 in x) to right, and vice-versa
        if (transform.position.x <= -8.5f)
        {
            transform.position = new Vector3(8.5f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= 8.5f)
        {
            transform.position = new Vector3(-8.5f, transform.position.y, transform.position.z);
        }

        // if the Player's position in y > 0 then y = 0 (to restrict to zero)
        // or if less than -3.8

        // NB Input for the Input Manager
        // just for demo
        horizontalInputDemo = Input.GetAxis("Horizontal");
    }

    // 5-39 Cleaning Up/Refactoring laser firing
    private void FireLaser()
    {
        // if space key hit, spawn object (use keyboard input manager)

        //if (Input.GetKeyDown(KeyCode.Space))
        //if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)        // 5-31 Instantiating 'laser' object & 5-37 'Cooling-down' system
        //{
            _nextFire = Time.time + _fireRate;  // 5-37 Cooling-Down system - set next time at which it'll be possible to fire
            // Debug.Log("Space key pressed!!!");
            //Instantiate(_laserPrefab,transform.position, Quaternion.identity);   // spawn 'laser' object at Player's position, and default rotated
            // 5-36 offset laser object's instantiation position to avoid clipping Player primitive
            Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + _laser_spawn_offset, transform.position.z), Quaternion.identity);   // spawn 'laser' object at Player's position, and default rotated
        //}
    }
}

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
    [SerializeField]
    private GameObject _tripleShotLaserPrefab;      // 8-62 triple shot prefab (must be serializable so as to drag&drop to Player in Inspector)
    private float _laser_spawn_offset = 1.05f;       // 8-59 laser 3d to 2d - was 0.8 when 3d
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire ;
    
    [SerializeField]
    private float _player_top_max = 0f;
    
    [SerializeField]
    private float _player_bottom_max = -3.8f;
    [SerializeField]
    private float _player_left_side_max = -8.5f;
    [SerializeField]
    private float _player_right_side_max = 8.5f;

    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;  // 7-54 the (script) component of the SpawnManager game object

    [SerializeField]
    private bool _tripleShotActive = false;  // Is triple shot active? challenge 9-62 

    // for demo purposes check horizontal input
    public float horizontalInputDemo;

    // Start is called before the first frame update
    void Start()
    {
        // 7-54 (Stop spawning when player has died) - just use plain Find (as per Inspector name) in this case
        _spawnManager = GameObject.Find("Spawn_Manager").transform.GetComponent<SpawnManager>();
    
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
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y,_player_bottom_max,_player_top_max), transform.position.z);


        // 4-24 user bounds/wrap challenge
        // vertical/horizontal bounds in separate clauses else only the 1st may be checked-for (and the 2nd ignored)
        // wrap from left side (approx -8.5 in x) to right, and vice-versa
        if (transform.position.x <= _player_left_side_max)
        {
            transform.position = new Vector3(_player_left_side_max, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= _player_right_side_max)
        {
            transform.position = new Vector3(_player_right_side_max, transform.position.y, transform.position.z);
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

         if (_tripleShotActive)      // Is triple shot active? challenge 9-62 
        {
            // if triple shot, instantiate triple shot prefab
            // otherwise just fire 1 laser
            Instantiate(_tripleShotLaserPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);   // spawn 'laser' object at Player's position, and default rotated
        }
        else
        {
            // 5-36 offset laser object's instantiation position to avoid clipping Player primitive
            Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + _laser_spawn_offset, transform.position.z), Quaternion.identity);   // spawn 'laser' object at Player's position, and default rotated
            }                                                                                                                                                                   //}
    }




    public void Damage()        // 6-47 Player lives/damage NB public so as to be accssible from Enemy
    {
        _lives--;

        // check if Player dead - destroy Player if so
        if (_lives < 1)
        {
            _spawnManager?.OnPlayerDeath();  // 7-54 Communicate with spawn manager (and null check)
            if (_spawnManager == null)
            {
                Debug.LogError("no Spawn Manager found (null)");
            }
            Destroy(this.gameObject);
        }
    }
}

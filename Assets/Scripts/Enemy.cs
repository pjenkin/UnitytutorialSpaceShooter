﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private float _enemy_bottom_max = -3.8f;
    [SerializeField]
    private float _enemy_top_max = 3.8f;
    [SerializeField]
    private float _enemy_left_side_max = -8.5f;
    [SerializeField]
    private float _enemy_right_side_max = 8.5f;
    [SerializeField]
    private float _enemy_random_x_pos = 0f;

    Player _player;      // us, the enemy's foe! 12-95 Score implementation (review)

    Animator _anim;    // 13-106 Enemy explosion implementation

    AudioManager _audioManager;     // my take on 15-122 Explosion sound audio

    // private Random _rand;       // System.Random not needed, and this would be Unity.Random (can use abstract)

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<Player>();    // move this to start
        if (_player == null)
        {
            Debug.Log("_player null");      // could be a test
        }

        _anim = GetComponent<Animator>();        // 13-106 Enemy explosion implementation (this.transform not needed)

        _audioManager = GameObject.Find("Audio_Manager")?.transform.GetComponent<AudioManager>();        // my take on 15-122 Explosion sound
        if (_audioManager == null)
        {
            Debug.LogError("_audioManager is null");
        }

    }

    // Update is called once per frame
    void Update()
    {
        // 6-43 move enemy object downwards at 4 units/metres per second (translate) - challenge
        // when off the bottom of the screen, respawn object at top at a random x position (if then position)
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= _enemy_bottom_max)
        {
            // _rand = new Random();   // System.Random not needed, and this would be Unity.Random (can use abstract)
            _enemy_random_x_pos =  Random.Range(_enemy_left_side_max, _enemy_right_side_max);
            //Mathf.Clamp(_enemy_random_x_pos, _enemy_left_side_max, _enemy_right_side_max);

            // transform.Translate(new Vector3(_enemy_random_x_pos, _enemy_top_max, 0));    
            transform.position = new Vector3(_enemy_random_x_pos, _enemy_top_max, 0);       // NB Translate to move relative; position to set absolute! 6-43
        }
    }

    // private void OnTriggerEnter(Collider other)
    private void OnTriggerEnter2D(Collider2D other)     // 58 Enemy 3D altered to 2D 
    {
        // if other (collider) is a Player
        //if (other.transform.name == "Player")
         if (other.tag == "Player")
        {
            // other.transform.GetComponent<Player>().Damage();    // 6-47 Player lives--/damage sustained by GetComponent generic - unsafe with no null check
            Player player = other.transform.GetComponent<Player>();
            // null check for Player (script) component in collided-with object
            player?.Damage(); // damage the player (6-47) Player lives--/damage sustained

            // then Destroy this (the Enemy)
            ExplosionAnim();                // 13-106 Enemy explosion implementation
            _audioManager.PlayExplosion();  // my take on 15-122 Explosion sound audio
            _speed = 0;                             // Stop Enemy in its tracks to avoid continuing & hitting Player
            Destroy(this.gameObject, 2.4f);       // Delay for explosion animation 13-106
            Debug.Log("Hit with: " + other.transform.name); // 6-45
        }
        // if other (collider) is a Laser
        //else if (other.transform.name == "Laser") // transform.name didn't seem to work for laser
        if (other.tag == "Laser")
        {
            // Destroy the laser
            Destroy(other.gameObject);
            // Add 10 to the Player's score
            // Player player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<Player>();    // move this to start
             _player?.AddScore(10);         // 12-94 Score implementation
            // then Destroy this (the Enemy)
            ExplosionAnim();                // 13-106 Enemy explosion implementation
            _audioManager.PlayExplosion();  // my take on 15-122 Explosion sound audio
            _speed = 0;                             // Stop Enemy in its tracks
            Destroy(this.gameObject, 2.4f);   // Delay for explosion animation 13-106
            Debug.Log("Hit with: " + other.transform.name); // 6-45
        }
    }

    /// <summary>
    /// Run enemy explosion animation
    /// 13-106 Enemy explosion implementation
    /// </summary>
    public void ExplosionAnim()
    {
        _anim.SetTrigger("OnEnemyDestroy");
    }
}

// instart, null check for player
// instart, assign the animator component to Anim

// handle to animator component
// access the Animator component, 
// trigger animation on both/all impacts
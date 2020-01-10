using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
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

    // private Random _rand;       // System.Random not needed, and this would be Unity.Random (can use abstract)

    // Start is called before the first frame update
    void Start()
    {
        
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

    private void OnTriggerEnter(Collider other)
    {
        // if other (collider) is a Player
        //if (other.transform.name == "Player")
        if (other.tag == "Player")
        {
            // damage the player
            //player.damage++; 
            // then Destroy this (the Enemy)
            Destroy(this.gameObject);
            Debug.Log("Hit with: " + other.transform.name); // 6-45
        }
        // if other (collider) is a Laser
        //else if (other.transform.name == "Laser") // trnasform.name didn't seem to work for laser
        if (other.tag == "Laser")
        {
            // Destroy the laser
            Destroy(other.gameObject);
            // then Destroy this (the Enemy)
            Destroy(this.gameObject);
            Debug.Log("Hit with: " + other.transform.name); // 6-45
        }
    }
}

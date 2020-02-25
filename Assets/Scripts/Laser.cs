using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 8f;      // 5-32 Laser behaviour (speed of 8)
    private float _screen_top = 8f;  // 5-34 destroy laser objects

    void Start()
    {
        // the laser object is already instantiated by the Player object, at the Player's location   
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);  // 5-32 Laser behaviour (movement upward)
        if (transform.position.y >= _screen_top) // 5-34 destroy laser objects at top of screen challenge
        {
            Destroy(this.gameObject.transform.parent?.gameObject);   // 9-68 Destroy parent object too, if object has a parent
            Destroy(this.gameObject);
            // NB not just Destroy(this);!
            // could have been Destroy(gameObject) or on a timer (e.g.)
            
            
        }
    }
}
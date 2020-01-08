using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 8f;      // 5-32 Laser behaviour (speed of 8)

    void Start()
    {
        // the laser object is already instantiated by the Player object, at the Player's location   
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);  // 5-32 Laser behaviour (movement upward)
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private float _asteroid_bottom_max = -3.8f;
    [SerializeField]
    private float _asteroid_top_max = 3.8f;
    [SerializeField]
    private float _asteroid_left_side_max = -8.5f;
    [SerializeField]
    private float _asteroid_right_side_max = 8.5f;
    [SerializeField]
    private float _asteroid_random_x_pos = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime * 0);    // stay on the spot for the mo
        Rotate();   // rotate the asteroid 13-108 Asteroid behaviour
    }

    /// <summary>
    /// Rotate an asteroid sprite at a given angular velocity
    /// 13-108 Asteroid behaviour
    /// </summary>
    /// <param name="angular_speed"></param>
    public void Rotate(float angular_speed = 1f)
    {
        transform.Rotate(Vector3.forward * angular_speed);
    }

}

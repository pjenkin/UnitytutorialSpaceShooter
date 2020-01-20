using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private float _powerup_bottom_max = -3.8f;
    [SerializeField]
    private float _powerup_top_max = 3.8f;
    [SerializeField]
    private float _powerup_left_side_max = -8.5f;
    [SerializeField]
    private float _powerup_right_side_max = 8.5f;
    [SerializeField]
    private float _powerup_random_x_pos = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);   // move down at speed
        if (transform.position.y <= _powerup_bottom_max)   // destory this object when bottom reached
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)     // 9-64 Triple shot powerup behaviour
    {
        if (collision.tag == "Player")
        {
            Player player = collision.transform.GetComponent<Player>();    // get the Player object's Player script component

            player?.TripleShot();
        }
    }
}

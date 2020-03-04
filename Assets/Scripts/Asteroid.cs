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

    // really? We have to do this :-/ Serialize variable for prefab (drag & drop), in order to Instantiate later
    [SerializeField]
    public GameObject _explosionPrefab;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();  // 13-112 Controlling the spawn wave via the asteroid's fate
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
    public void Rotate(float angular_speed = 20f)
    {
        transform.Rotate(Vector3.forward * angular_speed * Time.deltaTime);
    }


    // 13-110 Explode the asteroid
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            // check for laser collision
            // instantiate explosion animation at the position of the asteroid and destroy
            // destroy the explosion after 3 seconds

            GameObject newExplosion = Instantiate(_explosionPrefab, this.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);             // Destroy the (colliding) laser along with everything else
            _spawnManager.StartSpawning();              // 13-112 Controlling the spawn wave through the asteroid - start spawning only when asteroid destroyed
            Destroy(this.gameObject.transform.parent?.gameObject);
            Destroy(this.gameObject, 0.25f); // Slight delay as animation a little tardy to get going
            // Destroy(newExplosion, 3);        // Instead have the explosion destroy itself (cf Explosion::Start method)
        }

        // should perhaps check for Player collision also?
    }

}

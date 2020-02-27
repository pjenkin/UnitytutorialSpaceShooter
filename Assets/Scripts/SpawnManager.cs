using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour       // bespoke Enemy spawn manager class/(actually empty GameObject in hierarchy)
{
    // 7-53 Tidying up spawning (in hierarchy, via container object)
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _tripleshotContainer;    // 9-71 (need to make new empty container & drag & drop this to poperty too)
    [SerializeField]
    private GameObject _speedContainer;    // 10-78 (need to make new empty container & drag & drop this to poperty too)


    // 7-51 Challenge: Spawn Routine
    [SerializeField]                   // *must* be serialized, so as to be able to drag&drop Prefab to this field in Inspector
    public GameObject _enemyPrefab;    // this must be manually dragged from the Prefabs folder to this field in the Inspector (NB public also?)
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
    [SerializeField]
    private Vector3 _enemy_position;        // do not initialise start position yet, for the mo
    [SerializeField]
    private float _spawn_interval = 5f;

    // 9-72 Tripleup spawn routine
    [SerializeField]                   // *must* be serialized, so as to be able to drag&drop Prefab to this field in Inspector
    public GameObject _tripleshotPrefab;    // this must be manually dragged from the Prefabs folder to this field in the Inspector (NB public also?)
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
    [SerializeField]
    private Vector3 _tripleshot_position;        // do not initialise start position yet, for the mo
    [SerializeField]
    private float _tripleshot_spawn_interval_min = 3f;
    [SerializeField]
    private float _tripleshot_spawn_interval_max = 7f;


    [SerializeField]
    public GameObject _speedPrefab;             // 10-78 Speed boost implementation
    [SerializeField]
    private Vector3 _speed_position;        // do not initialise start position yet, for the mo
    [SerializeField]
    private float _speed_spawn_interval_min = 3f;
    [SerializeField]
    private float _speed_spawn_interval_max = 7f;

    private IEnumerator _spawn_enemy_coroutine;          // declare a coroutine of type IEnumerator (which can be yield'd?)
    private IEnumerator _spawn_tripleshot_coroutine;    // 9-71 declare a coroutine of type IEnumerator (which can be yield'd?) for tripleshots
    private IEnumerator _spawn_speed_coroutine;
    private bool _stopSpawning = false;      // 7-54 stop spawning on player death


    // Start is called before the first frame update
    void Start()
    {

        _spawn_enemy_coroutine = SpawnEnemyRoutine(_spawn_interval);
        StartCoroutine(_spawn_enemy_coroutine);         // run the Spawning method of the SpawnManager every 5 seconds e.g.
        // NB could have used a string as the name of the coroutine routine
        _spawn_tripleshot_coroutine = SpawnTripleShotPowerupRoutine(_tripleshot_spawn_interval_min);      // 9-71 Spawning tripleshot powerup
        StartCoroutine(_spawn_tripleshot_coroutine);
        _spawn_speed_coroutine = SpawnSpeedPowerupRoutine(_speed_spawn_interval_min);
        StartCoroutine(_spawn_speed_coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Spawn game objects every 5 seconds
    // Create a coroutine of type IEnumerator - to *yield* events (yield event will give 'time to breathe' from infinite loop)
    // declare infinite while loop within coroutine

    IEnumerator SpawnEnemyRoutine(float interval = 5f)
    {
        // while (true) // while loop (infinite loop)
        while (!_stopSpawning)    // 7-54 stop spawning when Player dies (from .Damage)
        {
            _enemy_random_x_pos = Random.Range(_enemy_left_side_max, _enemy_right_side_max);    // Enemy position random in x axis, and at top y

            _enemy_position = new Vector3(_enemy_random_x_pos, _enemy_top_max, 0);              // spawn Enemy object at random top position, and default rotated       

            GameObject newEnemy = Instantiate(_enemyPrefab, _enemy_position, Quaternion.identity);                    // Instantiate an Enemy prefab

            // 7-53 Tidying up spawning in hierarchy by instantating within SpawnManager's EnemyContainer
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(interval);    // (when should we run this code again?) yield 'wait for 5 seconds'
            // NB yield is required by IEnumerator
        }
    }

    IEnumerator SpawnTripleShotPowerupRoutine(float interval = 3f)
    {
        // 9-71 Spawn tripleshot powerup every 3-7 seconds
        while (!_stopSpawning)
        {
            _powerup_random_x_pos = Random.Range(_powerup_left_side_max, _powerup_right_side_max);

            _tripleshot_position = new Vector3(_powerup_random_x_pos, _powerup_top_max, 0);
            GameObject newTripleShot = Instantiate(_tripleshotPrefab, _tripleshot_position, Quaternion.identity);

            if (newTripleShot.transform.parent != null)
            {
                newTripleShot.transform.parent = _tripleshotContainer.transform;
            }

            yield return new WaitForSeconds(Random.Range(_tripleshot_spawn_interval_min, _tripleshot_spawn_interval_max));
        }

    }
    IEnumerator SpawnSpeedPowerupRoutine(float interval = 3f)
    {
        // 10-78 Speed boost implementation
        while (!_stopSpawning)
        {
            _powerup_random_x_pos = Random.Range(_powerup_left_side_max, _powerup_right_side_max);
            _speed_position = new Vector3(_powerup_random_x_pos, _powerup_top_max, 0);
            GameObject newSpeed = Instantiate(_speedPrefab, _speed_position, Quaternion.identity);

            if (newSpeed.transform.parent != null)
            {
                newSpeed.transform.parent = _speedContainer.transform;
            }
            
            yield return new WaitForSeconds(Random.Range(_speed_spawn_interval_min, _speed_spawn_interval_max));
        }
    }
    public void OnPlayerDeath()     // public - callable by Script communication from another game object
    {
        _stopSpawning = true;       // 7-54 Stop spawning when Player dies
    }
}

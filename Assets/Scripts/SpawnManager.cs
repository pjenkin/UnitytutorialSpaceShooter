using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour       // bespoke Enemy spawn manager class/(actually empty GameObject in hierarchy)
{
    // 7-51 Challenge: Spawn Routine
    [SerializeField]
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

    private IEnumerator _coroutine;          // declare a coroutine of type IEnumerator (which can be yield'd?)

    // Start is called before the first frame update
    void Start()
    {

//        Instantiate(_enemyPrefab, _enemy_position, Quaternion.identity);                    // Instantiate an Enemy prefab
        // _enemyPrefab = Resources.Load<GameObject>("Enemy") as GameObject;    // not needed - just drag&drop in Inspector (https://gamedev.stackexchange.com/a/158713) https://docs.unity3d.com/Manual/InstantiatingPrefabs.html
        _coroutine = SpawnRoutine(_spawn_interval);
        StartCoroutine(_coroutine);         // run the Spawning method of the SpawnManager every 5 seconds e.g.

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Spawn game objects every 5 seconds
    // Create a coroutine of type IEnumerator - to *yield* events (yield event will give 'time to breathe' from infinite loop)
    // declare infinite while loop within coroutine

    IEnumerator SpawnRoutine(float interval = 5f)
    {
        while (true) // while loop (infinite loop)
        {
            _enemy_random_x_pos = Random.Range(_enemy_left_side_max, _enemy_right_side_max);    // Enemy postion random in x axis, and at top y

            _enemy_position = new Vector3(_enemy_random_x_pos, _enemy_top_max, 0);              // spawn Enemy object at random top position, and default rotated       

            Instantiate(_enemyPrefab, _enemy_position, Quaternion.identity);                    // Instantiate an Enemy prefab

            yield return new WaitForSeconds(interval);    // (when should we run this code again?) yield 'wait for 5 seconds'
        }
    }
}

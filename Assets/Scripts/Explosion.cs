using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    private AudioManager _audioManager;     // my take on 15-122 Explosion sound

    // Start is called before the first frame update
    void Start()
    {
        _audioManager = GameObject.Find("Audio_Manager")?.transform.GetComponent<AudioManager>();        // my take on 15-122 Explosion sound
        if (_audioManager == null)
        {
            Debug.LogError("_audioManager is null");
        }

        _audioManager.PlayExplosion();      // my take on 15-122 Explosion sound
        Destroy(this.gameObject, 3f);      // 13-111 Explode the asteroid - and the Explosion can clean itself away, in due course
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

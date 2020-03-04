using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]        // my take on 15-122 Explosion sound
    AudioSource _explosionAudio;


    void Start()
    {
        _explosionAudio = GameObject.Find("Explosion_audio").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 1 place from which to play explosion audio, via Audio Manager
    /// my take on 15-122 Explosion sound
    /// </summary>
    public void PlayExplosion()
    {
        _explosionAudio.Play();
    }
}

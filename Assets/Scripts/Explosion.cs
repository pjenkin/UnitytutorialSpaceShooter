using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 3f);      // 13-111 Explode the asteroid - and the Explosion can clean itself away, in due course
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

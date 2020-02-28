using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // 12-94 implementation of score

public class UIManager : MonoBehaviour
{
    // A handle to the Score text's text component 12-94 score implementation
    [SerializeField]
    private Text _scoreText; // Text (Canvas child object) needing UnityEngine.UI        
    private Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<Player>();       // must do this from within Start or Update (once should be enough)
        // assign text component of Text object to the handle
        _scoreText.text = $"Score: {player?.GetScore()}";
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = $"Score: {player?.GetScore()}";
    }
}

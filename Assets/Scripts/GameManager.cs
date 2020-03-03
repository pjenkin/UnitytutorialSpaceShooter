using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// (in 12-102 R key to restart level)
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    // Update is called once per frame
    void Update()
    {
        // if the R key was pressed then restart the current scene
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);     // 12-101 Key R to restart 'level' - scene named actually "Game" - File/Build Settings/Add Open Scenes - or index 0
            // SceneManager.LoadScene(0);     // 12-101 Key R to restart 'level' - scene named actually "Game" - File/Build Settings/Add Open Scenes - or index 0
            // SceneManager.LoadScene("Game");     // 12-101 Key R to restart 'level' - scene named actually "Game" - File/Build Settings/Add Open Scenes - or index 0
        }

        // text to appear when game over only (lives at zero)
        // pressable only when game over (lives at zero)
        // reload the Unity Scene    

    }
}

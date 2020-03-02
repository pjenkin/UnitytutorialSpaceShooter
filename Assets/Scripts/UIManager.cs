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
    [SerializeField]
    private Text _gameOverText;     // 12-97 Game over text (need to drag&drop in Inspector to populate this)
    
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _livesSprites;  // 12-96 Lives display NB UnityEngine.Sprite
    
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

    /// <summary>
    /// 12-96 Lives display, to be called from Player.Damage
    /// </summary>
    /// <param name="currentLives"></param>
    public void UpdateLives(int currentLives)
    {
        // display the image sprite
        // change image sprite for lives based on the currentLives index
        _LivesImg.sprite = _livesSprites[currentLives];

        if (currentLives == 0)   // 12-97 Game Over
        {
            DisplayGameOver();
            StartCoroutine(GameOverFlickerRoutine());   // 12-100 blink text
        }

    }

    /// <summary>
    /// 12-97 Game Over text - display text
    /// </summary>
    public void DisplayGameOver()
    {
        _gameOverText.gameObject.SetActive(true);       // or possibly .enabled?
    }

    // 12-100 Game Over text flicker/blink
    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER!!!";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
            // but - why???
        }
    }
}

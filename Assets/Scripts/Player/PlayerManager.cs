using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
    public static bool levelStarted;
    public static bool gameOver;
    public static int gems;
    public static int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gemText;
    public TextMeshProUGUI highScoreText;
    public GameObject startMenuPanle;
    public GameObject gameOverPanle;
    public Banner bannerScript;
    // Start is called before the first frame update
    void Start()
    {
        gameOverPanle.SetActive(false);
        gameOver = levelStarted = false;
        Time.timeScale = 1;
        gems = 0;
        score = 0;
        bannerScript.CreateBannerView();
        
        // Find the InterstitialManager in the scen
    }

    // Update is called once per frame
    void Update()
    {
        gemText.text = (PlayerPrefs.GetInt("Total Gems", 0) + gems).ToString();
        scoreText.text = "Score: " + score.ToString();
        Touchscreen ts = Touchscreen.current;
        if (ts != null && ts.primaryTouch.press.isPressed && !levelStarted)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            levelStarted = true;
            startMenuPanle.SetActive(false);
        }
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanle.SetActive(true);
            PlayerPrefs.SetInt("Total Gems", PlayerPrefs.GetInt("Total Gems", 0) + gems);
            if (score > PlayerPrefs.GetInt("HighScore", 0))
            {
                highScoreText.text = "New High Score: " + score;
                PlayerPrefs.SetInt("HighScore", score);
            }
            
             

            // Show interstitial ad on the third restart

            this.enabled = false;
        }
        bannerScript.LoadAd();
    }
}
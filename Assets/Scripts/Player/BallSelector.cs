using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BallSelector : MonoBehaviour
{
    public GameObject[] ballSkins;
    int selectedBall;
    public Ball[] balls;
    public Button[] buttons;
    public Button unLockButton;
    // Start is called before the first frame update

    private void Awake()
    {
        int counter = 0;
        foreach(Ball b in balls)
        {
            b.index = counter;
            if (counter == 0)

                b.isLocked = false;
            else
            {
                if (PlayerPrefs.GetInt(b.index.ToString(), 1) == 1)
                {
                    b.isLocked = true;
                }
                else
                {
                    b.isLocked = false;
                }
                buttons[b.index].interactable = !b.isLocked;
            }
            
            counter++;
        }
    }
    
    void Start()
    {
        selectedBall = PlayerPrefs.GetInt("SelectedBall", 0);
        foreach(GameObject skin in ballSkins)
        {
            skin.SetActive(false);
        }
        ballSkins[selectedBall].SetActive(true);
        
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("Total Gems", 0) < 50)
            unLockButton.interactable = false;
        else
            unLockButton.interactable = true;

    }

    public void OnBallChanged(int index)
    {
        ballSkins[selectedBall].SetActive(false);
        selectedBall = index;
        ballSkins[selectedBall].SetActive(true);
        PlayerPrefs.SetInt("SelectedBall", index);
    }

    public void Unlock()
    {
        //get the lockedball
        List<Ball> lockedBalls = new List<Ball>();
        foreach(Ball b in balls)
        {
            if (b.isLocked)
                lockedBalls.Add(b);
        }
        if(lockedBalls.Count == 0)

            return;

        //select a random ball

        int randomBall = Random.Range(0, lockedBalls.Count);

        //unlock the ball
        int ballInex = lockedBalls[randomBall].index;
        balls[ballInex].isLocked = false;
        buttons[ballInex].interactable = true;
        PlayerPrefs.SetInt(ballInex.ToString(), 0);
        PlayerPrefs.SetInt("TotalGems", PlayerPrefs.GetInt("Total Gems") - 50);

        //select the ball

        buttons[ballInex].onClick.Invoke();

    }
}

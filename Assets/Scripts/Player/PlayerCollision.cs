using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    AudioManager audioManager;
  //  InterstitialManager interstitialManager;
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        //interstitialManager = FindObjectOfType<InterstitialManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacles"))
        {
            Time.timeScale = 0;
            audioManager.Play("gameOver");
            PlayerManager.gameOver = true;
            //interstitialManager.ShowInterstitialAd();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            audioManager.Play("coin");
            PlayerManager.gems++;
            PlayerManager.score++;
            Destroy(other.gameObject);
        }
    }

}

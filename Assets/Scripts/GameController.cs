using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int count = 0;
    public GameObject player1, player2, Assets, gameOver;
    AIBot bot;

    private void Start()
    {
        bot = GameObject.FindObjectOfType<AIBot>();
        gameOver.SetActive(false);
        Assets.SetActive(true);
        StartCoroutine(Timer());
    }

    void Update()
    {
        PlayerTurn();
    }

    IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(120f);
        Assets.SetActive(false);
        gameOver.SetActive(true);
    }

    public void PlayerTurn()
    {
        if (count % 2 == 0)
        {
            player1.SetActive(true);
            player2.SetActive(false);
        }
        else
        {
            player2.SetActive(true);
            player1.SetActive(false);
            bot.hasHit = false;
        }
    }
}

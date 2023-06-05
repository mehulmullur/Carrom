using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinCollector : MonoBehaviour
{
    public int point = 0;
    public TMP_Text points;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Coins")
        {
            if (collision.gameObject.name == "Queen")
            {
                point += 2;
            }
            Destroy(collision.gameObject);
            point += 1;
        }
        points.text = point.ToString();
    }
}

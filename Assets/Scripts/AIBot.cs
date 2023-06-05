using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBot : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject nearest;
    float distance; 
    float nearesDistance = 10000;

    public Rigidbody2D rb;
    float x = 0;

    public GameController gc;
    public bool hasHit = false;
    Vector3 startPos;

    private void Start()
    {
        startPos = this.transform.position;
    }
    void Update()
    {
        objects = new GameObject[0];
        objects = GameObject.FindGameObjectsWithTag("Coins");
        for(int i = 0; i < objects.Length; i++)
        {
            distance = Vector2.Distance(transform.position, objects[i].transform.position);
            if (distance < nearesDistance)
            {
                nearest = objects[i];
                nearesDistance = distance;
            }
        }
        
        if (nearest != null && rb.velocity.magnitude == 0 && !hasHit)
        {
            BotShoot();
        }
        if (rb.velocity.magnitude < 0.1f && rb.velocity.magnitude != 0)
        {
            StrikerReset();
        }
    }

    public void BotShoot()
    {
        if (gc.player2.activeSelf)
        {
            if (rb.velocity.magnitude == 0)
            {
                x = Vector2.Distance(transform.position, nearest.transform.position);
            }
        }
        Vector2 direction = (Vector2)(nearest.transform.position - transform.position);

        direction.Normalize();

        rb.AddForce(direction * x * 150);
        hasHit = true;
    }

    public void StrikerReset()
    {
        rb.velocity = Vector2.zero;
        hasHit = false;
        transform.position = startPos;
        gc.count++;
    }
}

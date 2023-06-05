using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrikerController : MonoBehaviour
{
    public Slider strikerSlider;

    [SerializeField] Camera main;


    Vector3 mousepos1, mousepos2;
    public LineRenderer line;

    Vector2 startPos, direction;
    Rigidbody2D rb;

    bool posSet, hasStriked;

    public GameController gc;
    
    void Start()
    {
        gc = GameObject.FindObjectOfType<GameController>();
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        strikerSlider.onValueChanged.AddListener(StrikerXPos);
    }

    void Update()
    {
        line.enabled = false;
        mousepos1 = main.ScreenToWorldPoint(Input.mousePosition);
        mousepos2 = new Vector3(-mousepos1.x, -mousepos1.y - 3, mousepos1.z);

        if (!hasStriked && !posSet)
            transform.position = new Vector2(strikerSlider.value, startPos.y);

        RaycastHit2D hit = Physics2D.Raycast(main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            if (Input.GetMouseButton(0))
            {
                if (!posSet)
                    posSet = true;
            }
        }

        if(mousepos2.y > 0.956)
        {
            mousepos2.y = 0.956f;
        }
        if(mousepos2.y < -5.8)
        {
            mousepos2.y = -5.8f;
        }
        if (mousepos2.x < -5.82)
        {
            mousepos2.x = -5.82f;
        }
        if (mousepos2.x > 5.82)
        {
            mousepos2.x = 5.82f;
        }

        if (posSet && rb.velocity.magnitude == 0)
        {
            line.enabled = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, mousepos2);
        }
        
        if (Input.GetMouseButtonUp(0) && rb.velocity.magnitude == 0 && posSet)
            Shoot();

        if(rb.velocity.magnitude < 0.1f && rb.velocity.magnitude != 0)
        {
            StrikerReset();
            gc.count++;
        }
        
    }

    public void Shoot()
    {
        float x = 0;
        if (rb.velocity.magnitude == 0 && posSet) //Check for velocity to be 0 and pos finalised
        {
            x = Vector2.Distance(transform.position, mousepos1); //Check for distance between the striker and the mouseposition
        }
        direction = (Vector2)(mousepos2 - transform.position);
        direction.Normalize();
        rb.AddForce(direction *x* 200);
        hasStriked = true;
    }

    public void StrikerXPos(float Value)
    {
        transform.position = new Vector3(Value, transform.position.y, 0f);
    }

    public void StrikerReset()
    {
        rb.velocity = Vector2.zero;
        hasStriked = false;
        posSet = false;
        line.enabled = true;
    }
}
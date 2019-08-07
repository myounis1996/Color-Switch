using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 5f;
    [SerializeField] Color[] colors;
    [SerializeField] GameObject colorChanger;
    [SerializeField] Text pointsText;
    [SerializeField] GameObject plusOne;

    string[] stringColors = { "#35E2F2", "#F6DF0E", "#FF0080", "#8C13FB" };
    public string CurrentColor;
    SpriteRenderer sr;

    int pointsCounter;

    void Awake()
    {
        int counter = 0;
        pointsCounter = 0;
        foreach (string strColor in stringColors)
        {
            ColorUtility.TryParseHtmlString(strColor, out Color color);
            colors[counter] = color;
            counter++;
        }   
        SetRandomColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.up * speed;
            rb.gravityScale = 1.5f;
        }
        if (transform.position.y > Camera.main.transform.position.y)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
        }

    }

    public void SetRandomColor()
    {
        sr = GetComponent<SpriteRenderer>();
        int index = Random.Range(0, colors.Length);
        sr.color = colors[index];
        string[] colorsNames = { "Cyan", "Yellow", "Pink", "Purple" };
        CurrentColor = colorsNames[index];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "StartBar")
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
            return;
        } 
        if(collision.tag == "LoseCollider")
        {
            Debug.Log("Game over");
            Destroy(gameObject);
            return;
        }
        if(collision.tag == "ColorChanger")
        {  
            SetRandomColor();
            collision.gameObject.SetActive(false);
            return;
        }
        
        if (collision.tag == "Points")
        {
            //plusOne.SetActive(true);// here qaddora
            pointsCounter++;
            pointsText.text = pointsCounter.ToString();
            collision.gameObject.SetActive(false);
            return;
        } 

        if (collision.tag != CurrentColor)
        {
            Destroy(gameObject);
            return;
        }
    }
}

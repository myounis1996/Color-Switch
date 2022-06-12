using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{
    public GameObject startPanel;
    public Rigidbody2D rb;
    public float speed = 5f;
    public Color[] colors;
    public GameObject colorChanger;
    public Text pointsText;
    public GameObject plusOne;

    string[] stringColors = { "#35E2F2", "#F6DF0E", "#FF0080", "#8C13FB" };
    string[] colorsNames = { "Cyan", "Yellow", "Pink", "Purple" };

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

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (startPanel.activeInHierarchy)
                startPanel.SetActive (false);
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
        int index = UnityEngine.Random.Range(0, colors.Length);
        sr.color = colors[index];
        CurrentColor = colorsNames[index];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "StartBar")
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
            startPanel.SetActive (true);
            return;
        } 
        if(Array.IndexOf(colorsNames, collision.tag) != -1 && collision.tag !=CurrentColor)
        {
            Debug.Log("Game over");
            Destroy(gameObject);
            SceneManager.LoadScene (0);
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
            Destroy(Instantiate (plusOne, transform.position, Quaternion.identity), 1);
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

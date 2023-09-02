using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasketBehavior : MonoBehaviour
{
    public float BasketSpeed = 5.0f;
    public GameObject GroceryBaggerBehavior;

    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {

        
        Movment();
    }

    bool InFrenzyMode()
    {
        return GroceryBaggerBehavior.GetComponent<GroceryBaggerBehavior>().InFrenzyMode;
    }

    void LeaveFrenzyMode()
    {
        GroceryBaggerBehavior.GetComponent<GroceryBaggerBehavior>().InFrenzyMode = false;
    }


    void Movment()
    {
        if (Input.GetKey(KeyCode.A) && transform.position.x >= -7)
        {
            transform.position -= new Vector3(BasketSpeed * Time.deltaTime, 0, 0);

        }   
        if (Input.GetKey(KeyCode.D) && transform.position.x <= 7)
        {
            transform.position += new Vector3(BasketSpeed * Time.deltaTime, 0, 0);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Grocery")
        {
            FindObjectOfType<AudioManager>().Play("Good Pickup");
            Destroy(collision.gameObject);
            score += 100;
            GameObject.Find("Score").GetComponent<Text>().text = score.ToString();
        }
        else if (collision.gameObject.tag == "Bad Item")
        {
            if (InFrenzyMode())
            {
                LeaveFrenzyMode();
            }
            FindObjectOfType<AudioManager>().Play("Bad Pickup");
            Destroy(collision.gameObject);
            score -= 100;
            if (score < 0)
            {
                score = 0;
            }
            GameObject.Find("Score").GetComponent<Text>().text = score.ToString();
        }
    }
}
        
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehavior : MonoBehaviour
{
    public GameObject GroceryBaggerBehavior;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool InFrenzyMode()
    {
        return GroceryBaggerBehavior.GetComponent<GroceryBaggerBehavior>().InFrenzyMode;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("reached 1");
        if (InFrenzyMode())
        {
            Debug.Log("reached 2");
            if (collision.gameObject.tag == "Grocery" || collision.gameObject.tag == "Bad Item")
            {
                Debug.Log("reached 3");
                Rigidbody2D FrenzyItemRB = collision.gameObject.GetComponent<Rigidbody2D>();
                FrenzyItemRB.velocity = new Vector2(-1f*FrenzyItemRB.velocity.x, FrenzyItemRB.velocity.y);
            }
        }

    }
    
}

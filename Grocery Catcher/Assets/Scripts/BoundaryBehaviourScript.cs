using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoundaryBehaviourScript : MonoBehaviour
{

    private List<GameObject> Baskets;
    private GameObject Basket;
    // Start is called before the first frame update
    void Start()
    {
        Basket = GameObject.Find("Basket");
        Baskets = new List<GameObject>();

        for (int i = 0; i < 3; i++)
        {
            GameObject b = Instantiate<GameObject>(Basket);
            Vector3 bpos = b.transform.position;
            bpos.y = Basket.transform.position.y - (0.4f * i);
            b.transform.position = bpos;
            Baskets.Add(b);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Grocery")
        {
            FindObjectOfType<AudioManager>().Play("Monkey Pickup");
            Destroy(collision.gameObject);
            int ind = Baskets.Count - 1;
            if (ind > 0)
            {
                Destroy(Baskets[ind]);
                Baskets.RemoveAt(ind);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        else if (collision.gameObject.tag == "Bad Item")
        {
            Destroy(collision.gameObject);
        }
    }
}

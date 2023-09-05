using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoundaryBehaviourScript : MonoBehaviour
{
    public GameObject GroceryBaggerBehavior;
    private List<GameObject> Baskets;
    private GameObject Basket;
    private bool ExitingFrenzyMode;
    public bool lose = false;
    public GameObject Life;
    public List<GameObject> Lives;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        lose = false;
        Basket = GameObject.Find("Basket");
        Baskets = new List<GameObject>();
        


        for (int i = 0; i < 3; i++)
        {
            GameObject L = Instantiate<GameObject>(Life);
            Vector3 Lpos = L.transform.position;
            Lpos.x = Life.transform.position.x + 1.2f * i;
            L.transform.position = Lpos;
            Lives.Add(L);
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
        ExitingFrenzyMode = GroceryBaggerBehavior.GetComponent<GroceryBaggerBehavior>().ExitingFrenzyMode;

        GameOver();

    }

    bool InFrenzyMode()
    {
        return GroceryBaggerBehavior.GetComponent<GroceryBaggerBehavior>().InFrenzyMode;
    }


    public void GameOver()
    {
        if (lose)
        {

            SceneManager.LoadScene(2);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (InFrenzyMode())
        {
            Destroy(collision.gameObject);
        }
        else
        {
            if (collision.gameObject.tag == "Grocery")
            {
                FindObjectOfType<AudioManager>().Play("Monkey Pickup");
                Destroy(collision.gameObject);
                if (!ExitingFrenzyMode)
                {
                    int ind = Baskets.Count - 1;
                    
                    if (ind > 0)
                    {
                        Destroy(Baskets[ind]);
                        Destroy(Lives[ind]);
                        Baskets.RemoveAt(ind);
                        Lives.RemoveAt(ind);
                    }
                    else
                    {
                        lose = true;
                        
                    }
                }
                
            }
            else if (collision.gameObject.tag == "Bad Item")
            {
                Destroy(collision.gameObject);
            }
        }
        
    }
}

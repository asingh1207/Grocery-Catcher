using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoundaryBehaviourScript : MonoBehaviour
{
    public GameObject GroceryBaggerBehavior;
    private List<GameObject> Baskets;
    private GameObject Basket;
    private bool ExitingFrenzyMode;
    public bool lose = false;
    public GameObject Life;
    public List<GameObject> Lives;
    public GameObject Watchout;
    public GameObject Yousuck;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        Watchout.SetActive(false);
        Yousuck.SetActive(false);
        lose = false;
        Basket = GameObject.Find("Basket");
        Baskets = new List<GameObject>();

        


        for (int i = 0; i < 3; i++)
        {
            GameObject L = Instantiate<GameObject>(Life);
            Vector3 Lpos = L.transform.position;
            Lpos.x = Life.transform.position.x + 1f * i;
            L.transform.position = Lpos;
            Lives.Add(L);
            GameObject b = Instantiate<GameObject>(Basket);
            b.GetComponent<SpriteRenderer>().sortingOrder = i;
            Vector3 bpos = b.transform.position;
            bpos.y = Basket.transform.position.y - (0.4f * i);
            b.transform.position = bpos;
            Baskets.Add(b);
        }
    }

    // Update is called once per frame
    void Update()

    {
       // Screen.fullScreen = false;
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

    public void DisableWatchout()
    {
        Watchout.SetActive(false);
    }

    public void DisableYousuck()
    {
        Yousuck.SetActive(false);
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

                    if (ind == 2)
                    {
                        Watchout.SetActive(true);
                        Invoke("DisableWatchout", 2.0f);
                        

                    }
                    else if (ind == 1)
                    {
                        Watchout.SetActive(false);
                        Yousuck.SetActive(true);
                        Invoke("DisableYousuck", 2.0f);
                    }
                    
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

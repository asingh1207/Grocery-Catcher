using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroceryBaggerBehavior : MonoBehaviour
{
    public float GroceryBaggerSpeed = 2.5f;

    // Start is called before the first frame update

    public GameObject bananaPrefab;

    void Start()
    {

       Invoke("Drop", 2.0f);
    }
        
    // Update is called once per frame
    void Update()
    {
        transform.Translate(GroceryBaggerSpeed * Time.deltaTime, 0, 0);
        
    }

    void Drop()
    {
        GameObject Banana = Instantiate<GameObject>(bananaPrefab);
        Banana.transform.position = transform.position;
        Invoke("Drop", 1.0f);
    }
    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.name == "Bounds")
            GroceryBaggerSpeed *= -1;   
    }
}

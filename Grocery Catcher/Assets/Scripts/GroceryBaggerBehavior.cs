using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroceryBaggerBehavior : MonoBehaviour
{
    public float GroceryBaggerSpeed = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(GroceryBaggerSpeed * Time.deltaTime, 0, 0);
        
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.name == "Bounds")
            GroceryBaggerSpeed *= -1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroceryBaggerBehavior : MonoBehaviour
{
    public float GroceryBaggerSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(GroceryBaggerSpeed * Time.deltaTime, 0, 0);
        if (transform.position.x >= 5.0f || transform.position.x <= -5.0f)
            GroceryBaggerSpeed *= -1;
        
    }
}

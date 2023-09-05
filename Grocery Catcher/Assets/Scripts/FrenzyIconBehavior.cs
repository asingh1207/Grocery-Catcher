using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrenzyIconBehavior : MonoBehaviour
{
    public Image FrenzyIcon;
    public GameObject GroceryBaggerBehavior;
    private float val;
    // Start is called before the first frame update
    void Start()
    {
        val = 0;
        FrenzyIcon.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        val += Time.deltaTime;
        val %= 0.5f;

        float ScaleAddition = val;
        if (val>0.25f)
        {
            ScaleAddition = 0.25f - (val - 0.25f);
        }
        FrenzyIcon.transform.localScale = new Vector3(1f+ScaleAddition, 0.2f+ ScaleAddition, 1f+ ScaleAddition);
        FrenzyIcon.enabled = InFrenzyMode();
    }

    bool InFrenzyMode()
    {
        return GroceryBaggerBehavior.GetComponent<GroceryBaggerBehavior>().InFrenzyMode;
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GroceryBaggerBehavior : MonoBehaviour
{

    //public BasketBehavior BasketBehaviorClass;
    private float GroceryBaggerSpeed = 2.0f;
    private float DropIntervalRate = 1.5f;
    private int DroppedItems = 0;
    private int RemainingItemsInWave = 3;
    private int WaveSize = 3;
    private int Wave = 0;
    private float PauseBetweenDropWaves = 3.0f;
    private int BaggerDirectionChangeRandomScale = 12;
    private System.Random RandomGenerator = new System.Random();


    // Start is called before the first frame update

    public GameObject BananaPrefab;
    public GameObject ApplePrefab;
    public int NumPrefabs = 2;

    void Start()
    {
        DropIntervalRate = 1.5f;
        DroppedItems = 0;
        RemainingItemsInWave = 3;
        WaveSize = 3;
        Wave = 0;
        PauseBetweenDropWaves = 3.0f;
        Invoke("Drop", PauseBetweenDropWaves);
    }
        
    // Update is called once per frame
    void Update()
    {
        float PotentialX = transform.position.x + GroceryBaggerSpeed * Time.deltaTime;
        if (PotentialX >= 7.2)
        {
            FlipBaggerDirection();
            transform.position += new Vector3(GroceryBaggerSpeed * Time.deltaTime, 0, 0);
        }
        else if (PotentialX <= -7.2)
        {
            FlipBaggerDirection();
            transform.position += new Vector3(GroceryBaggerSpeed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.position += new Vector3(GroceryBaggerSpeed * Time.deltaTime, 0, 0);
        }
        

        /*if (Time.deltaTime * GroceryBaggerSpeed>0.04f)
            Debug.Log(Time.deltaTime*GroceryBaggerSpeed);*/
        


    }

    // Manages Dropping Items, Drop Waves, and random Bagger direction change
    void Drop()
    {
        int ItemNum = RandomGenerator.Next(0, NumPrefabs);
        
        if (ItemNum==0)
        {
            GameObject DroppedItem = Instantiate<GameObject>(ApplePrefab);
            DroppedItem.transform.position = transform.position;
        }
        else
        {
            GameObject DroppedItem = Instantiate<GameObject>(BananaPrefab);
            DroppedItem.transform.position = transform.position;
        }
        
        
        RemainingItemsInWave--;
        DroppedItems++;
        if (RemainingItemsInWave%3 == 2 && RandomGenerator.Next(0, BaggerDirectionChangeRandomScale) == 0)
        {
            FlipBaggerDirection();
        }
        if (RemainingItemsInWave == 0)
        {
            NewWave();
            Invoke("Drop", PauseBetweenDropWaves);
        }
        else
        {
            Invoke("Drop", DropIntervalRate);
        }
        //Invoke("Drop", 1.0f);
    }

    private void NewWave()
    {
        Wave++;
        WaveSize = 3 + Wave;
        RemainingItemsInWave = WaveSize;
        if (Wave <= 10)
        {
            DropIntervalRate = 1.2f - ((float)Wave) / 10.0f;
            BaggerDirectionChangeRandomScale = 12 - Wave;
        }
        else
        {
            DropIntervalRate = 0.2f;
            BaggerDirectionChangeRandomScale = 2;
        }

    }

    /*
    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.name == "Bounds")
            FlipBaggerDirection();
    }*/

    private void FlipBaggerDirection()
    {
        GroceryBaggerSpeed *= -1;
    }
}

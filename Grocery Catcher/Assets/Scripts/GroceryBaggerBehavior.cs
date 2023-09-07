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
    public bool InFrenzyMode = false;
    public bool ExitingFrenzyMode = false;
    private int RemainingFrenzyDrops = 0;
    public List<GameObject> FrenzyDroppedItems;
    public List<float> FrenzyDroppedItemsDeltaX;
    public GameObject MonkeySprite;
    public GameObject BrianSprite;
    // Start is called before the first frame update

    public GameObject StartStatement;
    public static AudioManager instance;
    public GameObject BananaPrefab;
    public GameObject ApplePrefab;
    public GameObject KnifePrefab;
    public GameObject DookiePrefab;
    public GameObject BeansPrefab;

    void Start()
    {
        DropIntervalRate = 1.5f;
        DroppedItems = 0;
        RemainingItemsInWave = 3;
        WaveSize = 3;
        Wave = 0;
        PauseBetweenDropWaves = 3.0f;
        StartStatement.SetActive(true);
        Invoke("Drop", PauseBetweenDropWaves);
        Invoke("HideStartStatement", 2.5f);
        InFrenzyMode = false;
        ExitingFrenzyMode = false;
    }

    void HideStartStatement()
    {
        StartStatement.SetActive(false);
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

        /*if (InFrenzyMode)
        {
            for (int i=0; i<FrenzyDroppedItems.Count; i++)
            {
                if (FrenzyDroppedItems[i] != null && !GameObject.Equals(FrenzyDroppedItems[i], null))
                {
                    FrenzyDroppedItems[i].transform.position += new Vector3(FrenzyDroppedItemsDeltaX[i] * Time.deltaTime, 0, 0);

                }
            }
        }*/
        

    }

    // Manages Dropping Items, Drop Waves, and random Bagger direction change
    void Drop()
    {
        if (!ExitingFrenzyMode)
        {
            int ItemNum = RandomGenerator.Next(0, 3);
            GameObject DroppedItem;
            if (ItemNum == 0)
            {
                ItemNum = RandomGenerator.Next(0, 2);
                if (ItemNum == 0)
                {
                    DroppedItem = Instantiate<GameObject>(KnifePrefab);
                }
                else
                {
                    DroppedItem = Instantiate<GameObject>(DookiePrefab);
                }

                DroppedItem.transform.position = transform.position;
            }
            else
            {
                ItemNum = RandomGenerator.Next(0, 3);
                if (ItemNum == 0)
                {
                    DroppedItem = Instantiate<GameObject>(BananaPrefab);
                }
                else if (ItemNum == 1)
                {
                    DroppedItem = Instantiate<GameObject>(BeansPrefab);
                }
                else
                {
                    DroppedItem = Instantiate<GameObject>(ApplePrefab);
                }
                DroppedItem.transform.position = transform.position;
            }
            DroppedItems++;
            RemainingItemsInWave--;
        }


        // Debug.Log("Items remaining: " + RemainingItemsInWave);

        if (RemainingItemsInWave%3 == 2 && RandomGenerator.Next(0, BaggerDirectionChangeRandomScale) == 0)
        {
            FlipBaggerDirection();
        }
        if (RemainingItemsInWave == 0)
        {
            if (Wave%5 == 0 && !InFrenzyMode && !ExitingFrenzyMode && Wave>0) {
                Invoke("FrenzyMode", PauseBetweenDropWaves);
            }
            else
            {
                DestroyFrenzyDroppedItems();
                ExitingFrenzyMode = false;
                InFrenzyMode = false;
                NewWave();
                Invoke("Drop", PauseBetweenDropWaves);
            }
            
        }
        else
        {
            Invoke("Drop", DropIntervalRate);
        }
        //Invoke("Drop", 1.0f);
    }

    private void FrenzyMode()
    {
        // Debug.Log("Freny Mode Started");
        MonkeySprite.GetComponent<SpriteRenderer>().enabled = false;
        BrianSprite.GetComponent<SpriteRenderer>().enabled = true;
        FindObjectOfType<AudioManager>().StopPlaying("Theme");
        FindObjectOfType<AudioManager>().Play("Frenzy Theme");
        InFrenzyMode = true;
        ExitingFrenzyMode = false;
        RemainingFrenzyDrops = 50;
        FrenzyDroppedItems = new List<GameObject>();
        FrenzyDroppedItemsDeltaX = new List<float>();
        FrenzyModeDrop();
        
    }

    private void DestroyFrenzyDroppedItems()
    {
        for (int i = 0; i < FrenzyDroppedItems.Count; i++)
        {
            if (FrenzyDroppedItems[i] != null && !GameObject.Equals(FrenzyDroppedItems[i], null))
            {
                Destroy(FrenzyDroppedItems[i]);
            }
        }
    }

    private void FrenzyModeDrop()
    {
        if (InFrenzyMode && RemainingFrenzyDrops>0)

        {
            int ItemNum = RandomGenerator.Next(0, 11);
            GameObject DroppedItem;
            if (ItemNum == 0)
            {
                ItemNum = RandomGenerator.Next(0, 2);
                if (ItemNum==0)
                {
                    DroppedItem = Instantiate<GameObject>(KnifePrefab);
                }
                else
                {
                    DroppedItem = Instantiate<GameObject>(DookiePrefab);
                }
                
                DroppedItem.transform.position = transform.position;
            }
            else
            {
                ItemNum = RandomGenerator.Next(0, 3);
                if (ItemNum == 0)
                {
                    DroppedItem = Instantiate<GameObject>(BananaPrefab);
                }
                else if (ItemNum == 1)
                {
                    DroppedItem = Instantiate<GameObject>(BeansPrefab);
                }
                else
                {
                    DroppedItem = Instantiate<GameObject>(ApplePrefab);
                }
                DroppedItem.transform.position = transform.position;
            }

            RemainingFrenzyDrops--;
            FrenzyDroppedItems.Add(DroppedItem);
            FrenzyDroppedItemsDeltaX.Add(5.0f);
            Rigidbody2D FrenzyItemRB = DroppedItem.GetComponent<Rigidbody2D>();
            FrenzyItemRB.velocity = new Vector2((10f*(float)RandomGenerator.NextDouble() - 5f), FrenzyItemRB.velocity.y);
            Invoke("FrenzyModeDrop", 0.1f);
        }
        else
        {
            // Debug.Log("Frenzy Mode Over");
            ExitingFrenzyMode = true;
            FindObjectOfType<AudioManager>().StopPlaying("Frenzy Theme");
            FindObjectOfType<AudioManager>().Play("Theme");
            MonkeySprite.GetComponent<SpriteRenderer>().enabled = true;
            BrianSprite.GetComponent<SpriteRenderer>().enabled = false;
            Invoke("Drop", 3.0f);
        }

        
    }

    private void NewWave()
    {
        Wave++;
        // Debug.Log("Wave: " + Wave);
        WaveSize = 3 + Wave;
        RemainingItemsInWave = WaveSize;
        //Debug.Log("Items remaining (New Wave):" + RemainingItemsInWave);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject Finalscore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FinalScene();

    }   


    public void FinalScene()
    {
        Finalscore.GetComponent<Text>().text = BasketBehavior.score.ToString();
        if (Input.GetMouseButton(0))
        {
            SceneManager.LoadScene(1);
        }
    }
}

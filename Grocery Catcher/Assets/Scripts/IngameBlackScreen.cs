using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameBlackScreen : MonoBehaviour
{
    public Image bks;
    public float fadeTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {


        bks.color = Color.black;
        bks.canvasRenderer.SetAlpha(1.0f);
        bks.CrossFadeAlpha(0f, fadeTime, false);
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

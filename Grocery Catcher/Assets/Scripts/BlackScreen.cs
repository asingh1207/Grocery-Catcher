using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BlackScreen : MonoBehaviour
{
    public Image bkscreen;

    public float fadeDuration = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeFromBlack()
    {
        bkscreen.color = Color.black;
        bkscreen.canvasRenderer.SetAlpha(0f);
        bkscreen.CrossFadeAlpha(1.0f, fadeDuration , false);
    }

    
}

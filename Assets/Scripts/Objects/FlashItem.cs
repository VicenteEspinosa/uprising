using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashItem : MonoBehaviour
{
    float timer = 0.0f;
    bool isWhite = true;
    [SerializeField]
    float timeWhite = 1.0f;
    [SerializeField]
    float timeGray = 0.4f;
    Color white = new Color(1, 1, 1, 1);
    Color gray = new Color(0.5f, 0.5f, 0.5f, 1);

    void Update()
    {
        timer += Time.deltaTime;
        if (isWhite && timer >= timeWhite)
        {
            isWhite = false;
            timer = 0.0f;
            setColor(gray);
        }
        else if (!isWhite && timer >= timeGray)
        {
            isWhite = true;
            timer = 0.0f;
            setColor(white);
        }
        // if (timer > timeToWait)
        // {
        //     timer = 0.0f;
        //     phase++;
        //     if (phase == 2)
        //     {
        //         phase = 0;
        //         setColor(white);
        //     }
        //     else if (phase == 1)
        //     {
        //         setColor(gray);
        //     }
        // }
    }
    public void setColor(Color color)
    {
        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
        mySprite.color = color;
    }
}



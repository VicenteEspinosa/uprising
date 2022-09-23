using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private Image content;
    [SerializeField]
    private int maximum = 100;
    [SerializeField]
    private float current = 0f;
    [SerializeField]
    private Text text;
    [SerializeField]
    private int timeToEnd = 3;
    // [SerializeField]
    // private bool showText = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        current += Time.deltaTime / timeToEnd * maximum;
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float fillAmount = (float)current / (float)maximum;
        content.fillAmount = fillAmount;
        text.text = ((int)(fillAmount * maximum)).ToString() + "%";
        if (fillAmount >= 1)
        {
            SelfDestroy();
        }
    }

    void SelfDestroy()
    {
        Destroy(gameObject);
        // Object.Destroy(this.gameObject)
    }
}

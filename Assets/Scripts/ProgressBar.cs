using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private Image content;
    [SerializeField]
    private int maximum = 100;
    [SerializeField]
    private int current = 0;
    [SerializeField]
    private Text text;
    // [SerializeField]
    // private bool showText = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float fillAmount = (float)current / (float)maximum;
        content.fillAmount = fillAmount;
        text.text = (fillAmount * maximum).ToString() + "%";
    }
}

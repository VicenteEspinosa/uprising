using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class HealthsManager : MonoBehaviour
{

    public TextMeshProUGUI DetectiveHealth_Text;
    public TextMeshProUGUI FireManHealth_Text;

    GameObject Firefighter;
    GameObject Detective;

    // Start is called before the first frame update
    void Start()
    {
        Firefighter = GameObject.FindGameObjectsWithTag("Firefighter")[0];
        Detective = GameObject.FindGameObjectsWithTag("Detective")[0];

    }

    // Update is called once per frame
    void Update()
    {
        if (Detective)
        {
            PlayerPrefs.SetFloat("DH", Mathf.Max((float)Variables.Object(Detective).Get("Current Health"), 0));
        }
        if (Firefighter)
        {
            PlayerPrefs.SetFloat("FH", Mathf.Max((float)Variables.Object(Firefighter).Get("Current Health"), 0));
        }

        var DH = PlayerPrefs.GetFloat("DH");
        var FH = PlayerPrefs.GetFloat("FH");

        DetectiveHealth_Text.text = "" + DH;
        FireManHealth_Text.text = "" + FH;
    }
}

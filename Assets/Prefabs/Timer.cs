using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float CurrentTime;
    public Text TimerText;

    public void Start()
    {
        CurrentTime = 0;    
    }

    void Update()
    {
        CurrentTime += Time.deltaTime;
        TimerText.text = Mathf.Round(CurrentTime/60) + " : " + Mathf.Round(CurrentTime%59);
    }
}

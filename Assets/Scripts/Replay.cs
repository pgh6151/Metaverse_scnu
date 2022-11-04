using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        GameObject Char = GameObject.Find("Player(Clone)");
        GameObject End = GameObject.Find("Arrival");
        GameObject Btn = GameObject.Find("Button");

        if (sceneName == "MazeScene")
        {
            
            Char.transform.position = new Vector3(0.0f, 0.1f, 0.0f);
            GameObject.Find("Quad").GetComponent<Timer>().CurrentTime = 0;
            End.SetActive(false);
            Btn.SetActive(false);
            
            Time.timeScale = 1;
        }

        if (sceneName == "CinemachineScene")
        {
            End.SetActive(false);
            Btn.SetActive(false);
            Time.timeScale = 1;
            //SceneManager.LoadScene(sceneName);
            Btn.GetComponent<MIniGamemanager>().Exit_BTN();

        }
    }
}

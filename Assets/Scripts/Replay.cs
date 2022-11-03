using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        GameObject Char = GameObject.Find("Character");
        GameObject End = GameObject.Find("Arrival");
        GameObject Btn = GameObject.Find("Button");

        if (sceneName == "MazeScene")
        {
            //캐릭터 시작 위치 이동
            Char.transform.position = new Vector3(0.0f, 0.1f, 0.0f);
            GameObject.Find("Quad").GetComponent<Timer>().CurrentTime = 0;
            End.SetActive(false);
            Btn.SetActive(false);
            //씬 일시정지 해제
            Time.timeScale = 1;
        }

        if (sceneName == "CinemachineScene")
        {
            End.SetActive(false);
            Btn.SetActive(false);
            SceneManager.LoadScene(sceneName);
            Time.timeScale = 1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        //씬 새로고침
        SceneManager.LoadScene(sceneName);
        //씬 일시정지 해제
        Time.timeScale = 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        //�� ���ΰ�ħ
        SceneManager.LoadScene(sceneName);
        //�� �Ͻ����� ����
        Time.timeScale = 1;
    }
}

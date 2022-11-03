using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    private bool state;
    public GameObject EndScreen;
    public GameObject Button;

    void Start()
    {
        state = false;
    }

    //OnCollisionEnte(충돌 체크)
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Finish"))
        {
            //도착 라인 통과 시 엔딩 화면 활성화
            EndScreen.SetActive(true);
            Debug.Log("도착");
            //씬 일시정지
            Time.timeScale = 0;
            //다시시작 버튼 활성화
            Button.SetActive(true);
        }
    }
}
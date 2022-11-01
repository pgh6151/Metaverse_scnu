using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Col : MonoBehaviour
{
    private bool state;
    public GameObject ColScreen;

    void Start() {
        state = false;
    }

    //벽 충돌시 화면 색 변경 후 씬 새로고침
    //OnCollisionEnte(충돌 체크)
    void OnCollisionEnter(Collision collision)    {
        if (collision.collider.gameObject.CompareTag("Wall"))
        {
            ColScreen.SetActive(true);
            SceneManager.LoadScene("MazeScene");
        }
    }
}

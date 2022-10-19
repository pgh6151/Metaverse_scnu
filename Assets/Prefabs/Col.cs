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

// 벽 충돌시 화면 색 변경 후 씬 새로고침(씬 인덱스 변경)
    void OnCollisionEnter(Collision collision)    {
        if (collision.collider.gameObject.CompareTag("Wall"))
        {
            ColScreen.SetActive(true);
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene("MazeScene");
        }
    }
}

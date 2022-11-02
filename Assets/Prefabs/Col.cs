using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Col : MonoBehaviour
{
    private bool state;
    public GameObject ColScreen;
    GameObject Char;
    GameObject Lever;

    void Start() {
        state = false;
        
        Char = GameObject.Find("Character");
        Lever = GameObject.Find("Lever");
    }

    //벽 충돌시 화면 색 변경 후 씬 새로고침
    //OnCollisionEnte(충돌 체크)
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Wall"))
        {
            Char.transform.position = new Vector3(0, 0, 0);
            ColScreen.SetActive(true);
            Time.timeScale = 0;

            Char.GetComponent<Timer>().CurrentTime = 0;
            Lever.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        ColScreen.SetActive(false);
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1;
    }

}

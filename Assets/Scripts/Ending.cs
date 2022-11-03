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

    //OnCollisionEnte(�浹 üũ)
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Finish"))
        {
            //���� ���� ��� �� ���� ȭ�� Ȱ��ȭ
            EndScreen.SetActive(true);
            Debug.Log("����");
            //�� �Ͻ�����
            Time.timeScale = 0;
            //�ٽý��� ��ư Ȱ��ȭ
            Button.SetActive(true);
        }
    }
}
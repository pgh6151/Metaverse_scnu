using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MIniGamemanager : MonoBehaviour
{
    // 기본적인 변수 전달 싱글톤으로 진행
    private static MIniGamemanager instance = null;


    public GameObject StartCanv;
    public GameObject ReStartCanv;

    public bool ST;
    public bool RST;

    private void Awake() {

         if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static MIniGamemanager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ST = false;
        RST = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ST_BTN()
    {
        StartCanv.SetActive(false);
        ST = true;
    }
    public void RST_BTN()
    {
        SceneManager.LoadScene("Minigame1");
        StartCanv.SetActive(false);
    }

    public void Exit_BTN()
    {
        SceneManager.LoadScene("CinemachineScene");
    }


}

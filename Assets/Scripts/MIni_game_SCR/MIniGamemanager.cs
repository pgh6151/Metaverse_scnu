using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MIniGamemanager : MonoBehaviour
{

    // 기본적인 변수 전달 싱글톤으로 진행
    private static MIniGamemanager instance = null;

    // 스톱워치
    [SerializeField] float timeStart;
    [SerializeField] Text timeText;

    bool timeActive = false;


    public GameObject StartCanv;
    public GameObject ReStartCanv;

    public GameObject player;

    public bool ST;
    public bool RST;
    public int MoveVec;


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

        timeText.text = timeStart.ToString("F2");
        
    
        ST = false;
        RST = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeActive = ST;
        

        StartTime();
        
        
    }

    public void ST_BTN()
    {
        timeStart = 0f;
        StartCanv.SetActive(false);
        Debug.Log("작동");
        ST = true;
    }
    public void RST_BTN()
    {
        ST = true;
        timeStart = 0f;
        Instantiate(player,new Vector3(0,0,0), gameObject.transform.rotation);
        ReStartCanv.SetActive(false);
    }

    public void Exit_BTN()
    {
        SceneManager.LoadScene("CinemachineScene");
        // DestroyImmediate(this.gameObject, true);
    }

    public void StartTime()
    {
        if(timeActive)
        {
            timeStart += Time.deltaTime;
            timeText.text = timeStart.ToString("F2");
        }
        
    }   
    public void leftBtn()
    {
        MoveVec = -1;
    }
    
    public void rightBtn()
    {
        MoveVec = 1;

    }

    
}

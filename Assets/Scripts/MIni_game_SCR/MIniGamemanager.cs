using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MIniGamemanager : MonoBehaviour
{

    // 스톱워치
    [SerializeField] float timeStart;
    [SerializeField] Text timeText;
    [SerializeField] GameObject startBtn;
    [SerializeField] GameObject exitBtn;
    
    bool timeActive = false;

    
    public GameObject StartCanv;
    public GameObject ReStartCanv;

    //미니게임 프리펩, 변수 (spawner)
    public GameObject Rock;
    Vector3 RanSpawner;
    int routineControll = 1;

    //공용변수 
    public bool ST;
    public bool RST;
    public Vector3 MoveVec = new Vector3 (0,0,0);


    private void Awake() 
    {
        

    }

    // Start is called before the first frame update
    void Start()
    {   
        timeText.text = timeStart.ToString("F2");
        
        ST = false;
        RST = false;

        if (PhotonNetwork.IsMasterClient)
        {
            startBtn.SetActive(true);
            exitBtn.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //미니게임 시간제어
        timeActive = ST;
        StartTime();

        
        RanSpawner = new Vector3(Random.Range(-7, 4), 1.18f, 44.93f);

        if(ST && routineControll == 1)
        {
            StartCoroutine("InstRock");
            routineControll--;
        }else if (ST == false)
        {
            StopCoroutine("InstRock");
            routineControll = 1;
        }
        
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
        ReStartCanv.SetActive(false);
    }

    public void Exit_BTN()
    {
        if (PhotonNetwork.IsMasterClient)
            CoroutineHandler.Instance.StartCoroutine(NetworkManager.Instance.SceneSync("CinemachineScene"));
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
        Debug.Log("왼쪽");
        MoveVec = new Vector3(-1,0,0);
    }
    
    public void rightBtn()
    {
        Debug.Log("오른쪽");
        MoveVec = new Vector3(1,0,0);

    }

    IEnumerator InstRock()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            PhotonNetwork.Instantiate("rock", RanSpawner, gameObject.transform.rotation);
        }
    }

    public void RockTrigger()
    {
        if (PhotonNetwork.IsMasterClient)
            ReStartCanv.SetActive(true);
        ST = false;
    }
    
}

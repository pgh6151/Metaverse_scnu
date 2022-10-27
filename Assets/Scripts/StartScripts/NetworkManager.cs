using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Text StatusText;
    public InputField NickNameInput;
    [SerializeField] GameObject playerPrefab;
    
    
    Scene scene;
    GameObject _player;
    
    #region
    private void Awake() 
    {
        //화면비율 조정필요
        Screen.SetResolution(1440, 3200, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
        
        DontDestroyOnLoad(gameObject);
        scene = SceneManager.GetActiveScene();


    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected) PhotonNetwork.Disconnect();


        if(scene.name == "StartScene")
        {
            StatusText.text = PhotonNetwork.NetworkClientState.ToString();
        } 

    } 

    public  void Connect() => PhotonNetwork.ConnectUsingSettings();

    // OnConnectedToMaster
    // PhotonNetwork.autoJoinLobby 이 false인 경우에만 마스터 서버에 연결되고 인증 후에 호출

    public override void OnConnectedToMaster()
    {
        print("서버접속");
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        
        CoroutineHandler.Instance.StartCoroutine(LoadLevelAsync("CinemachineScene"));
        //씬넘기기
        PhotonNetwork.JoinOrCreateRoom("CinemachineRoom", new RoomOptions {MaxPlayers = 10}, null);
        Debug.Log("OnConnectedToMaster");
    }

    public void DisConnect() => PhotonNetwork.Disconnect();

    // 나가기 버튼 누르면 어플리케이션이 나가지도록
    public override void OnDisconnected(DisconnectCause cause) 
    {
        PhotonNetwork.LoadLevel(0);
        print("연결끊김");
    }

    public void QuitApp()
    {
        Application.Quit();
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("JoinRoom");
        OnLevelLoaded();

    }
    
    
    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");
    }
    

    #endregion
    
    // 씬이동시 무조건 이거사용
    public void moveScene_gunha()
    {
        Debug.Log("moveScene_gunha");
    }

    public void MoveToBeach()
    {
        Debug.Log("MoveToBeach");
        CoroutineHandler.Instance.StartCoroutine(LoadLevelAsync("DemoScene"));
    }
    
    

    IEnumerator LoadLevelAsync(string sceneName)
    {
        if (_player)
        {
            PhotonNetwork.Destroy(_player);
            Debug.Log("Destroy");
        }
        
        PhotonNetwork.IsMessageQueueRunning = false;
        PhotonNetwork.LoadLevel(sceneName);
       
        while (SceneManager.GetActiveScene().name != sceneName)
        {
            Debug.Log(PhotonNetwork.LevelLoadingProgress);
            yield return null;
        }     
        PhotonNetwork.IsMessageQueueRunning = true;

        if (PhotonNetwork.InRoom && !_player && SceneManager.GetActiveScene().name == sceneName)
        {
            _player = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
            Debug.Log("Instantiate");
        }
        else
        {
            Debug.Log($"PhotonNetwork.InRoom : {PhotonNetwork.InRoom} !_player : {!_player} SceneManager.GetActiveScene().name == sceneName {SceneManager.GetActiveScene().name == sceneName} {SceneManager.GetActiveScene().name}");
            
        }
    }

    void OnLevelLoaded()
    {
        if (!_player)
            _player = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }
}
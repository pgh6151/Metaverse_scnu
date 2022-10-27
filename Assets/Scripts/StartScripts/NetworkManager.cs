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
    [SerializeField] InputField NickNameInput;

    PhotonView PV;
    Scene scene;
    
    
    #region
    private void Awake() 
    {
        
        DontDestroyOnLoad(gameObject);
        
        //화면비율과 가로고정
        Screen.SetResolution(3200, 1440, true);
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;

        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
        PhotonNetwork.AutomaticallySyncScene = true;
        
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
        
        //씬넘기기
        SceneManager.LoadScene("CinemachineScene");
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions {MaxPlayers = 10}, null);
    }

    public void DisConnect() => PhotonNetwork.Disconnect();

    // 나가기 버튼 누르면 어플리케이션이 나가지도록
    public override void OnDisconnected(DisconnectCause cause) 
    {
        
        PhotonNetwork.LeaveRoom();
        Destroy(gameObject);
        print("연결끊김");
       
    }

    public void QuitApp()
    {
        Application.Quit();
    }
    public override void OnJoinedRoom()
    {
        Sqawn();
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }


    #endregion
    
    // 씬이동시 무조건 이거사용
    public void moveScene_gunha()
    {
        
        SceneManager.LoadScene("Minigame1");
        
    }
    public void ARcam_ON()
    {
        SceneManager.LoadScene("ARScene");
    }

    public void Sqawn()
    {
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }


    
    
}
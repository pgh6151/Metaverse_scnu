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
    Scene scene;
    
    #region
    private void Awake() 
    {
        //화면비율 조정필요
        Screen.SetResolution(1280, 780, false);
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
        
        //가볍게 씬넘기기
        SceneManager.LoadScene("CinemachineScene");
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions {MaxPlayers = 10}, null);
    }

    public void DisConnect() => PhotonNetwork.Disconnect();

    // 나가기 버튼 누르면 어플리케이션이 나가지도록
    public override void OnDisconnected(DisconnectCause cause) 
    {
        
        SceneManager.LoadScene("StartScene");
        print("연결끊김");
       
    }
    public override void OnJoinedRoom()
    {
        Sqawn();
    }

    #endregion
    
    public void Sqawn()
    {
        PhotonNetwork.Instantiate("Character", new Vector3 (Random.Range(-5,5),0,0), Quaternion.identity);
    }




}
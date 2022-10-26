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
        
        //씬넘기기
        PhotonNetwork.LoadLevel(1);
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions {MaxPlayers = 10}, null);
        
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
        PhotonNetwork.Instantiate(playerPrefab.name, playerPrefab.transform.position, Quaternion.identity);
    }
    
    
    // public override void OnLeftRoom()
    // {
    //     PhotonNetwork.LoadLevel(PhotonNetwork.CurrentRoom.Name);
    // }
    

    #endregion
    
    // 씬이동시 무조건 이거사용
    public void moveScene_gunha()
    {
        PhotonNetwork.LoadLevel("Minigame1");
    }

    public void MoveToBeach()
    {
        PhotonNetwork.IsMessageQueueRunning = false;
        PhotonNetwork.LoadLevel(3);
        photonView.RPC("UpdatePlayer", RpcTarget.All);
    }

    [PunRPC]
    public void UpdatePlayer()
    {
        if (!photonView.IsMine)
            Destroy(gameObject);
    }

}
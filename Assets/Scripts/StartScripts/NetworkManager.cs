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
    
    Scene scene;

    // Photon Networking 유튜브 영상 출처 (이거아님)
    private List<PlayerListing> _listings = new List<PlayerListing>();
    private PlayerListing _playerListing;
    
    
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
        
        //씬 자동동기화, 핑조절
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
        PhotonNetwork.AutomaticallySyncScene = true;
        
        scene = SceneManager.GetActiveScene();


    }

    // Photon Networking 유튜브 영상 출처
    // private void GetCurrentRoomPlayers()
    // {
    //     if(!PhotonNetwork.IsConnected)
    //         return;
    //     if(PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
    //         return;
        
    //     foreach(KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
    //     {
    //         AddPlayerListing(playerInfo.Value);
    //     }
    // }

    // Photon Networking 유튜브 영상 출처

    // private void AddPlayerListing(Player player)
    // {
    //     PlayerListing listing = Instantiate(_playerListing);
    //     if(listing != null)
    //     {
    //         listing.SetPlayerInfo(player);
    //         _listings.Add(listing);
    //     }
    // }

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
        print(PhotonNetwork.LocalPlayer.NickName);

        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;

        // PhotonNetwork.JoinLobby();
        //씬넘기기
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("CinemachineScene");
        }
        
        PhotonNetwork.JoinOrCreateRoom("schoolRoom", new RoomOptions {MaxPlayers = 10}, null);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

    }
    
    public override void OnCreatedRoom()
    {
        print("방만들기 성공!");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("방만들기 실패");
    }

    public void DisConnect() => PhotonNetwork.Disconnect();


    // 나가기 버튼 누르면 어플리케이션이 나가지도록
    public override void OnDisconnected(DisconnectCause cause) 
    {
        
        PhotonNetwork.LeaveRoom();
        Destroy(gameObject);

        SceneManager.LoadScene(0);
        print("연결끊김");
       
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    // public override void OnJoinedLobby()
    // {
    //     SceneManager.LoadScene("CinemachineScene");
    //     PhotonNetwork.InstantiateRoomObject("Player", Vector3.zero, Quaternion.identity);
    // }
    
    public override void OnJoinedRoom()
    {
        //(볼것) 방마다 스폰 새로할수 있도록
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);

    }
    public override void OnLeftRoom()
    {
        
    }

    #endregion

    public void LeaveRoom() => PhotonNetwork.LeaveRoom();
    
    // 씬이동시 이거사용 (볼것)
    public void moveScene_gunha()
    {  
        SceneManager.LoadScene(2);
        
    }
    
    // Photon Networking 유튜브 영상 출처
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listings.FindIndex(x => x.Player == otherPlayer);
        if( index != -1)
        {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }

    


    public void ARcam_ON()
    {
        SceneManager.LoadScene("ARScene");
        
    }

    // public void Sqawn()
    // {   
    //      if (TPSCharacterController.LocalPlayerInstance==null)
    //      {
    //          PhotonNetwork.InstantiateRoomObject("Player", Vector3.zero, Quaternion.identity);
    //      }
    // }


    
    
}
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
    
    public PhotonView PV;
    Scene scene;

    // Photon Networking 유튜브 영상 출처
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
    private void GetCurrentRoomPlayers()
    {
        if(!PhotonNetwork.IsConnected)
            return;
        if(PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
            return;
        
        foreach(KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
    }

    // Photon Networking 유튜브 영상 출처

    private void AddPlayerListing(Player player)
    {
        PlayerListing listing = Instantiate(_playerListing);
        if(listing != null)
        {
            listing.SetPlayerInfo(player);
            _listings.Add(listing);
        }
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
        print(PhotonNetwork.LocalPlayer.NickName);

        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        
        //씬넘기기
        SceneManager.LoadScene("CinemachineScene");
        PhotonNetwork.JoinOrCreateRoom("StartLobby", new RoomOptions {MaxPlayers = 10}, TypedLobby.Default);
        
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
        print("연결끊김");
       
    }

    public void QuitApp()
    {
        Application.Quit();
    }
    public override void OnJoinedRoom()
    {
        
        PhotonNetwork.InstantiateRoomObject("Player", Vector3.zero, Quaternion.identity);
        
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
        PhotonNetwork.CreateRoom("MinigameRoom", new RoomOptions {MaxPlayers = 10}, TypedLobby.Default);
        PhotonNetwork.JoinRoom("GameRoom");
        // PhotonNetwork.InstantiateRoomObject("Player", Vector3.zero, Quaternion.identity);
        
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

    public void OnPhotonInstantiate()
    {

    }
    


    public void ARcam_ON()
    {
        SceneManager.LoadScene("ARScene");
        
    }

    public void Sqawn()
    {   
        // if (TPSCharacterController.LocalPlayerInstance==null)
        // {
        //     PhotonNetwork.InstantiateRoomObject("Player", Vector3.zero, Quaternion.identity);
        // }
    }


    
    
}
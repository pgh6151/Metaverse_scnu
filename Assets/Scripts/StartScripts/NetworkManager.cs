using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks
{
    static NetworkManager _instance;
    static List<PhotonView> photonViewsToDestroy = new List<PhotonView>();

    public static NetworkManager Instance
    {
        get
        {
            if (!_instance)
            {
                GameObject go = new GameObject();
                _instance = go.AddComponent<NetworkManager>();
            }
            return _instance;
        }
    }
    
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
        _instance = this;
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
        CoroutineHandler.Instance.StartCoroutine(LoadLevelAsync("CinemachineScene"));
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
        PhotonNetwork.IsMessageQueueRunning = false;
        PhotonNetwork.LoadLevel(sceneName);
       
        while (SceneManager.GetActiveScene().name != sceneName)
        {
            Debug.Log(PhotonNetwork.LevelLoadingProgress);
            yield return null;
        }     
        PhotonNetwork.IsMessageQueueRunning = true;
        if (_player)
            DestroySceneObject(_player.GetPhotonView());
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

    
    
    public static void DestroySceneObject(PhotonView photonView)
    {
        if (PhotonNetwork.IsConnected)
        {
            if (photonView.isRuntimeInstantiated) // instantiated at runtime
            {
                if (photonView.IsMine)
                {
                    PhotonNetwork.Destroy(photonView);
                }
                else
                {
                    photonView.RequestOwnership();
                    photonViewsToDestroy.Add(photonView);
                }
            }
            else // scene view loaded in the scene
            {
                photonView.RPC("LocalSelfDestroy", RpcTarget.AllBuffered);
                //otherPhotonView.RPC("LocalDestroy", RpcTarget.AllBuffered, photonView.ViewID); // another option
            }
        }
        else
        {
            Destroy(photonView); // photonView.LocalSelfDestroy();
        }
    }

    [PunRPC]
    void LocalSelfDestroy()
    {
        Destroy(photonView);
    }


    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
    {
        
    }
}
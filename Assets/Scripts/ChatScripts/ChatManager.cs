using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ChatManager : MonoBehaviour, IChatClientListener {

	private ChatClient chatClient;
	private string userName;
	private string currentChannelName;

	public InputField inputField;
	public Text outputText;
	public GameObject Chatting;
	public GameObject ChattingONBtn;
	public Scrollbar Scrollbar;

	// Use this for initialization
	void Start () {

		Application.runInBackground = true;

		userName = PhotonNetwork.LocalPlayer.NickName;
		currentChannelName = "Channel 001";

		//ChatClinet 클래스를 이용해 인스턴스 생성 (인스턴스 생성이 되면 접속 가능)
		chatClient = new ChatClient(this);
		chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, "1.0", new AuthenticationValues(userName));

		// chatClient.SendPrivateMessage("Arthur", "2*3*7"); 비밀 메세지 전송
		AddLine(string.Format("연결시도", userName));
	}
	public void ChatQuit()
	{
		Chatting.SetActive(false);
		ChattingONBtn.SetActive(true);
	}
	public void ChatON()
	{
		Chatting.SetActive(true);
		ChattingONBtn.SetActive(false);
	}

	//화면에 챗팅 송출
	public void AddLine(string lineString)
	{
		outputText.text += lineString + "\r\n";
	}

	#region 
	public void OnApplicationQuit ()
	{
		if (chatClient != null)
		{
			chatClient.Disconnect();
		}
	}
	// 채팅 시스템의 모든 정보와 로그를 얻을 수 있도록 API 콜백을 적절히 구현해야함
	public void DebugReturn (ExitGames.Client.Photon.DebugLevel level, string message)
	{
		if (level == ExitGames.Client.Photon.DebugLevel.ERROR)
		{
			Debug.LogError(message);
		}
		else if (level == ExitGames.Client.Photon.DebugLevel.WARNING)
		{
			Debug.LogWarning(message);
		}
		else
		{
			Debug.Log(message);
		}
	}
	//연결시 명령
	public void OnConnected ()
	{
		AddLine ("서버에 연결되었습니다.");
		AddLine (userName + "님이 입장하셨습니다");

		chatClient.Subscribe(new string[]{currentChannelName}, 10);
	}
	// Disconnect 명령
	public void OnDisconnected ()
	{
		AddLine ("서버에 연결이 끊어졌습니다.");
	}
	//챗팅 연결상황 콘솔에 디버그
	public void OnChatStateChange (ChatState state)
	{
		Debug.Log("OnChatStateChange = " + state);
	}
	// 채널명 출력 (참여한 채널명)
	public void OnSubscribed(string[] channels, bool[] results)
	{
		AddLine (string.Format("채널 입장 ({0})", string.Join(",",channels)));
	}

	public void OnUnsubscribed(string[] channels)
	{
		// chatClient.Subscribe(new string[]{"channelA", "channelB"}); 채널을 구독하는 사람은 
		// 모두 채널 내 공개된 모든 메시지들을 받음 첫번째 구독인 경우 == 새로운 채널이 생성
		AddLine (string.Format("채널 퇴장 ({0})", string.Join(",",channels)));
	}
	// 클라이언트의 메세지 받아오기 (IChatClientListener)
	public void OnGetMessages(string channelName, string[] senders, object[] messages)
	{
		for (int i = 0; i < messages.Length; i++)
		{
			AddLine (string.Format("{0} : {1}", senders[i], messages[i].ToString()));
		}
		Scrollbar.value = 0;
	}

	public void OnPrivateMessage(string sender, object message, string channelName)
	{
		Debug.Log("OnPrivateMessage : " + message);
	}

	public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
	{
		Debug.Log("status : " + string.Format("{0} is {1}, Msg : {2} ", user, status, message)); 
	}
	public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }
	#endregion


	void Update ()
	{
		//지속적으로 호출하여 연결을 유지하고 수신 메세지를 받음 (필수 구현)
		chatClient.Service();
	}

	public void Input_OnEndEdit (string text)
	{
		if (chatClient.State == ChatState.ConnectedToFrontEnd)
		{
			//chatClient.PublishMessage(currentChannelName, text);
			chatClient.PublishMessage(currentChannelName, inputField.text);

			inputField.text = "";
		}
	}

	public void UpdateChat()
	{
		if(inputField.text.Equals("")) return;

		chatClient.PublishMessage(currentChannelName, inputField.text);

		inputField.text = "";
		
	}



}

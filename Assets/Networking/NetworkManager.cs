using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    string gameVersion = "1";
    public Text statusText;
    public string nick, roomname;
    public string connect_status;
    public GameObject playerPrefab; // 플레이어 프리팹
    //public Transform spawnPoint; // 플레이어가 생성될 위치


    private void Awake()
    {
        print("awake");
        PhotonNetwork.AutomaticallySyncScene = true;
        //스터 클라이언트와 일반 클라이언트들이 레벨을 동기화할지 결정한다.
        //true로 설정하면 마스터 클라에서 LoadLevel()로 레벨을 변경하면 모든 클라이언트들이 자동으로 동일한 레벨을 로드.


    }

    // Start is called before the first frame update
    void Start()
    {
        print("start");
        Connect();
    }

    // Update is called once per frame
    void Update()
    {

        statusText.text = PhotonNetwork.NetworkClientState.ToString();
    }

    public void Connect()
    {

        if (PhotonNetwork.IsConnected)
        {
            print("PhotonNetwork.IsConnected");
        }
        else
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings(); //Photon Online Server에 접속하기 가장 중요
        }


    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    public void JoinLobby()
    {
        PhotonNetwork.JoinLobby();
    }

    public void CreateRoom()
    {
        // 방 생성하고, 참가.
        // 방 이름, 최대 플레이어 수, 비공개 등을 지정 가능.
        roomname = "room1";
        PhotonNetwork.CreateRoom(roomname, new RoomOptions { MaxPlayers = 6 });
    }

    public void JoinRoom()
    {
        // 방 참가하기.
        // 방 이름으로 입장 가능.
        PhotonNetwork.JoinRoom("room1");
    }

    public void JoinOrCreateRoom()
    {
        // 방 참가하는데, 방이 없으면 생성하고 참가.
        PhotonNetwork.JoinOrCreateRoom("room1", new RoomOptions { MaxPlayers = 6 }, null);
    }

    //콜백함수들 

    //포톤 온라인 서버에 접속하면 불리는 콜백함수
    //PhotonNetwork.ConnectUsingSettings()가 성공하면 호출됨
    public override void OnConnectedToMaster()
    {
        print("서버 접속 완료.");

        // 현재 플레이어 닉네임 설정.
        nick = "tmp";
        PhotonNetwork.LocalPlayer.NickName = nick;
        //JoinOrCreateRoom();
        JoinLobby();

    }

    //연결이 끊기면 불리는 콜백함수
    //PhotonNetwork.Disconnect()가 성공하면 불린다.
    public override void OnDisconnected(DisconnectCause cause)
    {
        print("연결 끊김 누가? ->" + nick);
    }

    //로비에 접속하면 불리는 콜백함수
    //PhotonNetwork.JoinLobby()가 성공하면 호출됨
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        {
            print("로비접속완료 누가? ->" + nick);
        }
    }
    // 방 생성하면 불리는 콜백 함수.
    // PhotonNetwork.CreateRoom()가 성공하면 불린다.
    public override void OnCreatedRoom()
    {
        print("방 만들기 완료 누가? ->" + nick);
        print("방 이름은 ? ->" + roomname);
    }

    /// 방 참가하면 불리는 콜백 함수.
    /// PhotonNetwork.CreateRoom(), PhotonNetwork.JoinedRoom()가 성공하면 불린다.
    public override void OnJoinedRoom()
    {
        print("방 참가완료 누가? ->" + nick);
        print("방 이름은 ? ->" + roomname);

        PhotonNetwork.Instantiate(playerPrefab.name, Vector2.zero, Quaternion.identity);
    }

    //방생성 실패시 불리는 콜백함수
    //PhotonNetwork.CreateRoom()를 호출할때 이름이 같은 방이있ㅎ으면 실패할수있다.
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("방만들기 실패 " + message);
    }

    //방참가 실패시 호출되는 콜백함수
    //PhotonNetwork.JoinRoom()할때 방인원수가 꽉차있으면 실패,아래함수가 호출될수있다.
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print("방 참가 실패.");
    }

    /// 방 랜덤 참가 실패하면 불리는 콜백 함수.
    /// PhotonNetwork.JoinRandomRoom()를 호출할 때, 방 인원수가 모두 차있거나 존재하지 않으면 실패할 수 있다.
    /// 다른 사람이 더 빠르게 들어갔거나, 방을 닫았을 수 있다.

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("방 랜덤 참가 실패.");
    }
}
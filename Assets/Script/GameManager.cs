using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SceneLoadManager;

public class GameManager : MonoBehaviour
{
    //싱글턴 처리
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TestData();
        initPlayerNumber(); //인원파악용 딕셔너리세팅
    }

    //테스트용 서버(2번째: 박물관)
    void TestData()
    {
        //HostedServer.Add(2, "11111111"); //임시로 만듦 수정 필요
        //EnterPlayerNumber.Add(2, 0);
    }

    //-------------------------------------------------------------------------------플레이어 데이터
    public string playerId { get; set; }
    public string playerPassword { get; set; }
    public bool isFristAccount { get; set; }

    //포톤 유저 데이터 처리는 여기서
    public bool LogInPlayer()
    {
        bool isCorrect = true;
        //플레이어 아이디, 비밀번호 매칭하는 함수
        PrintPlayerData();
        return isCorrect;
    }
    public bool SignUpPlayer()//플레이어 아이디, 비밀번호 DB에 보내는 함수
    {
        bool isSuccessful = false;
        //아이디 겹치지않게 잘 등록되었는지 보냄
        return isSuccessful;

    }
    void PrintPlayerData()
    {
        Debug.Log(playerId);
        Debug.Log(playerPassword);
    }
    //--------------------------------------------------------------------------------------------서버 연결 유무
    //딕셔너리로 보관 키는 서버위치 / 값은 서버주소?(무튼 호스트 할 때 뜨는 값)
    Dictionary<int, string> HostedServer = new Dictionary<int, string>();
    //서버 위치는 1~5까지 int형으로 표현
    /* 
     1 = 마켓
    2 = 박물관
    3 = 일반 건물
    4 = 사무실
    5 = 호텔
     */

    const int maxPlayerNum = 5;
    //서버 입장 제한
    Dictionary<int, int> EnterPlayerNumber = new Dictionary<int, int>();

    void initPlayerNumber()
    {
        for(int i = 1; i <6; i++)
        {
            EnterPlayerNumber.Add(i, 0);

        }
    }

    public int CountPlusPlayer(int _serverLocation)
    {
        //포톤으로 플레이어 구분 필요
        int playerCount = 0;
        if (EnterPlayerNumber.ContainsKey(_serverLocation))
        {

            if (playerCount < maxPlayerNum) //데이터가 없다면 추가
            {

                Debug.Log(EnterPlayerNumber[_serverLocation]);
                EnterPlayerNumber[_serverLocation] += 1;
                return EnterPlayerNumber[_serverLocation];
            }
        }
        else //없다면
        {
            playerCount++;
            EnterPlayerNumber.Add(playerCount, _serverLocation);
            return EnterPlayerNumber[_serverLocation];

        }
        return playerCount;
    }

    public int CountSubPlayer(int _serverLocation)
    {
        //포톤으로 플레이어 구분 필요

        if (EnterPlayerNumber.ContainsKey(_serverLocation))
        {
            if(EnterPlayerNumber[_serverLocation] != 0)
            {
                return EnterPlayerNumber[_serverLocation] -= 1;

            }
        }
            return 0; //최대인원
    }

    public bool CheckingHostServer( int _serverLocation)
    {
        bool isHost = HostedServer.ContainsKey(_serverLocation);
        Debug.Log(_serverLocation + "번째 서버 호스팅 상태: " + isHost);

        return isHost;
    }

    public void CreatServer(int _serverLocation)
    {
        //포톤 서버 여는 코드 작성
        NextScene = SceneSet.server;
        //서버 호스팅 정보 저장
        string _serverNumber = null; //값 넣어줘야함
        HostedServer.Add(_serverLocation, _serverNumber);
        //서버 입장하는 코드 작성
    }

    public void EnterServer(int _serverLocation)
    {

        //서버 입장하는 코드 작성 딕셔너리로 정보 가져올 것
        NextScene = SceneSet.server;

    }

    public string GetServerName(int _serverLocation)
    {
        string serverName = "none_Tilte";

        if (HostedServer.ContainsKey(_serverLocation))
        {
            //서버 이름 가져오기
        }
        //포톤 DB에서 데이터 가져옴
        return serverName;
    }

    public void EndHosting(int _serverLocation)
    {
        //해당 서버 호스팅 종료
        HostedServer.Remove(_serverLocation);
        EnterPlayerNumber.Remove(_serverLocation);
    }

    //--------------------------------------------------------------------------------------------씬 이동
    [SerializeField] SceneLoadManager sceneManager; //씬 넘어갈때마다  상태전환 필수

    SceneSet nextScene;
    public SceneSet NextScene
    {
        get => nextScene;
        set
        {
            switch(value)
            {
                case SceneSet.start:
                    StartCoroutine(sceneManager.LoadScene(value));
                    break;
                case SceneSet.lobby:
                    StartCoroutine(sceneManager.LoadScene(value));
                    break;
                case SceneSet.server:
                    StartCoroutine(sceneManager.LoadScene(value));
                    break;
            }
        }
    }

}

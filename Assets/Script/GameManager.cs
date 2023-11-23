using MoreMountains.Tools;
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

    //-------------------------------------------------------------------------------플레이어 데이터
    public string playerId { get; set; }
    public string playerPassword { get; set; }
    public bool isFristAccount { get; set; }

    //포톤 서버 연결은 여기서
    public bool LogInPlayer()
    {
        bool isCorrect = true; //테스트용 true, false로 변경 필요
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
    //--------------------------------------------------------------------------------------------씬 이동
    [SerializeField] SceneLoadManager sceneManager;

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

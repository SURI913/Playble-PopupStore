using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenScene : MonoBehaviour
{
    [SerializeField] string SceneName;

    private bool isOpenServer = false;
    private Button hostButton;
    private Button clientButton;


    private void Awake()
    {
        //서버가 열려있는지 표시하는 UI
        hostButton = transform.GetChild(0).transform.GetChild(0).GetComponent<Button>();
        if(hostButton == null)
        {
            Debug.Log("호스트 버튼 없음");
        }
        
        clientButton = transform.GetChild(0).transform.GetChild(1).GetComponent<Button>();
        if (clientButton == null)
        {
            Debug.Log("클라이언트 버튼 없음");

        }
        hostButton.onClick.AddListener(HostButtonClick);
        //clientButton.onClick.AddListener(); 호스트 된 서버 클라이언트 연결하는 스크립트 필요

    }

    //게임 매니저에서 값 받아와야하나

    private void HostButtonClick()
    {
        if (!isOpenServer)
        {
            isOpenServer=true;
            // 포톤 서버 생성 스크립트 추가 
            //해당 서버로 입장 => 포톤 서버 입장하는 스크립트 추가
            NextScene();
        }
        else
        {
            Debug.Log("서버가 이미 열린 상태");
        }
    }


    private  void NextScene()
    {
        if(SceneName != null && isOpenServer)
        {
            isOpenServer = true;
            SceneManager.LoadScene(SceneName);
        }

    }
}

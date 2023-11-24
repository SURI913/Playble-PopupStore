using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenScene : MonoBehaviour
{
    [SerializeField] int serverLocation;

    private bool isOpenServer = false;
    private Button hostButton;
    private Button clientButton;
    private Text serverName;
    private Text serverPlayerCount;

    private Animator myAnim;
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
        clientButton.onClick.AddListener(ClientButtonClick);
        serverName = transform.GetChild(1).GetComponent<Text>();
        serverPlayerCount = transform.GetChild(2).GetComponent<Text>();
        myAnim = GetComponent<Animator>();
    }
    IEnumerator CloseAnimStart() //호스트 활성화 시
    {

        myAnim.SetTrigger("Close");
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);

        //씬 이동
        GameManager.Instance.CreatServer(serverLocation);
        yield return null;
    }

    private void SetServerData()
    {
        serverName.text = GameManager.Instance.GetServerName(serverLocation);
        serverPlayerCount.text = string.Format("# / 5",GameManager.Instance.CountPlusPlayer(serverLocation)); //입장인원체크겸 값 전달 받음
    }

    //게임 매니저에서 값 받아와야하나

    private void HostButtonClick()
    {
        if (!GameManager.Instance.CheckingHostServer(serverLocation))
        {
            isOpenServer=true;
            SetServerData();

            // 포톤 서버 생성
            StartCoroutine(CloseAnimStart());
        }
        else
        {
            Debug.Log("서버가 이미 열린 상태");
        }
    }

    private void ClientButtonClick()
    {
        if (GameManager.Instance.CheckingHostServer(serverLocation))
        {

            SetServerData();
            // 포톤 서버 입장
            StartCoroutine(CloseAnimStart());
        }
        else
        {
            Debug.Log("서버 호스팅을 해야함");
        }
    }
    
}

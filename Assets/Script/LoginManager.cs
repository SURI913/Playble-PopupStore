using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SceneLoadManager;

public class LoginManager : MonoBehaviour
{
    //유저 데이터
    [SerializeField] Text playerID;
    [SerializeField] Text playerPassword;


    [SerializeField] GameObject usedIDUI;
    [SerializeField] GameObject unavailableUI;
    [SerializeField] GameObject noneDataUI;
    [SerializeField] GameObject registerSuccessUI;

    [SerializeField] Animator logInUI;


    const float uiLoadSecond = 2f;
    private Animator registerAnim;

    private void Awake()
    {

        //다중 씬 생성 (움직이는 배경)
        usedIDUI.SetActive(false);
        unavailableUI.SetActive(false);
        noneDataUI.SetActive(false);
        registerSuccessUI.SetActive(false);

        registerAnim = GetComponent<Animator>();
    }

    void ExitLogInUI()
    {
        logInUI.SetTrigger("Close");
    }

    IEnumerator LoadUI( GameObject UI)
    {
        UI.SetActive(true);
        UI.GetComponent<Animator>().SetTrigger("Open");
        yield return new WaitForSeconds(uiLoadSecond);
        UI.GetComponent<Animator>().SetTrigger("Close");
        yield return new WaitForSeconds(0.3f);
        UI.SetActive(false);
    }

    IEnumerator RegisterSuccessMsg() //가입버튼 활성화 시
    {
        registerSuccessUI.SetActive(true);
        registerAnim.SetTrigger("Open");
        yield return new WaitForSeconds(uiLoadSecond);
        registerAnim.SetTrigger("Close");
        yield return new WaitForSeconds(0.3f);
        registerSuccessUI.SetActive(false);

        //씬 이동
        GameManager.Instance.NextScene = SceneSet.lobby;
    }

    //로그인 버튼 클릭
    public void LogInButtonOnClick()
    {
        GameManager.Instance.playerId = playerID.text;
        GameManager.Instance.playerPassword = playerPassword.text;
        GameManager.Instance.isFristAccount = false;
        ExitLogInUI();

        if (GameManager.Instance.LogInPlayer()) //유저 데이터 확인하도록 수정
        {
            //유저 데이터 확인 후 맞다면
            
            //씬이동
            GameManager.Instance.NextScene = SceneSet.lobby;
        }
        else
        {
            //아니라면
            StartCoroutine(LoadUI(noneDataUI));
            //유저데이터가 없습니다. UI띄우기
            return;
        }
    }

    public void RegisterButtonOnClick()
    {
        GameManager.Instance.playerId = playerID.text;
        GameManager.Instance.playerPassword = playerPassword.text;
        GameManager.Instance.isFristAccount = true;

        ExitLogInUI();

        if (GameManager.Instance.SignUpPlayer()) //유저 데이터 확인 후 등록
        {
            //데이터 등록 완료

            StartCoroutine(RegisterSuccessMsg());
        }
        else
        {
            StartCoroutine(LoadUI(usedIDUI));
            //아니라면
            //아이디 중복 메세지 띄움
            return;
        }

    }

    public void MissMyPasswordButtonOnClick()
    {
        //비밀번호 찾기 기능 = > 구현 x
        StartCoroutine(LoadUI(unavailableUI));
        //사용불가 메세지 띄우기
    }
}

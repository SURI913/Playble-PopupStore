using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour
{
    [SerializeField] string SceneName;

    private bool isOpenServer = false;

    private void Awake()
    {
        //서버가 열려있는지 표시하는 UI
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            //플레이어일때 실행

            if (isOpenServer)
            {
                //서버가열렸다면 
                //해당 서버로 입장 => 포톤 서버 입장하는 스크립트 추가
            }
            else
            {
                // 포톤 서버 생성 스크립트 추가
                OpenServer();


            }
        }
        //서버 열기 or 서버 들어가기
    }

    private  void OpenServer()
    {
        if(SceneName != null)
        {
            isOpenServer = true;
            SceneManager.LoadScene(SceneName);
        }

    }
}

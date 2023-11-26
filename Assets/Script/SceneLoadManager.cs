using MoreMountains.InventoryEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadManager : MonoBehaviour
{
    public enum SceneSet { start, lobby, server }

    public SceneSet sceneSet;
    private Stack<string> unLoadSceneName = new Stack<string>(); //언로드를 위한 씬이름 보관

    //페이드 인아웃 처리
    [SerializeField] Image fadeObj;

    [SerializeField] GameObject inventoryUI;

    private Color fadeAlpha;
    private float fadeSpeed = 0.05f;
    public bool isFadeInOut { get; private set; } = false;

    private IEnumerator Start()
    {
        fadeAlpha.a = 1f;

        //첫 씬
        sceneSet = SceneSet.start;
        StartCoroutine(LoadScene(sceneSet));
        yield return null;
    }

    public IEnumerator LoadScene(SceneSet sceneName)
    {
        isFadeInOut = true;
        fadeObj.gameObject.SetActive(true);
        while (fadeAlpha.a < 1)   // 점점 불투명하게
        {
            fadeAlpha.a += fadeSpeed; // 페이드아웃 속도 결정
            fadeObj.color = fadeAlpha;

            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
        //이 사이에 씬 로드 해야함

        //이 사이에 로딩씬에 필요한 코드 작성

        //해당 씬에 없어야 할 씬들
        if (unLoadSceneName.Count != 0) //비어있지않다면
        {
            foreach(var name in unLoadSceneName)
            {
                SceneManager.UnloadSceneAsync(name);
            }
            unLoadSceneName.Clear();
        }
        //이 사이에 로딩씬에 필요한 코드 작성

        if (sceneName == SceneSet.start)
        {
            SceneManager.LoadScene("StartScene", LoadSceneMode.Additive);
            SceneManager.LoadScene("StartBackGroundScene", LoadSceneMode.Additive);
            unLoadSceneName.Push("StartScene");
            unLoadSceneName.Push("StartBackGroundScene");
            inventoryUI.SetActive(false);
        }
        if (sceneName == SceneSet.lobby)
        {
            SceneManager.LoadScene("Lobby", LoadSceneMode.Additive);
            unLoadSceneName.Push("Lobby");
            FindPlayInventory();
        }
        if (sceneName == SceneSet.server)
        {
            SceneManager.LoadScene("MapMaker", LoadSceneMode.Additive);
            //민팅하는 UI씬 추가
            SceneManager.LoadScene("MarketConnectionUI", LoadSceneMode.Additive); //상품 서버와 연결
            unLoadSceneName.Push("MapMaker");
            unLoadSceneName.Push("MarketConnectionUI");
            FindPlayInventory();

        }

        while (fadeAlpha.a > 0)   // 점점 투명하게
        {
            fadeAlpha.a -= fadeSpeed;
            fadeObj.color = fadeAlpha;

            yield return null;
        }
        fadeObj.gameObject.SetActive(false);
        isFadeInOut = false;
    }

    bool isFindPlayerInventory = false;
    private void FindPlayInventory()
    {
        //민팅 씬 있을때마다 거기에 있는 인벤토리 찾아서 플레이어 이름 바꿔줌
        if (!isFindPlayerInventory)
        {
            inventoryUI.SetActive(true);
            Inventory playerInventory = GameObject.Find("RogueMainInventory").GetComponent<Inventory>();
            InventoryDisplay playerInventoryDisplay = GameObject.Find("RogueMainInventoryDisplay").GetComponent<InventoryDisplay>();
            if (playerInventory != null && playerInventoryDisplay != null)
            {
                playerInventory.PlayerID = GameManager.Instance.playerId;
                playerInventoryDisplay.PlayerID = GameManager.Instance.playerId;
                isFindPlayerInventory = true;
            }
        }
        
    }


}

using MoreMountains.InventoryEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MintingConnection : MonoBehaviour
{

    [SerializeField] Button acceptButton;
    [SerializeField] ItemPicker ticketItem;
    private void Awake()
    {
        acceptButton.onClick.AddListener(StartWebView);
        acceptButton.gameObject.SetActive(true);
    }
    public void ReceiveDataFromWebpage(string data)
    {
        Debug.Log("Received data from webpage: " + data);
    }

    IEnumerator SendRequest()
    {
        //해당 URL은 NFT 발급한 해시값+아이디로 check요청합니다.
        //해당 NFT가 존재할 경우에 "0x0A2..."형식의 주소값이 반환되구요,
        //없다면 string으로 false라는 문자열이 반환되고, 해당 값이 result에 담기게 됩니다.
        //이미지는 "https://ipfs.io/ipfs/QmYb6maCE5iSm12WxEYLQAs5wogvGi52Q2w3jXG52qjWFo?filename=Ticket.jpg"
        //해당 링크에 요청을 쏘면 받을 수 있는데, "?" 뒤의 값은 선택사항이라서,
        //"http://ipfs.io/ipfs/"링크에 뒤에 해시값을 붙여서 웹으로 쏘면 이미지를 받을 수 있는 구조입니다.

        string url = "http://115.85.181.212:30001/check?itemname=" + "QmYb6maCE5iSm12WxEYLQAs5wogvGi52Q2w3jXG52qjWFo"
            + GameManager.Instance.playerId;

        UnityWebRequest www = UnityWebRequest.Get(url);

        www.SendWebRequest();
        //yield return new WaitUntil(()=> www.downloadHandler.text != "");

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            // 요청이 성공한 경우 결과를 처리.
            string result = "false";
            Debug.Log("Result: " + result);

            float loadTime = 100f;

            //result = www.downloadHandler.text;
            Debug.Log("First\nresult" + result + "\nurl: " + url);
            
            while (result == "false")
            {
                //WebRequest 한번에 한 번만 할 수 있는 Unity 정책회피를 위해
                //서버에 요청 보낼 때 마다 새로 만든다.
                UnityWebRequest www2 = UnityWebRequest.Get(url);

                //매번 서버에 요청을 보내서 결과를 확인
                www2.SendWebRequest();
                yield return new WaitForSeconds(0.5f);
                result = www2.downloadHandler.text;
                
                Debug.Log("Result: " + result + "\nurl: " + url);
                loadTime -= 0.5f;
                if(loadTime <= 0)
                {
                    Debug.Log("load time smaller than 0.5");
                    break;
                }
            }

            Debug.Log("Result: " + result);
            if (result != "false")
            {
                ticketItem.Pick("RogueMainInventory", GameManager.Instance.playerId);
                acceptButton.gameObject.SetActive(false);

            }
            else
            {
                //발급 실패 ui띄우기
            }

            //성공하면 인벤토리에 아이템 들어감
            
        }
    }

    public void StartWebView()
    {
        //해당 minturl이 mint하는데 필요한 값들을 포함해서 접속하면 되는 URL입니다.
        //NFT아이템 이름은 유저에게 보이는 이름이니까 그냥 XX MUSEUM TICKET 정도여도 될거 같습니다.
        //아이템 해시값은 이미지 하나 찾아서 업로드하고 해시값 보내드리겠습니다.
        //유저ID번호는 낮에 이야기했던 유저 각 고유 ID번호를 붙이면 될 것 같습니다.
        string minturl = "http://115.85.181.212:30001/mint?itemname=" + ticketItem.Item.ItemID
            + "&itemhash=" + "QmYb6maCE5iSm12WxEYLQAs5wogvGi52Q2w3jXG52qjWFo" +  GameManager.Instance.playerId;

        //위의 URL사용해서 OpenURL로 웹브라우저 열면 mint하는 페이지로 이동합니다!
        Application.OpenURL(minturl);

        StartCoroutine(SendRequest());

        //윈도우 지원가능한 패키지 찾아볼 것
    }
}

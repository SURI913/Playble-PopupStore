using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class ShopItem : MonoBehaviour
{
    [SerializeField] string itemShopUrl;
    [SerializeField] Sprite itemSprite;
    [SerializeField] string itemName;
    //뱃지 용도 결정 x 필요한 데이터 값 생각해보고 띄우는 걸로

    //private WebViewObject webViewObject;


    private Text myItemName;
    private Image myItemImg;
    private Button myItemButton;
    private void Start()
    {
        myItemName = transform.GetChild(2).GetComponent<Text>();
        myItemButton = transform.GetChild(3).GetComponent<Button>();
        myItemImg = transform.GetChild(4).GetComponent<Image>();

        if (myItemImg == null)
        {
            Debug.Log("아이템 이미지오브젝트 없음");
            return;
        }
        if (myItemButton == null)
        {
            Debug.Log("아이템 버튼오브젝트 없음");
            return;
        }
        if (itemShopUrl == null)
        {
            Debug.Log("웹뷰어 url없음");
            return;
        }

        if (itemSprite == null)
        {
            Debug.Log("아이템 스프라이트 없음");
            return;
        }
        if (itemName == null)
        {
            Debug.Log("아이템 이름 없음");
            return;
        }
        myItemName.text = itemName;
        /*글자 크기 줄이는 부분 필요할끼?*/
        myItemImg.sprite = itemSprite;
        myItemButton.onClick.AddListener(StartWebView);
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
            + "유저ID번호";

        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            // 요청이 성공한 경우 결과를 처리.
            string result = www.downloadHandler.text;
            Debug.Log("Result: " + result);
        }
    }

    public void ReceiveDataFromWebpage(string data)
    {
        Debug.Log("Received data from webpage: " + data);
    }

    /*웹 뷰*/
    public void StartWebView()
    {
        StartCoroutine(SendRequest());

        //해당 minturl이 mint하는데 필요한 값들을 포함해서 접속하면 되는 URL입니다.
        //NFT아이템 이름은 유저에게 보이는 이름이니까 그냥 XX MUSEUM TICKET 정도여도 될거 같습니다.
        //아이템 해시값은 이미지 하나 찾아서 업로드하고 해시값 보내드리겠습니다.
        //유저ID번호는 낮에 이야기했던 유저 각 고유 ID번호를 붙이면 될 것 같습니다.
        string minturl = "http://115.85.181.212:30001/mint?itemname=" + "NFT아이템이름(유저에게 보임)"
            + "&itemhash=" + "QmYb6maCE5iSm12WxEYLQAs5wogvGi52Q2w3jXG52qjWFo" + "유저ID번호";

        //위의 URL사용해서 OpenURL로 웹브라우저 열면 mint하는 페이지로 이동합니다!
        Application.OpenURL(minturl);


        //윈도우 지원가능한 패키지 찾아볼 것
    }
}

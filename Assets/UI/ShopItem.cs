using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] string itemShopUrl;
    [SerializeField] Sprite itemSprite;
    [SerializeField] string itemName;
    //뱃지 용도 결정 x 필요한 데이터 값 생각해보고 띄우는 걸로

    private WebViewObject webViewObject;


    private Text myItemName;
    private Image myItemImg;
    private Button myItemButton;
    private void Start()
    {
        myItemName = transform.GetChild(2).GetComponent<Text>();
        myItemButton = transform.GetChild(3).GetComponent<Button>();
        myItemImg = transform.GetChild(4).GetComponent<Image>();

        if(myItemImg == null) {
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

    /*웹 뷰*/
    public void StartWebView()
    {

        //윈도우 지원가능한 패키지 찾아볼 것
    }
}

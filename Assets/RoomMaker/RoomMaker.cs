using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class RoomMaker : MonoBehaviour
{

    [SerializeField] GameObject[] Wall;
    [SerializeField] GameObject[] Floor;
    [SerializeField] Sprite[] themaImage;

    
    [SerializeField] Tilemap[] tilemap;
    [SerializeField] TileBase[] decoTiles;
    [SerializeField] TileBase[] funitureTiles;
    [SerializeField] Sprite[] funitureImage;
    [SerializeField] Sprite[] decoImage;

    [SerializeField] MouseSelect selectObject;


    [SerializeField] GameObject[] Items;

    List<GameObject> Previous = new List<GameObject>();

    Image[] itemImage;
    Button[] itemButton;
    Text[] itemText;

    [SerializeField] string[] themaColorName =  { "Thema", };
    [SerializeField] string[] funitureName =  { "Funiture", };
    [SerializeField] string[] decoName =  { "Deco",  };

    //활성화 버튼 체크
    string itemName;
    int showThemaNum = 0;
    int showFunitureNum = 0;
    int showDecoNum = 0;
    Button themaButtom, funitureButtom, decoButtom;
    private void Awake()
    {
        //if(!IsServer) { enabled = false; }

        foreach (GameObject go in Wall)
        {
            if (go != null) { 
                go.SetActive(false);
            }
        }
        foreach (GameObject go in Floor)
        {
            if (go != null)
            {
                go.SetActive(false);
            }
        }
        //-----------------시작 세팅

        themaButtom = transform.GetChild(1).GetComponent<Button>();
        funitureButtom = transform.GetChild(2).GetComponent<Button>();
        decoButtom = transform.GetChild(3).GetComponent<Button>();

        itemImage = new Image[Items.Length];
        //레이어속 아이템 데이터 저장
        for (int i =0; i<Items.Length;i++)
        {
            itemImage[i] = Items[i].transform.GetChild(0).GetComponent<Image>();
        }
        itemButton = transform.GetChild(0).GetComponentsInChildren<Button>();
        itemText = transform.GetChild(0).GetComponentsInChildren<Text>();

        //--------------버튼 연결
        themaButtom.onClick.AddListener(() => SetItemButtonData(themaImage, themaColorName, showThemaNum)); //첫번째 아이템 부터
        themaButtom.onClick.AddListener(() => AtiveThemaButton(showThemaNum));

        funitureButtom.onClick.AddListener(() => SetItemButtonData(funitureImage, funitureName, showFunitureNum));
        funitureButtom.onClick.AddListener(() => AtiveFunitureButton(showFunitureNum));

        decoButtom.onClick.AddListener(() => SetItemButtonData(decoImage, decoName, showDecoNum));
        decoButtom.onClick.AddListener(() => AtiveDecoButton(showDecoNum));

        //테마 버튼 눌린 상태로 두기 (애니메이션 조절)
        themaButtom.onClick.Invoke(); //버튼 활성화
        itemName = themaColorName[0]; //처음은 테마로 활성화
    }

    public void ChangeThema(int index)
    {
        //if (!IsServer) return;

        //이전에 선택한 것 지우고
        if (Previous != null)
        {
            foreach (GameObject go in Previous)
            {
                go.SetActive(false);
            }
            Previous.Clear();
        }

        //새 아이템 활성화
        Wall[index].SetActive(true);
        Previous.Add(Wall[index]);
        Floor[index].SetActive(true);
        Previous.Add(Floor[index]);
    }
    
    void SetItemButtonData(Sprite[] _itemImage, string[] _itemName, int index)
    {
        for (int i  = 0; i < itemButton.Length; i++)
        {
            itemButton[i].onClick.RemoveAllListeners();
            itemImage[i].sprite = _itemImage[i+index];
            //이미지 스프라이트에 맞춰 조절
            RectTransform rect = (RectTransform)itemImage[i].transform;

            rect.sizeDelta = new Vector2(_itemImage[i+ index].rect.width, _itemImage[i + index].rect.height);
            if(_itemImage[i + index].rect.width >= 60 || _itemImage[i + index].rect.height >= 60)
            {
                rect.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            }

            itemText[i].text = _itemName[i+ index +1];
        }
        itemName = _itemName[0];
        
    }

    void AtiveThemaButton(int index)
    {
        for (int i = 0; i < itemButton.Length; i++)
        {
            itemButton[i].onClick.RemoveAllListeners();
        }
        itemButton[0].onClick.AddListener(() => ChangeThema(index));
        itemButton[1].onClick.AddListener(() => ChangeThema(index+1));
        itemButton[2].onClick.AddListener(() => ChangeThema(index+2));
        itemButton[3].onClick.AddListener(() => ChangeThema(index+3));

    }

    void AtiveFunitureButton(int index)
    {
        for (int i = 0; i < itemButton.Length; i++)
        {
            itemButton[i].onClick.RemoveAllListeners();
        }
        itemButton[0].onClick.AddListener(() => SelectFuniture(index));
        itemButton[1].onClick.AddListener(() => SelectFuniture(index+1));
        itemButton[2].onClick.AddListener(() => SelectFuniture(index+2));
        itemButton[3].onClick.AddListener(() => SelectFuniture(index+3));
        //itemButton[4].onClick.AddListener(() => SelectFuniture(4));

    }

    void SelectFuniture(int index)
    {
        selectObject.tilemap = tilemap[0];  //가구용타일맵
        selectObject.changeTile = funitureTiles[index];
        selectObject.gameObject.SetActive( true );

    }

    void AtiveDecoButton(int index)
    {
        for (int i = 0; i < itemButton.Length; i++)
        {
            itemButton[i].onClick.RemoveAllListeners();
        }
        itemButton[0].onClick.AddListener(() => SelectDeco(index));
        itemButton[1].onClick.AddListener(() => SelectDeco(index+1));
        itemButton[2].onClick.AddListener(() => SelectDeco(index+2));
        itemButton[3].onClick.AddListener(() => SelectDeco(index + 3));
        //itemButton[4].onClick.AddListener(() => SelectDeco(4));

    }

    void SelectDeco(int index)
    {
        selectObject.tilemap = tilemap[1];  //테코용 타일맵 양탄자 같은거
        selectObject.changeTile = decoTiles[index];
        selectObject.gameObject.SetActive(true);

    }

    //아이템 이동
    public void RightButtonClick()
    {
        if (itemName == themaColorName[0] && showThemaNum<themaImage.Length-4)
        {
            showThemaNum++;
            themaButtom.onClick.Invoke(); //버튼 활성화
        }
        else if(itemName == funitureName[0] && showFunitureNum < funitureImage.Length-4)
        {
            showFunitureNum++;
            funitureButtom.onClick.Invoke();
        }
        else if(itemName == decoName[0] && showDecoNum < decoImage.Length - 4)
        {
            showDecoNum++;
            decoButtom.onClick.Invoke();
        }
        else
        {
            //이후에  UI 추가
            Debug.Log(itemName+"에 더이상 아이템이 없습니다");
        }
    }

    public void LeftButtonClick()
    {
        if (itemName == themaColorName[0] && showThemaNum > 0)
        {
            showThemaNum--;
            themaButtom.onClick.Invoke(); //버튼 활성화
        }
        else if (itemName == funitureName[0] && showFunitureNum > 0)
        {
            showFunitureNum--;
            funitureButtom.onClick.Invoke();
        }
        else if (itemName == decoName[0] && showDecoNum > 0)
        {
            showDecoNum--;
            decoButtom.onClick.Invoke();
        }
        else
        {
            //이후에  UI 추가
            Debug.Log(itemName+"에 더이상 아이템이 없습니다");
        }
    }

}

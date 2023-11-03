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

    string[] themaColorName =  { "Red", "Yellow", "Green", "Blue"};
    string[] funitureName =  { "Plants00", "Plants01", "Plants02", "Plants03", "Plants04" };
    string[] decoName =  { "FreamEmpt", "Fream01", "Fream02", "Fream03" };

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
        //-----------------���� ����

        Button themaButtom = transform.GetChild(1).GetComponent<Button>();
        Button funitureButtom = transform.GetChild(2).GetComponent<Button>();
        Button decoButtom = transform.GetChild(3).GetComponent<Button>();

        itemImage = new Image[Items.Length];
        //���̾�� ������ ������ ����
        for (int i =0; i<Items.Length;i++)
        {
            itemImage[i] = Items[i].transform.GetChild(0).GetComponent<Image>();
        }
        itemButton = transform.GetChild(0).GetComponentsInChildren<Button>();
        itemText = transform.GetChild(0).GetComponentsInChildren<Text>();

        //--------------��ư ����
        themaButtom.onClick.AddListener(() => SetItemButtonData(themaImage, themaColorName));
        themaButtom.onClick.AddListener(() => AtiveThemaButton());

        funitureButtom.onClick.AddListener(() => SetItemButtonData(funitureImage, funitureName));
        funitureButtom.onClick.AddListener(() => AtiveFunitureButton());

        decoButtom.onClick.AddListener(() => SetItemButtonData(decoImage, decoName));
        decoButtom.onClick.AddListener(() => AtiveDecoButton());

        //�׸� ��ư ���� ���·� �α� (�ִϸ��̼� ����)
        themaButtom.onClick.Invoke(); //��ư Ȱ��ȭ
    }

    public void ChangeThema(int index)
    {
        //if (!IsServer) return;

        //������ ������ �� �����
        if (Previous != null)
        {
            foreach (GameObject go in Previous)
            {
                go.SetActive(false);
            }
            Previous.Clear();
        }

        //�� ������ Ȱ��ȭ
        Wall[index].SetActive(true);
        Previous.Add(Wall[index]);
        Floor[index].SetActive(true);
        Previous.Add(Floor[index]);
    }



    void SetItemButtonData(Sprite[] _itemImage, string[] _itemName)
    {
        for (int i  = 0; i < itemButton.Length; i++)
        {
            itemButton[i].onClick.RemoveAllListeners();
            itemImage[i].sprite = _itemImage[i];
            //�̹��� ��������Ʈ�� ���� ����
            RectTransform rect = (RectTransform)itemImage[i].transform;
            

            rect.sizeDelta = new Vector2(_itemImage[i].rect.width, _itemImage[i].rect.height);

            itemText[i].text = _itemName[i];
        }
    }

    void AtiveThemaButton()
    {
        for (int i = 0; i < itemButton.Length; i++)
        {
            itemButton[i].onClick.RemoveAllListeners();
        }
        itemButton[0].onClick.AddListener(() => ChangeThema(0));
        itemButton[1].onClick.AddListener(() => ChangeThema(1));
        itemButton[2].onClick.AddListener(() => ChangeThema(2));
        itemButton[3].onClick.AddListener(() => ChangeThema(3));

    }

    void AtiveFunitureButton()
    {
        for (int i = 0; i < itemButton.Length; i++)
        {
            itemButton[i].onClick.RemoveAllListeners();
        }
        itemButton[0].onClick.AddListener(() => SelectFuniture(0));
        itemButton[1].onClick.AddListener(() => SelectFuniture(1));
        itemButton[2].onClick.AddListener(() => SelectFuniture(2));
        itemButton[3].onClick.AddListener(() => SelectFuniture(3));
        //itemButton[4].onClick.AddListener(() => SelectFuniture(4));

    }

    void SelectFuniture(int index)
    {
        selectObject.tilemap = tilemap[0];  //������Ÿ�ϸ�
        selectObject.changeTile = funitureTiles[index];
        selectObject.gameObject.SetActive( true );

    }

    void AtiveDecoButton()
    {
        for (int i = 0; i < itemButton.Length; i++)
        {
            itemButton[i].onClick.RemoveAllListeners();
        }
        itemButton[0].onClick.AddListener(() => SelectDeco(0));
        itemButton[1].onClick.AddListener(() => SelectDeco(1));
        itemButton[2].onClick.AddListener(() => SelectDeco(2));
        itemButton[3].onClick.AddListener(() => SelectDeco(3));
        //itemButton[4].onClick.AddListener(() => SelectDeco(4));

    }

    void SelectDeco(int index)
    {
        selectObject.tilemap = tilemap[1];  //���ڿ� Ÿ�ϸ� ��ź�� ������
        selectObject.changeTile = decoTiles[index];
        selectObject.gameObject.SetActive(true);

    }
}

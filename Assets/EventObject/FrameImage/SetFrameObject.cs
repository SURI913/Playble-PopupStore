using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class SetFrameObject : MonoBehaviour
{
    [SerializeField] Tilemap tilemap; //�� ��ư���� ���� Ÿ�ϸ� �ٸ� 
    [SerializeField] TileBase Frame; //�� ��ư���� ���� ������ �ٸ�

    [SerializeField] UploadImageData uploadImageData; //������ Ȯ�ο�

    [SerializeField] MouseSelect selectObject;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetFrame);
        tilemap.gameObject.SetActive(false);
    }

    private void SetFrame()
    {
        if (!uploadImageData.imageData)
        {
            Debug.Log("�̹����� ���� UI ��� ��");
            return;
        }
        selectObject.tilemap = tilemap;  //������Ÿ�ϸ�
        selectObject.changeTile = Frame;

        tilemap.gameObject.SetActive(true);
        selectObject.gameObject.SetActive(true);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class SetFrameObject : MonoBehaviour
{
    [SerializeField] Tilemap tilemap; //각 버튼마다 넣을 타일맵 다름 
    [SerializeField] TileBase Frame; //각 버튼마다 넣을 프레임 다름

    [SerializeField] UploadImageData uploadImageData; //데이터 확인용

    [SerializeField] MouseSelect selectObject;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetFrame);
    }

    private void SetFrame()
    {
        if (!uploadImageData.imageData)
        {
            Debug.Log("이미지가 없음 UI 띄울 것");
            return;
        }
        selectObject.tilemap = tilemap;  //가구용타일맵
        selectObject.changeTile = Frame;
        selectObject.gameObject.SetActive(true);
    }

}

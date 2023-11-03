using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class MouseSelect : MonoBehaviour
{
    public Tilemap tilemap { get; set; }
    public TileBase changeTile { get;  set; }
    [SerializeField] TileBase selectTile;

    private SpriteRenderer myImage;

    void Start()
    {
        myImage = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        if(!EventSystem.current.IsPointerOverGameObject()) //UI와 카메라 무빙이 없을 때 선택 가능
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            mousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));
            transform.position = mousePosition;

            if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("삭제를 위해 오른쪽마우스 클릭");
                myImage.color = new Color(1f, 0.7f, 0.7f, 0.5f);

                if (Input.GetMouseButton(0))
                {
                    //타일맵 지우기
                    tilemap.SetTile(new Vector3Int((int)mousePosition.x, (int)mousePosition.y, 0), null);
                }
            }
            else
            {
                myImage.color = new Color(1f, 1f, 1f, 1f);
                //우클릭시
                if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    tilemap.SetTile(new Vector3Int((int)mousePosition.x, (int)mousePosition.y, 0), changeTile);
                    //쉽게 그려짐
                }
            }
        }

    }

    public void OnClickButtonSelected() //적용완료 상태일 경우
    {
        gameObject.SetActive(false);
    }
}

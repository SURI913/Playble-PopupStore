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

        if(!EventSystem.current.IsPointerOverGameObject()) //UI�� ī�޶� ������ ���� �� ���� ����
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            mousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));
            transform.position = mousePosition;

            if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("������ ���� �����ʸ��콺 Ŭ��");
                myImage.color = new Color(1f, 0.7f, 0.7f, 0.5f);

                if (Input.GetMouseButton(0))
                {
                    //Ÿ�ϸ� �����
                    tilemap.SetTile(new Vector3Int((int)mousePosition.x, (int)mousePosition.y, 0), null);
                }
            }
            else
            {
                myImage.color = new Color(1f, 1f, 1f, 1f);
                //��Ŭ����
                if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    tilemap.SetTile(new Vector3Int((int)mousePosition.x, (int)mousePosition.y, 0), changeTile);
                    //���� �׷���
                }
            }
        }

    }

    public void OnClickButtonSelected() //����Ϸ� ������ ���
    {
        gameObject.SetActive(false);
    }
}

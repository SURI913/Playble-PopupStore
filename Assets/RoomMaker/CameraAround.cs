using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraAround : MonoBehaviour
{
    public float speed = 10.0f;
    private float temp_value;
    Camera Camera;

    Vector2 clickPoint;
    float dragSpeed = 30.0f;


    private void Awake()
    {
        Camera = GetComponent<Camera>();
    }

    void ViewMoving()
    {
        // ���콺 ���� Ŭ�� ���� ��ġ ���
        if (Input.GetMouseButtonDown(1))
        {
            clickPoint = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
             Vector3 position
            = Camera.main.ScreenToViewportPoint((Vector2)Input.mousePosition - clickPoint);

            Vector3 move = -position * (Time.deltaTime * dragSpeed);

            transform.Translate(move);
        }
    }

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * speed;

        // scroll < 0 : scroll down�ϸ� ����
        if (Camera.orthographicSize <= 2.67f && scroll > 0)
        {
            temp_value = Camera.orthographicSize;
            Camera.orthographicSize = temp_value; // maximize zoom in

            // �ִ�� Zoom in ���� �� Ư�� ���� �������� ��

            // �ִ� �� �� ������ ��� �� ���� ���߷��� �ѹ� �� �ƿ� �Ǵ� ������ ����
        }

        // scroll > 0 : scroll up�ϸ� �ܾƿ�
        else if (Camera.orthographicSize >= 20.03f && scroll < 0)
        {
            temp_value = Camera.orthographicSize;
            Camera.orthographicSize = temp_value; // maximize zoom out
        }
        else{ Camera.orthographicSize -= scroll * 0.5f; }

        if (Input.GetKey(KeyCode.Space)  && !EventSystem.current.IsPointerOverGameObject())
        {
            ViewMoving();
        }
    }
}

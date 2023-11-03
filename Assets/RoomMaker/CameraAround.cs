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
        // 마우스 최초 클릭 시의 위치 기억
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

        // scroll < 0 : scroll down하면 줌인
        if (Camera.orthographicSize <= 2.67f && scroll > 0)
        {
            temp_value = Camera.orthographicSize;
            Camera.orthographicSize = temp_value; // maximize zoom in

            // 최대로 Zoom in 했을 때 특정 값을 지정했을 때

            // 최대 줌 인 범위를 벗어날 때 값에 맞추려고 한번 줌 아웃 되는 현상을 방지
        }

        // scroll > 0 : scroll up하면 줌아웃
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class BackGoundView : MonoBehaviour
{
    enum MoveVector
    {
        left,
        right,
        down,
        up,
    }

    [Header("카메라 움직임 속도 값이 클수록 천천히 움직임")]
    [Range(1f, 20f)]
    [SerializeField] float speed;

    Vector3 leftTop = new Vector3(-40, 19, -10);
    Vector3 rightTop = new Vector3(77, 19, -10);
    Vector3 rightBottom = new Vector3(77, -20, -10);
    Vector3 leftBottom = new Vector3(-40, -20, -10);

    MoveVector state;
    Vector3 vel = Vector3.zero;


    [SerializeField] Vector2 center;
    [SerializeField] Vector2 size;

    float height;
    float width;
    private void Awake()
    {
        state = MoveVector.right; //처음은 오른쪽으로 움직임
        transform.position = leftTop;

        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    void Update()
    {
        switch (state)
        {
            case MoveVector.right:
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(77, transform.position.y,-10f), ref vel, speed);
                if (transform.position.x >= rightTop.x - 0.05f)
                {
                    state = MoveVector.down;
                }
                break;
            case MoveVector.down:
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, -20, -10f), ref vel, speed);
                if (transform.position.y <= rightBottom.y + 0.05f)
                {
                    state = MoveVector.left;
                }
                break;
            case MoveVector.left:
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(-40, transform.position.y, -10f), ref vel, speed);
                if (transform.position.x <= leftBottom.x + 0.05f)
                {
                    state = MoveVector.up;
                }
                break;
            case MoveVector.up:
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, 19, -10f), ref vel, speed);
                if (transform.position.y >=leftTop.y - 0.05f)
                {
                    state = MoveVector.right;
                }
                break;
        }

        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);

    }
}

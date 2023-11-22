using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport.Relay;
using UnityEditor;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [Header ("플레이어 오브젝트 입력")]
    [SerializeField] Transform target;

    [Space (10f)]
    [Header("카메라 움직임 속도 / 카메라가 움직일 범위")]
    [Range (0.5f, 10f)]
    [SerializeField] float speed;

    [SerializeField] Vector2 center;
    [SerializeField] Vector2 size;

    float height;
    float width;
    private void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    //카메라가 움직일 범위를 시각적으로 보기 위한 기즈모 생성

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
        
        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx+center.x);
        
        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
}

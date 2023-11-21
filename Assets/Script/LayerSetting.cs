using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LayerSetting : MonoBehaviour
{
    private TilemapRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<TilemapRenderer>();
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y)*-1;
        //포지션y에 값에 반비례하게 적용
    }
}

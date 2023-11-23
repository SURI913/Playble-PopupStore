using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLobbyUI : MonoBehaviour
{
    [SerializeField] private GameObject ui;

    private void Awake()
    {
        ui.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            //플레이어라면
            ui.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            //플레이어라면
            ui.SetActive(false);
        }
    }
}

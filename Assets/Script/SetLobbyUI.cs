using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLobbyUI : MonoBehaviour
{
    [SerializeField] private GameObject ui;

    private Animator uiAnim;

    private void Awake()
    {
        ui.SetActive(false);
        uiAnim = ui.GetComponent<Animator>();
    }

    IEnumerator CloseAnimStart()
    {

        uiAnim.SetTrigger("Close");
        yield return new WaitForSeconds(0.3f);
        ui.SetActive(false);
        yield return null;
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
            StartCoroutine(CloseAnimStart());
        }
    }
}

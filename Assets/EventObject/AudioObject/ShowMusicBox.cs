using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMusicBox : MonoBehaviour
{
    [SerializeField] private GameObject showMusicUI;

    private void Awake()
    {
        showMusicUI.SetActive(false);

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        showMusicUI.SetActive(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        showMusicUI.SetActive(false);

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class SetMyMusic : MonoBehaviour
{
    [SerializeField] Button ativeMusic;
    [SerializeField] GameObject setMusicObjcet;
    [SerializeField] Button showMusicList;
    [SerializeField] Button setMusicBox;

    //---------------------음악타일 설치
    [SerializeField] MouseSelect selectObjcet;
    [SerializeField] Tilemap tilemap;
    [SerializeField] TileBase tile;


    private void Awake()
    {
        setMusicObjcet.SetActive(true);
        ativeMusic.onClick.AddListener(AtiveMusic);
        setMusicBox.onClick.AddListener(SetMusicBox);
    }

    bool ativeButton = false;
    private void AtiveMusic()
    {
        if (!ativeButton)
        {
            setMusicObjcet.SetActive(true);
            ativeButton = false;
            return;
        }
        setMusicObjcet.SetActive(true);
        ativeButton = true;

    }

    private void SetMusicBox()
    {
        selectObjcet.gameObject.SetActive(true);
        selectObjcet.tilemap = tilemap;
        selectObjcet.changeTile = tile;
    }

}

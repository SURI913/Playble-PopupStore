using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class SetMyMusic : MonoBehaviour
{

    [SerializeField] Slider ativeMusicSlider;
    [SerializeField] Button handleBtn;
    [SerializeField] GameObject setMusicObjcet;
    [SerializeField] Button setMusicBox;

    //---------------------음악타일 설치
    [SerializeField] MouseSelect selectObjcet;
    [SerializeField] Tilemap tilemap;
    [SerializeField] TileBase tile;


    private void Awake()
    {
        setMusicObjcet.SetActive(false);
        handleBtn.onClick.AddListener(AtiveMusic);
        setMusicBox.onClick.AddListener(SetMusicBox);
        ativeMusicSlider.value = 0;
    }

    bool ativeButton = false;
    private void AtiveMusic()
    {
        if (!ativeButton)
        {
            setMusicObjcet.SetActive(true);
            ativeButton = true;
            ativeMusicSlider.value = 1;
            return;
        }
        setMusicObjcet.SetActive(false);
        ativeButton = false;
        ativeMusicSlider.value = 0;

    }

    //스크롤 할때도 값 바뀌게
    public void AtiveMusic(float value)
    {
        if (value == 1)
        {
            setMusicObjcet.SetActive(true);
            ativeButton = true;
            return;
        }
        setMusicObjcet.SetActive(false);
        ativeButton = false;
    }

    private void SetMusicBox()
    {
        selectObjcet.gameObject.SetActive(true);
        selectObjcet.tilemap = tilemap;
        selectObjcet.changeTile = tile;
    }

}

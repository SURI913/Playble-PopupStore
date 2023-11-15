using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] SoundData bgList;
    AudioSource bgSound;
    private int index = 0;

    [SerializeField] GameObject soundListUI;
    [SerializeField] GameObject soundPlayUI;
    Button leftButton, rightButton, playButton, pauseButton;
    Text title;

    private void Awake()
    {
        bgSound = GetComponent<AudioSource>();

        //UI
        title = soundListUI.transform.GetChild(1).GetComponent<Text>();
        rightButton = soundListUI.transform.GetChild(2).GetComponent<Button>();
        rightButton.onClick.AddListener(RightButton);

        leftButton = soundListUI.transform.GetChild(3).GetComponent<Button>();
        leftButton.onClick.AddListener(LeftButton);



        playButton = soundPlayUI.transform.GetChild(0).GetComponent<Button>();
        playButton.onClick.AddListener(OnPlayBGSound);

        pauseButton = soundPlayUI.transform.GetChild(1).GetComponent<Button>();
        pauseButton.onClick.AddListener(StopPlaySound);

        SetUI();
    }
    //서버 만들어지면 서버 연 유저만 음악 변경하게 끔 변경 하는 스크립트 고려(나중에 여유있다면)

    //배경음 변경 함수
    private void LeftButton()
    {
        if(index > 0)
        {
            index--;
            SetUI();
        }
    }

    private void RightButton()
    {
        if(index < bgList.sound.Length-1)
        {
            index++;
            SetUI();
        }
    }

    //UI변경 
    private void SetUI()
    {
        title.text = bgList.sound[index].soundName;

    }

    //배경음 플레이 함수
    public void OnPlayBGSound()
    {
        SetUI();
        bgSound.clip = bgList.sound[index].soundClip;
        bgSound.loop = true;
        bgSound.volume = 0.1f;
        bgSound.Play();
    }
    //배경음 정지 함수
    public void StopPlaySound()
    {
        bgSound.Stop();
    }
    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountFrame : MonoBehaviour
{

    [SerializeField] GameObject countObject;
    [SerializeField] Slider AtiveFrameSlider;
    [SerializeField] Button HandleBtn;

    [SerializeField] Button plusButton;
    [SerializeField] Button minusButton;
    [SerializeField] Text countText;
    [SerializeField] GameObject SettingObject1;
    [SerializeField] GameObject SettingObject2;
    [SerializeField] GameObject SettingObject3;

    [SerializeField] GameObject FrameTilemap1;
    [SerializeField] GameObject FrameTilemap2;
    [SerializeField] GameObject FrameTilemap3;



    private void Awake()
    {
        if(!HandleBtn) { Debug.Log("�����̴��� ��ư ����"); return; }
        HandleBtn.onClick.AddListener(AtiveFrameObjcet);
        plusButton.onClick.AddListener(PlusButton);
        minusButton.onClick.AddListener(MinusButton);
        AtiveFrameSlider.value = 0;
    }

    bool ativeFrameObject = false;
    void AtiveFrameObjcet()
    {
        if(!ativeFrameObject)
        {
            AtiveFrameSlider.value = 1;
            ativeFrameObject = true;
            countObject.SetActive(true);

            SettingObject1.SetActive(true);
            FrameTilemap1.SetActive(true);

        }
        else
        {
            AtiveFrameSlider.value = 0;
            ativeFrameObject = false;
            countObject.SetActive(false);

            SettingObject1.SetActive(false);
            SettingObject2.SetActive(false);
            SettingObject3.SetActive(false);

            FrameTilemap1.SetActive(false);
            FrameTilemap2.SetActive(false);
            FrameTilemap3.SetActive(false);

        }

    }

    public void AtiveFrameObjcet(float value)
    {
        if (value == 1)
        {
            ativeFrameObject = true;
            countObject.SetActive(true);

            SettingObject1.SetActive(true);
            FrameTilemap1.SetActive(true);

        }
        else
        {
            ativeFrameObject = false;
            countObject.SetActive(false);

            SettingObject1.SetActive(false);
            SettingObject2.SetActive(false);
            SettingObject3.SetActive(false);

            FrameTilemap1.SetActive(false);
            FrameTilemap2.SetActive(false);
            FrameTilemap3.SetActive(false);

        }
    }

    void PlusButton()
    {
        if(!plusButton)
        {
            Debug.Log("+��ư ����");
            return;
        }
        if(frameCount < 3)
        {
            frameCount++;
            countText.text = frameCount.ToString();
        }
        CountOject();
    }

    void MinusButton()
    {
        if (!minusButton)
        {
            Debug.Log("+��ư ����");
            return;
        }
        if (frameCount > 1)
        {
            frameCount--;
            countText.text = frameCount.ToString();

        }
        CountOject();
    }

    int frameCount = 1;
    void CountOject()
    {
        if (frameCount == 1)
        {
            SettingObject1.SetActive(true);
            SettingObject2.SetActive(false);
            SettingObject3.SetActive(false);

            FrameTilemap1.SetActive(true);
            FrameTilemap2.SetActive(false);
            FrameTilemap3.SetActive(false);
        }
        else if (frameCount == 2)
        {
            SettingObject1.SetActive(true);
            SettingObject2.SetActive(true);
            SettingObject3.SetActive(false);

            FrameTilemap1.SetActive(true);
            FrameTilemap2.SetActive(true);
            FrameTilemap3.SetActive(false);
        }
        else if (frameCount == 3)
        {
            SettingObject1.SetActive(true);
            SettingObject2.SetActive(true);
            SettingObject3.SetActive(true);

            FrameTilemap1.SetActive(true);
            FrameTilemap2.SetActive(true);
            FrameTilemap3.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShowFrame : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] UploadImageData uploadImageData; //�� ��ư���� ���� ������ �ٸ�
    [SerializeField] Image image;
    [SerializeField] GameObject ShowImageObject;
    [SerializeField] GameObject ShowButton;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            image.sprite = uploadImageData.imageData;
            ShowButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ShowButton.SetActive(false);
        }
    }

}

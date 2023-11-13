using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;

public class FrameImage : MonoBehaviour
{
    private Button button;
    private Button showButton;
    private Texture2D texture;

    [SerializeField] UploadImageData uploadImageData;

    [SerializeField] Image image;
    [SerializeField] GameObject ShowImage;
    [SerializeField] Text FrameName;

    private string filePath = "";

    private void Awake()
    {
        button = GetComponent<Button>();
        showButton = transform.GetChild(1).GetComponent<Button>();
        button.onClick.AddListener(OpenFile);
        showButton.onClick.AddListener(ShowFrame);
    }

    private void OpenFile()
    {

#if UNITY_EDITOR
        filePath = EditorUtility.OpenFilePanel("Overwrite with png"
                                            , Application.streamingAssetsPath
                                            , "png");
#endif
        if (filePath.Length != 0)
        {
            WWW www = new WWW("file://" + filePath);
            texture = new Texture2D(32, 32);
            www.LoadImageIntoTexture(texture);

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            uploadImageData.imageData = sprite;
            uploadImageData.imageName = Path.GetFileNameWithoutExtension(filePath);
        }
    }

    public void ShowFrame()
    {
        if (!uploadImageData.imageData)
        {
            Debug.Log("이미지가 없음 UI 띄울 것");
            return;
        }
        FrameName.text = uploadImageData.imageName;
        image.sprite = uploadImageData.imageData;
        ShowImage.SetActive(true);
    }


}

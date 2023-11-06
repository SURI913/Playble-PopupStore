using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Image Data", menuName = "Scriptable Object/Image Data")]
public class UploadImageData : ScriptableObject
{
    [SerializeField]
    public string imageName;
    public Sprite imageData;
}

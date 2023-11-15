using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Sound Data", menuName = "Scriptable Object/Sound Data")]
public class SoundData : ScriptableObject
{
    [SerializeField] public Sound[] sound;
}

[Serializable]
public class Sound
{
    [SerializeField] public string soundName;
    [SerializeField] public AudioClip soundClip;
}

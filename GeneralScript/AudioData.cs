using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "Scriptable Object Asset/AudioData")]
public class AudioData : ScriptableObject
{
    public DATA<AudioClip> clip = new DATA<AudioClip>();
}

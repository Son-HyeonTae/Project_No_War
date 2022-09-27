using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region SoundDataRegion 
public abstract class SoundData : ScriptableObject
{
    public DATA<int> ID = new DATA<int>();
    public DATA<AudioClip> Clip = new DATA<AudioClip>();
}

#region SoundTypeReegion
public enum BGMSoundType
{
    StartMenu,
    Stage1,
    Stage2,
    Stage3,
    Stage4,
    Stage5,
    NONE
}
public enum SFXSoundType
{
    FireBullet,
    GrenadeExplosion,
    FlashBangExplosion,
    NONE
}
public enum UISoundType
{
    NONE
}
#endregion

public class BGMSound : SoundData
{
    public BGMSoundType bgmType;
    public bool bLoop;
}
public class SFXSound : SoundData
{
    public SFXSoundType sfxType;
}
public class UISound : SoundData
{
    public UISoundType uiType;
}
#endregion

/**
* 게임 사운드 매니저
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-30::15:48
*/
/*public class AudioManager : Singleton<AudioManager>
{
    [Range(0, 1)] public float MusicVolume;
    [Range(0, 1)] public float SoundVolume;

    [SerializeField] private AudioSource MusicSource;
    [SerializeField] private AudioSource SoundSource;

}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public float volume;

    AudioSource m_AudioSource;
    
    [Header("Basic")]
    public AudioClip HitSound;   

    [Header("Class Skill")]
    public AudioClip ClassSkill_General;
    public AudioClip Earth_EarthClass;
    public AudioClip Earth_EarthClassAdd;
    public AudioClip Wind_WindClassBuff;
    public AudioClip Water_WaterClass;
    public AudioClip Water_WaterClassAdd;
    public AudioClip Thunder_ThunderClass;

    [Header("Fire Item")]
    public AudioClip Fire_Beam;
    public AudioClip Fire_FireBite;
    public AudioClip Fire_Lava;
    public AudioClip Fire_MeteorStart;
    public AudioClip Fire_MeteorEnd;

    [Header("Earth Item")]
    public AudioClip Earth_Impale;
    public AudioClip Earth_Saw;
    public AudioClip Earth_Crash;
    public AudioClip Earth_Spike;

    [Header("Wind Item")]
    public AudioClip Wind_Shadow;
    public AudioClip Wind_Tornado;
    public AudioClip Wind_Slash;
    public AudioClip Wind_Cutter;

    [Header("Water Item")]
    public AudioClip Water_MineStart;
    public AudioClip Water_MineEnd;
    public AudioClip Water_Splash;
    public AudioClip[] Water_WaterWhip = new AudioClip[3];
    public AudioClip Water_IceShot;

    [Header("Thunder Item")]
    public AudioClip Thunder_Ball;
    public AudioClip Thunder_MineStart;
    public AudioClip Thunder_MineEnd;
    public AudioClip Thunder_Storm;
    public AudioClip Thunder_Veil;

    [Header("CC State")]
    public AudioClip Freeze_SFX;
    public AudioClip Burn_SFX;

    //사운드 중복 출력으로 인해 소리가 깨지는 걸 막기 위한 변수
    [Header("PlayMinNum")]
    public int PlayingHitSoundNum = 0;
    public int PlayingFire_MeteorEndSoundNum = 0;
    public int PlayingEarthSoundNum = 0;
    public int PlayingWind_TornadoSoundNum = 0;
    public int PlayingWaterSoundNum = 0;
    public int PlayingThunder_StormNum = 0;
    public int PlayingExtraSoundNum = 0;

    //_SM.SoundPlay(_SM.Fire_MeteorEnd);

    // Start is called before the first frame update
    void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
        instance = this;
    }
    
    public void SoundPlay(AudioClip _AudioClip, float _Volume = 0.15f)
    {// 사운드 재생 함수
        m_AudioSource.PlayOneShot(_AudioClip, _Volume);
    }
    
    public IEnumerator SoundPlayLoop(AudioClip _AudioClip, float _Volume = 0.15f, int time = 1, float term = 0.1f)
    {// 사운드 재생 함수 -> 소리를 연속으로 출력해야 할 경우
        for (int i = 0; i < time; i++)
        {
            m_AudioSource.PlayOneShot(_AudioClip, _Volume);
            yield return new WaitForSeconds(term);
        }
    }
    public IEnumerator MeteorSoundNum()
    {
        PlayingFire_MeteorEndSoundNum += 1;
        yield return new WaitForSeconds(0.2f);
        PlayingFire_MeteorEndSoundNum -= 1;
    }
    public IEnumerator TornadoSoundNum()
    {
        PlayingWind_TornadoSoundNum += 1;
        yield return new WaitForSeconds(0.1f);
        PlayingWind_TornadoSoundNum -= 1;
    }
    
    // 시간이 된다면 재생 함수도 사운드 매니져에서 관리하는게 어떨까 생각해서 일단 냅뒀습니다.
    //public void SoundPlaya(string snd)
    //{
    //    switch (snd)
    //    {
    //        case "":
    //            break;
    //        case "HitSound":
    //            PlaySound(HitSound,0);
    //            break;

    //        case "Fire_Beam":
    //            PlaySound(Fire_Beam, 0);
    //            break;
    //        case "Fire_FireBite":
    //            PlaySound(Fire_FireBite, 0);
    //            break;
    //        case "Fire_Lava":
    //            PlaySound(Fire_Lava, 0);
    //            break;
    //        case "Fire_MeteorStart":
    //            PlaySound(Fire_MeteorStart, 0);
    //            break;
    //        case "Fire_MeteorEnd":
    //            PlaySound(Fire_MeteorEnd, 0);
    //            break;


    //        case "Earth_Impale":
    //            PlaySound(Earth_Impale, 0);
    //            break;
    //        case "Earth_Saw":
    //            PlaySound(Earth_Saw, 0);
    //            break;
    //        case "Earth_Crash":
    //            PlaySound(Earth_Crash, 0);
    //            break;
    //        case "Earth_Spike":
    //            PlaySound(Earth_Spike, 0);
    //            break;
    //        case "Earth_EarthClass":
    //            PlaySound(Earth_EarthClass, 0);
    //            break;
    //        case "Earth_EarthClassAdd":
    //            PlaySound(Earth_EarthClassAdd, 0);
    //            break;


    //        case "Wind_WindClassBuff":
    //            PlaySound(Wind_WindClassBuff, 0);
    //            break;
    //        case "Wind_Shadow":
    //            PlaySound(Wind_Shadow, 0);
    //            break;
    //        case "Wind_Tornado":
    //            PlaySound(Wind_Tornado, 0);
    //            break;
    //        case "Wind_Slash":
    //            PlaySound(Wind_Slash, 0);
    //            break;
    //        case "Wind_Cutter":
    //            PlaySound(Wind_Cutter, 0);
    //            break;
    //        case "Water_WaterClass":
    //            PlaySound(Water_WaterClass, 0);
    //            break;
    //        case "Water_MineStart":
    //            PlaySound(Water_MineStart, 0);
    //            break;

    //    }
    //}
}

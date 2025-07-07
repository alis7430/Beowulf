using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name; //곡 이름
    public AudioClip cilp;
}

public class SoundManager : MonoBehaviour
{
    static public SoundManager instance;

    public AudioSource[] audioSourceSFX;
    public AudioSource audioSourceBGM;
    public AudioSource audioSourceUI;

    public string[] playSoundName;
    public Sound[] effectSounds;
    public Sound[] UISounds;
    public Sound[] bgmSounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    public void Start()
    {
        playSoundName = new string[audioSourceSFX.Length + 2];
    }
    public void PlaySFX(string _name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if (_name == effectSounds[i].name)
            {
                for (int j = 0; j < audioSourceSFX.Length; j++)
                {
                    if (!audioSourceSFX[j].isPlaying)
                    {
                        playSoundName[j] = effectSounds[i].name;
                        audioSourceSFX[j].clip = effectSounds[i].cilp;
                        audioSourceSFX[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 가용 AudioSource가 사용중입니다");
                return;
            }
        }
    }
    public void PlaySFX(string _name, float duration)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if (_name == effectSounds[i].name)
            {
                for (int j = 0; j < audioSourceSFX.Length; j++)
                {
                    if (!audioSourceSFX[j].isPlaying)
                    {
                        playSoundName[j] = effectSounds[i].name;
                        audioSourceSFX[j].clip = effectSounds[i].cilp;
                        audioSourceSFX[j].PlayDelayed(duration);
                        return;
                    }
                }
                Debug.Log("모든 가용 AudioSource가 사용중입니다");
                return;
            }
        }
    }
    public void StopAllSFX()
    {
        for (int i = 0; i < audioSourceSFX.Length; i++)
        {
            audioSourceSFX[i].Stop();
        }
    }

    public void StopSFX(string _name)
    {
        for (int i = 0; i < audioSourceSFX.Length; i++)
        {
            if (playSoundName[i] == _name)
            {
                audioSourceSFX[i].Stop();
                break;
            }
        }
    }
    public void PlayUISound(string _name)
    {
        for (int i = 0; i < UISounds.Length; i++)
        {
            if (_name == UISounds[i].name)
            {
                audioSourceUI.clip = UISounds[i].cilp;
                audioSourceUI.Play();
            }
        }
    }
    public void StopUISound()
    {
        audioSourceUI.Stop();
    }
    public void PlayBGM(string _name)
    {
        for (int i = 0; i < bgmSounds.Length; i++)
        {
            if (_name == bgmSounds[i].name)
            {
                audioSourceBGM.clip = bgmSounds[i].cilp;
                audioSourceBGM.Play();
            }
        }
    }
    public void StopBGM()
    {
        audioSourceBGM.Stop();
    }
}
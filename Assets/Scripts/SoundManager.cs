using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Lick,
    BornTadpole0,
    BornTadpole1,
    Ouch,
    KillTadpole
}

public class SoundManager : MonoBehaviour
{
    public AudioSource au_lick;
    public AudioSource au_bornT0;
    public AudioSource au_bornT1;
    public AudioSource au_ouch;
    public AudioSource au_killTadpole;

    private Dictionary<SoundType, float> dic_SoundStartTime = new Dictionary<SoundType, float>();

    public void Init()
    {
        InitTime();
    }

    public void InitTime()
    {
        dic_SoundStartTime.Clear();
        dic_SoundStartTime.Add(SoundType.Lick, 2f);
        dic_SoundStartTime.Add(SoundType.BornTadpole0, 0.2f);
        dic_SoundStartTime.Add(SoundType.BornTadpole1, 0.4f);
        dic_SoundStartTime.Add(SoundType.Ouch, 0.2f);
        dic_SoundStartTime.Add(SoundType.KillTadpole, 0.3f);
    }

    public float GetTime(SoundType soundType)
    {
        if (dic_SoundStartTime.ContainsKey(soundType))
        {
            return dic_SoundStartTime[soundType];
        }
        else
        {
            return 0.2f;
        }
    }

    public void PlaySound(SoundType soundType)
    {
        AudioSource tempSound = au_ouch;

        switch (soundType)
        {
            case SoundType.Lick:
                tempSound = au_lick;
                break;
            case SoundType.BornTadpole0:
                tempSound = au_bornT0;
                break;
            case SoundType.BornTadpole1:
                tempSound = au_bornT1;
                break;
            case SoundType.Ouch:
                tempSound = au_ouch;
                break;
            case SoundType.KillTadpole:
                tempSound = au_killTadpole;
                break;
        }
        tempSound.time = GetTime(soundType);
        tempSound.Play();
    }
}

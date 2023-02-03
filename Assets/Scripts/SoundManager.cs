using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Lick,
    BornTadpole
}

public class SoundManager : MonoBehaviour
{
    public AudioSource au_lick;
    public AudioSource au_bornTadpole;

    public void Init()
    {

    }

}

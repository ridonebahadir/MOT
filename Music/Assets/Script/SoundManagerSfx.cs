using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerSfx : MonoBehaviour
{
    [Header("AUDÝO SFX")]
    public static AudioClip glassBreak;
    public static AudioClip Explosion;
    static AudioSource audioSrc;
    private void Start()
    {
        glassBreak = Resources.Load<AudioClip> ("GlassBreak");
        Explosion = Resources.Load<AudioClip> ("Explosion");
        audioSrc = GetComponent<AudioSource>();
    }
    public static void PlaySfx(string clip)
    {
        switch (clip)
        {
            case "GlassBreakk":
                audioSrc.PlayOneShot(glassBreak);
                break;
            case "Explosion":
                audioSrc.PlayOneShot(Explosion);
                break;
            default:
                break;
        }
    }
}

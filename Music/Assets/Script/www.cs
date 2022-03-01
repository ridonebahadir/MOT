using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class wwww : MonoBehaviour
{
    public string Link = "https://ciihuy.com/downloads/music.mp3";
    public AudioMeasureCS audioMeasureCS;
    public InputField InputField;
    AudioSource audioSource;
    AudioClip myClip;

    [System.Obsolete]
    void Start()
    {
        audioSource = audioMeasureCS.audioSource;
       
        Debug.Log("Starting to download the audio...");
    }
    public void Play() { 
    }
    [System.Obsolete]
    IEnumerator GetAudioClip()
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(Link, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                myClip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = myClip;
                audioSource.Play();
                Debug.Log("Audio is playing.");
            }
        }
    }


    public void pauseAudio()
    {
        audioSource.Pause();
    }

    public void playAudio()
    {
        audioSource.Play();
    }

    public void stopAudio()
    {
        audioSource.Stop();

    }
}
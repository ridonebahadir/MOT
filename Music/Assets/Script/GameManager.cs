using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
//using DG.Tweening;
using UnityEngine.Networking;
using TMPro;
using YoutubePlayer;

public class GameManager : MonoBehaviour
{
   
   
    public Player playerCs;
    public RectTransform StartPanel;
    public RectTransform TutorialPanel;
    public Text MusicName;
    public Text MusicPriceText;
    public AudioClip[] audioClip;
    //public AudioClip[] audioClipTutorial;
    //public int[] MusicPrice;
    public int number;
    public AudioMeasureCS audioMeasureCS;
    public cam_follow cam_Follow;
    private int TotalScore;
    public Text TotalScoreText;
    public Button BuyButton;

    public GameObject WaveTransfrom;

    [Header("Mikrofon")]
    public bool useMicrofone;
    public bool useUrl;
    public string selectMicrofon;
    public AudioMixerGroup mixerGroupMicrophone, mixerGroupMaser;

    [Header("WWWW")]
    public string Link;
    public InputField InputField;
    AudioClip myClip;
    
    [Header("PANELS")]
    //public int measureLevel;
    public GameObject tutorialPanel;
    public GameObject startPanel;

    [Header("LEVEL")]
    public GameObject planets;
    public TextMeshPro[] plakTextMeh;
    public int level = 0;
    public int rank = 0;
    //public GameObject levels;
    public RectTransform Scroll;
    //public int ScrollX = 0;
    public Slider slider;
    public Slider sliderYoutube;
    public Sprite[] sprite;
    int sliderValue;
    int sliderValueYoutube;
    public GameObject Content;
    public float ContentX;
    public GameObject ContentYoutube;
    [Header("YOUTUBE")]
    public GameObject youtubePanel;
    public PrepareYoutubeVideo prepareYoutubeVideo;
    //[Header("ATTATION")]
    //public Text[] attationText;
    private void Start()
    {
        ContentX = PlayerPrefs.GetFloat("ContentX",360);
        Content.GetComponent<RectTransform>().position = new Vector3(ContentX, Content.transform.position.y, Content.transform.position.z);

        sliderValue = (int)slider.value;
        sliderValueYoutube = (int)sliderYoutube.value;
          //level = PlayerPrefs.GetInt("LEVEL");
          rank = PlayerPrefs.GetInt("RANK");
        if (rank<10)
        {
            planets.transform.GetChild(rank).GetChild(sliderValue).gameObject.SetActive(true);

        }
        //ScrollX = PlayerPrefs.GetInt("ScrollX");
        //Scroll.transform.localPosition = new Vector3(ScrollX,0 ,0);
        Slider();
        for (int i = 0; i < Content.transform.childCount-1; i++)
        {
            Content.transform.GetChild(i).GetChild(2).GetChild(0).GetComponent<Text>().text = audioClip[i].name;
          
        }

        for (int i = 0; i <= rank; i++)
        {
            Content.transform.GetChild(i).GetChild(3).GetComponent<Image>().color = Color.green;
            Content.transform.GetChild(i).GetChild(3).GetComponent<Button>().interactable = true;
            Content.transform.GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(false);
            Content.transform.GetChild(i).GetChild(0).GetChild(1).gameObject.SetActive(true);
        }

       






          //MusicPriceText.text = "Price = " + MusicPrice[number].ToString() + " Total Score";
          TotalScore = PlayerPrefs.GetInt("TOTALSCORE");
        TotalScoreText.text ="Total Score = " +TotalScore.ToString();
        //MusicName.text = audioClip[number].name;
    }









   
    public void Play(int PlayLevel)
    {
        if (youtube)
        {
            //planets.transform.GetChild(rank).GetChild(sliderValueYoutube).gameObject.SetActive(false);
            sliderValueYoutube = (int)sliderYoutube.value;
            playerCs.levelId = PlayLevel;
            level = PlayLevel;
            //planets.transform.GetChild(level).GetChild(sliderValueYoutube).gameObject.SetActive(true);
            plakTextMeh[0].text = prepareYoutubeVideo.musicName[level];
        }
        else
        {
            planets.transform.GetChild(rank).GetChild(sliderValue).gameObject.SetActive(false);
            sliderValue = (int)slider.value;
            playerCs.levelId = PlayLevel;
            level = PlayLevel;
            planets.transform.GetChild(level).GetChild(sliderValue).gameObject.SetActive(true);
            ListPlay();
            plakTextMeh[level].text = audioClip[level].name;
        }
       
       
        
        //Debug.Log("sliderValue" + sliderValue);
        //AudioMeasureCS.run = true;
        audioMeasureCS.enabled = true;
        playerCs.gravityScale = playerCs.gravityValue;
        StartPanel.gameObject.SetActive(false);
        WaveTransfrom.GetComponent<Animator>().enabled = false;

       


       
    }

   
    public void ListPlay()
    {
        audioMeasureCS.audioSource.outputAudioMixerGroup = mixerGroupMaser;
        audioMeasureCS.audioSource.clip = audioClip[level];
        audioMeasureCS.audioSource.Play();
    }
   
    public void ButtonMicOpen()
    {
        useMicrofone = true;
    }
    public void ButtonUrlOpen()
    {
        youtube = true;
        youtubePanel.SetActive(false);
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
                audioMeasureCS.audioSource.clip = myClip;
                audioMeasureCS.audioSource.Play();
                Debug.Log("Audio is playing.");
            }
        }
    }

    [System.Obsolete]
    public void PlayOnLink()
    {
        Link = InputField.text;
        StartCoroutine(GetAudioClip());
    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    public bool youtube;
    public void YoutubeButton()
    {
        
            youtubePanel.SetActive(true);
            //sliderValueYoutube = (int)sliderYoutube.value;
            //planets.transform.GetChild(rank).GetChild(sliderValue).gameObject.SetActive(false);
            SliderYouTube();
            youtube = true;
            
        
      
    }
    
    
  
    public void Slider()
    {
        
        sliderValue = (int)slider.value;
        
        switch (sliderValue)
        {
            case 0:
                slider.handleRect.GetChild(0).GetComponent<Text>().text ="PLANET";
                if (rank<10)
                {
                    planets.transform.GetChild(rank).GetChild(sliderValue).gameObject.SetActive(true);
                    planets.transform.GetChild(rank).GetChild(sliderValue + 1).gameObject.SetActive(false);
                }
              

                for (int i = 0; i < Content.transform.childCount; i++)
                {
                    Content.transform.GetChild(i).GetChild(1).GetChild(sliderValue).gameObject.SetActive(true);
                    Content.transform.GetChild(i).GetChild(1).GetChild(sliderValue+1).gameObject.SetActive(false);
                }
                break;
            case 1:
                slider.handleRect.GetChild(0).GetComponent<Text>().text = "RECORD";
                if (rank<10)
                {
                    planets.transform.GetChild(rank).GetChild(sliderValue).gameObject.SetActive(true);
                    planets.transform.GetChild(rank).GetChild(sliderValue - 1).gameObject.SetActive(false);
                }

                for (int i = 0; i < Content.transform.childCount; i++)
                {
                    Content.transform.GetChild(i).GetChild(1).GetChild(sliderValue).gameObject.SetActive(true);
                    Content.transform.GetChild(i).GetChild(1).GetChild(sliderValue - 1).gameObject.SetActive(false);
                }
                break;
            

            default:
                break;
        }

    }

    public void SliderYouTube()
    {

        sliderValueYoutube = (int)sliderYoutube.value;

        switch (sliderValueYoutube)
        {
            case 0:
                sliderYoutube.handleRect.GetChild(0).GetComponent<Text>().text = "PLANET";
                planets.transform.GetChild(level).GetChild(sliderValueYoutube).gameObject.SetActive(true);
                planets.transform.GetChild(level).GetChild(sliderValueYoutube + 1).gameObject.SetActive(false);

                for (int i = 0; i < PlayerPrefs.GetInt("Save")+1; i++)
                {
                    ContentYoutube.transform.GetChild(i).GetChild(1).GetChild(sliderValueYoutube).gameObject.SetActive(true);
                    ContentYoutube.transform.GetChild(i).GetChild(1).GetChild(sliderValueYoutube + 1).gameObject.SetActive(false);
                }
                break;
            case 1:
                sliderYoutube.handleRect.GetChild(0).GetComponent<Text>().text = "RECORD";
                planets.transform.GetChild(level).GetChild(sliderValueYoutube).gameObject.SetActive(true);
                planets.transform.GetChild(level).GetChild(sliderValueYoutube - 1).gameObject.SetActive(false);

                for (int i = 0; i < PlayerPrefs.GetInt("Save")+1; i++)
                {
                    ContentYoutube.transform.GetChild(i).GetChild(1).GetChild(sliderValueYoutube).gameObject.SetActive(true);
                    ContentYoutube.transform.GetChild(i).GetChild(1).GetChild(sliderValueYoutube - 1).gameObject.SetActive(false);
                }
                break;


            default:
                break;
        }

    }
    //public void PlayYoutube(int PlayLevel)
    //{
    //    planets.transform.GetChild(rank).GetChild(sliderValueYoutube).gameObject.SetActive(false);
    //    sliderValueYoutube = (int)sliderYoutube.value;
    //    playerCs.levelId = PlayLevel;
    //    level = PlayLevel;
    //    //foreach (Transform item in planets.transform)
    //    //{
    //    //    item.gameObject.SetActive(false);
    //    //}
    //    plakTextMeh[level].text = audioClip[level].name;
    //    planets.transform.GetChild(level).GetChild(sliderValueYoutube).gameObject.SetActive(true);
    //    Debug.Log("sliderValue" + sliderValue);
    //    //AudioMeasureCS.run = true;
    //    audioMeasureCS.enabled = true;
    //    playerCs.gravityScale = playerCs.gravityValue;
    //    StartPanel.gameObject.SetActive(false);
    //    WaveTransfrom.GetComponent<Animator>().enabled = false;

        



    //}
}

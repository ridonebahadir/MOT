using System;
using System.Collections;

using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

namespace YoutubePlayer
{
    public class PrepareYoutubeVideo : MonoBehaviour
    {

       
        public YoutubePlayer youtubePlayer;
        public DownloadYoutubeVideo download;
        public InputField UrlInput;
        //public Button playUrlButton;
        public Transform Content;
       
        //public Button playButton;
        public int saveNumber;
        public string[] musicName;
        public async void Prepare()
        {
           
            youtubePlayer.youtubeUrl = UrlInput.text;
            //playUrlButton.gameObject.SetActive(true);
            //playButton.gameObject.SetActive(false);
            await youtubePlayer.PrepareVideoAsync();

           
            

        }
        private void Start()
        {

            string saveName = saveNumber.ToString() + ".mp4";
            string curFile = Application.persistentDataPath + "/" + saveName;


            if (File.Exists(curFile))
            {
                saveNumber = PlayerPrefs.GetInt("Save");
                Array.Resize(ref musicName, saveNumber + 1);
                for (int i = 0; i < musicName.Length; i++)
                {
                    musicName[i] = PlayerPrefs.GetString("MusicName" + i);
                    Content.GetChild(i).gameObject.SetActive(true);
                    Content.GetChild(saveNumber).GetChild(3).GetComponent<Button>().interactable = true;
                    string value = musicName[i];
                    value = value.Substring(0, value.Length - 4);
                    Content.GetChild(i).GetChild(2).GetChild(0).GetComponent<Text>().text = value;
                }
            }
            else
            {
                saveNumber = PlayerPrefs.GetInt("Save");
                Array.Resize(ref musicName, saveNumber + 1);
                for (int i = 1; i < musicName.Length; i++)
                {
                    musicName[i] = PlayerPrefs.GetString("MusicName" + i);
                    Content.GetChild(i).gameObject.SetActive(true);
                    string value = musicName[i];
                    value = value.Substring(0, value.Length - 4);
                    Content.GetChild(i).GetChild(2).GetChild(0).GetComponent<Text>().text = value;
                }
            }

           
           
        }
        //public void PlayUrl()
        //{

        //    videoUrl = @Path.GetFullPath(download.filePathSave);
        //    youtubePlayer.VideoPlayer.url = "file:///" + videoUrl;
        //    youtubePlayer.VideoPlayer.Play();
        //}

        [System.Obsolete]
        public void DowLoad()
        {
            
            StartCoroutine(DownloadVideoNew());
        }

        [System.Obsolete]

       

        IEnumerator DownloadVideoNew()
        {
            
            saveNumber = PlayerPrefs.GetInt("Save");
            Prepare();
            _ = youtubePlayer.DownloadVideoAsync();

            Content.GetChild(saveNumber).gameObject.SetActive(true);
            Content.GetChild(saveNumber).GetChild(3).GetComponent<Button>().interactable = false;
            Content.GetChild(saveNumber).GetChild(2).GetChild(0).GetComponent<Text>().text = "PLEASE WAIT FOR DOWNLOAD";


            yield return new WaitForSeconds(5f);
            UnityWebRequest www = UnityWebRequest.Get(youtubePlayer.VideoPlayer.url);
            yield return www.SendWebRequest();

            if (!string.IsNullOrEmpty(www.error)) // Resmi indirirken bir hata alıp almadığımıza bak
                
            Content.GetChild(saveNumber).GetChild(2).GetChild(0).GetComponent<Text>().text = "PLEASE TRY AGAIN";
            else
            {
                string saveName = saveNumber.ToString() + ".mp4";
                string curFile = Application.persistentDataPath + "/" + saveName;


                if (File.Exists(curFile))
                {
                    saveNumber = PlayerPrefs.GetInt("Save");
                    saveNumber++;
                    PlayerPrefs.SetInt("Save", saveNumber);
                    Debug.Log("SAVE NUmber"+saveNumber);
                }

                saveNumber = PlayerPrefs.GetInt("Save");
                Array.Resize(ref musicName, saveNumber+1);
                saveName = saveNumber.ToString() + ".mp4";
                string PathFile = Path.Combine(Application.persistentDataPath, saveName);
                File.WriteAllBytes(PathFile, www.downloadHandler.data);
                Debug.Log("m " + PathFile);
                Debug.Log("Music name"+youtubePlayer.music);

                musicName[saveNumber] = youtubePlayer.music;
                Content.GetChild(saveNumber).GetChild(3).GetComponent<Button>().interactable = true;
                for (int i = 0; i <= musicName.Length; i++)
                {
                    PlayerPrefs.SetString("MusicName" + i, musicName[i]);
                    musicName[i] = PlayerPrefs.GetString("MusicName" + i);
                    Content.GetChild(i).gameObject.SetActive(true);
                    string value = musicName[i];
                    value = value.Substring(0, value.Length - 4);
                    Content.GetChild(i).GetChild(2).GetChild(0).GetComponent<Text>().text = value;
                }

               

            }
        }

        public void Play(int number)
        {
           
            string saveName= number.ToString() + ".mp4";
            var vPlayer = youtubePlayer.VideoPlayer;
            vPlayer.url = Application.persistentDataPath + "/"+ saveName;
            vPlayer.prepareCompleted += Prepared;
            vPlayer.Prepare();
           
            
        }
        void Prepared(UnityEngine.Video.VideoPlayer vPlayer)
        {
            Debug.Log("End reached!");
           
            vPlayer.Play();
        }
       
        
      
    }
}

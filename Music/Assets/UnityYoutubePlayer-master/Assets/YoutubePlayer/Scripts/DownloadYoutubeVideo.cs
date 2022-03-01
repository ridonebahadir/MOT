using System;
using System.IO;
using UnityEngine;

namespace YoutubePlayer
{
    public class DownloadYoutubeVideo : MonoBehaviour
    {
        public YoutubePlayer youtubePlayer;
        public Environment.SpecialFolder destination;
        public string filePath;
        public string filePathMobile;
        public string filePathMobileSave;
        public string filePathSave;
        public void Start()
        {
            //filePathSave = PlayerPrefs.GetString("FilePath");
            //filePathMobileSave = PlayerPrefs.GetString("FilePathMobile");
        }
        public async void Download()
        {
            //Debug.Log("Downloading, please wait...");

            //var videoDownloadTask = youtubePlayer.DownloadVideoAsync(Environment.GetFolderPath(destination));
            //filePath = await videoDownloadTask;
            //PlayerPrefs.SetString("FilePath", filePath);
            //filePathSave = PlayerPrefs.GetString("FilePath");
            //Debug.Log($"Video saved to {Path.GetFullPath(filePath)}");







            //var videoDownloadTaskMobile = youtubePlayer.DownloadVideoAsync(Application.persistentDataPath);
            //filePathMobile = await videoDownloadTaskMobile;
            //PlayerPrefs.SetString("FilePathMobile", filePathMobile);
            //filePathMobileSave = PlayerPrefs.GetString("FilePathMobile");
            //Debug.Log($"Video saved to {Path.GetFullPath(filePathMobile)}");
          
           

         

        }
    }
}

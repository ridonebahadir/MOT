using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioMeasureCS : MonoBehaviour
{
    public GameManager gameManager;
    public Transform WaveFormTransfrom;
    public GameObject WaveFormObject;
    public Material material;
    public GameObject Coin;
    public GameObject Speed;
    public GameObject Escape;
    GameObject coinClone;
    GameObject SpeedClone;
    GameObject EscapeClone;
    public GameObject Player;
    public static bool run;
    public AudioSource audioSource;

    public float RmsValue;
    public float DbValue;
    public float PitchValue;

    public float visualModifier = 50.0f;
    public float maxVisualScale = 25.0f;
    public float smoothSpeed = 10.0f;
    public float keepPercentge = 0.5f;

    private const int QSamples = 512;
    //private const float RefValue = 0.1f;
    //private const float Threshold = 0.02f;

    float[] _samples;
    private float[] _spectrum;
    private float _fSample;

    private Transform[] visualList;
    private float[] visualScale;
    public int amnvisual = 64;
    public float rotateSpeed;
    public float rotateSpeedStart;

    public List<Color> valuesList;
  
    private float colorIntensity;
    [Header("UPDATE COLOR")]
    //public Material changeColorObjectMaterial;
    //public Color minColor;
    //public Color maxColor;
    //[Space(10)]
    //public Material changeColorObjectMaterial2;
    //public Color minColor2;
    //public Color maxColor2;

    [Space(10)]
    public Material BackGroundColor;
    public Material GlowColor;
    public Color minColorBackGroundColor;
    public Color maxColorBackGroundColor;
    //public Light pointLight;
    
    [Header("COLOR")]
    public Color[] color;

    [Header("Shake")]

    public float ShakePower;
    public float ShakeTime;

    public float ShakePowerWrong;
    public float ShakeTimeWrong;
    
    private Vector3 velocity;
    //[Header("PANELS")]
    //public GameObject startPanel;
    //public GameObject tutorialpanel;
   
    void Start()
    {
       
        
        rotateSpeedStart = rotateSpeed;

        //colors[0] = Color.cyan;
        //colors[1] = Color.red;
        //colors[2] = Color.green;
       
        //colors[3] = Color.yellow;
        //colors[4] = Color.magenta;





        audioSource = GetComponent<AudioSource>();
       

       
        _samples = new float[QSamples];
        _spectrum = new float[QSamples];
        _fSample = AudioSettings.outputSampleRate;
        
        SpawnCircle();
       




        if ((gameManager.level<2)&&(!gameManager.useUrl))
        {
            switch (gameManager.level)
            {
                case 0:
                    InvokeRepeating("SpeedSpawn", 2f, 32f);
                   


                    break;
                case 1:
                    InvokeRepeating("EscapeSpawn", 1f, 16f);
                    InvokeRepeating("SpeedSpawn", 2f, 32f);
                  
                    break;
              
            }
        }
        else
        {
            InvokeRepeating("CoinSpawn", 0f, 8f);
            InvokeRepeating("SpeedSpawn", 2f, 32f);
            InvokeRepeating("EscapeSpawn", 1f, 16f);
            Player.GetComponent<Player>().oilCountSlider.gameObject.SetActive(true);


            if (gameManager.youtube==false)
            {
               
                Player.GetComponent<Player>().oilCountSlider.maxValue = audioSource.clip.length;
                Player.GetComponent<Player>().oilCountSlider.value = audioSource.clip.length;
            }
            else
            {
                Player.GetComponent<Player>().oilCountSlider.maxValue = 100;
                Player.GetComponent<Player>().oilCountSlider.value = 100;

            }
            



        }

       



        for (int i = 0; i < color.Length; i++)
        {
            ChangeColor(i);
        }

       
    }
    public void Play()
    {
        //audioSource = GetComponent<AudioSource>();
        //_samples = new float[QSamples];
        //_spectrum = new float[QSamples];
        //_fSample = AudioSettings.outputSampleRate;
        Player.GetComponent<Player>().enabled = true;
       
        //Player.GetComponent<Player>().oilCountSlider.maxValue = audioSource.time;

    }
    void WaveFormTurn()
    {
        WaveFormTransfrom.transform.Rotate(0, 0, +rotateSpeed);
        
    }
    private void SpawnCircle()
    {
        visualScale = new float[amnvisual];
        visualList = new Transform[amnvisual];
        Vector3 center = Vector3.zero;
        float radius = 10.0f;

        for (int i = 0; i < amnvisual; i++)
        {
            float ang = i * 1.0f / amnvisual;
            ang = ang * Mathf.PI * 2;
            float x = center.x + Mathf.Cos(ang) * radius;
            float y = center.y + Mathf.Sin(ang) * radius;

            Vector3 pos = center + new Vector3(x, y, 0);
            GameObject go = /*GameObject.CreatePrimitive(PrimitiveType.Cube)*/Instantiate(WaveFormObject) as GameObject;
            go.transform.parent = WaveFormTransfrom.GetChild(0).transform;
            go.transform.position = pos;
            go.transform.rotation = Quaternion.LookRotation(Vector3.forward, pos);
            visualList[i] = go.transform;
        }
    }

    void Update()
    {


        //Debug.Log("audioSource.time " + audioSource.clip.length);
       
        if (gameManager.level>1)
        {
            Player.GetComponent<Player>().oilCountSlider.value -= 1 * Time.deltaTime;
        }
            AnalyzeSound();
            UpdateVisual();
            WaveFormTurn();
            UpdateColor();

        

    }
    
    Color[] colors = new Color[6];
    int a;
    int b;
  
    private void UpdateColor()
    {
        a = Random.Range(0, colors.Length);
        b = Random.Range(0, colors.Length);
        colorIntensity -= Time.deltaTime * smoothSpeed;
        if (colorIntensity<DbValue/40)
        {
            colorIntensity = DbValue / 40;
        }
        //changeColorObjectMaterial.color = Color.Lerp(maxColor, minColor, -colorIntensity);
        //changeColorObjectMaterial2.color = Color.Lerp(maxColor2, minColor2, -colorIntensity);
        //pointLight.range = Mathf.Lerp(0,1000,-colorIntensity);
        //var main = BackGroundColor.main;
        //main.startColor= Color.Lerp(maxColorBackGroundColor, minColorBackGroundColor, -colorIntensity);

        BackGroundColor.color = Color.Lerp(colors[a], colors[b], -colorIntensity);
        float abc = Mathf.Lerp(4, 0, -colorIntensity);
       
        //GlowColor.SetFloat("_MKGlowPower",abc );
        //GlowColor.SetColor("_MKGlowColor", Color.Lerp(colors[a], colors[b], -colorIntensity));
        /*Color.Lerp(colors[a], colors[b], -colorIntensity);*/


    }

    void SpawnLine()
    {
        visualScale = new float[amnvisual];
        visualList = new Transform[amnvisual];

        for (int i = 0; i < amnvisual; i++)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube) as GameObject;
            visualList[i] = go.transform;
            visualList[i].position = Vector3.right * i;
        }
    }
    public void StartButton()
    {
        _samples = new float[QSamples];
        _spectrum = new float[QSamples];
        _fSample = AudioSettings.outputSampleRate;
        GetComponent<AudioSource>().Play();
        SpawnLine();
    }
    void UpdateVisual()
    {
        int visualIndex = 0;
        int spectrumIndex = 0;
        int averageSize = (int)((QSamples * keepPercentge) / amnvisual);

        while (visualIndex < amnvisual)

        {

            int j = 0;

            float sum = 0;

            while (j < averageSize)

            {

                sum += _spectrum[spectrumIndex];

                spectrumIndex++;

                j++;



            }
            float scaley = sum / averageSize * visualModifier;
            visualScale[visualIndex] -= Time.deltaTime * smoothSpeed;
            if (visualScale[visualIndex] < scaley)
            {
                visualScale[visualIndex] = scaley;
            }
            if (visualScale[visualIndex] > maxVisualScale)
            {
                visualScale[visualIndex] = maxVisualScale;
            }
           
            visualList[visualIndex].localScale = Vector3.one + Vector3.up * visualScale[visualIndex];
            visualIndex++;
        }




    }

    void AnalyzeSound()
    {
        GetComponent<AudioSource>().GetOutputData(_samples, 0); 
        int i = 0;
        float sum = 0;
        for (; i < QSamples; i++)
        {
            sum = _samples[i] * _samples[i]; 
        }
        RmsValue = Mathf.Sqrt(sum / QSamples); 
        DbValue = 20 * Mathf.Log10(RmsValue / 0.1f); 
        if (DbValue < -160) DbValue = -160;
                                            
        GetComponent<AudioSource>().GetSpectrumData(_spectrum, 0, FFTWindow.BlackmanHarris);
        float maxV = 0;
        var maxN = 0;
        for (i = 0; i < QSamples; i++)
        { 
            if (!(_spectrum[i] > maxV) || !(_spectrum[i] > 0.0f))
                continue;

            maxV = _spectrum[i];
            maxN = i;
        }
        float freqN = maxN; 
        if (maxN > 0 && maxN < QSamples - 1)
        { 
            var dL = _spectrum[maxN - 1] / _spectrum[maxN];
            var dR = _spectrum[maxN + 1] / _spectrum[maxN];
            freqN += 0.5f * (dR * dR - dL * dL);
        }
        PitchValue = freqN * (_fSample / 2) / QSamples; 
    }

    void CoinSpawn()
    {
        int randomChild = Random.Range(0, WaveFormTransfrom.GetChild(0).childCount);
        coinClone = Instantiate(Coin, WaveFormTransfrom.transform.position+new Vector3(0,Random.Range(-75,-90),0), Quaternion.identity);
        //coinClone = Instantiate(Coin, new Vector3(WaveFormTransfrom.GetChild(0).GetChild(randomChild).transform.position.x, WaveFormTransfrom.GetChild(0).GetChild(randomChild).transform.position.y + 60f), Quaternion.identity);
        coinClone.transform.parent = WaveFormTransfrom.GetChild(1).transform;
    }
    void SpeedSpawn()
    {
        int randomChild = Random.Range(0, WaveFormTransfrom.GetChild(0).childCount);
        SpeedClone = Instantiate(Speed, WaveFormTransfrom.transform.position + new Vector3(0, Random.Range(-55, -70), 0), Quaternion.identity);
        //SpeedClone = Instantiate(Speed, new Vector3(WaveFormTransfrom.GetChild(0).GetChild(randomChild).transform.position.x, WaveFormTransfrom.GetChild(0).GetChild(randomChild).transform.position.y + 50f), Quaternion.identity);
        SpeedClone.transform.parent = WaveFormTransfrom.GetChild(1).transform;
    }
    void EscapeSpawn()
    {
        int randomChild = Random.Range(0, WaveFormTransfrom.GetChild(0).childCount);
        EscapeClone = Instantiate(Escape, WaveFormTransfrom.transform.position + new Vector3(0, Random.Range(-40, -50), 0), Quaternion.identity);
        //EscapeClone = Instantiate(Escape, new Vector3(WaveFormTransfrom.GetChild(0).GetChild(randomChild).transform.position.x, WaveFormTransfrom.GetChild(0).GetChild(randomChild).transform.position.y + 40f), Quaternion.identity);
        EscapeClone.transform.parent = WaveFormTransfrom.GetChild(1).transform;
    }


    void DestroySpeed()
    {
        Destroy(SpeedClone);
    }

    void ChangeColor(int a)
    {

        for (int i = a; i < WaveFormTransfrom.GetChild(0).transform.childCount; i += color.Length)
        {
            //WaveFormTransfrom.GetChild(0).GetChild(i).GetChild(0).GetComponent<Renderer>().material.color = color[a];
            //valuesList.Add(WaveFormTransfrom.GetChild(0).GetChild(i).GetChild(0).GetComponent<Renderer>().material.color);
            material.color = color[a];
            valuesList.Add(WaveFormTransfrom.GetChild(0).GetChild(i).GetChild(0).GetComponent<Renderer>().material.color);

        }

    }
   
    public void Shake()
    {
       
        gameObject.transform.DOShakePosition(ShakeTime, ShakePower, fadeOut: true);
    }
    public void ShakeWrong()
    {

        gameObject.transform.DOShakePosition(ShakeTimeWrong, ShakePowerWrong, fadeOut: true);
        
    }
}
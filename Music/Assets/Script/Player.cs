using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public RhythmVisualizatorPro rhythm;
    public Material fog;
    public Light pointLight;
    public int levelId;
    public GameObject FloatingParent;
    public Image damageImage;
    public GameObject parentWaveForm;
    public Button SkipButton;
    private Material Cube;
    public GameObject HeadSet;
    public Button nextLevel;
    public GameObject FinishCamera;
    public AudioMeasureCS measureCS;
    public GameManager gameManager;
    Rigidbody rb;
    [Range(0,2000)]
    public float boundPowwer;
    public float stoopSpeed;
    public ParticleSystem particle;
    public ParticleSystem Earthparticle;
    public ParticleSystem Earthparticle_bir;
    public Color PlayerColorNumber;
    public Slider oilCountSlider;
    public Transform ZoomInCamera;
    public float ZoomInCameraSpeed;
    public float ZoomOutCameraSpeed;
    public Transform EarthTransfrom;
    private Vector3 mesafe;
    //public Text WarningOilText;
    public Transform canvas;
    private int stickCount;
    public Text stickCountText;
    int health;
    
    public Transform HealthTransfrom;
   
    private int SpeedCount;
    public Text SpeedCountText;

    [Header("COMBO")]
    public int ComboCount;
    public Text ComboCountText;
    public TextMeshPro ComboCountTextMesh;
    public GameObject ComboCountTextMeshAnim;
    

    [Header("SCORE")]
    private int Score;
    private int TotalScore;
    public TextMeshPro ScoreFinishText;
    public Text ScoreFinish;
    public Text ScoreText;
   

    [Header("EARTHEXPO")]
    public Color earthExpoBlue;
    public Color earthExpoGreen;
    void Start()
    {
        

        health = 3;
        Cube = GetComponent<Renderer>().material;
       
        TotalScore = PlayerPrefs.GetInt("TOTALSCORE");
        //mesafe = Camera.main.transform.position - gameObject.transform.position;
        //Camera.main.transform.position = ZoomInCamera.position;
        stickCount = measureCS.amnvisual;
        stickCountText.text = "End " +stickCount.ToString()+" Stick";
        rb = GetComponent<Rigidbody>();
        //PlayerColor();
        rb.useGravity = false;
        //fog.color = gameObject.GetComponent<Renderer>().material.color;
        PlayerColor();
    }

   
    void FixedUpdate()
    {
       
        
        if (Input.GetMouseButton(0))
        {

            rb.velocity = Vector2.zero;
            rb.velocity = Vector2.up * boundPowwer * Time.deltaTime * 1.25f;
            transform.eulerAngles = new Vector3(0,0, 60.0f);
            gravityScale = 0;

        }
        else
        {
            gravityScale = gravityValue;
            transform.eulerAngles = Vector3.zero;
            //rb.velocity = Vector2.zero;
        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    rb.velocity = Vector2.up * boundPowwer * Time.deltaTime * 1.25f;
           
        //}
        //if (Input.GetMouseButtonUp(0))
        //{
        //    //transform.eulerAngles = Vector3.zero;
         
        //    //rb.velocity = Vector2.up * boundPowwer * Time.deltaTime;
           

        //}
        //else
        //{


        //    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        //}


    }
    public GameObject plane;
    private void LateUpdate()
    {
        //transform.position = new Vector3(0, Mathf.Clamp(transform.position.y, 10, 85), 0);
        if (damageImage.color.a > 0)
        {
            var color = damageImage.color;
            color.a -= 0.01f;
            damageImage.color = color;
        }
        ComboCountTextMesh.text = ComboCount.ToString() + "X";

        if (oilCountSlider.value <= 0)
        {

            Finish();
            plane.SetActive(false);

        }


        Debug.Log("stickCount" + stickCount);
      


        //if (oilCountSlider.value <= 25)
        //{
        //    WarningOilText.gameObject.SetActive(true);
        //    WarningOilText.color = Color.Lerp(Color.red, Color.yellow, Mathf.PingPong(Time.time, 0.5f));

        //}
        //else
        //{
        //    WarningOilText.gameObject.SetActive(false);
        //}
    }
    private void Finish()
    {
        TotalScore += Score;
        PlayerPrefs.SetInt("TOTALSCORE", TotalScore);
        ParticleSystem go = Instantiate(particle, gameObject.transform.position, Quaternion.identity);
        go.GetComponent<ParticleSystemRenderer>().material.color = gameObject.GetComponent<Renderer>().material.color;

       
        //oilCountSlider.value = 100;
        //WarningOilText.gameObject.SetActive(false);
        measureCS.rotateSpeed = 0;
            ScoreFinish.text = "Score = "+Score.ToString();
            boundPowwer = 0;
            rb.velocity = Vector3.zero;
            Invoke("RestartScene",3f);

        gameObject.SetActive(false);

        for (int i = 0; i < canvas.childCount - 1; i++)
        {
            canvas.GetChild(i).gameObject.SetActive(false);
        }
        ScoreFinish.gameObject.SetActive(true);

    }
    public Transform soundBarsTransform;
    private void OnTriggerEnter(Collider other)
    {

       
        if (other.gameObject.tag == "WaveFormObject")
        {
            SoundManagerSfx.PlaySfx("GlassBreakk");
           
            stickCount--;
            stickCountText.text = "End = " + stickCount.ToString() + " Stick";
           
           
            ParticleSystem go = Instantiate(particle, other.transform.position, Quaternion.identity);
            go.GetComponent<ParticleSystemRenderer>().material.color = other.GetComponent<Renderer>().material.color;

            if (stickCount <= 0)
            {
                EarthExpolotion();
                Final();


                other.gameObject.SetActive(false);
                return;
            }


            if (GetComponent<Renderer>().material.color == other.gameObject.GetComponent<Renderer>().material.color)
            {
               
                //measureCS.Shake();
                //measureCS.valuesList.Remove(PlayerColorNumber);
                measureCS.valuesList.Remove(other.gameObject.GetComponent<Renderer>().material.color);
                ComboCount++;
                ComboCountText.text = "Combo = " + ComboCount.ToString();
                Vibration.Vibrate(100);
                StartCoroutine(CoroutineTest());
                Score += 3*ComboCount;
                ScoreText.text ="Score = "+ Score.ToString();
                Floating.True = true;
                measureCS.rotateSpeed += 0.1f;
                SpeedCount++;
                SpeedCountText.text = "Speed = " + SpeedCount.ToString();
                other.gameObject.SetActive(false);
               
                PlayerColor();

                if (ComboCount<9)
                {
                    int a = 16 / (ComboCount * 2);

                    for (int i = ComboCount - 1; i < 16; i += a)
                    {
                        soundBarsTransform.transform.GetChild(i).gameObject.SetActive(true);

                    }
                }
              
            }
            else
            {
                PlayerColor();
                Vibration.Vibrate(1000);
                var color= damageImage.color;
                color.a = 0.3f;
                damageImage.color = color;
                measureCS.ShakeWrong();
                Floating.True = false;
                measureCS.valuesList.Remove(other.gameObject.GetComponent<Renderer>().material.color);
                ComboCount = 0;
                ComboCountText.text = "Combo = " + ComboCount.ToString();
                SpeedCount = 0;
                //if (SpeedCount <= 0)
                //    SpeedCount = 0;
                SpeedCountText.text = "Speed = " + SpeedCount.ToString();
                other.gameObject.SetActive(false);
                //gameObject.SetActive(false);
                measureCS.rotateSpeed =measureCS.rotateSpeedStart;
                //Invoke("RestartScene", 3f);
                health -= 1;
               
                HealthTransfrom.GetChild(health).gameObject.SetActive(false);

                foreach (Transform item in soundBarsTransform)
                {
                    item.gameObject.SetActive(false);
                }

            }
            Instantiate(FloatingParent, transform.position + new Vector3(0, 5, 0), Quaternion.identity);
        }
        if (other.gameObject.tag == "Coin")
        {
            ParticleSystem go = Instantiate(particle, other.transform.position, Quaternion.identity);
            go.GetComponent<ParticleSystemRenderer>().material.color =other.GetComponent<Renderer>().material.color;
            Score += 20;
            ScoreText.text = "Score = " + Score.ToString();
            //oilCountSlider.value += 35;
            Destroy(other.gameObject);
            //if (measureCS.level == 3)
            //{
            //    SkipButton.gameObject.SetActive(true);
            //}
        }
        if (other.gameObject.tag == "Speed")
        {
            ParticleSystem go = Instantiate(particle,other.transform.position,Quaternion.identity);
            go.GetComponent<ParticleSystemRenderer>().material.color = other.GetComponent<Renderer>().material.color;
            SpeedCount++;
            SpeedCountText.text = "Speed = " + SpeedCount.ToString();
            measureCS.rotateSpeed += 0.5f;
            Destroy(other.gameObject);
            //if (measureCS.level==1)
            //{
            //    SkipButton.gameObject.SetActive(true);
            //}
        }
        if (other.gameObject.tag=="Escape")
        {
            Finish();
        }
        if (health <= 0)
        {

            Finish();
            measureCS.audioSource.Stop();

        }
    }
    //public void Skip()
    //{
    //    EarthExpolotion();
    //    Final();
    //    parentWaveForm.SetActive(false);
    //}
    void Final()
    {
        if (levelId==PlayerPrefs.GetInt("RANK"))
        {
            if ((levelId < 10)&&(gameManager.youtube==false))
            {
                gameManager.rank++;
            }
           
        }
       
        //gameManager.ScrollX -= 350;
        //PlayerPrefs.SetInt("LEVEL", gameManager.level);
        PlayerPrefs.SetInt("RANK", gameManager.rank);
        //PlayerPrefs.SetInt("ScrollX", gameManager.ScrollX);

        gameObject.SetActive(false);
    }
    IEnumerator CoroutineTest()
    {
        ComboCountTextMeshAnim.gameObject.SetActive(false);
        yield return null; // 1 frame bekle
        ComboCountTextMeshAnim.gameObject.SetActive(true);
    }
    void PlayerColor()
    {
        
        int listColorNumber = Random.Range(0, measureCS.valuesList.Count);
        PlayerColorNumber = measureCS.valuesList[listColorNumber];
        StartCoroutine(ColorChange(listColorNumber));
      

        Cube.color = measureCS.valuesList[listColorNumber];
       HeadSet.gameObject.GetComponent<Renderer>().material.color = measureCS.valuesList[listColorNumber];
       
       
        
        
    }
    public IEnumerator ColorChange(int listColorNumber)
    {
       
        
        float ElapsedTime = 0.0f;
        float TotalTime = 2.0f;
        while (ElapsedTime < TotalTime)
        {
            ElapsedTime += Time.deltaTime;
            fog.color = Color.Lerp(fog.color, measureCS.valuesList[listColorNumber], (ElapsedTime / TotalTime));
            rhythm.colors[0] = Color.Lerp(rhythm.colors[0], measureCS.valuesList[listColorNumber], (ElapsedTime / TotalTime));
            pointLight.color = Color.Lerp(pointLight.color, measureCS.valuesList[listColorNumber], (ElapsedTime / TotalTime));
            yield return null;
        }
        fog.color = measureCS.valuesList[listColorNumber];
        pointLight.color = measureCS.valuesList[listColorNumber];
    }
        public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   
   
   void EarthExpolotion()
    {
        FinishCamera.SetActive(true);

        for (int i = 0; i < canvas.childCount - 1; i++)
        {
            canvas.GetChild(i).gameObject.SetActive(false);
        }
        Invoke("NextLevel", 1.5f);

        measureCS.audioSource.Stop();
        SoundManagerSfx.PlaySfx("Explosion");


    }

    private void NextLevel()
    {


            gameManager.ContentX -= 460f;
            PlayerPrefs.SetFloat("ContentX", gameManager.ContentX);

             nextLevel.gameObject.SetActive(true);
            ParticleSystem EarthBlue = Instantiate(Earthparticle, EarthTransfrom.transform.position, Quaternion.identity);

            ParticleSystem EarthGreen = Instantiate(Earthparticle_bir, EarthTransfrom.transform.position, Quaternion.identity);
            ParticleSystem go = Instantiate(particle, transform.position, Quaternion.identity);
            go.GetComponent<ParticleSystemRenderer>().material.color = PlayerColorNumber;

            TotalScore += Score;
            PlayerPrefs.SetInt("TOTALSCORE", TotalScore);


            ScoreFinishText.text = "Score = " + Score.ToString();
            EarthTransfrom.gameObject.SetActive(false);
            gameObject.SetActive(false);
        
    }

    [Header("GRAVÝTY")]
    public float gravityScale = 0;
    [Range(0,20)]
    public float gravityValue = 1.0f; 
    public static float globalGravity = -9.81f;
    Rigidbody m_rb;
    private Vector3 velocity;

    void OnEnable()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.useGravity = false;
    }
    void Update()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        m_rb.AddForce(gravity, ForceMode.Acceleration);
     
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ScrolViewMove : MonoBehaviour
{
    //ScrollView bileþeninin olduðu obje
    [SerializeField]
    RectTransform scrollParent;
   public ScrollRect scrollRect;
    //Referans olarak kullanacaðýmýz pozisyon
    Vector3 boyutlandirmaMerkezi;

    //ScrollView alanýnýn büyüklüðü
    float alanGenisligi;
    //Objelerin genel büyüklüðü
    float altObjeGenisligi;
    public GameObject obj;
    /*
    Boyutlandýrma için animationCurve kullanýyoruz.
    Bu sayede istediðimiz mesafe için istediðimi boyutu elle
    ayarlayabiliyoruz.
    */
    [SerializeField]
    AnimationCurve boyutlandirmaOrani;

    //Kontrol
    [SerializeField]


    //Horizontal veya Vertical kullanýlabileceðinden ikisini de kontrol etmek için bu tipi kullanýyoruz.

    
   

    void Start()
    {
        //Scrollview pozisyonu merkez noktasýnda olduðundan 
        boyutlandirmaMerkezi = scrollParent.position;
        
       
        GorunurAlanGenisligiHesapla();
        AltObjeBoyutuHesapla();
        Invoke("changeposition",0.1f);
       
    }
   public void changeposition()
    {
        obj.transform.localPosition = new Vector3(-1, obj.transform.localPosition.y, obj.transform.localPosition.z);
    }
    //ScrollView OnValueChange üzerinden atanmasý gereken fonksiyon. Bu sayede her kaydýrma sonucunda bu fonksiyon çaðýrýlacak.
    public void ObjeleriBoyutlandir()
    {
         float yakinlikOrani;
        //her bir altobjeyi dolanýyoruz
        foreach (Transform altObje in transform)
        {
            //merkez arasýndaki mesafeyi alýp, görünür alanda olabileceði en uzak mesafeye oranýný elde ediyoruz
            float mesafe = Vector3.Distance(boyutlandirmaMerkezi, altObje.position);
            mesafe = Mathf.Abs(mesafe);
            yakinlikOrani = mesafe / alanGenisligi;
            //animationcurve üzerinden belirlediðimiz orana göre deðeri çekmiþ oluyoruz.
            //Animation curve deðerinin loop modunda olmamasýna dikkat edin. Derste bahsetmemiþtim ama önemli bir konu
            altObje.localScale = Vector3.one * boyutlandirmaOrani.Evaluate(yakinlikOrani);
            if (altObje.localScale.x>1.2f)
            {
                altObje.GetChild(0).gameObject.SetActive(true);
                altObje.GetChild(3).gameObject.SetActive(true);
            }
            else
            {
                altObje.GetChild(0).gameObject.SetActive(false);
                altObje.GetChild(3).gameObject.SetActive(false);

            }
        }
    }

    //Layout group içerisindeki ve altobjeler arasýndaki mesafeyi ayarlýyoruz
   
    /*
    her elemanýn eþit boyutta olduðunu kabul ederek
    ilk elemandan boyut deðerlerini alýyoruz.
    */
    void AltObjeBoyutuHesapla()
    {
        altObjeGenisligi = transform.GetChild(0).GetComponent<RectTransform>().rect.width;
        
    }
    //Scroll içerisinde objelerin görünür olduðu alanýn boyutlarýný alýyoruz.
    void GorunurAlanGenisligiHesapla()
    {
        alanGenisligi = scrollParent.rect.width;
    
        
    }
    
}

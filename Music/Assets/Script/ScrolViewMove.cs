using UnityEngine;
using UnityEngine.UI;

public class ScrolViewMove : MonoBehaviour
{
    //ScrollView bile�eninin oldu�u obje
    [SerializeField]
    RectTransform scrollParent;
   public ScrollRect scrollRect;
    //Referans olarak kullanaca��m�z pozisyon
    Vector3 boyutlandirmaMerkezi;

    //ScrollView alan�n�n b�y�kl���
    float alanGenisligi;
    //Objelerin genel b�y�kl���
    float altObjeGenisligi;
    public GameObject obj;
    /*
    Boyutland�rma i�in animationCurve kullan�yoruz.
    Bu sayede istedi�imiz mesafe i�in istedi�imi boyutu elle
    ayarlayabiliyoruz.
    */
    [SerializeField]
    AnimationCurve boyutlandirmaOrani;

    //Kontrol
    [SerializeField]


    //Horizontal veya Vertical kullan�labilece�inden ikisini de kontrol etmek i�in bu tipi kullan�yoruz.

    
   

    void Start()
    {
        //Scrollview pozisyonu merkez noktas�nda oldu�undan 
        boyutlandirmaMerkezi = scrollParent.position;
        
       
        GorunurAlanGenisligiHesapla();
        AltObjeBoyutuHesapla();
        Invoke("changeposition",0.1f);
       
    }
   public void changeposition()
    {
        obj.transform.localPosition = new Vector3(-1, obj.transform.localPosition.y, obj.transform.localPosition.z);
    }
    //ScrollView OnValueChange �zerinden atanmas� gereken fonksiyon. Bu sayede her kayd�rma sonucunda bu fonksiyon �a��r�lacak.
    public void ObjeleriBoyutlandir()
    {
         float yakinlikOrani;
        //her bir altobjeyi dolan�yoruz
        foreach (Transform altObje in transform)
        {
            //merkez aras�ndaki mesafeyi al�p, g�r�n�r alanda olabilece�i en uzak mesafeye oran�n� elde ediyoruz
            float mesafe = Vector3.Distance(boyutlandirmaMerkezi, altObje.position);
            mesafe = Mathf.Abs(mesafe);
            yakinlikOrani = mesafe / alanGenisligi;
            //animationcurve �zerinden belirledi�imiz orana g�re de�eri �ekmi� oluyoruz.
            //Animation curve de�erinin loop modunda olmamas�na dikkat edin. Derste bahsetmemi�tim ama �nemli bir konu
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

    //Layout group i�erisindeki ve altobjeler aras�ndaki mesafeyi ayarl�yoruz
   
    /*
    her eleman�n e�it boyutta oldu�unu kabul ederek
    ilk elemandan boyut de�erlerini al�yoruz.
    */
    void AltObjeBoyutuHesapla()
    {
        altObjeGenisligi = transform.GetChild(0).GetComponent<RectTransform>().rect.width;
        
    }
    //Scroll i�erisinde objelerin g�r�n�r oldu�u alan�n boyutlar�n� al�yoruz.
    void GorunurAlanGenisligiHesapla()
    {
        alanGenisligi = scrollParent.rect.width;
    
        
    }
    
}

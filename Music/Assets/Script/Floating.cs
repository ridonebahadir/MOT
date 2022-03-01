using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public TextMesh textMesh;
    public string[] floatingText;
    public string[] floatingTextWrong;
    public static bool True;
    void Start()
    {
        if (True)
        {
            textMesh.text = floatingText[Random.Range(0, floatingText.Length)];
           
         
        }

        else
        {
            textMesh.text = floatingTextWrong[Random.Range(0, floatingTextWrong.Length)];
        }

        Destroy(gameObject, 1f);
    }

    
   
}

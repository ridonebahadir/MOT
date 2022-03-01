
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class cam_follow : MonoBehaviour {

	public Transform Player;
	public Transform twoDTransfrom;
public Transform PlayerDİstancePoint;
public Transform EarthTransform;
    public float speed;
    public Transform StartPos;
    float z = -50;
    private Vector3 asd;
    private float timer = 0;
    private float StartaCameraSize;
    public Transform[] players;
    private void Start()
    {
      
        StartPos.position = transform.position;
        StartPos.rotation = transform.rotation;
        asd = new Vector3(0, PlayerDİstancePoint.transform.position.y - EarthTransform.transform.position.y, 0);
        StartaCameraSize = transform.GetComponent<Camera>().orthographicSize;

    }
    
    [System.Obsolete]
    private void FixedUpdate()
    {
      
        float dist = Vector3.Distance(PlayerDİstancePoint.position, Player.transform.position);
       
        
      
        //Debug.Log("Dist = "+dist);
       
           
            Vector3 centerPos = new Vector3(Player.position.x + PlayerDİstancePoint.position.x, Player.position.y + PlayerDİstancePoint.position.y) / 2f;


            if (dist>5)
            {
                transform.DOLookAt(((3 * (centerPos)) / 2), 5f);

                
               
            }
            else
            {
                transform.DOLookAt(centerPos, 5f);

            }
            //SetCameraPos();
            Debug.Log("Z" + z);
            if (Input.GetMouseButton(0))
            {

                timer += Time.deltaTime;
              
                if (timer > 1)
                {
                    if (dist>10)
                    {
                        z -= 45 * Time.deltaTime;
                        transform.position = new Vector3(Mathf.SmoothStep(transform.position.x,0,0.01f* Time.deltaTime), transform.position.y, Mathf.SmoothStep(transform.position.z, z, speed * Time.deltaTime));

                    }


                }

            }
            else
            {
                timer = 0;

                if (dist<15)
                {
                    transform.position = new Vector3(Mathf.SmoothStep(transform.position.x, StartPos.position.x, speed * Time.deltaTime), transform.position.y, Mathf.SmoothStep(transform.position.z, StartPos.position.z, speed * Time.deltaTime));

                }

            }

            if (dist < 15)
            {
                z = -50;
            }






            //else
            //{
            //    transform.position = Vector3.Lerp(transform.position, StartTransfrom.position, Time.deltaTime * 1);
            //    transform.rotation = Quaternion.RotateTowards(transform.rotation, StartTransfrom.rotation, 10f * Time.deltaTime);
            //}
        
       
        //void SetCameraPos()
        //{
        //    Vector3 middle = Vector3.zero;
        //    int numPlayers = 0;

        //    for (int i = 0; i < players.Length; ++i)
        //    {
        //        if (players[i] == null)
        //        {
        //            continue; //skip, since player is deleted
        //        }
        //        middle += players[i].position;
        //        numPlayers++;
        //    }//end for every player

        //    //take average:
        //    middle /= numPlayers;
        //           //transform.LookAt(middle);
        //    var rotation = Quaternion.LookRotation((middle+new Vector3(0,5,0)) - transform.position);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
        //}
        
    }
   
   

}

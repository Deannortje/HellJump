using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraManager : MonoBehaviour
{

    public GameObject Following;
    // Start is called before the first frame update
    private int starttimeY;
    private int starttimeX;
    private float deltaY;
    private float deltaX;
    public int maxDeltaY;
    public int minDeltaY;
    public int maxDeltaX;
    public int minDeltaX;
    public int timeToLerpInMS;
    void Start()
    {
        this.transform.position =  new Vector3(0,Following.transform.position.y,-10);   
    }

    // Update is called once per frame 
    void Update()
    {
    }

    void FixedUpdate() {
        deltaY = Mathf.Abs(Following.transform.position.y - this.transform.position.y);
        
        deltaX = Mathf.Abs(Following.transform.position.x - this.transform.position.y);
        
        //Debug.Log(deltaY);
        float newX = this.transform.position.x;
        float newY = this.transform.position.y;
        if(deltaY > maxDeltaY)
        {
            starttimeY = 1;
        }
        if(deltaY<minDeltaY)
        {
            starttimeY = 0;
        }
        if(starttimeY>0)
        {   
            starttimeY ++;
            newY = Mathf.Lerp(this.transform.position.y,Following.transform.position.y, Mathf.Clamp01((float)starttimeY/timeToLerpInMS));
            //Debug.Log(Following.transform.position.y);
            this.transform.position =  new Vector3(0,newY,-10);
        }
              deltaY = Mathf.Abs(Following.transform.position.y - this.transform.position.y);
        
        //Debug.Log(deltaY);
        if(deltaX > maxDeltaX)
        {
            starttimeX = 1;
        }
        if(deltaX<minDeltaX)
        {
            starttimeX = 0;
        }
        if(starttimeX>0)
        {   
            starttimeX ++;
            newX = Mathf.Lerp(this.transform.position.x,Following.transform.position.x, Mathf.Clamp01((float)starttimeX/timeToLerpInMS));
            //Debug.Log(Following.transform.position.y);
        }
        this.transform.position =  new Vector3(newX,newY,-10);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frozenCursor : MonoBehaviour
{
    public Transform sphereTransform;
    public float NewFriction;
    public PhysicsMaterial2D pm;
    
    public LayerMask safeZone;
    private Vector3 fixer = new Vector3(0,0,10);
    public int deathcount = 0;
    
    public bool invincibility = false;
    public Transform RespawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
 
    void Update ()
    {
        sphereTransform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + fixer;
    }

    void OnTriggerStay2D(Collider2D other)
{
        if(other.gameObject.layer == 9)
        {
            if(!invincibility){
                other.gameObject.transform.position = RespawnPoint.position;
                deathcount++;   
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        // ContactFilter2D cf = new ContactFilter2D();
        // cf.layerMask = safeZone;
        // cf.useLayerMask = true;
        // List<Collider2D> list = new List<Collider2D>{};
        // other.OverlapCollider(cf,list);
        // Debug.Log("Safe Space Count = " + list.Count);
       
        
        if(other.gameObject.layer == 8)
        {
                other.gameObject.GetComponent<MeshRenderer>().enabled  = true;
                other.gameObject.GetComponent<Rigidbody2D>().sharedMaterial = pm;
                //other.gameObject.GetComponent<Rigidbody2D>().sharedMaterial.friction = NewFriction;
                other.gameObject.GetComponent<PolygonCollider2D>().isTrigger = false;
        }
        if(other.gameObject.layer == 10)
        {
                invincibility = true;
        }
        
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == 8)
        {
                other.gameObject.GetComponent<MeshRenderer>().enabled  = false;
                //other.gameObject.GetComponent<Rigidbody2D>().sharedMaterial.friction = NewFriction;
                other.gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
        }
        if(other.gameObject.layer == 10)
            {
                 invincibility = false;
            }
    }
}

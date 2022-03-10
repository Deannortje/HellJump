using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Playerhandler : MonoBehaviour
{
    public float JumpHeight;
    public float MoveSpeed;
    // Start is called before the first frame update
    
    public GameObject character;
    public Rigidbody2D body;
    public float waterEnterModifier;
    public float AirMobilityFactor = 1;
    public float waterExitModifier;
    private float canJump = 1;
    public float groundDrag = 1;
    public float waterDrag = 5;
    public float airDrag = 0.2f;
    public float iceDrag = 0.3f;
    public float WaterJumpModifier;
    public float JumpReleaseModifier = 0.5f;

    public KeyCode jumpID;

    private bool isGrounded = false;
    //public Transform IsGroundedCheck;
    public float CheckGroundRadius;
    public int defaultAdditionalJumps = 0;
    private int additionalJumps;

    public LayerMask ground;
    public LayerMask water;
    public LayerMask ice;
    public LayerMask death;
    private bool isUnderwater;
    private bool isOnIce;
    public Collider2D colliderWater;
    public Collider2D colliderGround;
    public Transform RespawnPoint;
    void Start()
    {
        //colliderGround =  Physics2D.OverlapCircle(IsGroundedCheck.position, CheckGroundRadius, ground);
    }
    // Update is called once per frame
    void Update()
    {
       // Updatecharacter.transform.position = new Vector3(x,y,0);
        //HandleInputMovement();
        CheckIfDeath();
        CheckIfGrounded();
        CheckIfOnIce();
        CheckIfSubmerged();
        jump();
        Move();
        StopJump();
        if(Input.GetKey(KeyCode.Q))
        { 
            if(!isGrounded) Debug.Log("Airborn");
            if(isGrounded) Debug.Log("Grounded");
            if(isUnderwater) Debug.Log("underwater");
            if(isOnIce) Debug.Log("On ice");
        }
        
    }

    void Move()
    {
        if(!isGrounded)
       {    
           //TODO add delta time fixing here
            if(Input.GetKey(KeyCode.A)) body.AddForce(new Vector2(-MoveSpeed*AirMobilityFactor,0));
            if(Input.GetKey(KeyCode.D)) body.AddForce(new Vector2(MoveSpeed*AirMobilityFactor,0));
       }
       else
       {
            body.velocity = new Vector2(0,body.velocity.y);
            if(Input.GetKey(KeyCode.A)) body.velocity = new Vector2(-MoveSpeed,body.velocity.y);
            if(Input.GetKey(KeyCode.D)) body.velocity = new Vector2(MoveSpeed,body.velocity.y);
       }
    }

    void CheckIfDeath()
    {
        
        if(colliderGround.IsTouchingLayers(death)) character.transform.position = RespawnPoint.position;
    }

    void CheckIfGrounded()
    {
        //A collider placed by the players feet
        // colliderGround = Physics2D.OverlapCircle(IsGroundedCheck.position, CheckGroundRadius, ground);

        if (colliderGround.IsTouchingLayers(ground))
        {
            isGrounded = true;
            body.drag = groundDrag;
            //additionalJumps = defaultAdditionalJumps;
        }
        else if (isGrounded)
        {
            isGrounded = false;
            body.drag = airDrag;
        }
    }
    void CheckIfSubmerged()//this is to check if the character is in water
    {
      
        if (colliderWater.IsTouchingLayers(water))
        {   
            isUnderwater = true;
            body.drag = waterDrag;
            //additionalJumps = defaultAdditionalJumps;
        }
        else if (isUnderwater)
        {
            //condition ? consequent : alternative
            isUnderwater = false;
            if(isGrounded) body.drag = groundDrag;
            else           body.drag = airDrag;
        }
    }
    
    void CheckIfOnIce()
    {
        //A collider placed by the players feet

        if (colliderGround.IsTouchingLayers(ice))
        {
            ContactFilter2D cf = new ContactFilter2D();
            cf.layerMask = ice;
            cf.useLayerMask = true;
            List<Collider2D> list = new List<Collider2D>{};
            colliderGround.OverlapCollider(cf,list);
            bool found = false;
            foreach (Collider2D iceBlock in list)
            {
                if (iceBlock.gameObject.GetComponent<MeshRenderer>().enabled)
                {
                    isOnIce = true;
                    isGrounded = true;
                    body.drag = iceDrag;
                    found = true;
                    break;
                }
            }
            
            if (!found)
            {
                isOnIce = false;
                if(isGrounded) body.drag = groundDrag;
                else           body.drag = airDrag;
            }
            //additionalJumps = defaultAdditionalJumps;
        }
    }

    void jump()
    {
        //makes the player jump but only if on the gorund 
        if (Input.GetKey(jumpID) && (isGrounded||isOnIce))
        {
            float WaterModifier;
            if(isUnderwater) WaterModifier = WaterJumpModifier;
            else             WaterModifier = 1;
            body.velocity = new Vector2(body.velocity.x, JumpHeight*WaterModifier);
        }
    }

    void StopJump()
    {
        if(Input.GetKeyUp(jumpID))
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y*JumpReleaseModifier);
        }
    }
}
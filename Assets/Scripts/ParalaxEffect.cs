using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxEffect : MonoBehaviour
{

    public Vector2 ParalaxEffectScale;

    private Transform Cam;
    private Vector3 LastPos;
    private float textureSizeX;
    private float textureSizeY;
    void Start()
    {
        Cam = Camera.main.transform;
        LastPos = Cam.position;
        Sprite sprite = this.GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureSizeX = texture.width / sprite.pixelsPerUnit*10;
        textureSizeX = texture.height / sprite.pixelsPerUnit*10;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 DeltaMov = Cam.position - LastPos;
        transform.position += new Vector3(DeltaMov.x * ParalaxEffectScale.x, DeltaMov.y * ParalaxEffectScale.y);
        LastPos = Cam.position;

        if (Mathf.Abs(Cam.position.x - this.transform.position.x) > textureSizeX)
        {
            this.transform.position = new Vector3(Cam.position.x,Cam.position.y,0);
        }
    }
}

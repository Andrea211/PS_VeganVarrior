﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class Bean1 : MonoBehaviour
{
    // PhotonView is necessary for calling RPC functions
    private PhotonView PV;

    private const float GRAVITY = 2.0f;

    public bool IsActive { set; get; }
    public SpriteRenderer sRenderer;

    private float verticalVelocity;
    private float speed;
    private bool isSliced = false;

    public Sprite[] sprites;
    private int spriteIndex;
    private float lastSpriteUpdate;
    private float spriteUpdateDelta = 0.1f;
    private float rotationSpeed;

    void Start(){
        PV = GetComponent<PhotonView>();

    }

    public void LaunchBean1(float verticalVelocity, float xSpeed, float xStart)
    {
        IsActive = true;
        speed = xSpeed;
        this.verticalVelocity = verticalVelocity;
        transform.position = new Vector3(xStart, -1, -1);
        isSliced = false;

        spriteIndex = 0;
        sRenderer.sprite = sprites[spriteIndex];

        rotationSpeed = Random.Range(-180, 180);
    }

    private void Update()
    {
        if (!IsActive)
            return;

        verticalVelocity -= GRAVITY * Time.deltaTime;
        transform.position += new Vector3(speed, verticalVelocity,0) * Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, rotationSpeed)*Time.deltaTime);

        if (isSliced)
        {
            if(spriteIndex != sprites.Length-1 && Time.time-lastSpriteUpdate > spriteUpdateDelta)
            {
                lastSpriteUpdate = Time.time;
                spriteIndex++;
                sRenderer.sprite = sprites[spriteIndex];
            }
        }

        // if we don't see the vegetable anymore
        if(transform.position.y == -1)
        {
            GameManagerMultiplayer.Instance.DecrementScore1(2);
        }
    }

    public void Slice()
    {
        Debug.Log("enter slice");
        if (isSliced)
            return;

        // if vegetable is falling then after the slice we want it to go up a bit
        if(verticalVelocity < 0.5f)
        {
            verticalVelocity = 0.7f;
        }

        speed = speed*0.5f;
        isSliced = true;
        Debug.Log("is sliced = true");

        //SoundManager.Instance.PlaySound(0);
        Debug.Log("making sound");

        
        if(PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            PhotonNetwork.LocalPlayer.AddScore(4);
        }
        else 
        {
            PhotonNetwork.LocalPlayer.AddScore(-4);
        }

    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class Player2Points : MonoBehaviour, IPunObservable
{
    public Text pointsPlayer2;
    private PhotonView pv;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        PhotonNetwork.SendRate = 20;
        //hotonNetwork.SendRateOnSerialize = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if(pv.IsMine)
        {
// do nothing
        }
        else
        {
            
                pv.TransferOwnership(PhotonNetwork.PlayerList[1]);
        }
    }

   public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        
        Debug.Log("We're in OnPhotonSerializeView if statement");
        
        if(stream.IsWriting)
        {
            stream.SendNext(PhotonNetwork.PlayerList[1].GetScore());
            Debug.Log("Player 2 score is: " + PhotonNetwork.PlayerList[1].GetScore());
        }
        else 
        {
            Debug.Log("We're in OnPhotonSerializeView else statement");
            pointsPlayer2.text = stream.ReceiveNext().ToString();
        }
    }
}
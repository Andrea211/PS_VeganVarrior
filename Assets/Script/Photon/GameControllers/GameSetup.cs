using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;

public class GameSetup : MonoBehaviour
{
    public static GameSetup GS;
    public Transform[] spawnPoints;
    
    private PhotonView PV;

    public Text pointsPlayer1;
    public Text pointsPlayer2;

    public Button escapeButton;
    
    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    private void OnEnable()
    {
        if(GameSetup.GS == null)
        {
            GameSetup.GS = this;
        }
    }


    public void DisconnectPlayer()
    {

        

         if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            DisconnectAndLoad();
        }
        else {
            PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
            Debug.Log("Changing master client to: " + PhotonNetwork.LocalPlayer);
            DisconnectAndLoad();
        }
        SceneManager.LoadScene(7);

/*
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {}
        else {
            PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
            Debug.Log("Changing master client to: " + PhotonNetwork.LocalPlayer);
        }

        
        Destroy(PhotonRoom.room.gameObject);
        Debug.Log("destroy room");

        StartCoroutine(DisconnectAndLoad());
        Debug.Log("start coroutine");
        */
    }

    IEnumerator DisconnectAndLoad()
    {
        //PhotonNetwork.Disconnect();
        Debug.Log("We are leaving room");
        
        PhotonNetwork.LeaveRoom();
        Debug.Log("We have left the room");

        while(PhotonNetwork.InRoom)
        {
            yield return null;
        }
        Debug.Log("We are after the while loop");

        if (PV.IsMine)
            PhotonNetwork.Destroy(PV);
        Debug.Log("We are after the if condition");

        //PhotonNetwork.CloseConnection(PhotonNetwork.LocalPlayer);

        SceneManager.LoadScene(7);
        Debug.Log("We have loaded the 7th scene");
    }

}

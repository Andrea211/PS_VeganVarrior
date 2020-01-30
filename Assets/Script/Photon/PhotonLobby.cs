using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;
    public GameObject battleButton;
    public GameObject cancelButton;
    public GameObject waitingText;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.GameVersion="1.0";
        // connects to master photon server
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to the Photon master server");
        Debug.Log("Number of players connected to the master: " + PhotonNetwork.CountOfPlayersOnMaster);
        PhotonNetwork.AutomaticallySyncScene = true;
        waitingText.SetActive(false);
        battleButton.SetActive(true);

    }

    public void OnBattleButtonClicked()
    {
        Debug.Log("Battle button was clicked");
        battleButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to join a random game but failed. There must be no open games available");
        CreateRoom();
    }

    void CreateRoom()
    {
        Debug.Log("Trying to create a new room");
        int randomRoomName = Random.Range(0,10000);
        RoomOptions roomOps = new RoomOptions() {IsVisible = true, IsOpen = true, PublishUserId = true, MaxPlayers = (byte) MultiplayerSetting.multiplayerSetting.maxPlayers};
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
        Debug.Log("Created room " + randomRoomName);
    }

    
    
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create a new room but failed. There must already be a room with the same name");
        CreateRoom();
    }

    public void OnCancelButtonClicked()
    {
        Debug.Log("Cancel button was clicked");
        cancelButton.SetActive(false);
        battleButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(1);
    }
}

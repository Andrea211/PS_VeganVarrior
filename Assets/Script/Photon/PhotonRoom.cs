using System.IO;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    
    // room info
    public static PhotonRoom room;
    private PhotonView PV;
    public static bool isGameLoaded;
    public int currentScene;

    // player info
    Player[] photonPlayers;
    public int playersInRoom;
    public int myNumberInRoom;
    public int playerInGame;

    // delayed start
    private bool readyToCount;
    private bool readyToStart;
    public float startingTime;
    private float lessThanMaxPlayers;
    private float atMaxPlayers;
    private float timeToStart;

    private void Awake()
    {
        // set up singleton
        if(PhotonRoom.room == null)
        {
            PhotonRoom.room = this;
        }
        else
        {
            if(PhotonRoom.room != this)
            {
                Destroy(PhotonRoom.room.gameObject);
                PhotonRoom.room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
    
    public override void OnEnable()
    {
        // subscribe to functions
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        // set private variables
        PV = GetComponent<PhotonView>();
        readyToCount = false;
        readyToStart = false;
        lessThanMaxPlayers = startingTime;
        atMaxPlayers = 6;
        timeToStart = startingTime;

        PhotonNetwork.AutomaticallySyncScene = true;

    }

    // Update is called once per frame
    void Update()
    {
        // for delay start only, count down to start
        if(MultiplayerSetting.multiplayerSetting.delayStart)
        {
            if(playersInRoom == 1)
            {
                RestartTimer();
            }
            if(!isGameLoaded)
            {
                if(readyToStart)
                {
                    atMaxPlayers -= Time.deltaTime;
                    lessThanMaxPlayers = atMaxPlayers;
                    timeToStart = atMaxPlayers;
                }
                else if(readyToCount)
                {
                    lessThanMaxPlayers -= Time.deltaTime;
                    timeToStart = lessThanMaxPlayers;
                }
                Debug.Log("Display time to start to the players " + timeToStart);
                if(timeToStart<=0)
                {
                    Debug.Log("We have started the game!");
                    StartGame();
                }
            }
        }


    }

 void OnPhotonPlayerConnected(PhotonPlayer newPlayer){
      //Doesn't get called on the local player, just remote players, so you would still need something to handle on the second player
     if (playersInRoom == 2) {
          PhotonNetwork.AutomaticallySyncScene = true;
          StartGame();
          
      } 
      else if (playersInRoom == 1) {
          Debug.Log ("Not Enough PLayers");
      }
  }

    public override void OnJoinedRoom()
    {
        // sets player data when we join the room
        base.OnJoinedRoom();
        Debug.Log("We are now in a room");
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
        myNumberInRoom = playersInRoom;
        PhotonNetwork.NickName = myNumberInRoom.ToString();

        // for delay start only
        if(MultiplayerSetting.multiplayerSetting.delayStart)
        {
            Debug.Log("Displayer players in room out of max players possible (" + playersInRoom + ":" + MultiplayerSetting.multiplayerSetting.maxPlayers + ")");
            if(playersInRoom > 1)
            {
                readyToCount = true;
            }
            if(playersInRoom == MultiplayerSetting.multiplayerSetting.maxPlayers)
            {
                readyToStart = true;
                if(!PhotonNetwork.IsMasterClient)
                    return;
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }

        // for non delay start
        else if (playersInRoom == 2)
        {
            Debug.Log("We have started the game!");
            PhotonNetwork.AutomaticallySyncScene = true;
            StartGame();
            
        }

        else {
            Debug.Log("There's only 1 player");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("A new player has joined the room");
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom++;

        if(playersInRoom == 2){
            PhotonNetwork.AutomaticallySyncScene = true;
            StartGame();
        }

        if(MultiplayerSetting.multiplayerSetting.delayStart)
        {
            Debug.Log("Displayer players in room out of max players possible (" + playersInRoom + ":" + MultiplayerSetting.multiplayerSetting.maxPlayers + ")");
            if(playersInRoom > 1)
            {
                readyToCount = true;
            }
            if(playersInRoom == MultiplayerSetting.multiplayerSetting.maxPlayers)
            {
                readyToStart = true;
                if(!PhotonNetwork.IsMasterClient)
                    return;
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
    }

    void StartGame()
    {
        isGameLoaded = true;

            if(!PhotonNetwork.IsMasterClient)
                return;
            if(MultiplayerSetting.multiplayerSetting.delayStart)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }

            Debug.Log("Master is starting the game");
            PhotonNetwork.LoadLevel(MultiplayerSetting.multiplayerSetting.multiplayerScene);
           
        
      
    }

    void RestartTimer()
    {
        lessThanMaxPlayers = startingTime;
        timeToStart = startingTime;
        atMaxPlayers = 2;
        readyToCount = false;
        readyToStart = false;
    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        // called when multiplayer scene is loaded
        currentScene = scene.buildIndex;
        if(currentScene == MultiplayerSetting.multiplayerSetting.multiplayerScene)
        {
            isGameLoaded = true;

            // for delay start game
            if(MultiplayerSetting.multiplayerSetting.delayStart)
            {
                PV.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
            }

            // for non delay start game
            else
            {
                RPC_CreatePlayer();
            }
        }
    }

    [PunRPC]
    public void RPC_LoadedGameScene()
    {
        playersInRoom++;
        if(playerInGame == PhotonNetwork.PlayerList.Length)
        {
            PV.RPC("RPC_CreatePlayer", RpcTarget.All);
        }

    }

    [PunRPC]
    public void RPC_CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"),transform.position,Quaternion.identity, 0);
    }

    public override void OnPlayerLeftRoom(Player otherplayer)
    {
        base.OnPlayerLeftRoom(otherplayer);
        Debug.Log(otherplayer.NickName+ "Has left the game");
        playersInRoom--;
    }
}

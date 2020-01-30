using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerSetting : MonoBehaviour
{
    public static MultiplayerSetting multiplayerSetting;

    // if it's false then we're having a continuous loading game
    // if it's true then we're having a delay start game
    public bool delayStart;

    public int maxPlayers;
    public int menuScene;
    public int multiplayerScene;

    private void Awake()
    {
        if(MultiplayerSetting.multiplayerSetting == null)
        {
            MultiplayerSetting.multiplayerSetting = this;
        }
        else
        {
            if(MultiplayerSetting.multiplayerSetting != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
}

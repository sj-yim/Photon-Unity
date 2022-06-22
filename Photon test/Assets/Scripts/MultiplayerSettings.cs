using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerSettings : MonoBehaviour
{
    public static MultiplayerSettings multiplayersettings;

    public bool delayStart;
    public int maxPlayers;

    public int menuScene;
    public int multiplayerScene;

    private void Awake()
    {
        if(MultiplayerSettings.multiplayersettings == null)
        {
            MultiplayerSettings.multiplayersettings = this;
        }

        else
        {
            if(MultiplayerSettings.multiplayersettings != this)
            {
                Destroy(this.gameObject);
            }
        }
    }
}

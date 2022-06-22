using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class RoomNumberTracker : MonoBehaviour
{
    public TextMeshProUGUI roomTracker;
    public TextMeshProUGUI activeRoomNumber;

    private string roomName;

    private void Start()
    {
        roomName = PhotonNetwork.CurrentRoom.Name;
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Update()
    {
        roomTracker.text = "Room Number : " + roomName;
        activeRoomNumber.text = "Number of players : " + PhotonNetwork.CurrentRoom.PlayerCount;
    }
}

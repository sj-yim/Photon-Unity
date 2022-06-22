using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;

    public TMP_InputField roomNumberInput;


    public GameObject startButton;
    public GameObject cancelButton;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            gameObject.SetActive(false);
        }

        else
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master server");
        PhotonNetwork.AutomaticallySyncScene = true;
        startButton.SetActive(true);
        cancelButton.SetActive(false);
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("failed to join a random room");
        CreateRoom();
    }

    public void CreateRoom()
    {
        string RoomName = roomNumberInput.text;
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
        PhotonNetwork.CreateRoom(RoomName, roomOps);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room : " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("failed to create room");
        CreateRoom();
    }

    public void ChangeScene(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }

    public void OnStartButtonClicked()
    {
        PhotonNetwork.JoinRandomRoom();
        startButton.SetActive(false);
        cancelButton.SetActive(true);
    }

    public void OnCancelButtonClicked()
    {
        cancelButton.SetActive(false);
        startButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}

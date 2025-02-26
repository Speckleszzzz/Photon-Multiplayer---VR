using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[System.Serializable]
public class DefaultRoom
{
    public string Name;
    public int sceneIndex;
    public int maxPlayer;
}


public class NetworkManager : MonoBehaviourPunCallbacks
{

    public static NetworkManager instance;
    public List<DefaultRoom> defaultRooms;
    public GameObject RoomCanvas;

    public string roomNameToJoin = "test";

    void Awake()
    {
        instance = this;
    }
    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings(); 
        Debug.Log("Trying to connect to the Server...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to the server");
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Player has joined the lobby");
        RoomCanvas.SetActive(true);
        // PhotonNetwork.JoinOrCreateRoom(roomNameToJoin, null, TypedLobby.Default);
    }

    public void InitializeRoom()
    {

        DefaultRoom roomSetting = defaultRooms[0];

        // // loading the level
        PhotonNetwork.LoadLevel(roomSetting.sceneIndex);

        // // creation of the Room
        // RoomOptions roomOptions = new RoomOptions();
        // roomOptions.MaxPlayers = roomSetting.maxPlayer;
        // roomOptions.IsVisible = true;
        // roomOptions.IsOpen = true;
        PhotonNetwork.JoinOrCreateRoom(roomNameToJoin, null, TypedLobby.Default);
    }
    public void InitializeRoom(string roomName)
    {
        DefaultRoom roomSetting = defaultRooms[0];

        // // loading the level
         PhotonNetwork.LoadLevel(roomSetting.sceneIndex);
        PhotonNetwork.JoinOrCreateRoom(roomName, null, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room");
        // PhotonNetwork.JoinOrCreateRoom(roomNameToJoin, null, TypedLobby.Default);
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player has joined the lobby");
        base.OnPlayerEnteredRoom(newPlayer);
    }

}


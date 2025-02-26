using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public string roomNameToJoin = "test";

    public void Start()
    {
        Debug.Log("Connecting ...");

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Connected to the Server");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 10;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        PhotonNetwork.JoinOrCreateRoom(roomNameToJoin, roomOptions, TypedLobby.Default);

        Debug.Log("We are in a Lobby");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        Debug.Log("We are in a room ");
    }



    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("new player has joined");
        base.OnPlayerEnteredRoom(newPlayer);
    }

}

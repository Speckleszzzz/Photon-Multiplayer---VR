using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomItemButton : MonoBehaviour
{
    public string roomName;

    public void OnButtonPressed()
    {
        // RoomList.instance.JoinRoomByName(roomName);
        NetworkManager.instance.InitializeRoom(roomName);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomList : MonoBehaviourPunCallbacks
{
    public static RoomList instance ;

    public GameObject roomManagerGameObject;
    public NetworkManager networkManager;

    public Transform roomListParent;
    public GameObject roomListitemPrefab;

    private List<RoomInfo> cachedRoomList = new List<RoomInfo>();

    void Awake()
    {
        if (instance != null)
        {
            instance = this;
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (cachedRoomList.Count <= 0)
        {
            cachedRoomList = roomList;
        }
        else
        {
            foreach (var room in roomList)
            {
                for (int i = 0; i < cachedRoomList.Count; i++)
                {
                    if (cachedRoomList[i].Name == room.Name)
                    {
                        List<RoomInfo> newList = cachedRoomList;

                        if (room.RemovedFromList)
                        {
                            newList.Remove(newList[i]);
                        }
                        else
                        {
                            newList[i] = room;
                        }

                        cachedRoomList = newList;
                    }
                }
            }
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        foreach (Transform roomItem in roomListParent)
        {
            Destroy(roomItem);
        }

        foreach (var room in cachedRoomList)
        {
            GameObject temp = Instantiate(roomListitemPrefab, roomListParent);
            temp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = room.Name;
            temp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = room.PlayerCount + "/2";

            temp.GetComponent<RoomItemButton>().roomName = room.Name;
        }
    }

    public void ChangeRoomToCreateName(string _roomName)
    {
        networkManager.roomNameToJoin = _roomName;
        UpdateUI();
    }

    public void JoinRoomByName(string _name)
    {
        networkManager.roomNameToJoin = _name;
        roomManagerGameObject.SetActive(true);
        UpdateUI();
    }
}

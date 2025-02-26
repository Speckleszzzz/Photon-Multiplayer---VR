using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using Unity.XR;
using UnityEngine.XR;
using Photon.Pun;

public class PlayerSetup : MonoBehaviourPunCallbacks
{

    private GameObject spawnPlayerPreFab;



    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        spawnPlayerPreFab = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();

        Debug.Log("Player left the room");

        PhotonNetwork.Destroy(spawnPlayerPreFab);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public List<GameObject> yutSticks;
    public List<Transform> YutPositions;
    public Transform Hand;

    public void GetRollers()
    {
        for (int i = 0; i < yutSticks.Count; i++)
        {
            PhotonView photonView = yutSticks[i].GetComponent<PhotonView>();

            if (photonView != null && !photonView.IsMine)
            {
                photonView.RequestOwnership();
            }

            Rigidbody rb = yutSticks[i].GetComponent<Rigidbody>();

            yutSticks[i].transform.SetParent(Hand);
            yutSticks[i].transform.position = YutPositions[i].transform.position;
            yutSticks[i].transform.rotation = YutPositions[i].transform.rotation;

            rb.useGravity = false;
            rb.isKinematic = true;

            photonView.RPC("SyncGrab", RpcTarget.OthersBuffered);
        }
    }

    [PunRPC]
    void SyncGrab()
    {
        foreach (var stick in yutSticks)
        {
            Rigidbody rb = stick.GetComponent<Rigidbody>();

            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }

    public void ThrowRoller()
    {
        Vector3 direction = -transform.forward;

        //Debug.Log("EndActive Event Triggered");
        foreach (var stick in yutSticks)
        {
            Rigidbody rb = stick.GetComponent<Rigidbody>();

            stick.transform.SetParent(null);

            rb.useGravity = true;
            rb.isKinematic = false;

            stick.transform.Rotate(new Vector3(0, UnityEngine.Random.Range(0, 360), 0));
            rb.AddForce(direction * UnityEngine.Random.Range(0.1f, 0.2f), ForceMode.Impulse);
            //   stick.transform.Rotate(new Vector3(0, rotation, 0));
        }

        Debug.Log("The throw should have worked");
        // Calculate flat sides after throwing
        // Invoke("GetRollerValue", 1.0f); // Adjust delay for stick settling
    }
}

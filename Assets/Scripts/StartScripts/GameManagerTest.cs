using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class GameManagerTest : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Awake()
    {
        Sqawn();
    }

    public void Sqawn()
    {
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }
}

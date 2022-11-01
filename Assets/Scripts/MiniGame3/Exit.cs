using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Exit : MonoBehaviourPunCallbacks
{
    public void ExitButton()
    {
        if (PhotonNetwork.IsMasterClient)
            NetworkManager.Instance.BackToLobby();
    }
}

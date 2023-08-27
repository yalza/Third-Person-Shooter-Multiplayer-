using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        yield return new WaitForSeconds(2f);
        PhotonNetwork.JoinLobby();
        PhotonNetwork.LoadLevel(2);
    }
}

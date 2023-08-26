using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPhoton : MonoBehaviour
{
    public Text text;
    void Start()
    {
        text.text = PhotonNetwork.NetworkClientState.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

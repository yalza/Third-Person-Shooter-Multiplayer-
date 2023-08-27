using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matchmaking : Singleton<Matchmaking>
{
    #region Variables
    public byte maxPlayer;
    #endregion

    #region Unity Methods
    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        
    }
    #endregion

    #region Public Methods
    public void QuickMatch()
    {
        PhotonNetwork.JoinRandomRoom();
        PhotonNetwork.LoadLevel(3);
    }

    public void CreateRoomOnClick()
    {
        string roomName = "room_" + PhotonNetwork.Time + "_players";
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        roomOptions.MaxPlayers = maxPlayer;
        PhotonNetwork.CreateRoom(null, roomOptions,null);
    }
    #endregion
}


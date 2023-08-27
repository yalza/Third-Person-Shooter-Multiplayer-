using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using Random = System.Random;

public class LaunchManager : Singleton<LaunchManager>
{
    public string username;
    public bool cleanPrefs;
    public GameObject playerPrefab;

    #region Unity Methods
    private void Awake()
    {
        username = "username";

        if(cleanPrefs)
        {
            DeletePlayerPrefs();
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("PLAYERNAME"))
        {
            PhotonNetwork.NickName = SystemInfo.deviceName + "_" + SystemInfo.deviceModel;
            username = PhotonNetwork.NickName;
        }
        else
        {
            username = PlayerPrefs.GetString("PLAYERNAME");
        }
        LoadSettings();
    }

    private void Update()
    {
        
    }
    #endregion

    #region Public Methods
    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void LoadSettings()
    {
        Debug.Log(PlayerPrefs.GetString(Constant.username));
        if (PlayerPrefs.HasKey(Constant.username))
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString(Constant.username);
            ConnectToPhotonServers();
            PhotonNetwork.LoadLevel(1);
        }
    }

    public void ConnectToPhotonServers()
    {
        if(!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void InputName(TextMeshProUGUI name)
    {
        if(string.IsNullOrEmpty(name.text))
        {
            return;
        }
        username = name.text;
    }

    public void SetPlayerName(TextMeshProUGUI name)
    {
        username = name.text;
        PlayerPrefs.SetString(Constant.username, username);
        PhotonNetwork.NickName = username;
        PlayerPrefs.SetInt(Constant.rank, 1);
        ConnectToPhotonServers();
        PhotonNetwork.LoadLevel(1);
    }

    #endregion

    #region Pun callbacks
    public override void OnConnected()
    {
        Debug.Log(PhotonNetwork.NickName + "Has connected to Photon Servers");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.NickName + "Has connected to Master Server");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log(cause.ToString());
    }

    public override void OnJoinedLobby()
    {
        Debug.Log(PhotonNetwork.NickName + " has joined the lobby with " + PhotonNetwork.CountOfPlayers + " player & " + PhotonNetwork.CountOfRooms + "rooms created");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("We have joined " + PhotonNetwork.CurrentRoom.Name + " with " + PhotonNetwork.CurrentRoom.PlayerCount + " players");
        PhotonNetwork.AutomaticallySyncScene = true;

        Random random = new Random();
        int randomPoint = random.Next(-10, 10);

        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(randomPoint, 0f, randomPoint),Quaternion.identity);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Matchmaking.Instant.CreateRoomOnClick();
        Debug.Log(message);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("New Room was Created");
        Debug.Log("Created " + PhotonNetwork.CurrentRoom.Name);
        Matchmaking.Instant.CreateRoomOnClick();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log(newPlayer + " has entered " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }
    #endregion
}


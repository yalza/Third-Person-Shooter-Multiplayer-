using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;

public class LaunchManager : Singleton<LaunchManager>
{
    public string username;
    public bool cleanPrefs;

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
    #endregion
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class Singleton<T> : MonoBehaviourPunCallbacks where T : MonoBehaviourPunCallbacks
{
    private static T _instant = null;
    public static T Instant
    {
        get
        {
            if (_instant == null)
            {
                if (FindObjectOfType<T>() != null)
                    _instant = FindObjectOfType<T>();
                else
                    new GameObject().AddComponent<T>().name = "Singleton_" + typeof(T).ToString();
            }

            return _instant;
        }
    }

    void Awake()
    {
        if (_instant != null && _instant.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
        {
            Debug.LogError("Singleton already exist " + _instant.gameObject.name);
            Destroy(this.gameObject);
        }
        else
            _instant = this.GetComponent<T>();
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;

    GameObject controller;

    // Start is called before the first frame update
    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), new Vector3(142,6,159), Quaternion.identity, 0, new object[] { PV.ViewID });
        Debug.Log("controller is " + controller);
    }
}

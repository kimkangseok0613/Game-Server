using UnityEngine;
using Photon.Pun;
using System;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform createPosition;

    private static Vector3 Vector3(double v1, double v2, double v3)
    {
        throw new NotImplementedException();
    }

    private void Start()
    {
        Create();
    }
    public void Create()
    {
        PhotonNetwork.Instantiate("Character", createPosition.position, Quaternion.identity);
    }
}

using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;

public class MasterManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform createTransform;
    [SerializeField] float time = 5f;
    private IEnumerator Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            while (true)
            {
                GameObject clone = PhotonNetwork.InstantiateRoomObject("Robot", Vector3.zero, Quaternion.identity);

                clone.transform.position = createTransform.position;

                yield return new WaitForSeconds(time);
            }
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log(PhotonNetwork.PlayerList[0].NickName);

        PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[0]);
    }
}
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;

public class RoomStatus : MonoBehaviourPunCallbacks
{
    [SerializeField] Data data = new Data();

    [SerializeField] TextMeshProUGUI roomNameText;
    [SerializeField] TextMeshProUGUI roomIndexText;
    [SerializeField] TextMeshProUGUI roomPersonalText;

    [SerializeField] Button button;

    private void Start()
    {
        button.onClick.AddListener(() => PhotonNetwork.JoinRoom(data.name));
    }

    public void Refresh(RoomInfo roomInfo,int index)
    {
        data.Name = roomInfo.Name;
        data.index = index + 1;
        data.PlayerCount = roomInfo.PlayerCount;
        data.MaxPlayers = roomInfo.MaxPlayers;

        roomNameText.text = roomInfo.Name;

        // data.index.ToString(); 
        roomIndexText.text = data.index.ToString();

        // roomPersonalText.text = "(" + roomInfo.PlayerCount + " / " + roomInfo.MaxPlayers + ")";
        roomPersonalText.text = $"({roomInfo.PlayerCount}/{data.MaxPlayers})"; 
    }
}

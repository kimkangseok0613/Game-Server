using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using TMPro;
using System;
using UnityEngine.UI;

public class RoomPanel : MonoBehaviourPunCallbacks
{
    [SerializeField] int personal = 2;
    [SerializeField] Toggle[ ] toggles;
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] Button createRoomButton;
    
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();

        roomOptions.MaxPlayers = personal;

        roomOptions.IsOpen = true;

        roomOptions.IsVisible = true;

        PhotonNetwork.CreateRoom(roomNameInputField.text, roomOptions);

        gameObject.SetActive(false);
    }
    private void Start()
    {
        Select();
        OnRoomNameChanged();
    }
    public void Select()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            { 
                personal = i + 2; 
                break; 
            }
        }
    }
    public void OnRoomNameChanged()
    {
        createRoomButton.interactable = string.IsNullOrWhiteSpace(roomNameInputField.text) == false;
    }
}

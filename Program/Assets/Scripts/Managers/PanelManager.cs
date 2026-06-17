using System.Collections.Generic;
using UnityEngine;

public class PanelManager : Singleton<PanelManager>
{
    [SerializeField] GameObject clone = null;

    Dictionary<panel,GameObject> dictionary = new Dictionary<panel,GameObject>();
    public void Open(string message)
    {
        Debug.Log(message);
    }
}
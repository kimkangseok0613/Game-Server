using TMPro;
using UnityEngine;

public class ErrorPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TextMeshProUGUI;

    public void SetMessage(string message)
    {
        TextMeshProUGUI.text = message;
    }
}

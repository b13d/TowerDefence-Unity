using System;
using TMPro;
using UnityEngine;

public enum TypeMessage
{
    Info,
    Warning,
    Error,
}

public class Notification : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMessage;
    [SerializeField] private string message;
    [SerializeField] private TypeMessage type;

    private void Start()
    {
        Destroy(gameObject, 2.5f);
    }

    public string Message
    {
        get { return message; }
        set
        {
            if (value.Length > 0)
            {
                message = value;
                textMessage.text = value;
            }
        }
    }

    public TypeMessage PropertyMessage
    {
        get { return type; }
        set
        {
            type = value;
            switch (value)
            {
                case TypeMessage.Warning:
                    textMessage.color = Color.yellow;
                    break;
                case TypeMessage.Error:
                    textMessage.color = new Color32(255, 99, 71, 255);
                    break;
                case TypeMessage.Info:
                    textMessage.color = new Color32(138, 183, 255, 255);
                    break;
                default:
                    textMessage.color = new Color32(138, 183, 255, 255);
                    break;
            }
        }
    }
}
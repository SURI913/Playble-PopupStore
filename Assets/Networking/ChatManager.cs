using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class ChatManager : MonoBehaviourPunCallbacks
{
    //public InputField chatInput;
    //public Text chatDisplay;

    private void Start()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEventReceived;
    }

    private void OnDestroy()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEventReceived;
    }

    public void SendChatMessage(string tmp)
    {
        //string message = chatInput.text;
        string message = tmp;
        byte evCode = 0; // 사용할 이벤트 코드 (임의로 설정)
        PhotonNetwork.RaiseEvent(evCode, message, RaiseEventOptions.Default, SendOptions.SendReliable);
        //chatInput.text = "";
    }

    private void OnEventReceived(EventData photonEvent)
    {
        if (photonEvent.Code == 0) // 위에서 설정한 이벤트 코드와 일치하는지 확인
        {
            string receivedMessage = (string)photonEvent.CustomData;
            //chatDisplay.text += "\n" + receivedMessage;
            print(receivedMessage);
        }
    }
}

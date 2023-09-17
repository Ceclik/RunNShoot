using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class ConnectorToGame : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField _joinName;
    [SerializeField] private InputField _createName;

    public void CreateRoom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(_createName.text, options);
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(_joinName.text);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

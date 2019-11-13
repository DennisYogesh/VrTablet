using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    GameObject mainMenu; 

    // Start is called before the first frame update

    //Methode um Serververbindung aufzubauen
    public void Connect()
    {
        Debug.Log("Connect wird ausgeführt!");
        //Befehl für die Verbindung zum Photon-Network
        PhotonNetwork.ConnectUsingSettings("v1");
    }

    private void OnConnectedToMaster()
    {
        Debug.Log("Mit Master verbunden. Szene für Lobby laden.");
        PhotonNetwork.JoinLobby();
    }

    void OnJoinedLobby()
    {
        Debug.Log("Mit Lobby verbunden");
        mainMenu.SetActive(false);
        //Zufälligen Raum betreten
        PhotonNetwork.JoinRandomRoom();
    }
    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        //Spawn ausführen
        Spawn();
    }

    void Spawn()
    {
        int randomX = Random.Range(-8, +8);
        int randomY = Random.Range(-4, +4);
        //Erscheinen des Prefabs
        PhotonNetwork.Instantiate("Player", new Vector3(randomX, randomY, 0), Quaternion.identity, 0);
        //PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity, 0);

    }
    void Start()
    {
        //Ausgabe von aktuellem Serverstatus
        mainMenu = GameObject.Find("MainMenu");
        //Áusgabe Anzahl weiterer Spieler im Raum
        Debug.Log("Anzahl weiterer Spieler " + PhotonNetwork.otherPlayers.Length);
        
    }

    // Update is called once per frame
    void Update()
    {
        //Ausgabe des aktuellen Serverstatus
        Debug.Log(PhotonNetwork.connectionStateDetailed.ToString());

        //Ausgabe Anzahl weiterer Spieler im Raum
        Debug.Log("Anzahl weiterer Spieler: " + PhotonNetwork.otherPlayers.Length);
    }
}

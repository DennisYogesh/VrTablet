using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : Photon.MonoBehaviour
{
    private Vector3 correctPlayerPos;
    private Quaternion correctPlayerRot;  
    public GameObject myCamera;
    // Start is called before the first frame update
    void Start()
    {
        //Kamera aktivieren
        myCamera.SetActive(true);
        //Steuerung aktivieren
        GetComponent<PlayerMovement>().enabled = true;
        
    }

    void onPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            //Network player, receive data
            this.correctPlayerPos = (Vector3)stream.ReceiveNext();
            this.correctPlayerRot = (Quaternion)stream.ReceiveNext();

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.isMine)
        {
            /*
            //Kamera aktivieren
            myCamera.SetActive(true);
            //Steuerung aktivieren
            GetComponent<PlayerMovement>().enabled = true;
            */

            transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 5);
        }

    }
}

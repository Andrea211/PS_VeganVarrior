using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
public class AvatarCombat : MonoBehaviour
{
    private PhotonView PV;
    private AvatarSetup avatarSetup;
    public Transform rayOrigin;

    public Text healthDisplay;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        avatarSetup = GetComponent<AvatarSetup>();
        //healthDisplay = GameSetup.GS.healthDisplay;
    }

    // we create here the shooting mechanics
    void Update()
    {
        if(!PV.IsMine)
        {
            return;
        }
        if(Input.GetMouseButton(0))
        {
            PV.RPC("RPC_Shooting", RpcTarget.All);
        }
        healthDisplay.text=avatarSetup.playerHealth.ToString();
    }

    [PunRPC]
    void RPC_Shooting()
    {
        RaycastHit hit;
            if(Physics.Raycast(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward), out hit, 1000))
            {
                Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward)*hit.distance, Color.yellow);
                Debug.Log("Did hit");

                if(hit.transform.tag == "Avatar")
                {
                    hit.transform.gameObject.GetComponent<AvatarSetup>().playerHealth -= avatarSetup.playerDamage;
                }
            }
            else
            {
                Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward)*1000, Color.white);
                Debug.Log("Did not hit");
            }
    }
}

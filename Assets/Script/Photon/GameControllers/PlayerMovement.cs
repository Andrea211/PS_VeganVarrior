using System.Collections;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PhotonView PV;
    private CharacterController myCC;
    public float movementSpeed;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        myCC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PV.IsMine)
        {
            BasicMovement();
            BasicRotation();
        }
    }

    void BasicMovement()
    {
        // if the character is moving forward - pressing W on the keyboard
        if(Input.GetKey(KeyCode.W))
        {
            myCC.Move(transform.forward*Time.deltaTime*movementSpeed);
        }

        // if the character is moving left - pressing A on the keyboard
        if(Input.GetKey(KeyCode.A))
        {
            myCC.Move(-transform.right*Time.deltaTime*movementSpeed);
        }

        // if the character is moving backward - pressing S on the keyboard
        if(Input.GetKey(KeyCode.S))
        {
            myCC.Move(-transform.forward*Time.deltaTime*movementSpeed);
        }

        // if the character is moving right - pressing D on the keyboard
        if(Input.GetKey(KeyCode.D))
        {
            myCC.Move(transform.right*Time.deltaTime*movementSpeed);
        }
    }

    void BasicRotation()
    {
        float mouseX = Input.GetAxis("Mouse X")*Time.deltaTime*rotationSpeed;
        transform.Rotate(new Vector3(0,mouseX,0));

    }
}

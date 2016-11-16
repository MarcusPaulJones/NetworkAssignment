using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SyncTransform : NetworkBehaviour {

    public float lerpRate = 15.0f;

    public float posThresh = 0.5f;
    public float rotThresh = 5.0f;

    [SyncVar]
    private Vector3 syncPos;
    [SyncVar]
    private Quaternion syncRot;

    private Vector3 lastPos;
    private Quaternion lastRot;

    private Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TransmitPosition();
        TransmitRotation();

        LerpPos();
        LerpRot();
    }
    void LerpPos()
    {
        if (isLocalPlayer == false)
        {
            transform.position = Vector3.Lerp(transform.position, syncPos, Time.deltaTime * lerpRate);
        }
    }
    void LerpRot()
    {
        if (isLocalPlayer == false)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, syncRot, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void CmdSendPositionToServer(Vector3 pos)
    {
        syncPos = pos;
    }
    [ClientCallback]
    void TransmitPosition()
    {
        if (isLocalPlayer == true && Vector3.Distance(transform.position, lastPos) > posThresh)
        {
            CmdSendPositionToServer(transform.position);
            lastPos = transform.position;
        }
    }
    [Command]
    void CmdSendRotationToServer(Quaternion rot)
    {
        syncRot = rot;
    }
    [ClientCallback]
    void TransmitRotation()
    {
        if (isLocalPlayer == true && Quaternion.Angle(transform.rotation, lastRot) > rotThresh)
        {
            CmdSendRotationToServer(transform.rotation);
            lastRot = transform.rotation;
        }
    }
}

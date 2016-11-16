using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SyncRigid : NetworkBehaviour
{

    public float lerpRate = 15.0f;

    public float posThreshold = 0.5f;
    public float rotThreshold = 5.0f;

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
        LerpPosition();

        TransmitRotation();
        LerpRotation();
    }
    void LerpPosition()
    {
        if (isLocalPlayer == false)
        {
            rb.position = Vector3.Lerp(rb.position, syncPos, Time.deltaTime * lerpRate);
        }
    }
    void LerpRotation()
    {
        if (isLocalPlayer == false)
        {
            rb.rotation = Quaternion.Lerp(rb.rotation, syncRot, Time.deltaTime * lerpRate);
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
        if (isLocalPlayer == true && Vector3.Distance(rb.position, lastPos) > posThreshold)
        {
            CmdSendPositionToServer(rb.position);
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
        if (isLocalPlayer == true && Quaternion.Angle(rb.rotation, lastRot) > rotThreshold)
        {
            CmdSendRotationToServer(rb.rotation);
        }
    }
}

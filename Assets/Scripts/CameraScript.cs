using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public Transform target; //the player
    public float x; //the difference
    public float y;
    public float z;
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 pos = new Vector3(target.position.x + x, target.position.y + y, target.position.z + z); //tracks the player
        transform.position = pos;
	}
}

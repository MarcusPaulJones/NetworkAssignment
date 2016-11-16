using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour
{
    private bool isGrounded; //controls movement
    public float mSpeed;     //Speed
    public float jHeight;    //jump
    private Rigidbody rb;

    public GameObject prefab; // holds ammo prefab

    public float health; //controls health
    public float iTime = 0.0f;
    public bool immunity = true; //can player be damaged

    private Renderer render; // controllers renderer
    // Use this for initialization
    void Start()
    {
        render = GetComponent<Renderer>();
        render.material.color = Random.ColorHSV(); //Makes player a random color
        rb = GetComponent<Rigidbody>();
        int r = Random.Range(0, 2); //decides what weapon the player gets;
        if (r == 0)//Gives player sword
        {
            gameObject.AddComponent<Sword>();
            gameObject.GetComponent<Sword>().prefab = prefab;
        }
        if (r == 1)//gives player gun
        {
            gameObject.AddComponent<Gun>();
            gameObject.GetComponent<Gun>().prefab = prefab;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)//Checks the player to handle movement
        {
            HandleMove();
        }
    }
    void HandleMove()
    {
        KeyCode[] move = //Directions
        {
            KeyCode.W,
            KeyCode.A,
            KeyCode.D
        };
        foreach (var key in move)
        {
            if (Input.GetKey(key))
            {
                Move(key);
            }
        }
    }
    void Move(KeyCode key)
    {
        Vector3 pos = rb.position;
        switch (key)
        {
            case KeyCode.W: //Code to Jump
                if (isGrounded == true)
                {
                    rb.AddForce(Vector3.up * jHeight, ForceMode.Impulse);
                }
                break;
            case KeyCode.A: //Move Left
                pos -= Vector3.right * mSpeed * Time.deltaTime;
                break;
            case KeyCode.D: //Move Right
                pos += Vector3.right * mSpeed * Time.deltaTime;
                break;
        }
        rb.position = pos;
    }
    void Health() //Controls damage and invulnerability
    {
        if (iTime > 0.0f)
        {
            immunity = true;
        }
        else
        {
            immunity = false;
            iTime = 0.0f;
        }

        if (health <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }
    void OnDestroy()
    {

    }
    void OnCollisionEnter2D(Collision2D col) //Controls jumping
    {
        isGrounded = true;
    }
    void OnCollisionExit2D(Collision2D col)
    {
        isGrounded = false;
    }
}

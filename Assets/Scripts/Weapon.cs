using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Weapon : NetworkBehaviour
{
    public GameObject prefab; //projectile type
    public float reload;
    private float timer;

	void Update ()
    {
        timer += Time.deltaTime;
        if (timer >= reload && isLocalPlayer == true)//if player is local, handle shooting
        {
            HandleShoot();
            timer = 0.0f;
        }
	}
    void HandleShoot()
    {
        KeyCode[] shoot = //Directions
{
            KeyCode.UpArrow,
            KeyCode.LeftArrow,
            KeyCode.DownArrow,
            KeyCode.RightArrow
        };
        foreach (var key in shoot)
        {
            if (Input.GetKey(key))
            {
                Aim(key);
            }
        }
    }
    public virtual void Aim(KeyCode key)
    {
        Vector3 pos = transform.position;
        switch (key)
        {
            case KeyCode.UpArrow: //Aim Up
                Attack(Vector3.up);
                break;
            case KeyCode.LeftArrow: //Aim left
                Attack(Vector3.left);
                break;
            case KeyCode.RightArrow: //Aim Right
                Attack(Vector3.right);
                break;
        }
    }
    public virtual void Attack(Vector3 _direct)
    {

    }

    public class Ammo : NetworkBehaviour //What the weapon uses
    {
        public float damage;

        public virtual void OnTriggerEnter(Collider trig)
        {
            if (trig.gameObject.tag == "Player")
            {
                if (trig.gameObject.GetComponent<Player>().isLocalPlayer != true && trig.gameObject.GetComponent<Player>().immunity == false)// if the players is not the local player && if the player does not have immunity
                {
                    trig.gameObject.GetComponent<Player>().health -= damage; //Cause damage
                    trig.gameObject.GetComponent<Player>().iTime += 1.5f; //activate immunity
                }
            }
        }
    }
}


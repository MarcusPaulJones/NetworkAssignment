using UnityEngine;
using System.Collections;

public class Bullet : Weapon.Ammo
{
    void start()
    {
        damage = 5.0f;//Sets damage
    }
    public override void OnTriggerEnter(Collider trig)
    {
        base.OnTriggerEnter(trig);
        if (trig.gameObject.tag == "Player")
        {
            if (trig.gameObject.GetComponent<Player>().isLocalPlayer != true && trig.gameObject.GetComponent<Player>().immunity == false)// if the players is not the local player && if the player does not have immunity
            {
                Destroy(gameObject);//destroy gameobject
            }
        }
    }
}

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Blade : Weapon.Ammo
{
    float timer = 1;
    public GameObject user; //The player
    public Vector3 direct;
    void Start()//Sets damage
    {
        user = this.gameObject; //Sets player
        damage = 25.0f; //Sets damage

    }
    void Update()
    {
        print(direct);
        transform.position = user.transform.position + new Vector3(direct.x, direct.y, direct.z);//modifies position to be with the player
        timer -= Time.deltaTime;
        if (timer <= 0.0f) //destroys the object after certain time
        {
            Destroy(gameObject);
        }
    }
}

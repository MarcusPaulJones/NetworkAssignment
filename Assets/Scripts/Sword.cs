using UnityEngine;
using System.Collections;
using System;

public class Sword : Weapon
{
    void Start()
    {
        reload = 2.0f;
    }
    public override void Attack(Vector3 _direct)
    {
        GameObject weapon;
        if (_direct == Vector3.up) //Spawns blade above player
        {
            Vector3 direct = _direct * 2.26f;
            weapon = Instantiate(prefab, transform.position + direct, Quaternion.identity) as GameObject;
            weapon.gameObject.transform.localScale = new Vector3(0.5f, 2.0f, 0.5f);
        }
        else
        {
            Vector3 direct = _direct * 1.76f; //Spawns blade to the left or right of the player
            weapon = Instantiate(prefab, transform.position + direct, Quaternion.identity) as GameObject;
            weapon.gameObject.transform.localScale = new Vector3(2.0f, 0.5f, 0.5f);
        }
        weapon.AddComponent<Blade>(); //Adds script
        weapon.GetComponent<Blade>().direct = _direct;
    }
}

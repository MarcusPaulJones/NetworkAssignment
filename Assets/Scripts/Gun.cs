using UnityEngine;
using System.Collections;

public class Gun : Weapon
{

    // Use this for initialization
    void Start()
    {
        reload = 1f;
    }

    // Update is called once per frame
    public override void Aim(KeyCode key) //controls the direction of the bullet
    {
        Vector3 pos = transform.position;
        switch (key)
        {
            case KeyCode.UpArrow: //Fire up
                Attack(Vector3.up);
                break;
            case KeyCode.DownArrow://Fire Down
                Attack(Vector3.down);
                break;
            case KeyCode.LeftArrow://Fire Left
                Attack(Vector3.left);
                break;
            case KeyCode.RightArrow://Fire Right
                Attack(Vector3.right);
                break;
        }
    }
    public override void Attack(Vector3 _direct)
    {
        GameObject weapon = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;//creates projectile
        weapon.AddComponent<Bullet>();//adds component
        weapon.GetComponent<Rigidbody2D>().AddForce(_direct * 25.0f, ForceMode2D.Impulse);//moves it in the direction
    }
}


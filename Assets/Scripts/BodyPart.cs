using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [SerializeField] private int Damage;
    [SerializeField] private int Side = 0;

    public int SetDamage { set { Damage = value; } }

    public delegate void Hit(int damage, int side = 0);
    public static event Hit hit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Weapon")
            hit(Damage, Side);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Weapon")
            hit(5, Side);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoketLauncher : MonoBehaviour
{
    private int side = 0;

    public delegate void swapWeapon(int side, int index);
    public static event swapWeapon SwapWeapon;

    void Start()
    {
        BufLiveTimer.TimeLeft += Cast;
    }

    void Cast()
    {
        BufLiveTimer.TimeLeft -= Cast;
        SwapWeapon(side, 1);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Weapon")
        {
            side = collision.gameObject.GetComponent<Bullet>().Side;
            Cast();
        }
    }
}
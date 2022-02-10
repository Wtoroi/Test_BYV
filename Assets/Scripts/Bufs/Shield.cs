using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private int side = 0;

    public delegate void onShield(int side);
    public static event onShield OnShield;

    void Start()
    {
        BufLiveTimer.TimeLeft += Cast;
    }

    void Cast()
    {
        BufLiveTimer.TimeLeft -= Cast;
        OnShield(side);
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
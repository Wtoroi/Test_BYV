using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heel : MonoBehaviour
{
    [SerializeField] private Slider HPbar;

    private int HP = 10;
    private int side = 0;

    public delegate void hpCheng(int heel, int side);
    public static event hpCheng HpCheng;

    void Start()
    {
        BufLiveTimer.TimeLeft += Cast;
    }

    void Cast()
    {
        BufLiveTimer.TimeLeft -= Cast;
        HpCheng(-3, side);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Weapon")
        {
            HP--;
            HPbar.value = HP;
            side = collision.gameObject.GetComponent<Bullet>().Side;
        }

        if (HP <= 0)
            Cast();
    }
}
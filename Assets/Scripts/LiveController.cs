using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveController : MonoBehaviour
{
    [SerializeField] private int Health = 2;

    public delegate void IDeath();
    public static IDeath enemyDeath;

    void Start()
    {
        BodyPart.hit += Hited;
    }

    private void Hited(int damage, int side = 0)
    {
        Health -= damage;

        if (Health <= 0)
            Death();
    }

    private void Death()
    {
        BodyPart.hit -= Hited;
        foreach (Transform child in transform)
        {
            child.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        StartCoroutine(Waiter());
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(2f);
        enemyDeath();
        Destroy(gameObject);
    }
}
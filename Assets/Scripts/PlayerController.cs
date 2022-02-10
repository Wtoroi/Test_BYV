using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int Side;
    [SerializeField] private GameObject MyShield;

    private Rigidbody2D rb;
    private bool onGround = false;
    private Animator anim;
    private bool canByControld = true;

    void Start()
    {
        PVPOverseer.Jump += Jump;
        PVPOverseer.Death += Death;
        Shield.OnShield += ShieldOn;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Jump(int side)
    {
        if (!canByControld)
            return;
        if (!onGround)
            return;
        if (side != Side)
            return;

        rb.AddForce(new Vector2(0, 550));
        anim.SetTrigger("Jump");
        onGround = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
            onGround = true;
    }

    private void Death(int side)
    {
        canByControld = false;

        if (side != Side)
            return;

        anim.enabled = false;
        PVPOverseer.Death -= Death;
        foreach (Transform child in transform)
            child.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    private void ShieldOn(int side)
    {
        if (side != Side)
            return;

        MyShield.active = true;
        StartCoroutine(Waiter());
    }
    
    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(5f);
        MyShield.active = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    [SerializeField] private GameObject AxeHead;

    private GameObject Bluding;
    private bool onFli = false;
    private bool throwed = false;
    private float zRotate = 0f;

    public bool OnFli { set { onFli = value; } }

    void Start()
    {
        PlayerAnimations.isThrowed += isThrowed;
        Bluding = GameObject.Find("Blud");
    }

    private void FixedUpdate()
    {
        if (!onFli)
            return;

        float step = Time.fixedDeltaTime * 200f;
        zRotate = zRotate + step;
        transform.Rotate(0, 0, step);
        if (zRotate >= 480)
            onFli = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground")
            Destroy(gameObject);

        if (collision.transform.tag == "Enemy")
        {
            PlayerAnimations.isThrowed -= isThrowed;

            gameObject.transform.parent = collision.transform;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;

            Bluding.transform.position = AxeHead.transform.position;
            Bluding.GetComponent<ParticleSystem>().Play();

            Destroy(gameObject.GetComponent<PolygonCollider2D>());
            Destroy(this);
        }
    }

    void isThrowed()
    {
        onFli = true;
        throwed = true;
    }

    public void inShield()
    {
        PlayerAnimations.isThrowed -= isThrowed;
    }
}
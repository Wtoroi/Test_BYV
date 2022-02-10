using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private bool canExplosing = false;
    [SerializeField] private GameObject Exlpous;

    private Rigidbody2D rb;
    private float speed;
    private GameObject Bluding;
    private int side = 0;

    public float Speed { set { speed = value; } }
    public int Side { get { return side; } }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(speed < 0)
            transform.localScale = new Vector3(-0.5f, 0.5f, 1);
        Bluding = GameObject.Find("Blud");

        if (speed > 0)
            side = 1;
        else
            side = -1;
    }

    void Update()
    {
        rb.MovePosition(rb.position + new Vector2(1, 0) * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag != "Enemy")
        {
            Destroy(gameObject);
            return;
        }


        Bluding.transform.position = transform.position;
        if (side == 1)
            Bluding.transform.rotation = Quaternion.Euler(0, -90, 90);
        if (side == -1)
            Bluding.transform.rotation = Quaternion.Euler(180, -90, 90);

        Bluding.GetComponent<ParticleSystem>().Play();

        if (canExplosing)
            Instantiate(Exlpous, transform.position, Exlpous.transform.rotation);

        Destroy(gameObject);
    }
}
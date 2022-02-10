using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private GameObject Axe;
    [SerializeField] private GameObject Arm;
    [SerializeField] private float forse = 20;

    private Animator anim;
    private bool canThrow = true;
    private GameObject axe;

    public delegate void IsThrowed();
    public static IsThrowed isThrowed;

    void Start()
    {
        ThrowLine.throwAxe += ThrowAxe;


        anim = GetComponent<Animator>();
        createAxe();
    }

    void createAxe()
    {
        axe = Instantiate(Axe, Axe.transform.position, Axe.transform.rotation);
        axe.transform.parent = Arm.transform;
    }

    private void ThrowAxe(Vector2 direction)
    {
        if (!canThrow)
            return;

        canThrow = false;
        anim.SetTrigger("Throw");
        StartCoroutine(Waiter(direction));
    }

    IEnumerator Waiter(Vector2 direction)
    {
        yield return new WaitForSeconds(0.25f);
        axe.transform.parent = null;
        Rigidbody2D axeRB = axe.GetComponent<Rigidbody2D>();
        axeRB.bodyType = RigidbodyType2D.Dynamic;
        axeRB.gravityScale = 1;
        axeRB.AddForce(direction * forse * -1);
        isThrowed();
        yield return new WaitForSeconds(2);
        createAxe();
        canThrow = true;
    }
}
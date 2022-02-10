using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVESheld : MonoBehaviour
{
    private bool ShieldOn = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Weapon")
        {
            collision.GetComponent<Rigidbody2D>().drag = 20;
            collision.GetComponent<Rigidbody2D>().gravityScale = 0;
            collision.GetComponent<AxeController>().OnFli = false;
            collision.GetComponent<AxeController>().inShield();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Weapon")
        {
            collision.GetComponent<Rigidbody2D>().drag = 0;
            collision.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    public void OnShield()
    {
        if (ShieldOn)
            return;

        gameObject.active = true;
        ShieldOn = true;
        StartCoroutine(LiveTimeShield());
    }

    void OffShield()
    {
        gameObject.active = false;
        ShieldOn = false;
    }

    IEnumerator LiveTimeShield()
    {
        yield return new WaitForSeconds(6f);
        OffShield();
    }
}
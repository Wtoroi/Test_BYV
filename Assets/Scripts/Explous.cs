using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explous : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Character")
        {
            GameObject target = collision.gameObject;

            foreach (Transform child in target.transform)
                child.GetComponent<HingeJoint2D>().enabled = false;

        }

        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementRotation : MonoBehaviour
{
    [SerializeField] private float targetRotation;
    [SerializeField] private float forse;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, targetRotation, forse * Time.fixedDeltaTime));
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject Exlpous;

    void Start()
    {
        BufLiveTimer.TimeLeft += Cast;
    }

    void Cast()
    {
        BufLiveTimer.TimeLeft -= Cast;
        Instantiate(Exlpous, transform.position, Exlpous.transform.rotation);
        Destroy(gameObject);
    }
}
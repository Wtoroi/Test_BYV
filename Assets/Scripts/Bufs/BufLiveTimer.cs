using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BufLiveTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TimeTest;
    [SerializeField] private float liveTime = 5;

    public delegate void timeLeft();
    public static event timeLeft TimeLeft;

    private void FixedUpdate()
    {
        liveTime -= Time.fixedDeltaTime;

        TimeTest.text = ((int)liveTime).ToString();

        if (liveTime <= 0)
            TimeLeft();
    }
}
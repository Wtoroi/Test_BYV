using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PVPOverseer : MonoBehaviour
{
    [SerializeField] private GameObject HPbar;
    [SerializeField] private GameObject HPbar2;
    [SerializeField] private TextMeshProUGUI WinText;
    [SerializeField] private GameObject[] Bufs;
    [SerializeField] private GameObject MenuScreen;


    private int side;
    private int LeftHealth = 5;
    private int RightHealth = 5;
    private float TimaToNextBuff = 3f;

    public delegate void jump(int side);
    public static event jump Jump;

    public delegate void death(int side);
    public static event death Death;


    void Start()
    {
        BodyPart.hit += Hit;
        Heel.HpCheng += Hit;
    }

    private void FixedUpdate()
    {
        TimaToNextBuff -= Time.fixedDeltaTime;
        if(TimaToNextBuff <= 0)
        {
            SpawnBuff();
            TimaToNextBuff = Random.Range(6.0f, 8.0f);
        }
    }

    void Update()
    {
        if (Input.touchCount <= 0)
            return;

        foreach(Touch touch in Input.touches)
        {
            Vector2 CurrentPosition = GetWorldCoordinate(touch.position);
            if (CurrentPosition.x < -6.25 || CurrentPosition.x > 6.25)
                return;

            if (CurrentPosition.x < 0)
                side = 1;
            else
                side = -1;
            Jump(side);
        }
    }

    private Vector2 GetWorldCoordinate(Vector3 position)
    {
        Vector2 point = new Vector3(position.x, position.y, 1);
        return Camera.main.ScreenToWorldPoint(point);
    }

    private void Hit(int damage, int side)
    {
        if (side > 0)
            LeftHealth  = LeftHealth  - damage;
        else
            RightHealth = RightHealth - damage;

        if (LeftHealth > 5)
            LeftHealth = 5;
        if (RightHealth > 5)
            RightHealth = 5;

        UpdateHP();

        if (LeftHealth <= 0 || RightHealth <= 0)
        {
            Death(side);
            MenuScreen.active = true;

            if (RightHealth <= 0)
                WinText.text = "Left Player Win";
            if (LeftHealth <= 0)
                WinText.text = "Right Player Win";
        }
    }

    private void UpdateHP()
    {
        foreach (Transform child in HPbar.transform)
            child.gameObject.active = false;
        for (int i = 0; i < LeftHealth; i++)
            HPbar.transform.GetChild(i).gameObject.active = true;

        foreach (Transform child in HPbar2.transform)
            child.gameObject.active = false;
        for (int i = 0; i < RightHealth; i++)
            HPbar2.transform.GetChild(i).gameObject.active = true;
    }
    
    private void SpawnBuff()
    {
        GameObject currentObject = Bufs[Random.Range(0, Bufs.Length)];
        Instantiate(currentObject, currentObject.transform.position, currentObject.transform.rotation);
    }

    public void LoadScen(int index)
    {
        SceneManager.LoadScene(index);
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelOverseer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skoreText;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject Sheld;

    private int skore = 0;

    void Start()
    {
        LiveController.enemyDeath += EnemyDeath;
    }

    private void EnemyDeath()
    {
        skore++;
        skoreText.text = skore.ToString();

        Vector3 newPosition = new Vector3(Random.Range(4f, 7.4f), Enemy.transform.position.y, Enemy.transform.position.z);
        Instantiate(Enemy, newPosition, Enemy.transform.rotation);
        Sheld.transform.position = newPosition;
    }
}
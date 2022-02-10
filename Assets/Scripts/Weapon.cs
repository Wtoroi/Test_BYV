using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Weapon : ScriptableObject
{
    [SerializeField] private Sprite weapon;
    [SerializeField] private GameObject bullet;
    [SerializeField] private int damage = 1;

    public Sprite WeaponSprite { get { return weapon; } }
    public GameObject Bullet { get { return bullet; } }
    public int Damege { get { return damage; } }
}
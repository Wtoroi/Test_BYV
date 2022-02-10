using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Weapon[] Weapons;
    [SerializeField] private SpriteRenderer WeaponSprite;
    [SerializeField] private AudioSource soundHoot;
    [SerializeField] private GameObject ShootAnim;
    [SerializeField] private int side;


    private GameObject bullet;
    private int damage;
    private bool canShoot = true;


    void Start()
    {
        WeaponSprite.sprite = Weapons[0].WeaponSprite;
        bullet              = Weapons[0].Bullet;
        damage              = Weapons[0].Damege;
        PVPOverseer.Death += Death;
        RoketLauncher.SwapWeapon += ChengeWeapon;
    }

    private void Death(int side)
    {
        canShoot = false;
    }

    public void Shoot(int side)
    {
        if (!canShoot)
            return;

        soundHoot.Play();
        Vector3 SpawnPosition = new Vector3(WeaponSprite.transform.position.x + 1.5f * side, WeaponSprite.transform.position.y, WeaponSprite.transform.position.z);

        ShootAnim.transform.position = SpawnPosition;
        ShootAnim.GetComponent<ParticleSystem>().Play();

        Instantiate(bullet, SpawnPosition, bullet.transform.rotation).GetComponent<Bullet>().Speed = 5 * side;
        canShoot = false;
        StartCoroutine(Reload());
    }

    void ChengeWeapon(int Side, int Index)
    {
        if (side != Side)
            return;

        WeaponSprite.sprite = Weapons[Index].WeaponSprite;
        bullet              = Weapons[Index].Bullet;
        damage              = Weapons[Index].Damege;
        StartCoroutine(Waiter());
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(5f);

        WeaponSprite.sprite = Weapons[0].WeaponSprite;
        bullet = Weapons[0].Bullet;
        damage = Weapons[0].Damege;
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }
}
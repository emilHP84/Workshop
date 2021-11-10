using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject firePoint;

    [SerializeField] private float speed;
    [SerializeField] private int distance;
    [SerializeField] private bool isPlayer;
    
    private void Update()
    {
        if (isPlayer && Input.GetKeyDown(KeyCode.Space)) Shoot();
    }

    public void Shoot()
    {
        var currentBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation).GetComponent<Projectile>();
        currentBullet.Initialize(speed, distance, isPlayer);
    }
}

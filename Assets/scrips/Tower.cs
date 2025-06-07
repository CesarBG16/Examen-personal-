using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range = 5f;
    public float fireRate = 1f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private float fireCountdown = 0f;

    void Update()
    {
        GameObject target = GetNearestEnemy();

        if (target != null && fireCountdown <= 0f)
        {
            Shoot(target.transform);
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot(Transform target)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetTarget(target);
    }

    GameObject GetNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = range;
        GameObject nearest = null;

        foreach (var enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearest = enemy;
            }
        }

        return nearest;
    }
}
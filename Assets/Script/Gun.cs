using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Bullet Parameter")]
    [SerializeField]
    private GameObject bullet_prefab;
    [SerializeField]
    private float bulletScale = 0.5f;

    [SerializeField]
    private GameObject firePoint;

    [Header("Shooting Parameter")]
    [SerializeField]
    private GameObject shooter;
    [SerializeField]
    public float shooting_rate = 2f;
    [SerializeField]
    private float shooting_angle = 30f;

    float last_time_fire = 0f;


    void Start()
    {
        shooter = gameObject.transform.parent.gameObject;
        bullet_prefab.transform.localScale = Vector3.one * bulletScale;
    }
    public void shoot()
    {
        float shooting_interval = Time.time - last_time_fire;

        if (shooting_interval > (1 / shooting_rate))
        {
            Transform shooter = this.shooter.transform;
            GameObject bullet = Instantiate(bullet_prefab, shooter.position, Quaternion.identity);
            bullet.GetComponent<SpriteRenderer>().sortingOrder = 1;
            last_time_fire = Time.time;
        }
    }

    public void shoot(Transform target)
    {
        float shooting_interval = Time.time - last_time_fire;

        if (shooting_interval > (1 / shooting_rate))
        {
            Transform shooter = this.shooter.transform;
            GameObject bullet = Instantiate(bullet_prefab, gameObject.transform.position, Quaternion.identity);
            bullet.transform.rotation = Quaternion.LookRotation(target.position);
            bullet.GetComponent<SpriteRenderer>().sortingOrder = 1;
            last_time_fire = Time.time;
        }
    }
    
    public void reload()
    {
        // pass
    }

    public void setShooter(Transform shooter)
    {
        if (shooter == null)
            this.transform.parent = shooter.transform;
        else
            Debug.Log("The gun has a shooter already!");
    }
}
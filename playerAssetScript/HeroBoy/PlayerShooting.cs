using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {
    public int damagePerShot = 10;
    public float timeBetweenBullet = 0.15f;
    public float range = 100f;
    bool firing=false;

    PlayerHealth playerHealth;
    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    AudioSource gunAudio;
    LineRenderer gunLine;
    Light gunLight;
    float effectDisplayTime = 0.2f;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        playerHealth = GetComponentInParent<PlayerHealth>();
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
       gunLight = GetComponent<Light>();
        gunAudio = GetComponent<AudioSource>();
    }

	void Update () {
        timer += Time.deltaTime;
        if (firing==true&&timer>=timeBetweenBullet&&playerHealth.bulletvolume>0)
        {
            Shoot();
        }

        if (timer>=timeBetweenBullet*effectDisplayTime)
        {
            DisableEffects();
        }
	}

    public void DisableEffects()
    {
        gunLine.enabled = false;
      gunLight.enabled = false;
    }

    void Shoot()
    {
        timer = 0;
        gunAudio.Play();
        gunParticles.Stop();
        gunParticles.Play();
        gunLight.enabled = true;
        gunLine.enabled = true;
        playerHealth.bulletvolume--;
        gunLine.SetPosition(0, transform.position);
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        if (Physics.Raycast(shootRay,out shootHit,range,shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            
            if (enemyHealth!=null)
            {
                enemyHealth.TakeDamage(damagePerShot,shootHit);
            }
            gunLine.SetPosition(1, shootHit.point);         
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }

    public void Isfire()
    {
        firing = true;
    }

    public void offFire()
    {
        firing = false;
    }
}

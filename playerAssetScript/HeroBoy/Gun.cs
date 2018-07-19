using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    public Transform muzzle;
    public Projectiles projectile;
    public Transform bullet;

    //public int bulletvolume = 100;
    public GameObject player;
    PlayerHealth playerhealth;
    public float msBetweenShots = 100f;
    public float muzzleVelocity = 30f;
    public float effectDisplaytime=1f;

    [SerializeField] Light muzzlelight;

    [SerializeField] ParticleSystem muzzleFlash;

    float nextshotTime;

    private void Start()
    {
        playerhealth = player.GetComponent<PlayerHealth>();
    }

    public void shoot()
    {
        
        if (Time.time>nextshotTime&&playerhealth.bulletvolume>0)
        {         
            muzzleFlash.Stop();
            muzzleFlash.Play();
            StartCoroutine(effectdisplay(effectDisplaytime));
            nextshotTime = Time.time + msBetweenShots / 1000;
            Projectiles newPorjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectiles;
            newPorjectile.setspeed(muzzleVelocity);
            playerhealth.bulletvolume--;          
        }    
    }

    IEnumerator effectdisplay(float time)
    {
        muzzlelight.enabled = true;
        yield return new WaitForSeconds(time);
        muzzlelight.enabled = false;
    }
    //public void addAmor(int addBullets)
    //{
    //    bulletvolume += addBullets;
    //    Debug.Log(bulletvolume);
    //    if (bulletvolume > 100)
    //    {
    //        bulletvolume = 100;
    //    }
    //    if (bulletvolume <= 0)
    //    {
    //        bulletvolume = 0;
    //    }
    //}
}

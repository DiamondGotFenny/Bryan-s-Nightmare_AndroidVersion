using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour {
    public LayerMask collisionmask;
    public float speed = 30f;
    public int damagepershot = 10;
   //LineRenderer gunline;
    //Light gunlight;
    //float effectDisplay = 10f;

    private void Awake()
    {
       //gunline = GetComponent<LineRenderer>();
    //    gunlight = GetComponent<Light>();
    //    collisionmask = LayerMask.GetMask("shootable");
    }

    //this is for the purpose of setting different speeds for different weapons
    public void setspeed(float newspeed)
    {
        speed = newspeed;
    }
	
	// Update is called once per frame
	void Update () {
        float moveDistance = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * moveDistance);
        CheckCollisions(moveDistance);
	}

    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        //gunline.SetPosition(0, transform.position);
        if (Physics.Raycast(ray,out hit,moveDistance,collisionmask,QueryTriggerInteraction.Collide))
        {
          
            OnHitObject(hit);
            //gunline.SetPosition(1, hit.point);
        }
        else
        {
            GameObject.Destroy(gameObject,2f);
        }
        //else
        //{
        //    gunline.SetPosition(1, transform.position + transform.forward * 100f);
        //}
    }

    void OnHitObject(RaycastHit hit)
    {
        IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
        if (damageableObject!=null)
        {
            damageableObject.TakeDamage(damagepershot, hit);
        }
        GameObject.Destroy(gameObject);
    }
}

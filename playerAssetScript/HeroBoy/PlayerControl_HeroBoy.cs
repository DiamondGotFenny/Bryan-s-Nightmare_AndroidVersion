using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl_HeroBoy : MonoBehaviour {

    Animator animat;
    WeaponEquip weaponEquip;

    public float moveSpeed = 6f;
    public float turnSpeed = 10f;
    Vector3 movement;
   // float camRaylength = 100f;
    Rigidbody playerrigidbody;
    [SerializeField] TrailRenderer TR;

    AudioSource swordSound;

    public AudioClip attackSound;

    bool walking;
    bool running;
    bool firing=false;
    bool skilling;

    private void Awake()
    {
        animat = GetComponent<Animator>();
        weaponEquip = GetComponent<WeaponEquip>();
        playerrigidbody = GetComponent<Rigidbody>();
        TR = GetComponentInChildren<TrailRenderer>();
        swordSound = GetComponent<AudioSource>();
    }

    public void Start()
    {
        firing = false;
        skilling = false;
    }

    private void FixedUpdate()
    {
        float MoveH = CrossPlatformInputManager.GetAxis("Horizontal");
        float MoveV = CrossPlatformInputManager.GetAxis("Vertical");

        float TurnH= CrossPlatformInputManager.GetAxis("HorizontalT");
        float TurnV = CrossPlatformInputManager.GetAxis("VerticalT");

        move(MoveH, MoveV);
        Turning(TurnH,TurnV);
        AnimationControl(MoveH, MoveV);
    }

    void Update () {
        TR.time = animat.GetFloat("Blade");
    }
	

    void move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * moveSpeed * Time.deltaTime;
        playerrigidbody.MovePosition(transform.position + movement);
    }

    void AnimationControl(float h,float v)
    {
        if (weaponEquip.AKrifle == true && weaponEquip.Lightsaber == false)
        {
            animat.SetBool("SwordIdle", false);
        }

        if (weaponEquip.AKrifle == false && weaponEquip.Lightsaber == true)
        {
            animat.SetBool("SwordIdle", true);
        }

        if (weaponEquip.AKrifle==true)
        {
            walking = h != 0f || v != 0f;
            animat.SetBool("IsMove", walking);

            if (firing==true && walking == false)
            {
                animat.SetBool("IsShoot", true);
            }
            else
            {
                animat.SetBool("IsShoot", false);
            }
        }

        if (weaponEquip.Lightsaber==true)
        {
            running = h != 0f || v != 0f;
            animat.SetBool("IsMove", false);
            animat.SetBool("IsRun", running);

            if (firing==true&&running==false)
            {
                animat.SetBool("IsAttack", true);
            }
            else
            {
                animat.SetBool("IsAttack", false);
            }

            if (skilling && running == false)
            {
                OnSkill();
            }
            else
            {
                animat.SetBool("IsSkill", false);
            }
        }                
    }

    public void OnSkill()
    {
        animat.SetBool("IsSkill", true);
    }

    void Turning(float h,float v)
    {
        Vector3 turnDir = new Vector3(h, 0, v);
        if (turnDir!=Vector3.zero)
        {
            Vector3 playertoMouse = (transform.position+turnDir) - transform.position;
               playertoMouse.y = 0;
              Quaternion newQuaternion = Quaternion.LookRotation(playertoMouse);
               playerrigidbody.MoveRotation(newQuaternion);
        }
    }

    public void onFire()
    {
        firing = true;
    }

    public void offFire()
    {
        firing = false;
    }

    public void Isskill()
    {
        skilling = true;
    }
    public void Offskill()
    {
        skilling = false;
    }
}

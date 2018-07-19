using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    #region Singleton
    private static PlayerHealth _instance;

    public static PlayerHealth Instance
    {
        get
        {
            if (_instance==null)
            {
                _instance = GameObject.FindObjectOfType<PlayerHealth>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
#endregion

    public int startingHealth = 1000;
    public int currentHealth;
    public int maxBulletVolume=1000;

    [SerializeField]
    private Stat health;

    [SerializeField]
    private Text bulletVolumn;

    AudioSource playerAudio;

    public AudioClip deathclip;
    public AudioClip hurtclip;
    public AudioClip bulletSFX;
    public AudioClip healthSFX;

    public int bulletvolume;

    Animator animt;
  public  bool isDead;
    bool isDamage;
    
	void Awake () {

        #region SingletonRun
        if (_instance==null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this !=_instance)
            {
                Destroy(this.gameObject);
            }
        }
#endregion

        health.Initialize();
        health.MaxVal = startingHealth;
        bulletVolumn.text = "Bulluet Volumn: " + bulletvolume;
        animt = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        Messenger<int>.AddListener(GameEvent.ADD_LIFE, AddLife);
        Messenger<int>.AddListener(GameEvent.ADD_BULLIET, AddBullet);
    }

    private void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvent.ADD_LIFE, AddLife);
        Messenger<int>.RemoveListener(GameEvent.ADD_BULLIET, AddBullet);
    }
    void Start()
    {
        currentHealth = startingHealth;
        bulletvolume = maxBulletVolume;

       

    }
	
	void Update () {
         health.CurrentVal = currentHealth;
        bulletVolumn.text = "Bulluet Volumn: " + bulletvolume;

        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "MainMenu")
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead==true)
        {
            return;
        }
        isDamage = true;
        playerAudio.clip = hurtclip;
        playerAudio.Play();
        currentHealth -= amount;
        animt.SetTrigger("IsDamage");
        if (currentHealth<=0&&!isDead)
        {
            Deadth();
        }     
    }

    public void Deadth()
    {
        isDead = true;
        Messenger<bool>.Broadcast(GameEvent.PLAYER_DEAD,true);
        animt.SetBool("IsDead", true);
        playerAudio.clip = deathclip;
        playerAudio.Play();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        GameObject.Destroy(gameObject, 4f);
    }

     void AddLife(int addLife)
    {
        currentHealth += addLife;
        playerAudio.clip = healthSFX;
        playerAudio.Play();
        if (currentHealth >= startingHealth)
        {
            currentHealth = startingHealth;
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }

    void AddBullet(int addBullet)
    {
        bulletvolume += addBullet;
        playerAudio.clip = bulletSFX;
        playerAudio.Play();
        if (bulletvolume >= maxBulletVolume)
        {
            bulletvolume = 1000;
        }

        if (bulletvolume < 0)
        {
            bulletvolume = 0;
        }
    }

    void Exitdamage()
    {
       isDamage = false;
    }
}




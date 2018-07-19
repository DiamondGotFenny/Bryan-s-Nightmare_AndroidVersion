using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level01BossInstantiage : MonoBehaviour {

    [SerializeField] GameObject Boss;

    GameObject bossclone;

    private void Awake()
    {
        Messenger<int>.AddListener(GameEvent.Current_HOURGLASS, BossInstantiate);
    }

    private void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvent.Current_HOURGLASS, BossInstantiate);
    }

    // Use this for initialization
    void Start () {
       // hourglass = GameObject.FindObjectOfType<HourglassUI>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void BossInstantiate(int currentGlass)
    {
        bossclone = GameObject.Find(Boss.name+"(Clone)");
        if (currentGlass == 4&& bossclone == null)
        {
                Instantiate(Boss, this.transform.position, this.transform.rotation);
        }
    }
}

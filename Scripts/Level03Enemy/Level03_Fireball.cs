using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level03_Fireball : MonoBehaviour {
    public float speed = 1.0f;
    public int damage = 40;
    [SerializeField] GameObject fireballSplitePrefab;
    private GameObject _fireballSplite;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, 0, speed * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player!=null)
        {
            player.TakeDamage(damage);
        }
        if (_fireballSplite==null)
        {
            _fireballSplite = Instantiate(fireballSplitePrefab) as GameObject;
            _fireballSplite.transform.position = transform.position;
            _fireballSplite.transform.rotation = transform.rotation;
        }
        Destroy(this.gameObject);
    }
}

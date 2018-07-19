using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControl : MonoBehaviour {
    [SerializeField] GameObject block;
    [SerializeField] GameObject enter;

    AudioSource entrySFX;
    public AudioClip enterable;

	// Use this for initialization
	void Start () {
        entrySFX = GetComponent<AudioSource>();
        block.SetActive(true);
        enter.SetActive(false);
	}

    private void Awake()
    {
        Messenger<int>.AddListener(GameEvent.Current_HOURGLASS, activeEnter);
    }

    private void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvent.Current_HOURGLASS, activeEnter);
    }

    void activeEnter(int currentHourglass)
    {
        if (currentHourglass== 5)
        {
            entrySFX.clip = enterable;
            entrySFX.loop = true;
            entrySFX.Play();
            block.SetActive(false);
            enter.SetActive(true);
        }
    }
}

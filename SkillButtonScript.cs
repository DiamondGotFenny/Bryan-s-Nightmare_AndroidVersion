using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButtonScript : MonoBehaviour {

    private Animator animt;
    private PressedButtonBehaviour pressButtonBehaviour;
    private CooldownButtonBehaviour cooldownButtonBehaviour;

    public Slider rechargeSlider;
    public float rechargeEnd;
    public float rechargeTime;

    private void Awake()
    {
        animt = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
        pressButtonBehaviour = animt.GetBehaviour<PressedButtonBehaviour>();
        pressButtonBehaviour.skillButtonScript = this;

        cooldownButtonBehaviour = animt.GetBehaviour<CooldownButtonBehaviour>();
        cooldownButtonBehaviour.skillButtonScript = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartRecharge()
    {
        rechargeEnd = Time.time + rechargeTime;
        rechargeSlider.value = 1f;
    }

    public void Setoverlay()
    {
        rechargeSlider.value = (rechargeEnd - Time.time) / rechargeTime;
    }

    public void EndCharge()
    {
        rechargeSlider.value = 0f;
    }
}

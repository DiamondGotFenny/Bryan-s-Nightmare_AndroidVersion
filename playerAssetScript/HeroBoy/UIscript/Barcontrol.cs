using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barcontrol : MonoBehaviour {

    [SerializeField]
    private float fillAmount;

    [SerializeField]
    private float lerpSpeed=2f; //control the movespeed of bar;

    [SerializeField]
    private Image fill;

    [SerializeField]
    private Text healthText;

    public int MaxValue { get; set; }

    public int Value
    {
        set
        {
            string[] temp = healthText.text.Split(':');//split the string into 2 parts by ":"; the part previous is [0], the later part is [1].
            healthText.text = temp[0] + ": " + value;
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }
	
	// Update is called once per frame
	void Update () {
        Handlebar();
	}

    private void Handlebar()
    {
        if (fillAmount != fill.fillAmount)
        {
            fill.fillAmount = Mathf.Lerp(fill.fillAmount,fillAmount,Time.deltaTime*lerpSpeed); // make the bar move smoother;
        }     
    }

    private float Map(int intcurrentvalue, int intinMin, int intinMax, float outMin, float outMax)
    {
        float currentvalue = (float)intcurrentvalue;
        float inMin = (float)intinMin;
        float inMax = (float)intinMax;
        return (currentvalue - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        //if the player's current Health is 80, the max health is 100, the min health is 0; 
        //that is (80-0)*(1-0)/(100-0)+100=80*1/100+0=0.8; this number can fit to the fill amount, range 0~1;
        //if the player's current Health is 74, if the play upgrade, now the max health is 280, the min health is 0; 
        //that is (78-0)*(1-0)/(280-0)+0=78*1/280+0=0.278;
    }
}

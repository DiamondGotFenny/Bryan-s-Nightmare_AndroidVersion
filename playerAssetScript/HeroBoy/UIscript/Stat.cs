using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stat  {

   [SerializeField]
    private Barcontrol bar;

    [SerializeField]
    private int maxval;

    [SerializeField]
    private int currentval;

    public int CurrentVal
    {
        get
        {
            return currentval;
        }
        set
        {
            this.currentval = Mathf.Clamp(value,0,maxval);
            bar.Value = currentval;
        }
    }

    public int MaxVal
    {
        get
        {
            return maxval;
        }
        set
        {
            this.maxval = value;
            bar.MaxValue = maxval;
        }
    }

    public void Initialize()
    {
        this.MaxVal = maxval;
    }
}

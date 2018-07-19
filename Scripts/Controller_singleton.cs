using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_singleton : MonoBehaviour {

    #region Singleton
    private static Controller_singleton _instance;
    public static Controller_singleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Controller_singleton>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
    #endregion

    private void Awake()
    {
        #region SingletonRun
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != _instance)
            {
                Destroy(this.gameObject);
            }
        }
        #endregion        
    }


}

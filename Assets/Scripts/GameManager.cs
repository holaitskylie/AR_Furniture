using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    bool m_bPause = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }   
       
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
        
    public void SetPause()
    {
        OnApplicationPause(!m_bPause);
    }
    
    void OnApplicationPause(bool pauseStatus)
    {
        m_bPause = pauseStatus;
    }
}

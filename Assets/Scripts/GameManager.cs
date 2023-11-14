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
    

    // Update is called once per frame
    void Update()
    {
        // 백버튼을 눌렀을 경우 게임 종료
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // 임의로 버튼에 OnApplicationPause호출
    public void SetPause()
    {
        OnApplicationPause(!m_bPause);
    }
    // 백버튼 눌렀을 때 발생하는 함수
    void OnApplicationPause(bool pauseStatus)
    {
        m_bPause = pauseStatus;
    }
}

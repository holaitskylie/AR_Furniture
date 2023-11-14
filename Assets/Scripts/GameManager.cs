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
        // ���ư�� ������ ��� ���� ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // ���Ƿ� ��ư�� OnApplicationPauseȣ��
    public void SetPause()
    {
        OnApplicationPause(!m_bPause);
    }
    // ���ư ������ �� �߻��ϴ� �Լ�
    void OnApplicationPause(bool pauseStatus)
    {
        m_bPause = pauseStatus;
    }
}

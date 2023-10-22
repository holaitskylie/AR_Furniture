using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneLoad : MonoBehaviour
{
    [SerializeField] Image circleImg;
    [SerializeField] Text progressText;    
        
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());        
    }
     

    IEnumerator LoadScene()
    {
        yield return null;
        //지정한 씬(LobbyScene)을 로드하기 시작한다
        AsyncOperation operation = SceneManager.LoadSceneAsync("Main");
        //씬을 활성화하는 것을 false 처리 한다
        operation.allowSceneActivation = false;        
        

        //아직 로딩이 진행 중이라면, 슬라이더 바의 value를 조절해간다
        //isDone이 true가 되기 전까지 계속 반복
        while (!operation.isDone)
        {
            yield return null;
            //슬라이더의 value가 0.9f이 될 때까지 계속 증가 시킨다
            if (circleImg.fillAmount < 0.9f)
            {
                circleImg.fillAmount = Mathf.MoveTowards(circleImg.fillAmount, 0.9f, Time.deltaTime);
                progressText.text = Mathf.Floor(circleImg.fillAmount * 100).ToString();

            }
            else if (circleImg.fillAmount >= 0.9f)
            {
                circleImg.fillAmount = Mathf.MoveTowards(circleImg.fillAmount, 1f, Time.deltaTime);
                progressText.text = Mathf.Floor(circleImg.fillAmount * 100).ToString();
            }

            if (circleImg.fillAmount >= 1f && operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }

        }

    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneLoad : MonoBehaviour
{
    [SerializeField] Image circleImg;
    [SerializeField] Text progressText;            
    
    void Start()
    {
        StartCoroutine(LoadScene());        
    }
     

    IEnumerator LoadScene()
    {
        yield return null;
        
        AsyncOperation operation = SceneManager.LoadSceneAsync("Main");
        
        operation.allowSceneActivation = false;      
        
        
        while (!operation.isDone)
        {
            yield return null;
            
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

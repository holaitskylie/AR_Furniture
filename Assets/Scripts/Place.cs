using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Place : MonoBehaviour
{    
    //가구 프리팹 저장
    public List<GameObject> prefabs = new List<GameObject>();
    public Transform pool;

    //평면 감지
    public ARRaycastManager rayManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();    
    
    Vector2 vCenter;
        
    GameObject select;

    //오브젝트의 scale과 rotation 값을 저장하는 변수
    private float scale = 1.0f;
    private float angle = 0.0f;


    //슬라이더의 On Value Changed에 의해 슬라이더의 값이 바뀔 때마다 호출
    public void UpdateScale(float sliderValue)
    {
        scale = sliderValue;

        if(select)
        {           
            //오브젝트의 모든 세 축을 통해 균일하게 스케일 확대/축소 시킨다
            select.transform.localScale = Vector3.one * scale;
        }       

    }

    
   public void UpdateRotation(float sliderValue)
    {
        angle = sliderValue;

        if(select)
        {            
            select.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));           
        }
    }

    
    void Update()
    {      
       
        if(select != null)
        {           

           
            vCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
                       

            
           if (rayManager.Raycast(vCenter, hits, TrackableType.PlaneWithinPolygon))           
            {
                
                ARPlane plane = hits[0].trackable.GetComponent<ARPlane>();

                

                
                if(plane != null) 
                {          
                   
                   select.transform.position = hits[0].pose.position;
                                      
                   select.transform.localScale = Vector3.one * scale; //슬라이더에서 설정한 값으로 스케일 조정
                   select.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0)); //슬라이더에서 설정한 값으로 회전 조정
                }
                else
                {                    
                    select.transform.localScale = new Vector3(0, 0, 0);
                }
                
            }
        }
        
    }
    
    public void Select(int type)
    {        
        if(select != null) 
        {           
            Destroy(select);
            select = null ;
        }

        Debug.Log("index : " + type);

        select = Instantiate(prefabs[type]);
    }

   
    public void Set()
    {       
        select.transform.localScale = Vector3.one * scale;
        select.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
                
        select.transform.SetParent(pool);
       
        select = null;        

    }
}

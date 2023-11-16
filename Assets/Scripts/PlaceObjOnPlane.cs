using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObjOnPlane : MonoBehaviour
{
    //평면 감지
    [SerializeField]
    private ARRaycastManager rayManager;    
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    //AR 객체 저장
    private GameObject arObject;
    private GameObject selectedPrefab;    
    private ARObject selectedObject;

    [SerializeField] private Camera arCamera;
    private RaycastHit physicsHit;

    //오브젝트의 scale과 rotation 값을 저장
    private float scale = 1.0f; 
    private float angle = 0.0f;

    private UIManager uiManager;

    public void SetSelecterPrefab(GameObject selectedPrefab)
    {
        this.selectedPrefab = selectedPrefab;      

        Debug.Log("Object Name: " + selectedPrefab.name);
    }
    
    public void UpdateScale(float sliderValue)
    {        
        scale = sliderValue;

        if (selectedObject)
        {            
            selectedObject.transform.localScale = Vector3.one * scale;
        }

    }
    
    public void UpdateRotation(float sliderValue)
    {        
        angle = sliderValue;

        if (selectedObject)        {
            
            selectedObject.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
        }
    }
    
    void Awake()
    {
        rayManager = FindObjectOfType<ARRaycastManager>();
        uiManager = FindObjectOfType<UIManager>();
        selectedPrefab = null;
    }
    
    void Update()
    {
        if(Input.touchCount == 0)
        {
            return;
        }

        
        Touch touch = Input.GetTouch(0);
        
        Vector2 touchPosition = touch.position; //해당 터치의 화면 상의 위치
        
        //UI 뒤로 AR 레이가 쏘지 못하도록 방지
        if(IsPointOverUIObject(touchPosition))
        {
            return;
        }

       
        //첫 번째 터치가 일어났을 때
        if (touch.phase == TouchPhase.Began)
        {
                       
            SelectARObject(touchPosition);
                        
        }        
        else if(touch.phase == TouchPhase.Ended) //터치가 끝나면
        {
            if (selectedObject)
            {
                selectedObject.Selected = false;
            }
            
        }

        SelectARPlane(touchPosition);

    }

    private bool SelectARObject(Vector2 touchPosition)
    {       
        Ray ray = arCamera.ScreenPointToRay(touchPosition);
        
        if (Physics.Raycast(ray, out physicsHit))
        {
            selectedObject = physicsHit.transform.GetComponent<ARObject>();

            if (selectedObject)
            {                
                selectedObject.Selected = true;
                
                return true;
            }
        }

        return false;

    }

    private void SelectARPlane(Vector2 touchPosition)
    {
        //AR Plane에 터치가 발생하면
        if (rayManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            uiManager.tapToPlaceAnimation = false;

            Pose hitPose = hits[0].pose;
           
            if(!selectedObject)
            {              
                //터치한 곳에 selectedPrefab 생성                
                arObject = Instantiate(selectedPrefab, hitPose.position, hitPose.rotation);
                
                selectedObject = arObject.AddComponent<ARObject>();

            }          
            else if (selectedObject.Selected)
            {
                //선택된 오브젝트의 위치 업데이트
                selectedObject.transform.position = hitPose.position;
                selectedObject.transform.rotation = hitPose.rotation;

            }        
                     
                        
        }

    }

    public void RemoveSelectedObject()
    {        
        if (selectedObject)
        {            
            Destroy(selectedObject.gameObject);
            selectedObject = null;
        }
    }

    //UI Block
    bool IsPointOverUIObject(Vector2 pos)
    {
        PointerEventData eventDataCurPosition = new PointerEventData(EventSystem.current);
        eventDataCurPosition.position = pos;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurPosition, results);

        //results.Count가 0보다 크다는 것은 어떤 객체와 충돌했다는 뜻
        return results.Count > 0;

    }
}

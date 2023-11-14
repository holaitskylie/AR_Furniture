using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObjOnPlane : MonoBehaviour
{
    //AR Raycast를 사용하여 평면을 감지
    [SerializeField]
    private ARRaycastManager rayManager;
    //Raycast로 감지된 결과를 저장하는 리스트
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    //생성된 AR 객체를 저장하기 위한 변수
    private GameObject arObject;
    private GameObject selectedPrefab;
    
    private ARObject selectedObject;

    [SerializeField] private Camera arCamera;
    private RaycastHit physicsHit;

    //오브젝트의 scale과 rotation 값을 저장하는 변수
    private float scale = 1.0f; //초기화를 1로 안해주면 0 값이 들어가 화면에 안보임
    private float angle = 0.0f;

    public void SetSelecterPrefab(GameObject selectedPrefab)
    {
        this.selectedPrefab = selectedPrefab;      

        Debug.Log("Object Name: " + selectedPrefab.name);
    }

    //슬라이더의 On Value Changed에 의해 슬라이더의 값이 바뀔 때마다 호출
    //활성화 된 오브젝트의 스케일값 업데이트
    public void UpdateScale(float sliderValue)
    {
        //슬라이더의 값을 받아와 scale 값으로 할당
        scale = sliderValue;

        if (arObject)
        {
            //선택된 오브젝트의 local Scale은 Vector3.one * scale로 설정하여
            //오브젝트의 모든 세 축을 통해 균일하게 스케일 확대/축소 시킨다
            arObject.transform.localScale = Vector3.one * scale;
        }

    }

    //활성화 된 오브젝트의 로테이션값 업데이트
    public void UpdateRotation(float sliderValue)
    {
        //슬라이더의 값을 받아와 angle로 할당
        angle = sliderValue;

        if (arObject)
        {
            //선택한 오브젝트를 Y축 위주로 angle 값에 따라 회전하는 쿼터니언 생성
            arObject.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        rayManager = FindObjectOfType<ARRaycastManager>();
        selectedPrefab = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 0)
        {
            return;
        }

        //첫 번째 일어난 터치를 touch에 저장
        Touch touch = Input.GetTouch(0);
        //해당 터치의 화면 상의 위치를 저장
        Vector2 touchPosition = touch.position;

        //UI를 AR Raycast보다 우선 순위로 둔다
        //UI 뒤로 AR 레이가 쏘지 못하도록 한다
        if(IsPointOverUIObject(touchPosition))
        {
            return;
        }

       
        //첫 번째 터치가 일어났을 때
        if (touch.phase == TouchPhase.Began)
        {
            //터치한 곳에 AR 객체가 없다면
            if (!SelectARObject(touchPosition))
            {
                //터치한 곳에 AR 객체를 생성한다
                SelectARPlane(touchPosition);
            }
        }
        else if(touch.phase == TouchPhase.Ended) //터치가 끝나면
        {
            if (selectedObject)
            {
                selectedObject.Selected = false;
            }
            
        }
        
        
    }

    private bool SelectARObject(Vector2 touchPosition)
    {
        //카메라 중심으로 touchPosition에 ray를 쏜다
        Ray ray = arCamera.ScreenPointToRay(touchPosition);
        //ray와 물리적인 오브젝트와 충돌하면 physicsHit으로 값이 들어온다
        if (Physics.Raycast(ray, out physicsHit))
        {
            selectedObject = physicsHit.transform.GetComponent<ARObject>();
            if (selectedObject)
            {
                selectedObject.Selected = true;
                //selected 로 값을 바꿔주면 setter가 일어나 selectedObject의 컬러값 변경
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
            
            Pose hitPose = hits[0].pose;

            //터치한 곳에 selectedPrefab 생성
            //var newARObj = Instantiate(selectedPrefab, hitPose.position, hitPose.rotation);
            arObject = Instantiate(selectedPrefab, hitPose.position, hitPose.rotation);
           
            //새로운 객체가 생성될 때마다 ARObject 컴포넌트 추가
            arObject.AddComponent<ARObject>();
                        
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObjOnPlane : MonoBehaviour
{
    //AR Raycast를 사용하여 평면을 감지
    private ARRaycastManager rayManager;
    //Raycast로 감지된 결과를 저장하는 리스트
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private GameObject arObject;

    // Start is called before the first frame update
    void Awake()
    {
        rayManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 0)
        {
            return;
        }

        //첫 번째 터치가 일어난 곳으로 position 값을 받아와 레이를 쏜다
        //레이를 쏴서 반환되는 객체는 hits에 저장
        //TrackableType를 PlaneWithinPolygon으로 지정하여 평면 위의 폴리곤에서의 결과값만 hits에 저장
        if(rayManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
        {
            //hits는 리스트로 되어있기 때문에, 가장 먼저 충돌이 일어난 객체의 pose만 가져온다
            var hitPose = hits[0].pose;

            if(!arObject)
            {
                arObject = Instantiate(rayManager.raycastPrefab, hitPose.position, hitPose.rotation);

            }
            else
            {
                arObject.transform.position = hitPose.position;
                arObject.transform.rotation = hitPose.rotation;
            }
                  

        }
        
    }
}

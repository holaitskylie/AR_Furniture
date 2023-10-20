using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Place : MonoBehaviour
{
    //가구 오브젝트의 프리팹을 저장하는 리스트
    public List<GameObject> prefabs = new List<GameObject>();
    //AR Raycast를 사용하여 평면을 감지
    public ARRaycastManager rayManager;
    //Raycast로 감지된 결과를 저장하는 리스트
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    //가구 오브젝트를 배치할 부모 오브젝트
    public Transform pool;

    //스크린의 센터점(2D 좌표의 가운데 점)을 저장하는 변수
    Vector2 vCenter;

    //현재 선택된 가구 오브젝트를 저장하는 변수
    //사용자가 선택한 가구 프리팹이 할당
    GameObject select;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    //사용자가 가구를 선택하고 배치할 때 마다 호출
    //가구를 배치할 때 해당 가구 오브젝트 스케일을 조정하여 보이거나 감추는 역할을 한다
    void Update()
    {
        //사용자가 가구를 선택한 상태이다
        if(select != null)
        {
            //스크린의 센터점(2D 좌표의 가운데 점 : 가로 중심, 세로 중심의 좌표)를 저장
            vCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

            //스크린 중아에 레이캐스트를 쏴서 평면과 부딪힌 것들을 hits에 저장
            //TrackableType.PlaneWithinPolygon : 평면을 감지하는 폴리곤
            //카메라를 통해 들어온 정보를 통해 평면을 감지 (폴리곤을 통해 감지한 타입이 평면이면 감지한 것)
            if (rayManager.Raycast(vCenter, hits, TrackableType.PlaneWithinPolygon))
            {
                //충돌한 오브젝트가 평면인지 확인하기 위해 사용
                //0번은 감지한 것 중 가장 가깝고 큰 평면
                //리스트의 첫 번째 요소(충돌한 가장 가까운 오브젝트)의 trackable 컴포넌트를 가져와 ARPlane 타입의 변수 plane에 저장
                ARPlane plane = hits[0].trackable.GetComponent<ARPlane>();

                //충돌한 오브젝트가 평면으로 감지된 경우
                if(plane != null) { 
                    //감지해 들어온 정보(바닥 평면을 감지한)의 위치값을 통해 오브젝트를 위치 시킨다
                    //사용자가 가구를 움직일 때마다 가구가 평면에 따라 움직이게 된다
                    select.transform.position = hits[0].pose.position;

                    //배치할 때 스케일 값(1,1,1)로 지정
                    //가구를 배치할 때 항상 일정한 크기로 표시하게 한다
                    select.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    //평면이 없으면 스케일을 (0,0,0)으로 만들어 오브젝트를 화면에서 안보이게 한다
                    select.transform.localScale = new Vector3(0, 0, 0);
                }
                
            }
        }
        
    }

    //가구를 선택했을 때 호출되는 함수
    //type은 가구 오브젝트가 저장된 리스트의 인덱스가 된다
    public void Select(int type)
    {
        //이미 선택된 가구 오브젝트가 존재하면 다음 코드로 진행
        if(select != null) 
        {
            //기존에 선택된 가구 오브젝트를 삭제하고 새로운 가구가 선택 될 수 있도록 초기화
            Destroy(select);
            select = null ;
        }

        Debug.Log("index : " + type);

        select = Instantiate(prefabs[type]);
    }

    //선택된 가구를 화면에 배치한다 : pool 오브젝트의 자식으로 들어가게 된다
    public void Set()
    {
        select.transform.localScale = new Vector3(1, 1, 1);

        //select 오브젝트를 pool의 자식으로 들어가게 한다
        select.transform.SetParent(pool);

        //가구가 배치된 뒤에 변수를 초기화하여 새로운 가구를 선택할 수 있게 한다
        select = null;
        

    }
}

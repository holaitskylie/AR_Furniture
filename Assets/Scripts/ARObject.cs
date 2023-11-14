using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARObject : MonoBehaviour
{
    //객체가 선택되었는지 상태를 알려주는 변수
    private bool IsSelected;
    private MeshRenderer meshRender;
    private Color originColor;

    public bool Selected
    {
        get
        {
            return this.IsSelected;
        }

        set
        {
            IsSelected = value;
            //selected 값이 set 될 때마다(업데이트 될 때마다) 다음 메소드 호출
            UpdateMaterialColor();
        }
    }
   
    void Awake()
    {
        meshRender = GetComponent<MeshRenderer>();
        if (!meshRender)
        {
            //메쉬 렌더러가 없는 오브젝트에게는 메쉬 렌더러 컴포넌트 생성
            meshRender = this.gameObject.AddComponent<MeshRenderer>();
        }

        //초기 컬러값을 저장하여, 선택 해제 후 다시 컬러를 되돌릴 때 사용
        originColor = meshRender.material.color;
        
    }

    private void UpdateMaterialColor()
    {
        if(IsSelected)
        {
            meshRender.material.color = Color.gray;
        }
        else
        {
            meshRender.material.color = originColor;
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        nowCustom();
    }

    void nowCustom()    //현재 커스텀을 DB에서 받아옴, NowSettings에 저장
    {

    }

    public void SelectCustom() //커스텀 아이템 선택 시 실행 메소드
    {
        //해당 커스텀의 itemname 가져오고, data_dialog에서 아이템 row 찾기. 해당 아이템 row 찾으면, 해당하는 texture등으로 변경해준다.
    }
}

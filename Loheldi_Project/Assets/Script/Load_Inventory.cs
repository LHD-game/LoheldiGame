using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_Inventory : MonoBehaviour
{
    private int itemnum;
    public void GetMyItem()
    {
       /* Where where = new Where();
        where.Equal("item", "옷장");  컬럼값 불러와보려고 시도한 흔적*/
        /*where.BeginsWith("item", "옷장");
        where.BeginsWith("item", "세면대");*/

        var bro = Backend.GameData.GetMyData("INVENTORY", new Where(), 10);
        
        if (bro.IsSuccess() == false)
        {
            Debug.Log("요청 실패");
            return;
        }
        if (bro.GetReturnValuetoJSON()["rows"].Count <= 0)
        {
            // 요청이 성공해도 where 조건에 부합하는 데이터가 없을 수 있기 때문에
            // 데이터가 존재하는지 확인
            // 위와 같은 new Where() 조건의 경우 테이블에 row가 하나도 없으면 Count가 0 이하 일 수 있다.
            Debug.Log("요청 성공했지만 테이블에 row가 하나도 없음");
            return;
        }
        for (int i = 0; i < bro.Rows().Count; ++i)
        {
            var inDate = bro.Rows()[i]["itemCode"]["S"].ToString();
            Debug.Log(inDate);
        } /*이거는 유저 개인 모든 데이터 조회하는 itemCode 코드 int 형식은 잘 불러와지는데 string으로 불러오고 싶으면 json 한번 더 써야할듯.*/
        
        /*string item = bro.Rows()[0]["item"]["S"].ToString();
        Debug.Log(item);*/
        




    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

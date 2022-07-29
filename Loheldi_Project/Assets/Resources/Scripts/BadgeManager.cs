using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadgeManager : MonoBehaviour
{
    //배지 갱신 메소드
    public static void GetBadge(string b_code)  //상점에서 아이템 구매시, coin은 음수. 미니게임/퀘스트 등으로 코인을 얻는 경우 coin은 양수
    {
        SaveAccBadge(b_code);
    }

    //서버 상 ACC_BADGE에 저장하는 메소드
    static void SaveAccBadge(string b_code)
    {
        Where where = new Where();
        where.Equal("BCode", b_code);
        var bro = Backend.GameData.GetMyData("ACC_BADGE", where);

        if (bro.IsSuccess() == false)
        {
            Debug.Log("요청 실패");
        }
        else
        {
            JsonData rows = bro.GetReturnValuetoJSON()["rows"];
            //없을 경우 아이템 행 추가
            if (rows.Count <= 0)
            {
                Param param = new Param();
                param.Add("BCode", b_code);

                var insert_bro = Backend.GameData.Insert("ACC_BADGE", param);

                if (insert_bro.IsSuccess())
                {
                    Debug.Log("배지 수령 완료");
                }
                else
                {
                    Debug.Log("배지 수령 오류");
                }
            }
            else
            {
                //이미 해당 배지를 가지고 있음
                return;
            }


        }
    }
}

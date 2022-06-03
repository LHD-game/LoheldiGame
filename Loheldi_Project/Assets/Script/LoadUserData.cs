using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;

public class LoadUserData : MonoBehaviour
{
    public Text usernick;
    public Text userbirth;
    // Start is called before the first frame update
    void Start()
    {
        var bro = Backend.GameData.GetMyData("ACC_INFO", new Where(), 10);
        if (bro.IsSuccess() == false)
        {
            // 요청 실패 처리
            Debug.Log("load failed");
            return;
        }
        if (bro.GetReturnValuetoJSON()["rows"].Count <= 0)
        {
            // 요청이 성공해도 where 조건에 부합하는 데이터가 없을 수 있기 때문에
            // 데이터가 존재하는지 확인
            // 위와 같은 new Where() 조건의 경우 테이블에 row가 하나도 없으면 Count가 0 이하 일 수 있다.
            Debug.Log(bro);
            return;
        }
        // 검색한 데이터의 모든 row의 inDate 값 확인
        for (int i = 0; i < bro.Rows().Count; ++i)
        {
            string nick = bro.Rows()[i]["NICKNAME"]["S"].ToString();
            string birth = bro.Rows()[i]["BIRTH"]["S"].ToString();
            Debug.Log(nick);
            Debug.Log(birth);
            usernick.text = nick;
            userbirth.text = birth;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

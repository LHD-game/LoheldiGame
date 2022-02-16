using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using LitJson;
using System;

public class BackendInfo : MonoBehaviour
{
    Dictionary<string, string> charset = new Dictionary<string, string>
        {
            { "Level" , "1" },
            { "Coin", "2000"},
            { "Exp", "150" }
        }; 
    
        // 값을 Dictionary 로 사용하기
    /*Dictionary<string, int> equipment = new Dictionary<string, int>
        {
            { "weapon", 123 },
            { "armor", 111 },
            { "helmet", 1345 }
        };*/
    // Insert 는 '생성' 작업에 주로 사용된다. 
    /*public void OnClickInsertData()
    {

        *//*int charLevel = Random.Range(0, 99);
        int charExp = Random.Range(0, 9999);*//*
        //int charScore = Random.Range(0, 99999);

        // Param은 뒤끝 서버와 통신을 할 때 넘겨주는 파라미터 클래스 입니다. 
        Param param = new Param();
        param.Add("lv", 100);
        param.Add("exp", 30);
        param.Add("equipItem", equipment);

        BackendReturnObject BRO = Backend.GameInfo.Insert("character", param);


        if (BRO.IsSuccess())
        {
            print("동기 방식 데이터 입력 성공");
        }
        else Error(BRO.GetErrorCode(), "gameData");

    }*/
    //비동기 정보 삽입
    /*public void OnClickInsertData()
    {
        Param lunch = new Param();
        lunch.Add("how much", 332);
        lunch.Add("when", "yesterday");
        lunch.Add("what", "eat chocolate");

        Dictionary<string, int> dic = new Dictionary<string, int>
        {
            { "dic1", 1 },
            { "dic4", 2 },
            { "dic2", 4 }
        };

        Dictionary<string, string> dic2 = new Dictionary<string, string>
        {       
            { "mm", "j" },
            { "nn", "n" },
            { "dd", "2" }
        };

        string[] list = { "a", "b" };
        int[] list2 = { 400, 500, 600 };

        Param param = new Param();
        param.Add("이름", "cheolsu");
        param.Add("score", 99);
        param.Add("lunch", lunch);
        param.Add("dic_num", dic);
        param.Add("dic_string", dic2);
        param.Add("list_string", list);
        param.Add("list_num", list2);

        BackendAsyncClass.BackendAsync(Backend.GameInfo.Insert, "character", param, (callback) =>
        {
            if (callback.IsSuccess())
            {
                print("비동기 방식 데이터 삽입 성공");
            }
            else Error(callback.GetErrorCode(), "gameData");
        });
    }*/
    public void AddUserData()
    {
        /*Param lunch = new Param();
        lunch.Add("how much", 332);
        lunch.Add("when", "yesterday");
        lunch.Add("what", "eat chocolate");*/

        

        /*Dictionary<string, string> dic2 = new Dictionary<string, string>
        {
            { "mm", "j" },
            { "nn", "n" },
            { "dd", "2" }
        };

        string[] list = { "a", "b" };
        int[] list2 = { 400, 500, 600 };*/

        Param param = new Param();
        param.Add("이름", "cheolsu");
        param.Add("charset", charset);
        
        BackendReturnObject BRO = Backend.GameData.Insert("character", param);

        if (BRO.IsSuccess())
        {
            print("동기 방식 데이터 입력 성공");
        }
        else Error(BRO.GetErrorCode(), "gameData");
    }
    // Public 테이블에 저장된 데이터 불러오기
    public void OnClickPublicContents()
    {
        var bro = Backend.GameData.GetMyData("character", new Where(), 10);
        // default limit 는 10개이다. 아래 코드는 10개의 데이터를 가져온다.
        // BackendReturnObject BRO = Backend.GameInfo.GetPublicContents("custom");

        if (bro.IsSuccess())
        {
            bro.GetReturnValuetoJSON();
            bool l = charset.TryGetValue("Level", out string testValue);
            {
                print("Level:" + testValue);
            };
            bool c = charset.TryGetValue("Coin", out string testValue2);
            {
                print("Coin:" + testValue2);
            };
            bool e = charset.TryGetValue("Exp", out string testValue3);
            {
                print("Exp:" + testValue3);
            };
            /* print($"level: {charset.TryGetValue("Level", out string testValue)}"){
                 Debug.Log("level: " + testValue);
             };*/
           /* print($"coin: {charset}"[1]);
            print($"exp: {charset}"[2]);*/
        }

        else
        {
            // 에러 체크
            CheckError(bro);
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
            var inDate = bro.Rows()[i]["inDate"]["S"].ToString();
            Debug.Log(inDate);
        }
    }
    



    /*public void readData()
    {
        var bro = Backend.GameData.GetMyData("character", new Where(), 10);

        if (bro.IsSuccess() == false)
        {
            // 요청 실패 처리
            Debug.Log(bro);
            return;

            //print($"level: {level} coin: {coin} exp: {exp}");            
        }
        else
        {
            bro.GetReturnValuetoJSON();
            print($"level: {charset}");
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
            var inDate = bro.Rows()[i]["inDate"]["S"].ToString();
            Debug.Log(inDate);
        }
        
    }*/

    /*public void readDataAsync()
    {
        BackendAsyncClass.BackendAsync(Backend.GameInfo.GetPrivateContents, "character", (callback) =>
        {
            if (callback.IsSuccess())
            {
                JsonData jsonData = callback.GetReturnValuetoJSON();
                string level = jsonData["level"][0].ToString();
                string coin = jsonData["coin"][0].ToString();
                string exp = jsonData["exp"][0].ToString();

                //dataIndate = jsonData["inDate"][0].ToString();

                print($"Level : {level} Coin : {coin} Exp : {exp}");
                print("비동기 방식 정보 읽기 완료.");
            }
            else Error(callback.GetErrorCode(), "ReadData");
        });
        *//*BackendReturnObject BRO = Backend.GameInfo.GetPrivateContents("character");

        if (BRO.IsSuccess())
        {
            JsonData jsonData = BRO.GetReturnValuetoJSON()["rows"][0];
            string level = jsonData["level"][0].ToString();
            string coin = jsonData["coin"][0].ToString();
            string exp = jsonData["exp"][0].ToString();
            string dataIndate = jsonData["inDate"][0].ToString();
            print($"Level : {level} Coin : {coin} Exp : {exp}");
            print("동기 방식 정보 읽기 완료");
        }
        else Error(BRO.GetErrorCode(), "ReadData");*/
    /*string[] select = { "owner_inDate"};

    //자신의 owner_inDate
    string owner_inDate = Backend.UserInDate;
    // 타인의 ower_inDate
    //string owner_inDate = "2018-07-06T05:22:41.000Z";

    // 테이블 내 해당 rowIndate를 지닌 row를 조회
    var bro = Backend.GameData.Get("character", owner_inDate, select);
    Debug.Log(bro);

    // 테이블 내 해당 rowIndate를 지닌 row를 조회
    // select에 존재하는 컬럼만 리턴
    bro = Backend.GameData.Get("character", "rowIndate", select);
    Debug.Log(bro);*//*
}*/

    // 에러 코드 확인
    void CheckError(BackendReturnObject BRO)
    {
        switch (BRO.GetStatusCode())
        {
            case "200":
                Debug.Log("해당 유저의 데이터가 테이블에 없습니다.");
                break;

            case "404":
                if (BRO.GetMessage().Contains("gamer not found"))
                {
                    Debug.Log("gamerIndate가 존재하지 gamer의 indate인 경우");
                }
                else if (BRO.GetMessage().Contains("table not found"))
                {
                    Debug.Log("존재하지 않는 테이블");
                }
                break;

            case "400":
                if (BRO.GetMessage().Contains("bad limit"))
                {
                    Debug.Log("limit 값이 100이상인 경우");
                }

                else if (BRO.GetMessage().Contains("bad table"))
                {
                    // public Table 정보를 얻는 코드로 private Table 에 접근했을 때 또는
                    // private Table 정보를 얻는 코드로 public Table 에 접근했을 때 
                    Debug.Log("요청한 코드와 테이블의 공개여부가 맞지 않습니다.");
                }
                break;

            case "412":
                Debug.Log("비활성화된 테이블입니다.");
                break;

            default:
                Debug.Log("서버 공통 에러 발생: " + BRO.GetMessage());
                break;

        }
    }





    void Error(string errorCode, string type)
    {
        if (errorCode == "DuplicatedParameterException")
        {
            if (type == "UserFunc") print("중복된 사용자 아이디 입니다.");
            else if (type == "UserNickname") print("중복된 닉네임 입니다.");
            else if (type == "Friend") print("이미 요청되었거나 친구입니다.");
        }
        else if (errorCode == "BadUnauthorizedException")
        {
            if (type == "UserFunc") print("잘못된 사용자 아이디 혹은 비밀번호 입니다.");
            else if (type == "Message") print("잘못된 닉네임입니다.");
        }
        else if (errorCode == "UndefinedParameterException")
        {
            if (type == "UserNickname") print("닉네임을 다시 입력해주세요");
        }
        else if (errorCode == "BadParameterException")
        {
            if (type == "UserNickname") print("닉네임 앞/뒤 공백이 있거나 20자 이상입니다.");
            else if (type == "UserPW") print("잘못된 이메일입니다.");
            else if (type == "gameData") print("잘못된 유형의 테이블 입니다.");
            else if (type == "Message") print("보내는 사람, 받는 사람이 같습니다.");
        }
        else if (errorCode == "NotFoundException")
        {
            if (type == "UserPW") print("등록된 이메일이 없습니다.");
            else if (type == "Coupon") print("중복 사용이거나 기간이 만료된 쿠폰입니다.");
            else if (type == "gameData") print("해당 테이블을 찾을 수 없습니다.");
        }
        else if (errorCode == "Too Many Request")
        {
            if (type == "UserPW") print("요청 횟수를 초과하였습니다. (1일 5회)");
        }
        else if (errorCode == "PreconditionFailed")
        {
            if (type == "gameData") print("해당 테이블은 비활성화 된 테이블 입니다.");
            else if (type == "Friend") print("받는 사람 혹은 보내는 사람의 요청갯수가 꽉 찬 상태입니다.");
            else if (type == "Message") print("설정한 글자수를 초과하였습니다.");
        }
        else if (errorCode == "ServerErrorException")
        {
            if (type == "gameData") print("하나의 row이 400KB를 넘습니다");
        }
        else if (errorCode == "ForbiddenError")
        {
            if (type == "gameData") print("타인의 정보는 삭제가 불가능합니다.");
            else if (type == "Message") print("콘솔에서 쪽지 최대보유수를 설정해주세요");
        }
        else if (errorCode == "MethodNotAllowedParameterException")
        {
            if (type == "Message") print("상대방의 쪽지가 가득 찾습니다.");
        }
    }
}

    

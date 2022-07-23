using BackEnd;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedSelect : MonoBehaviour //씨앗 버튼에 달리도록 합니다.
{
    public Text FarmNum;

    public void Selected()  //씨앗을 선택한다. 씨앗은 빈 텃밭에 심겨지도록 한다.
    {
        for(int i=0; i< GardenControl.empty_ground.Length; i++)
        {
            if (GardenControl.empty_ground[i])  //빈 텃밭이라면
            {
                GameObject item_code = this.transform.Find("ItemCode").gameObject;
                Text item_code_txt = item_code.GetComponent<Text>();
                string i_code = item_code_txt.text;

                if (SeedAmount(i_code)) //씨앗 수가 0 이상인지 체크 및 씨앗 개수 1 감소
                {
                    string ground_num = "G1";
                    switch (i)
                    {
                        case 0:
                            ground_num = "G1";
                            break;
                        case 1:
                            ground_num = "G2";
                            break;
                        case 2:
                            ground_num = "G3";
                            break;
                        case 3:
                            ground_num = "G4";
                            break;
                        default:
                            break;
                    }

                    PlayerPrefs.SetString(ground_num, i_code);  //해당하는 텃밭에, 해당하는 씨앗 코드 저장
                    Debug.Log(ground_num);
                    Debug.Log(i_code);
                    DateTime datetime = DateTime.Now;
                    PlayerPrefs.SetString(ground_num + "Time", datetime.ToString("g"));  //해당하는 텃밭에, 심은 시각(현재시각)을 저장

                    GardenControl.instance.GroundIsUpdated();
                    break;
                }
                else    //씨앗 개수가 0 이하입니다. todo: 팝업을 띄우거나 하기
                {
                    Debug.Log("씨앗 개수가 0 이하입니다.");
                }
            }
            else
            {
                //빈 텃밭이 없습니다. todo: 팝업을 띄우거나 하기
                Debug.Log("빈 텃밭이 없습니다.");
            }
        }
        
    }

    bool SeedAmount(string icode)
    {
        //Inventory 테이블 불러와서, 여기에 해당하는 아이템과 일치하는 코드가 있을 경우 개수를 1감소시켜서 업데이트
        //만약 아이템 갯수가 0 이하였다면, return false.

        bool result = false;

        Where where = new Where();
        where.Equal("ICode", icode);
        var bro = Backend.GameData.GetMyData("INVENTORY", where);

        if (bro.IsSuccess() == false)
        {
            Debug.Log("요청 실패");
        }
        else
        {
            string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

            int item_amount = (int)bro.FlattenRows()[0]["Amount"];
            if(item_amount > 0)
            {
                result = true;

                item_amount--;

                Param param = new Param();
                param.Add("ICode", icode);
                param.Add("Amount", item_amount);

                var update_bro = Backend.GameData.UpdateV2("INVENTORY", rowIndate, Backend.UserInDate, param);
                if (update_bro.IsSuccess())
                {
                    Debug.Log("INVENTORY 테이블 업데이트 성공.");
                    GardenControl.is_pop_garden = false; //씨앗 리스트를 업데이트 시켜 화면에 새로이 출력하도록
                }
                else
                {
                    Debug.Log("INVENTORY 테이블 업데이트 실패.");
                }
            }
        }
        return result;  //기존 아이템 수가 0 이하였다면 false, 0 초과였라면 true를 반환
    }
}

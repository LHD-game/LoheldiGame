using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var bro = Backend.GameData.GetMyData("PLAY_INFO", new Where());
        
        Param param = new Param();
        if (bro.IsSuccess())
        {
            var bro2 = Backend.GameData.GetMyData("QUEST_INFO", new Where());
            
            if (bro2.IsSuccess())
            {
                if (bro2.GetReturnValuetoJSON()["rows"].Count <= 0)
                {
                   
                    /*string QID = bro2.FlattenRows()[0]["QID"].ToString();*/
                    /*string Exp = rows[0]["Reward"]["Exp"].ToString();
                    string Coin = rows[0]["Reward"]["Coin"].ToString();
                    string Gagu = rows[0]["Reward"]["1010102"].ToString();*/

                    param.Add("QID", "0_1");
                    param.Add("Exp", "10");
                    param.Add("Coin", "10");
                    param.Add("1010102", "3");

                    Backend.GameData.Insert("QUEST_INFO", param);
                    Debug.Log(param);
                }
                else
                {
                    string QuestPreg = bro.Rows()[0]["QuestPreg"]["S"].ToString();
                    Debug.Log(QuestPreg);
                    string newQuest = bro2.Rows()[0]["QID"]["S"].ToString();
                    Debug.Log(newQuest);
                    if (QuestPreg == newQuest)
                    {
                        Debug.Log("��������Ʈ �غ�Ϸ�");
                        string selectedProbabilityFileId = "54787";

                        var bro3 = Backend.Chart.GetChartContents(selectedProbabilityFileId);
                        if (!bro3.IsSuccess())
                        {
                            Debug.LogError(bro3.ToString());
                            return;
                        }

                        JsonData json = bro3.FlattenRows();
                        JsonData rows = bro3.GetReturnValuetoJSON()["rows"];

                        /*�Ʒ� �ݺ����� �迭 �ʰ� ������ ���⿡ �����صξ����ϴ�. - �̹���*/
                        for (int i = 0; i < rows.Count; i++)
                        {
                            if (rows.Count >= 1)
                            {
                                string QID = rows[i]["QID"]["S"].ToString();
                                //string EXP = rows[1][0]["Reward"]["Exp"].ToString();
                                /* string Coin = json[1]["Reward"]["Coin"]["S"].ToString();
                                 string Gagu = json[1]["Reward"]["1010102"]["S"].ToString();*/
                                
                                Debug.Log(QID);
                                //Debug.Log(EXP);
                                /*Debug.Log(Coin);
                                Debug.Log(Gagu);*/
                            }
                        }
                        

                    }
                    else
                    {
                        Debug.Log("���� �Ϸ���� ���� ����Ʈ�� �ֽ��ϴ�.");
                    }
                }
            }
            
            
            
        }
        /*string selectedProbabilityFileId = "560";

        var bro = Backend.Chart.GetChartContents(selectedProbabilityFileId);

        if (!bro.IsSuccess())
        {
            Debug.LogError(bro.ToString());
            return;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

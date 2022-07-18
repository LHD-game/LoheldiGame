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
                        Debug.Log("다음퀘스트 준비완료");
                        string selectedProbabilityFileId = "54787";

                        var bro3 = Backend.Chart.GetChartContents(selectedProbabilityFileId);
                        if (!bro3.IsSuccess())
                        {
                            Debug.LogError(bro3.ToString());
                            return;
                        }

                        JsonData json = bro3.FlattenRows();
                        JsonData rows = bro3.GetReturnValuetoJSON()["rows"];

                        Param param2 = new Param();
                        /*아래 반복문은 배열 초과 오류가 나기에 수정해두었습니다. - 이민진*/
                        for (int i = 0; i < rows.Count; i++)
                        {
                            string QID = rows[i]["QID"]["S"].ToString();
                            if (QID == QuestPreg)
                            {
                                string QID2 = rows[i+1]["QID"]["S"].ToString();
                                string QName = rows[i+1]["QName"]["S"].ToString();
                                string From = rows[i+1]["From"]["S"].ToString();
                                string Content = rows[i+1]["Content"]["S"].ToString();
                                string Reward = rows[i + 1]["Reward"]["S"].ToString(); 
                                /*string Exp = json[i + 1]["Reward"]["Exp"]["S"].ToString();
                                string Coin = json[i + 1]["Reward"]["Coin"]["S"].ToString();
                                string Gagu = json[i + 1]["Reward"]["1010102"]["S"].ToString();*/
                                string authorName = rows[i+1]["authorName"]["S"].ToString();
                                //string EXP = rows[1][0]["Reward"]["Exp"].ToString();
                                /* string Coin = json[1]["Reward"]["Coin"]["S"].ToString();
                                 string Gagu = json[1]["Reward"]["1010102"]["S"].ToString();*/

                                Debug.Log(QID2);
                                Debug.Log(QName);
                                Debug.Log(From);
                                Debug.Log(Content);
                                Debug.Log(Reward);
                                /*Debug.Log(Coin);
                                Debug.Log(Gagu);*/
                                Debug.Log(authorName);
                                //Debug.Log(EXP);
                                /*Debug.Log(Coin);
                                Debug.Log(Gagu);*/
                                param2.Add("QID", QID2);
                                param2.Add("QName", QName);
                                param2.Add("From", From);
                                param2.Add("Content", Content);
                                param2.Add("Reward", Reward);
                                param2.Add("authorName", authorName);
                                Backend.GameData.Insert("QUEST_INFO", param2);

                            }
                        }
                        

                    }
                    else
                    {
                        Debug.Log("아직 완료되지 않은 퀘스트가 있습니다.");
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

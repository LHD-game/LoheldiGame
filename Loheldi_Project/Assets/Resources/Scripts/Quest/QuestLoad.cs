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
        JsonData rows = bro.GetReturnValuetoJSON()["rows"];
        Param param = new Param();
        if (bro.IsSuccess())
        {
            if(rows.Count <= 0)
            {
                Backend.Chart.GetChartContents("54787");
                string QID = rows[0]["QID"]["S"].ToString();
                int Exp = int.Parse(rows[0]["Reward"]["Exp"]["N"].ToString());
                int Coin = int.Parse(rows[0]["Reward"]["Coin"]["N"].ToString());
                int Gagu = int.Parse(rows[0]["Reward"]["1010102"]["N"].ToString());

                param.Add("QID", QID);
                param.Add("Exp", Exp);
                param.Add("Coin", Coin);
                param.Add("Gagu", Gagu);

                Backend.GameData.Insert("QUEST_INFO", param);

            }
            else
            {
                string QuestPreg = bro.Rows()[0]["QuestPreg"]["S"].ToString();
                string newQuest = rows[0]["QID"]["S"].ToString();
                if(QuestPreg == newQuest)
                {
                    Debug.Log("다음퀘스트 준비완료");
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

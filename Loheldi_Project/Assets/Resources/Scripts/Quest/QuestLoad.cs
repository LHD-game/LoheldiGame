using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLoad : MonoBehaviour
{
    public GameObject TempObject;

    string QID;
    string QID2;
    string QID3;
    string QName;
    string From;
    string Content;
    string Reward;
    string authorName; 
    public QuestScript Quest;
    public QuestDontDestroy DontDestroy;

    public void QuestLoadStart()
    {
        DontDestroy = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        Quest = GameObject.Find("chatManager").GetComponent<QuestScript>();
        string selectedProbabilityFileId = "55472";
        var bro3 = Backend.Chart.GetChartContents(selectedProbabilityFileId);
        JsonData rows = bro3.GetReturnValuetoJSON()["rows"];

        var bro2 = Backend.GameData.GetMyData("QUEST_INFO", new Where());
        
        if (!bro3.IsSuccess())
        {
            Debug.LogError(bro3.ToString());
            return;
        }
        if (bro2.IsSuccess())
        {
            
            Param param2 = new Param();
            
            string QuestPreg;
            if (DontDestroy.weekend) //주말일 때
                QuestPreg = PlayerPrefs.GetString("WeeklyQuestPreg"); //주말 퀘스트 번호로 바뀔 예정
            else //주말이 아닐 떄
                QuestPreg = PlayerPrefs.GetString("QuestPreg");

            if (QuestPreg.Equals("0_0"))
            {
                QID2 = rows[0]["QID"]["S"].ToString();
                QName = rows[0]["QName"]["S"].ToString();
                From = rows[0]["From"]["S"].ToString();
                Content = rows[0]["Content"]["S"].ToString();
                Reward = rows[0]["Reward"]["S"].ToString();
                authorName = rows[0]["authorName"]["S"].ToString();

                DontDestroy.QuestIndex = QID2;
                DontDestroy.ButtonPlusNpc = authorName;
                DontDestroy.From = From;
                Debug.Log(DontDestroy.QuestIndex);

                Debug.Log(QID2);
                Debug.Log(QName);

                //이미 퀘스트가 들어가있는지 검사
                Where where = new Where();
                where.Equal("QID", QID2);
                var chk_bro = Backend.GameData.GetMyData("QUEST_INFO", where);
                JsonData chk_rows = chk_bro.GetReturnValuetoJSON()["rows"];
                
                if (chk_rows.Count <= 0)
                {
                    Param param = new Param();
                    param.Add("QID", QID2);
                    param.Add("QName", QName);
                    param.Add("From", From);
                    param.Add("Content", Content);
                    param.Add("Reward", Reward);
                    param.Add("authorName", authorName);
                    Backend.GameData.Insert("QUEST_INFO", param);
                    Debug.Log("퀘스트 수령 완료");
                }
                else
                {
                    Debug.Log("이미 해당 퀘스트를 수령했습니다.");
                }
            }
            else
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    string QID = rows[i]["QID"]["S"].ToString();
                    if (QID == QuestPreg)   //0_0은 아닌 상태에서 퀘스트 진행도와 일치
                    {
                        QID3 = rows[i + 1]["QID"]["S"].ToString();
                        QName = rows[i + 1]["QName"]["S"].ToString();
                        From = rows[i + 1]["From"]["S"].ToString();
                        Content = rows[i + 1]["Content"]["S"].ToString();  //replace는 우편함에서 실행하니 하지 않으셔도 무방합니다.
                        Reward = rows[i + 1]["Reward"]["S"].ToString();
                        authorName = rows[i + 1]["authorName"]["S"].ToString();

                        DontDestroy.QuestIndex = QID3;
                        DontDestroy.ButtonPlusNpc = authorName;
                        DontDestroy.From = From;
                        Debug.Log(QID3);
                        Debug.Log(QName);
                        Debug.Log(DontDestroy.QuestIndex);

                        //이미 퀘스트가 들어가있는지 검사
                        Where where = new Where();
                        where.Equal("QID", QID3);
                        var chk_bro = Backend.GameData.GetMyData("QUEST_INFO", where);
                        JsonData chk_rows = chk_bro.GetReturnValuetoJSON()["rows"];
                        if (chk_rows.Count <= 0)
                        {
                            param2.Add("QID", QID3);
                            param2.Add("QName", QName);
                            param2.Add("From", From);
                            param2.Add("Content", Content);
                            param2.Add("Reward", Reward);
                            param2.Add("authorName", authorName);
                            Backend.GameData.Insert("QUEST_INFO", param2);
                            Debug.Log("퀘스트 수령 완료");
                            break;
                        }
                        else
                        {
                            Debug.Log("이미 해당 퀘스트를 수령했습니다.");
                        }
                    }
                }
            }
            Debug.Log(DontDestroy.QuestIndex);
            Quest.QuestStart();
        }
        
    }
}

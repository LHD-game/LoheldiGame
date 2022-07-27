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
        string selectedProbabilityFileId = "55216";
        var bro3 = Backend.Chart.GetChartContents(selectedProbabilityFileId);
        Param param = new Param();
        Where where = new Where();
        where.Equal("QuestPreg", "0_0");
        JsonData rows = bro3.GetReturnValuetoJSON()["rows"];

        var bro4 = Backend.GameData.GetMyData("PLAY_INFO", where);
        JsonData rows2 = bro4.GetReturnValuetoJSON()["rows"]; //bro4 JSON
        

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
                QuestPreg = PlayerPrefs.GetString("QuestPreg"); //주말 퀘스트 번호로 바뀔 예정
            else //주말이 아닐 떄
                QuestPreg = PlayerPrefs.GetString("QuestPreg");
            Debug.Log(QuestPreg);

            /*string newQuest = bro2.Rows()[0]["QID"]["S"].ToString();
            Debug.Log(newQuest);*/

            //string QchartID = rows[0]["QID"]["S"].ToString();
            //if (QuestPreg == QchartID)
            //{
            //Debug.Log("다음퀘스트 준비완료");
            string QID0 = rows2[0]["QuestPreg"]["S"].ToString();
            Debug.Log(QID0);
            JsonData bjson = bro2.GetReturnValuetoJSON()["rows"];
            string Questinfo = bjson[0]["QID"]["S"].ToString();
            Debug.Log(Questinfo);
            if (QID0 == Questinfo)
            {
                
                QID2 = rows[0]["QID"]["S"].ToString();
                QName = rows[0]["QName"]["S"].ToString();
                From = rows[0]["From"]["S"].ToString();
                Content = rows[0]["Content"]["S"].ToString();
                Reward = rows[0]["Reward"]["S"].ToString();
                authorName = rows[0]["authorName"]["S"].ToString();

                DontDestroy.QuestIndex = QID2;
                DontDestroy.ButtonPlusNpc = authorName;

                Debug.Log(QID2);
                Debug.Log(QName);

                param.Add("QID", QID2);
                param.Add("QName", QName);
                param.Add("From", From);
                param.Add("Content", Content);
                param.Add("Reward", Reward);
                param.Add("authorName", authorName);
                Backend.GameData.Insert("QUEST_INFO", param);
                Debug.Log("param 입력완료");
                
            }
            else
            {
                
                for (int i = 0; i < rows.Count; i++)
                {
                    string QID = rows[i]["QID"]["S"].ToString();
                    if (QID == Questinfo)   //0_0은 아닌 상태에서 퀘스트 진행도와 일치
                    {
                        QID3 = rows[i + 1]["QID"]["S"].ToString();
                        QName = rows[i + 1]["QName"]["S"].ToString();
                        From = rows[i + 1]["From"]["S"].ToString();
                        Content = rows[i + 1]["Content"]["S"].ToString();  //replace는 우편함에서 실행하니 하지 않으셔도 무방합니다.
                        Reward = rows[i + 1]["Reward"]["S"].ToString();
                        authorName = rows[i + 1]["authorName"]["S"].ToString();

                        DontDestroy.QuestIndex = QID3;
                        DontDestroy.ButtonPlusNpc = authorName;
                        Debug.Log(QID3);
                        Debug.Log(QName);

                        param2.Add("QID", QID3);
                        param2.Add("QName", QName);
                        param2.Add("From", From);
                        param2.Add("Content", Content);
                        param2.Add("Reward", Reward);
                        param2.Add("authorName", authorName);
                        Backend.GameData.Insert("QUEST_INFO", param2);
                        Debug.Log("param2 입력완료");
                        break;

                    }
                }
            }
            



            //}
            //else
            //{
            //   Debug.Log("아직 완료되지 않은 퀘스트가 있습니다.");
            //}
            Quest.QuestStart();
        }
        /*string QID0 = rows2[0]["QuestPreg"]["S"].ToString();
        Debug.Log(QID0);
        if (QID0 == "0_0")
        {
            QID2 = rows[0]["QID"]["S"].ToString();
            QName = rows[0]["QName"]["S"].ToString();
            From = rows[0]["From"]["S"].ToString();
            Content = rows[0]["Content"]["S"].ToString();
            Reward = rows[0]["Reward"]["S"].ToString();
            authorName = rows[0]["authorName"]["S"].ToString();

            DontDestroy.QuestIndex = QID2;
            DontDestroy.ButtonPlusNpc = authorName;

            Debug.Log(QID2);
            Debug.Log(QName);

            param.Add("QID", QID2);
            param.Add("QName", QName);
            param.Add("From", From);
            param.Add("Content", Content);
            param.Add("Reward", Reward);
            param.Add("authorName", authorName);
            Backend.GameData.Insert("QUEST_INFO", param);
            Debug.Log("param 입력완료");
        }
        else
        {
            Debug.Log("다음퀘스트 준비완료");

        }*/




        /*string selectedProbabilityFileId = "560";

        var bro = Backend.Chart.GetChartContents(selectedProbabilityFileId);

        if (!bro.IsSuccess())
        {
            Debug.LogError(bro.ToString());
            return;
        }*/
    }
}

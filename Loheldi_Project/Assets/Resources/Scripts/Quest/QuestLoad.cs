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
    string QName;
    string From;
    string Content;
    string Reward;
    string authorName;
    public MailLoad MailLoad;
    public QuestDontDestroy DontDestroy;

    void Start()
    {
        DontDestroy = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        var bro = Backend.GameData.GetMyData("PLAY_INFO", new Where());
        
        Param param = new Param();
        if (bro.IsSuccess())
        {
            var bro2 = Backend.GameData.GetMyData("QUEST_INFO", new Where());

            string selectedProbabilityFileId = "55216";
            var bro3 = Backend.Chart.GetChartContents(selectedProbabilityFileId);
            if (!bro3.IsSuccess())
            {
                Debug.LogError(bro3.ToString());
                return;
            }
            if (bro2.IsSuccess())
            {
                /*if (bro2.GetReturnValuetoJSON()["rows"].Count <= 0)
                {
                   
                    *//*string QID = bro2.FlattenRows()[0]["QID"].ToString();*/
                /*string Exp = rows[0]["Reward"]["Exp"].ToString();
                string Coin = rows[0]["Reward"]["Coin"].ToString();
                string Gagu = rows[0]["Reward"]["1010102"].ToString();*/

                /*param.Add("QID", "0_0");
                param.Add("Exp", "10");
                param.Add("Coin", "10");
                param.Add("1010102", "3");

                Backend.GameData.Insert("QUEST_INFO", param);
                Debug.Log(param);*//*
            }*/
                string QuestPreg;
                if (DontDestroy.weekend) //주말일 때
                    QuestPreg = bro.Rows()[0]["QuestPreg"]["S"].ToString(); //주말 퀘스트 번호로 바뀔 예정
                else //주말이 아닐 떄
                    QuestPreg = bro.Rows()[0]["QuestPreg"]["S"].ToString();
                Debug.Log(QuestPreg);
                /*string newQuest = bro2.Rows()[0]["QID"]["S"].ToString();
                Debug.Log(newQuest);*/
                JsonData rows = bro3.GetReturnValuetoJSON()["rows"];
                string QchartID = rows[0]["QID"]["S"].ToString();
                if (QuestPreg == QchartID)
                {
                    Debug.Log("다음퀘스트 준비완료");
                        
                    for (int i = 0; i < rows.Count; i++)
                    {
                        string QID = rows[i]["QID"]["S"].ToString();
                        Param param2 = new Param();
                            
                        if (QID == QuestPreg)
                        {
                            QID2 = rows[i+1]["QID"]["S"].ToString();
                            QName = rows[i+1]["QName"]["S"].ToString();
                            From = rows[i+1]["From"]["S"].ToString();
                            Content = rows[i+1]["Content"]["S"].ToString().Replace("<n>", "\n"); ;
                            Reward = rows[i + 1]["Reward"]["S"].ToString(); 
                            authorName = rows[i+1]["authorName"]["S"].ToString();

                            DontDestroy.QuestIndex = QID2;
                            Debug.Log(QID2);
                            Debug.Log(QName);
                            Debug.Log(From);
                            Debug.Log(Content);
                            Debug.Log(Reward);
                            Debug.Log(authorName);

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
        /*string selectedProbabilityFileId = "560";

        var bro = Backend.Chart.GetChartContents(selectedProbabilityFileId);

        if (!bro.IsSuccess())
        {
            Debug.LogError(bro.ToString());
            return;
        }*/
    }

    // Update is called once per frame
    public void QuestLoadonMailPost()
    {
        Debug.Log(QName);
        TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/UI/QuestMail"), MailLoad.MailList);
        TempObject.transform.GetChild(0).GetComponent<Text>().text = QName;
        TempObject.transform.GetChild(1).GetComponent<Text>().text = From;
        TempObject.transform.GetChild(2).GetComponent<Text>().text = Content;
        TempObject.GetComponent<Button>().onClick.AddListener(MailLoad.MailLoading);
    }
}

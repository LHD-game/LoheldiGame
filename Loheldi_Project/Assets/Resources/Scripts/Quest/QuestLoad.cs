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
        
        Param param = new Param();

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
            string QuestPreg;
            if (DontDestroy.weekend) //주말일 때
                QuestPreg = PlayerPrefs.GetString("QuestPreg"); //주말 퀘스트 번호로 바뀔 예정
            else //주말이 아닐 떄
                QuestPreg = PlayerPrefs.GetString("QuestPreg");
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
                        Content = rows[i + 1]["Content"]["S"].ToString();  //.Replace("<n>", "\n"); -> replace는 우편함에서 실행하니 하지 않으셔도 무방합니다.
                        Reward = rows[i + 1]["Reward"]["S"].ToString(); 
                        authorName = rows[i+1]["authorName"]["S"].ToString();

                        DontDestroy.QuestIndex = QID2;
                        Debug.Log(QID2);
                        Debug.Log(QName);

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
            
            
            
        
        /*string selectedProbabilityFileId = "560";

        var bro = Backend.Chart.GetChartContents(selectedProbabilityFileId);

        if (!bro.IsSuccess())
        {
            Debug.LogError(bro.ToString());
            return;
        }*/
    }

/*    public void QuestLoadonMailPost()
    {
        Debug.Log(QName);
        TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/UI/QuestMail"), MailLoad.MailList);
        TempObject.transform.GetChild(0).GetComponent<Text>().text = QName;
        TempObject.transform.GetChild(1).GetComponent<Text>().text = From;
        TempObject.transform.GetChild(2).GetComponent<Text>().text = Content;
        TempObject.GetComponent<Button>().onClick.AddListener(MailLoad.MailLoading);
    }*/
}

using BackEnd;
using LitJson;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailSelect : MonoBehaviour //우편 프리펩에 붙는 스크립트.
{
    public static List<GameObject> reward_list = new List<GameObject>();
    public static List<GameObject> right_detail_text = new List<GameObject>();
    
    public static string this_qid = "null";

    //리스트 중 우편 선택시 -> 해당 버튼에 있는 제목 등등을 모두 가져와 오른쪽 화면에 띄운다.
    public void SelectMail()
    {
        QuestDontDestroy QDD;
        //버튼에 달려있던 정보를 가져온다.
        GameObject qid = this.transform.Find("QID").gameObject;
        Text qid_txt = qid.GetComponent<Text>();
        this_qid = qid_txt.text;

        GameObject title = this.transform.Find("Title").gameObject;
        Text title_txt = title.GetComponent<Text>();

        GameObject from = this.transform.Find("From").gameObject;
        Text from_txt = from.GetComponent<Text>();

        GameObject content = this.transform.Find("Content").gameObject;
        Text content_txt = content.GetComponent<Text>();

        GameObject reward = this.transform.Find("Reward").gameObject;
        Text reward_txt = reward.GetComponent<Text>();

        //세부 정보를 띄울 ui 공간(오브젝트)을 가져온다.
        GameObject QuestDetail = GameObject.Find("RightDetail");
        //세부 정보 오브젝트의 값을 변경해준다.
        GameObject qid_detail = QuestDetail.transform.Find("RQID").gameObject;
        Text qid_d_txt = qid_detail.GetComponent<Text>();
        qid_d_txt.text = qid_txt.text;

        GameObject title_detail = QuestDetail.transform.Find("RTitle").gameObject;
        Text title_detail_txt = title_detail.GetComponent<Text>();
        title_detail_txt.text = title_txt.text;

        GameObject from_detail = QuestDetail.transform.Find("RFrom").gameObject;
        Text from_detail_txt = from_detail.GetComponent<Text>();
        from_detail_txt.text = from_txt.text;

        GameObject content_detail = QuestDetail.transform.Find("RContent").gameObject;
        Text content_detail_txt = content_detail.GetComponent<Text>();
        content_detail_txt.text = content_txt.text;

        if(right_detail_text.Count <= 0)
        {
            right_detail_text.Add(qid_detail);
            right_detail_text.Add(title_detail);
            right_detail_text.Add(from_detail);
            right_detail_text.Add(content_detail);
        }


        //todo: reward
        MakeRewardList(reward_txt.text);

        //보상 받기 버튼 활성화: 퀘스트 진행도가 퀘스트 아이디보다 크거나 같아야 한다.
        //1. 현재 qid와 퀘스트의 qid를 가져와 _를 기준으로 앞부분 슬라이스
        //2. if (퀘스트 qid <= 현재 qid 앞부분 값)
        //3. -->뒷부분 슬라이스
        //4. -->if (퀘스트 qid <= 현재 qid 뒷부분 값)
        //5.    -->disable 버튼 비활성화
        GameObject reward_disable_btn = QuestDetail.transform.Find("ReceiveMailDisable").gameObject;
        GameObject already_recieve_btn = QuestDetail.transform.Find("ReceiveMailAlready").gameObject;
        QDD = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        already_recieve_btn.SetActive(false);
        reward_disable_btn.SetActive(true);
        string[] q_qid = qid_txt.text.Split('_');
        string QuestType = null;
        if (Int32.Parse(q_qid[0]) < 22)
        {
            QuestType = "QuestPreg";
        }
        else
            QuestType = "WeeklyQuestPreg";
        string[] my_qid = PlayerPrefs.GetString(QuestType).Split('_');
        int q_front = int.Parse(q_qid[0]);
        int my_front = int.Parse(my_qid[0]);
        int q_back = int.Parse(q_qid[1]);
        int my_back = int.Parse(my_qid[1]);
        if (q_front < my_front) //0_2, 1_1 -> 0<1
        {
            reward_disable_btn.SetActive(false);
        }
        else if(q_front == my_front && q_back <= my_back) //1_1, 1_2
        {
            //int q_back = int.Parse(q_qid[1]);
            //int my_back = int.Parse(my_qid[1]);
            //if (q_back <= my_back)
            //{
                reward_disable_btn.SetActive(false);
            //}
        }
        else
        {
            if (QDD.SDA)
            {
                content_detail_txt.text = "토요일은 퀘스트 진행이 불가합니다";
            }
            else if (!QDD.weekend && QuestType.Equals("WeeklyQuestPreg"))
            {
                content_detail_txt.text = "해당 퀘스트는 일요일에만 진행이 가능합니다";
            }
            else if (QDD.weekend && QuestType.Equals("QuestPreg"))
                content_detail_txt.text = "해당 퀘스트는 평일에만 진행이 가능합니다";
        }

    }

    //보상 리스트를 만든다.
    void MakeRewardList(string reward_txt)
    {
        for (int k = 0; k < reward_list.Count; k++)
        {
            Destroy(reward_list[k]);
        }
        reward_list.Clear();

        //Reward에 저장된 보상들(string)을 json 타입으로 변환
        JObject reward_json = JObject.Parse(reward_txt);

        //json 키값 추출
        string[] key = new string[reward_json.Count];
        int i = 0;
        foreach (var j in reward_json){
            key[i] = j.Key;
            i++;
        }

        //reward box 오브젝트 객체를 생성

        GameObject rewardBox = (GameObject)Resources.Load("Prefabs/UI/MailRewardBox");
        for (int j=0; j<reward_json.Count; j++)
        {
            GameObject parents = GameObject.Find("RewardContent");
            
            GameObject child = Instantiate(rewardBox);
            child.transform.SetParent(parents.transform);

            RectTransform rt = child.GetComponent<RectTransform>(); //아이템 박스 크기 재설정
            rt.localScale = new Vector3(1f, 1f, 1f);

            reward_list.Add(child);

            //todo: exp, coin, badge, item에 따라 i_code 다르게
            GameObject i_code = child.transform.Find("ICode").gameObject;
            Text i_code_txt = i_code.GetComponent<Text>();
            i_code_txt.text = key[j];

            GameObject name = child.transform.Find("Name").gameObject;
            Text name_txt = name.GetComponent<Text>();

            GameObject amount = child.transform.Find("Amount").gameObject;
            Text amount_txt = amount.GetComponent<Text>();
            amount_txt.text = reward_json[key[j]].ToString();

            GameObject img = child.transform.Find("Img").gameObject;
            Image img_img = img.GetComponent<Image>();

            if (key[j].Equals("Exp"))
            {
                img_img.sprite = Resources.Load<Sprite>("Sprites/FieldUI/exp_sprite");
                name_txt.text = "경험치";
            }
            else if (key[j].Equals("Coin"))
            {
                img_img.sprite = Resources.Load<Sprite>("Sprites/FieldUI/coin_sprite");
                name_txt.text = "코인";
            }
            else if (key[j].Equals("Badge"))  //배지
            {
                img_img.sprite = Resources.Load<Sprite>("Sprites/badgeList/" + reward_json[key[j]].ToString() + "_catalog");
                name_txt.text = "배지";
            }
            else    //기타 아이템
            {
                img_img.sprite = Resources.Load<Sprite>("Sprites/Catalog_Images/Store/" + key[j] + "_catalog");
                name_txt.text = "아이템";  //todo: 수정 요망! 챁트 내 검색 필요
            }
        }
    }

/*    void GetChartData(string item_type, string item_code)
    {
        string item_chart = "";
        item_chart = ChartNum.AllItemChart;
        //의상 아이템인지, 일반 아이템인지 모든 차트 뒤져야 한다. 
        
        BackendReturnObject bro = Backend.Chart.GetChartContents(item_chart);
        if (bro.IsSuccess())
        {

        }
    }*/
}

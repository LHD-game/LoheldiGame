using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailSelect : MonoBehaviour //우편 프리펩에 붙는 스크립트.
{
    //리스트 중 우편 선택시 -> 해당 버튼에 있는 제목 등등을 모두 가져와 오른쪽 화면에 띄운다.
    public void SelectMail()
    {
        //버튼에 달려있던 정보를 가져온다.
        GameObject qid = this.transform.Find("QID").gameObject;
        Text qid_txt = qid.GetComponent<Text>();

        GameObject title = this.transform.Find("Title").gameObject;
        Text title_txt = title.GetComponent<Text>();

        GameObject from = this.transform.Find("From").gameObject;
        Text from_txt = from.GetComponent<Text>();

        GameObject content = this.transform.Find("Content").gameObject;
        Text content_txt = content.GetComponent<Text>();

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

        //todo: reward

        //보상 받기 버튼 활성화: 퀘스트 진행도가 퀘스트 아이디보다 크거나 같아야 한다.
        //1. 현재 qid와 퀘스트의 qid를 가져와 _를 기준으로 앞부분 슬라이스
        //2. if (퀘스트 qid <= 현재 qid 앞부분 값)
        //3. -->뒷부분 슬라이스
        //4. -->if (퀘스트 qid <= 현재 qid 뒷부분 값)
        //5.    -->disable 버튼 비활성화
        GameObject reward_disable_btn = QuestDetail.transform.Find("ReceiveMailDisable").gameObject;
        reward_disable_btn.SetActive(true);

        string[] q_qid = qid_txt.text.Split('_');
        string[] my_qid = PlayerPrefs.GetString("QuestPreg").Split('_');

        int q_front = int.Parse(q_qid[0]);
        int my_front = int.Parse(my_qid[0]);
        if(q_front <= my_front)
        {
            int q_back = int.Parse(q_qid[1]);
            int my_back = int.Parse(my_qid[1]);
            if(q_back <= my_back)
            {
                reward_disable_btn.SetActive(false);
            }
        }
    }
}

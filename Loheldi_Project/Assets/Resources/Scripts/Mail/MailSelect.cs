using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailSelect : MonoBehaviour //���� �����鿡 �ٴ� ��ũ��Ʈ.
{
    //����Ʈ �� ���� ���ý� -> �ش� ��ư�� �ִ� ���� ����� ��� ������ ������ ȭ�鿡 ����.
    public void SelectMail()
    {
        //��ư�� �޷��ִ� ������ �����´�.
        GameObject qid = this.transform.Find("QID").gameObject;
        Text qid_txt = qid.GetComponent<Text>();

        GameObject title = this.transform.Find("Title").gameObject;
        Text title_txt = title.GetComponent<Text>();

        GameObject from = this.transform.Find("From").gameObject;
        Text from_txt = from.GetComponent<Text>();

        GameObject content = this.transform.Find("Content").gameObject;
        Text content_txt = content.GetComponent<Text>();

        //���� ������ ��� ui ����(������Ʈ)�� �����´�.
        GameObject QuestDetail = GameObject.Find("RightDetail");
        //���� ���� ������Ʈ�� ���� �������ش�.
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

        //���� �ޱ� ��ư Ȱ��ȭ: ����Ʈ ���൵�� ����Ʈ ���̵𺸴� ũ�ų� ���ƾ� �Ѵ�.
        //1. ���� qid�� ����Ʈ�� qid�� ������ _�� �������� �պκ� �����̽�
        //2. if (����Ʈ qid <= ���� qid �պκ� ��)
        //3. -->�޺κ� �����̽�
        //4. -->if (����Ʈ qid <= ���� qid �޺κ� ��)
        //5.    -->disable ��ư ��Ȱ��ȭ
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

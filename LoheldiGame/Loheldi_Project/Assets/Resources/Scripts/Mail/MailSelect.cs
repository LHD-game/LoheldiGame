using BackEnd;
using LitJson;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailSelect : MonoBehaviour //���� �����鿡 �ٴ� ��ũ��Ʈ.
{
    public static List<GameObject> reward_list = new List<GameObject>();
    public static List<GameObject> right_detail_text = new List<GameObject>();
    
    public static string this_qid = "null";

    //����Ʈ �� ���� ���ý� -> �ش� ��ư�� �ִ� ���� ����� ��� ������ ������ ȭ�鿡 ����.
    public void SelectMail()
    {
        QuestDontDestroy QDD;
        //��ư�� �޷��ִ� ������ �����´�.
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

        GameObject content_detail = QuestDetail.transform.Find("RContentsViewport").transform.Find("RContent").gameObject;
        Text content_detail_txt = content_detail.GetComponent<Text>();
        content_detail_txt.text = content_txt.text;

        if(right_detail_text.Count <= 0)
        {
            right_detail_text.Add(qid_detail);
            right_detail_text.Add(title_detail);
            right_detail_text.Add(from_detail);
            right_detail_text.Add(content_detail);
        }

        if (this_qid != "")
        {
            //todo: reward
            MakeRewardList(reward_txt.text);

            //���� �ޱ� ��ư Ȱ��ȭ: ����Ʈ ���൵�� ����Ʈ ���̵𺸴� ũ�ų� ���ƾ� �Ѵ�.
            //1. ���� qid�� ����Ʈ�� qid�� ������ _�� �������� �պκ� �����̽�
            //2. if (����Ʈ qid <= ���� qid �պκ� ��)
            //3. -->�޺κ� �����̽�
            //4. -->if (����Ʈ qid <= ���� qid �޺κ� ��)
            //5.    -->disable ��ư ��Ȱ��ȭ
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
            else if (q_front == my_front && q_back <= my_back) //1_1, 1_2
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
                    content_detail_txt.text = "������� ����Ʈ ������ �Ұ��մϴ�";
                }
                else if (!QDD.weekend && QuestType.Equals("WeeklyQuestPreg"))
                {
                    content_detail_txt.text = "�ش� ����Ʈ�� �Ͽ��Ͽ��� ������ �����մϴ�";
                }
                else if (QDD.weekend && QuestType.Equals("QuestPreg"))
                    content_detail_txt.text = "�ش� ����Ʈ�� ���Ͽ��� ������ �����մϴ�";
            }
        }
    }

    //���� ����Ʈ�� �����.
    void MakeRewardList(string reward_txt)
    {
        for (int k = 0; k < reward_list.Count; k++)
        {
            Destroy(reward_list[k]);
        }
        reward_list.Clear();

        //Reward�� ����� �����(string)�� json Ÿ������ ��ȯ
        JObject reward_json = JObject.Parse(reward_txt);

        //json Ű�� ����
        string[] key = new string[reward_json.Count];
        int i = 0;
        foreach (var j in reward_json){
            key[i] = j.Key;
            i++;
        }

        //reward box ������Ʈ ��ü�� ����

        GameObject rewardBox = (GameObject)Resources.Load("Prefabs/UI/MailRewardBox");
        for (int j=0; j<reward_json.Count; j++)
        {
            GameObject parents = GameObject.Find("RewardContent");
            
            GameObject child = Instantiate(rewardBox);
            child.transform.SetParent(parents.transform);

            RectTransform rt = child.GetComponent<RectTransform>(); //������ �ڽ� ũ�� �缳��
            rt.localScale = new Vector3(1f, 1f, 1f);

            reward_list.Add(child);

            //todo: exp, coin, badge, item�� ���� i_code �ٸ���
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
                name_txt.text = "����ġ";
            }
            else if (key[j].Equals("Coin"))
            {
                img_img.sprite = Resources.Load<Sprite>("Sprites/FieldUI/coin_sprite");
                name_txt.text = "����";
            }
            else if (key[j].Equals("Badge"))  //����
            {
                img_img.sprite = Resources.Load<Sprite>("Sprites/badgeList/" + reward_json[key[j]].ToString() + "_catalog");
                name_txt.text = "����";
            }
            else    //��Ÿ ������
            {
                img_img.sprite = Resources.Load<Sprite>("Sprites/Catalog_Images/Store/" + key[j] + "_catalog");
                name_txt.text = "������";  //todo: ���� ���! �QƮ �� �˻� �ʿ�
            }
        }
    }

/*    void GetChartData(string item_type, string item_code)
    {
        string item_chart = "";
        item_chart = ChartNum.AllItemChart;
        //�ǻ� ����������, �Ϲ� ���������� ��� ��Ʈ ������ �Ѵ�. 
        
        BackendReturnObject bro = Backend.Chart.GetChartContents(item_chart);
        if (bro.IsSuccess())
        {

        }
    }*/
}

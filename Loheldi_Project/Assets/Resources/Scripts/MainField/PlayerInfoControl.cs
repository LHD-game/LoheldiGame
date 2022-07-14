using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoControl : MonoBehaviour
{
    
    [SerializeField]
    Text PlayerNameTxt;
    [SerializeField]
    Text LevelTxt;
    [SerializeField]
    Text ExpTxt;
    [SerializeField]
    Slider ExpSlider;
    [SerializeField]
    Text BirthTxt;
    [SerializeField]
    Text QuestPregTxt;
    [SerializeField]
    Text WalletTxt;

    //category
    [SerializeField]
    GameObject c_exercise;
    [SerializeField]
    GameObject c_food;
    [SerializeField]
    GameObject c_home;
    [SerializeField]
    GameObject c_mind;


    GameObject badgeBtn;

    static bool is_get_b_chart = false; //이미 배지 차트 가져온 상태라면

    BackendReturnObject allBadgeChart = new BackendReturnObject();

    List<Dictionary<string, object>> exerciseB = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> foodB = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> homeB = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> mindB = new List<Dictionary<string, object>>();

    JsonData myBadge_rows = null;
    List<GameObject> ex_list = new List<GameObject>();   //상의 아이템을 저장하는 변수
    List<GameObject> food_list = new List<GameObject>();   //하의 아이템을 저장하는 변수
    List<GameObject> home_list = new List<GameObject>();   //신발 아이템을 저장하는 변수
    List<GameObject> mind_list = new List<GameObject>();   //악세서리 아이템을 저장하는 변수

    //내정보 창 열었을 때
    public void PopPlayerInfo()
    {
        UpdateInfoText();
        GetBadgeChart(ChartNum.BadgeChart);

        Debug.Log(exerciseB.Count);
        Debug.Log(foodB.Count);
        Debug.Log(homeB.Count);
        Debug.Log(mindB.Count);
        MakeCategory(c_exercise, exerciseB, ex_list, "Prefabs/UI/BadgeBox_E");
        MakeCategory(c_food, foodB, food_list, "Prefabs/UI/BadgeBox_F");
        MakeCategory(c_home, homeB, home_list, "Prefabs/UI/BadgeBox_H");
        MakeCategory(c_mind, mindB, mind_list, "Prefabs/UI/BadgeBox_M");
    }

    void UpdateInfoText()
    {
        PlayerNameTxt.text = PlayerPrefs.GetString("Nickname");
        LevelTxt.text = PlayerPrefs.GetInt("Level").ToString();
        float now_exp = PlayerPrefs.GetFloat("NowExp");
        float max_exp = PlayerPrefs.GetFloat("MaxExp");
        ExpTxt.text = now_exp + " / " + max_exp;
        BirthTxt.text = "생년월일: " + PlayerPrefs.GetString("Birth");
        Debug.Log(PlayerPrefs.GetString("Birth"));
        QuestPregTxt.text = "건강도: " + PlayerPrefs.GetString("QuestPreg");
        WalletTxt.text = PlayerPrefs.GetInt("Wallet").ToString();

        ExpSlider.value = (now_exp / max_exp)*100;  //백분율로 변환
    }

    void GetBadgeChart(string itemChart)
    {
        exerciseB.Clear();
        foodB.Clear();
        mindB.Clear();
        homeB.Clear();
        if (!is_get_b_chart)
        {
            allBadgeChart = Backend.Chart.GetChartContents(itemChart);
            is_get_b_chart = true;
        }
            var myBadge = Backend.GameData.GetMyData("ACC_BADGE", new Where(), 100);

            JsonData allBadge_rows = allBadgeChart.GetReturnValuetoJSON()["rows"];
            myBadge_rows = myBadge.GetReturnValuetoJSON()["rows"];
            ParsingJSON pj = new ParsingJSON();

            int e = 0, f = 0, h = 0, m = 0;

            for (int i = 0; i < allBadge_rows.Count; i++)
            {
                Badge data = pj.ParseBackendData<Badge>(allBadge_rows[i]);

                //아이템 테마에 따라 다른 리스트에 저장.

                if (data.Category.Equals("Exercise"))
                {
                    exerciseB.Add(new Dictionary<string, object>()); // list에 공간을 만들어줍니다.
                    initItem(exerciseB[e], data);
                    e++;
                }

                else if (data.Category.Equals("Food"))
                {
                    foodB.Add(new Dictionary<string, object>());
                    initItem(foodB[f], data);
                    f++;
                }
                else if (data.Category.Equals("Home"))
                {
                    homeB.Add(new Dictionary<string, object>());

                    initItem(homeB[h], data);
                    h++;
                }

                else if (data.Category.Equals("Mind"))
                {
                    mindB.Add(new Dictionary<string, object>());

                    initItem(mindB[m], data);
                    m++;
                }
            }
        
    }

    void initItem(Dictionary<string, object> item, Badge data)
    {
        item.Add("BCode", data.BCode);
        item.Add("BName", data.BName);
        item.Add("Bcontent", data.Bcontent);
        item.Add("Category", data.Category);
    }

    protected void MakeCategory(GameObject category, List<Dictionary<string, object>> dialog, List<GameObject> itemObject, string btnRoute)
    {
        badgeBtn = (GameObject)Resources.Load(btnRoute);

        ParsingJSON pj = new ParsingJSON();

        for (int i = 0; i < dialog.Count; i++)
        {
            GameObject child;

            if (itemObject.Count != dialog.Count)    //만약 처음 여는 것이면 새 객체 생성
            {
                //create caltalog box
                child = Instantiate(badgeBtn);    //create itemBtn instance
                child.transform.SetParent(category.transform);  //move instance: child
                //아이템 박스 크기 재설정
                RectTransform rt = child.GetComponent<RectTransform>();
                rt.localScale = new Vector3(1f, 1f, 1f);

                itemObject.Add(child);
            }
            else    //아니라면 기존 객체 재활용
            {
                child = itemObject[i];
            }

            //change catalog box img
            GameObject item_img = child.transform.Find("BadgeImg").gameObject;
            Image img = item_img.GetComponent<Image>();
            img.sprite = Resources.Load<Sprite>("Sprites/BadgeList/" + dialog[i]["BCode"] + "_catalog");


            //change catalog box item name (선택시 해당 아이템을 찾기 위한 꼬리표 용도)
            GameObject item_name_p = child.transform.Find("BadgeName").gameObject;
            GameObject item_name = item_name_p.transform.Find("Text").gameObject;

            Text txt = item_name.GetComponent<Text>();
            txt.text = dialog[i]["BName"].ToString();
            //Debug.Log("BGName:" + dialog[i]["BName"]);

            GameObject item_content = child.transform.Find("BadgeInfo").gameObject;

            Text txt2 = item_content.GetComponent<Text>();
            txt2.text = dialog[i]["Bcontent"].ToString();
            /*            //change catalog box item code
                        GameObject item_code = badgeBtn.transform.Find("ItemCode").gameObject;
                        Text item_code_txt = item_code.GetComponent<Text>();
                        item_code_txt.text = dialog[i]["ICode"].ToString();*/

            GameObject disable_img = child.transform.Find("Disable").gameObject;
            disable_img.SetActive(true);
            for (int j = 0; j < myBadge_rows.Count; j++)
            {
                Badge data = pj.ParseBackendData<Badge>(myBadge_rows[j]);

                if (data.BCode.Equals(dialog[i]["BCode"].ToString()))
                {
                    //비활성 창 오브젝트(Disable)를 비활성화
                    disable_img.SetActive(false);
                    break;
                }
            }
        }
    }
}

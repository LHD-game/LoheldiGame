using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccNariAdvice : MonoBehaviour
{
    public GameObject Nari;
    public GameObject NextBtn;

    public Text TextBox;

    Image nari_background;
    int script_num = 0;
    int print_line = 0;
    List<Dictionary<string, object>> nari_script = new List<Dictionary<string, object>>();
    // Start is called before the first frame update
    void Start()
    {
        nari_background = Nari.GetComponent<Image>();
        GetScript();
        PrintScript();
    }

    void GetScript()
    {
        nari_script = CSVReader.Read("DB/AccNariAdviceDB");
        script_num = (int)nari_script[0]["Num"];
    }

    void PrintScript()
    {
        int now_num = (int)nari_script[print_line]["Num"];
        if (script_num != now_num)
        {
            switch (now_num)
            {
                case 0:
                    Nari.transform.localPosition = new Vector3(0, 0, 0);
                    NextBtn.SetActive(true);
                    break;
                case 1:
                    Nari.transform.localPosition = new Vector3(1000, 0, 0);
                    NextBtn.SetActive(false);
                    BgColor(now_num);
                    break;
                case 5:
                    Nari.transform.localPosition = new Vector3(0, 0, 0);
                    NextBtn.SetActive(true);
                    BgColor(now_num);
                    break;
                default:
                    Nari.transform.localPosition = new Vector3(1000, 0, 0);
                    NextBtn.SetActive(false);
                    break;
            }
            script_num = now_num;
        }
        if (print_line >= nari_script.Count)
        {
            //todo: 터치를 기다리는 코드 추가
            return;
        }
        else
        {
            string txt = nari_script[print_line]["Text"].ToString();
            string txt2 = txt.Replace("n", "\n");
            string txt3 = txt2.Replace("<이름>", NewAccSave.uNickName);
            string result = txt3.Replace("<생년월일>", NewAccSave.uBirth.ToString("yyyy년 M월 d일"));
            TextBox.text = result;

            print_line++;
        }
    }

    void BgColor(int now_num)
    {
        Color bg_color = nari_background.color;
        if (now_num == 1)
        {
            bg_color.a = 0.0f;
            
        }
        else
        {
            bg_color.a = 255.0f;
        }
        nari_background.color = bg_color;

    }

    public void TouchScreen()
    {
        if (NewAccSave.nari_can_talk)
        {
            PrintScript();
        }
    }
}

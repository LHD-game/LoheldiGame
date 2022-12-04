using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class RealClock : MonoBehaviour
{
    public Text DayTxt;                 //�ð��� ��Ÿ���� Text ������Ʈ
    public Text TimeTxT;

    public Material DaySky;             //��ī�̹ڽ� ���͸��� �ҷ���
    public Material NoonSky;
    public Material NightSky;

    public GameObject Light;            //�ð������� ����� ��
    public GameObject NightLight;

    private int PreTime;                //�ð��� �������� Ȯ���ϱ� ���� ����
    public int Time;                    //�ð� ����
    private QuestScript Quest;

    QuestDontDestroy QDD;

    void Start()
    {
        QDD = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        if (SceneManager.GetActiveScene().name == "MainField")
            Quest = GameObject.Find("EventSystem").GetComponent<QuestScript>();
        GetCurrentDate();               //�ð� �ҷ����� �Լ�
        TimeSetting(Time);
        PreTime = Time;                 //���� �Լ� �ʱ�ȭ

        StartCoroutine(TimeCheckCorutine()); //�ð� �˻��ϴ� �޼ҵ� (�ڷ�ƾ���� 1�и��� �ݺ�)
    }

    public void GetCurrentDate()
    {
        /*string MonthAndDay = DateTime.Now.ToString(("MM�� dd��"));        //��¥ �ҷ�����
        DayTxt.text = "��¥ : " + MonthAndDay;

        string DayTime = DateTime.Now.ToString("t");                      //�ð� �ҷ�����
        TimeTxT.text = "�ð� : " + DayTime;*/

        Time = int.Parse(DateTime.Now.ToString("HH"));                    //String�� Int�� ���� (HH�� 24�ð� ����)
    }

    public void TimeSetting(float Time)
    {
        if (SceneManager.GetActiveScene().name == "MainField")
        {
            switch (Time)
            {
                case 06:
                case 07:
                case 08:
                case 09:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:                     //��ħ
                    RenderSettings.skybox = DaySky;                                                           //��ī�̹ڽ� ����
                    Light.GetComponent<Light>().color = new Color(255f / 255f, 244f / 255f, 214f / 255f);     //�� �� ����
                    NightLight.SetActive(false);                                                              //������ ��ħ�� ����
                    Light.transform.eulerAngles = new Vector3(50, (Time - 11) * 15, 0);                       //���� 15���� ����
                    break;
                case 00:
                case 01:
                case 02:
                case 03:
                case 04:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:                    //��
                    RenderSettings.skybox = NightSky;
                    Light.GetComponent<Light>().color = new Color(68f / 255f, 68f / 255f, 128f / 255f);
                    NightLight.SetActive(true);
                    Light.transform.eulerAngles = new Vector3(50, (Time - 23) * 15, 0);
                    break;
            }
            if (Time == 5 || Time == 17)    //����, ����
            {
                RenderSettings.skybox = NoonSky;
                Light.GetComponent<Light>().color = new Color(139f / 255f, 9f / 255f, 202f / 255f);
                Light.transform.rotation = Quaternion.Euler(50, -90, 0);    //���� ��ġ�� �ʱ�ȭ�� (�ذ� �߸� ���� �ذ�, ���� �߸� ���� �ذ�)
                if (Time == 17)             //������ ���ῡ ����
                {
                    NightLight.SetActive(true);
                }
            }
        }
    }
    IEnumerator TimeCheckCorutine()
    {
        GetCurrentDate();               //�� �����Ӹ��� �ð��� �ҷ���
        if (PreTime != Time)            //�ð��� ���� �� ������
        {
            PreTime = Time;             //���� �Լ� �ʱ�ȭ
            TimeSetting(Time);          //�ð��� ���� ���� �Լ�git 
        }
        yield return new WaitForSecondsRealtime(30f);
    }
}
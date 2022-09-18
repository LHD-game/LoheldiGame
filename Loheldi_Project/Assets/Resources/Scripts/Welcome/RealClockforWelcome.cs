using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class RealClockforWelcome : MonoBehaviour
{
    public Material DaySky;             //��ī�̹ڽ� ���͸��� �ҷ���
    public Material NoonSky;
    public Material NightSky;

    public GameObject Light;            //�ð������� ����� ��
    public GameObject NightLight;

    private int PreTime;                //�ð��� �������� Ȯ���ϱ� ���� ����
    public int Time;                    //�ð� ����

    QuestDontDestroy QDD;
    void Start()
    {
        QDD= GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        GetCurrentDate();               //�ð� �ҷ��� �Լ�
        TimeSetting(Time);
        PreTime = Time;                 //���� �Լ� �ʱ�ȭ

        StartCoroutine(TimeCheckCorutine()); //�ð� �˻��ϴ� �޼ҵ� (�ڷ�ƾ���� 1�и��� �ݺ�)
    }

    private void Update()
    {
    }

    public void GetCurrentDate()
    {
        Time = int.Parse(DateTime.Now.ToString("HH"));                    //String�� Int�� ���� (HH�� 24�ð� ����)
    }

    public void TimeSetting(float Time)
    {
        switch (Time)
        {
            case 6:
            case 7:
            case 8:
            case 9:
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
            case 1:
            case 2:
            case 3:
            case 4:
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailLoad : MonoBehaviour
{
    public Text RTitleText;                                           //������ ǥ�õǴ� ���� ����
    public Text RDetailText;
    public GameObject ThisTitle;                                      //���� ����
    public GameObject ThisSent;                                       //���� ���
    string Detail1 = "A";                                             //���� ���� (���߿� �������� �ҷ����¹������ ����)
    string Detail2 = "B";
    string Detail3 = "C";
    string Detail4 = "D";

     void Start()
    {
        RTitleText = GameObject.Find("RTitleText").GetComponent<Text>();    //RTitleText��� �̸��� Text������Ʈ�� ã�� RTitleText��� ������
        RDetailText = GameObject.Find("RDetailText").GetComponent<Text>();  //RDetailText�̶�� �̸��� Text������Ʈ�� ã�� RDetailText��� ������
    } 

    public void MailLoading()
    {
        ThisTitle = this.transform.GetChild(1).gameObject;                  //���Ͽ� ������ ������ (���߿� DB���� �ҷ����� ��ũ��Ʈ �ʿ�)
        ThisSent = this.transform.GetChild(2).gameObject;                   //���� �� ����� ������(                ''                )

        RTitleText.text = ThisTitle.GetComponent<Text>().text;              //������ ǥ�õǴ� ������ ������ ����� ���� ��
        RDetailText.text = Detail1;                                         //������ Detail1���� �ٲ�
    }
}
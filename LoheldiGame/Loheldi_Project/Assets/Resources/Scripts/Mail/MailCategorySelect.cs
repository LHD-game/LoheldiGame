using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailCategorySelect : MonoBehaviour
{
    public GameObject MailPost;
    public GameObject Announcement;
    public void Mail()  //���� �� Ŭ��
    {
        MailPost.SetActive(true);
        Announcement.SetActive(false);
        MailLoad.MailorAnnou = true;
    }
    public void Annou() //�������� �� Ŭ��
    {
        MailPost.SetActive(false);
        Announcement.SetActive(true);
        MailLoad.MailorAnnou = false;
        this.gameObject.GetComponent<MailLoad>().NoticeLoad();
    }
}

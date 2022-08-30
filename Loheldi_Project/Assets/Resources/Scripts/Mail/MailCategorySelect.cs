using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailCategorySelect : MonoBehaviour
{
    public GameObject MailPost;
    public GameObject Announcement;
    public void Mail()  //우편 탭 클릭
    {
        MailPost.SetActive(true);
        Announcement.SetActive(false);
        MailLoad.MailorAnnou = true;
    }
    public void Annou() //공지사항 탭 클릭
    {
        MailPost.SetActive(false);
        Announcement.SetActive(true);
        MailLoad.MailorAnnou = false;
        this.gameObject.GetComponent<MailLoad>().NoticeLoad();
    }
}

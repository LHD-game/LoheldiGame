using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailAnnouChanger : MonoBehaviour
{
    public GameObject MailPost;
    public GameObject Announcement;
    public void Mail()
    {
        MailPost.SetActive(true);
        Announcement.SetActive(false);
        MailLoad.MailorAnnou = true;
        this.gameObject.GetComponent<MailLoad>().UpdateList();
    }
    public void Annou()
    {
        MailPost.SetActive(false);
        Announcement.SetActive(true);
        MailLoad.MailorAnnou = false;
        this.gameObject.GetComponent<MailLoad>().UpdateList();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailAnnouChanger : MonoBehaviour
{
    public GameObject MailPost;
    public GameObject Announcement;

    public void Update()
    {
        Debug.Log(this.gameObject.name);
    }

    public void change()
    {
        if (this.gameObject.name == "AnnouButton")
        {
            MailPost.SetActive(false);
            Announcement.SetActive(true);
        }
        else
        {
            MailPost.SetActive(true);
            Announcement.SetActive(false);
        }
    }
}

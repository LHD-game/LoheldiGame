//CreateAcc Scene¿¡¼­ °èÁ¤ Á¤º¸¸¦ ÀúÀåÇÏ±â À§ÇÑ ½ºÅ©¸³Æ®

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using BackEnd;
using System;

public class NewAccSave : MonoBehaviour
{
    //panel
    [SerializeField]
    private GameObject NariField;   //³ª¸®ÀÇ ¼³¸í
    [SerializeField]
    private GameObject NickNameField;   //´Ğ³×ÀÓ ÀÔ·Â È­¸é
    [SerializeField]
    private GameObject BirthField;   //»ı³â¿ùÀÏ ÀÔ·Â È­¸é
    [SerializeField]
    private GameObject ParentsNoField;   //º¸È£ÀÚ ÀÎÁõ¹øÈ£ ÀÔ·Â È­¸é
    [SerializeField]
    private GameObject ResultField;   //°ª È®ÀÎ¿ë

    //°ª ÀÔ·Â ÇÊµå
    [SerializeField]
    private InputField InputNickName;   //°èÁ¤ ´Ğ³×ÀÓ
    [SerializeField]
    private Dropdown[] InputBirth = new Dropdown[3];      //°èÁ¤ÁÖ »ı³â,¿ù,ÀÏ
    [SerializeField]
    private InputField InputParentsNo;   //°èÁ¤ º¸È£ÀÚ ÀÎÁõ¹øÈ£
    public GameObject PrivacyChkBox;    //°³ÀÎÁ¤º¸ÀÌ¿ëµ¿ÀÇ Ã¼Å©¹Ú½º

    public static string uNickName;   // ¼­¹ö¿¡ ÀúÀåµÇ±â Àü °ªÀ» ´ã¾Æ³õ´Â º¯¼ö
    public static DateTime uBirth;    // À§¿Í °°À½
    public static string uParentsNo;   // ¼­¹ö¿¡ ÀúÀåµÇ±â Àü °ªÀ» ´ã¾Æ³õ´Â º¯¼ö

    public Text nick;
    public Text birth;
    public Text parentsNo;

    bool isPrivacyChk = false;
    public static bool nari_can_talk = true;

    void Start()
    {
        NariField.SetActive(true);
        NickNameField.SetActive(true);
        ParentsNoField.SetActive(false);
        BirthField.SetActive(false);
        ResultField.SetActive(false);
    }

    private void Update()
    {
        //ÀÔ·Â È®ÀÎ¿ë °á°ú Ãâ·Â
        if (ResultField.activeSelf)
        {
            nick.text = "´Ğ³×ÀÓ: " + uNickName;
            birth.text = "»ı³â¿ùÀÏ: " + uBirth.ToString("yyyy³â M¿ù dÀÏ");
            parentsNo.text = "º¸È£ÀÚ ÀÎÁõ¹øÈ£: " + uParentsNo;
        }
    }

    public void SaveNickName()  //´Ğ³×ÀÓ ÀÔ·Â ÈÄ ¹öÆ°À» ´­·¶À» °æ¿ì ½ÇÇà
    {
        Regex regex = new Regex(@"[a-zA-Z°¡-ÆR¤¡-¤¾0-9]{2,8}$"); //´Ğ³×ÀÓ Á¤±Ô½Ä. ¿µ´ë¼Ò¹®ÀÚ, ÇÑ±Û 2~8ÀÚ °¡´É

        if ((regex.IsMatch(InputNickName.text))) //Á¤±Ô½Ä ÀÏÄ¡½Ã,
        {
            uNickName = InputNickName.text; //uNickName º¯¼ö¿¡ ÀÔ·Â°ªÀ» ÀúÀåÇÏ°í,
            
            ShowNHide(BirthField, NickNameField);   //´Ğ³×ÀÓ ÀÔ·Â ºñÈ°¼ºÈ­, »ıÀÏ ÀÔ·Â È°¼ºÈ­
            nari_can_talk = true;
        }
        else    //Á¤±Ô½Ä ºÒÀÏÄ¡½Ã
        {
            //¿À·ù ÆË¾÷ È°¼ºÈ­
            Transform t = NickNameField.transform.Find("ErrorPop");
            t.gameObject.SetActive(true);
            nari_can_talk = false;
        }
    }

    public void SaveBirth() //»ı³â¿ùÀÏ ÀÔ·Â ÈÄ ¹öÆ°À» ´­·¶À» °æ¿ì ½ÇÇà
    {
        Regex regex = new Regex(@"[0-9]{1,5}$"); //»ı³â¿ùÀÏ Á¤±Ô½Ä
        bool isOK = true;
        for (int i=0; i<InputBirth.Length; i++)
        {
            string birthValue = InputBirth[i].options[InputBirth[i].value].text;
            if (!(regex.IsMatch(birthValue)))  //Á¤±Ô½Ä ºÒÀÏÄ¡ ½Ã,
            {
                isOK = false;
                Debug.Log(birthValue);
                nari_can_talk = false;
            }
        }

        if (isOK)   //¸ğµÎ Á¤±Ô½Ä ÀÏÄ¡ÇÏ¸é
        {
            string str = InputBirth[0].options[InputBirth[0].value].text + "/";
            str += InputBirth[1].options[InputBirth[1].value].text + "/";
            str += InputBirth[2].options[InputBirth[2].value].text; //yyyy/MM/dd
            Debug.Log(str);
            uBirth = Convert.ToDateTime(str);   //uBirth º¯¼ö¿¡ ÀÔ·Â°ª ÀúÀå
            ShowNHide(ParentsNoField, BirthField);
            nari_can_talk = true;
        }
        else    //Á¤±Ô½Ä ºÒÀÏÄ¡½Ã
        {
            //¿À·ù ÆË¾÷ È°¼ºÈ­
            Transform t = BirthField.transform.Find("ErrorPop");
            t.gameObject.SetActive(true);
            nari_can_talk = false;
        }
    }

    public void SaveParentsNo()  //º¸È£ÀÚ ÀÎÁõ¹øÈ£ ÀÔ·Â ÈÄ ¹öÆ°À» ´­·¶À» °æ¿ì ½ÇÇà
    {
        Regex regex = new Regex(@"[0-9]{4,6}$"); //Á¤±Ô½Ä. ¼ıÀÚ 4~6ÀÚ °¡´É

        if ((regex.IsMatch(InputParentsNo.text))) //Á¤±Ô½Ä ÀÏÄ¡½Ã,
        {
            uParentsNo = InputParentsNo.text; //uParentsNo º¯¼ö¿¡ ÀÔ·Â°ªÀ» ÀúÀåÇÏ°í,

            ShowNHide(ResultField, ParentsNoField);   //´Ğ³×ÀÓ ÀÔ·Â ºñÈ°¼ºÈ­, »ıÀÏ ÀÔ·Â È°¼ºÈ­
            nari_can_talk = true;
        }
        else    //Á¤±Ô½Ä ºÒÀÏÄ¡½Ã
        {
            //¿À·ù ÆË¾÷ È°¼ºÈ­
            Transform t = ParentsNoField.transform.Find("ErrorPop");
            t.gameObject.SetActive(true);
            nari_can_talk = false;
        }
    }

    private void ShowNHide(GameObject show, GameObject hide)    //È°¼ºÈ­ ÇÒ °Í Ã¹¹øÂ°, ºñÈ°¼ºÈ­ ÇÒ °ÍÀº µÎ¹øÂ° ÀÎÀÚ·Î ÁØ´Ù.
    {
        show.SetActive(true);
        hide.SetActive(false);
    }

    public void ClosePop(GameObject go) //¿À·ù ÆË¾÷ ´İ±â ¸Ş¼Òµå
    {
        go.SetActive(false);
    }

    public void PrivacyChk()
    {
        isPrivacyChk = PrivacyChkBox.GetComponent<Toggle>().isOn;
        if (isPrivacyChk)
        {
            nari_can_talk = true;
        }
        else
        {
            //¿À·ù ÆË¾÷ È°¼ºÈ­
            Transform t = ResultField.transform.Find("ErrorPop");
            t.gameObject.SetActive(true);
            nari_can_talk = false;
        }
    }

    //°èÁ¤ Á¤º¸¸¦ ´Ù½Ã ÀÔ·ÂÇÏ±â À§ÇÑ ¸Ş¼Òµå(¾Æ´Ï¾ß! ¼±ÅÃ)
    public void ReturnToFirst()
    {
        ResultField.SetActive(false);   //°á°ú ÆĞ³Î ºñÈ°¼ºÈ­
        NickNameField.SetActive(true);  //´Ğ³×ÀÓ ÀÔ·Â ÆĞ³Î È°¼ºÈ­
        AccNariAdvice.print_line = 6;
    }

    //ÃÖÁ¾ÀûÀ¸·Î ¼­¹ö¿¡ ´Ğ³×ÀÓ°ú »ı³â¿ùÀÏÀ» ÀúÀåÇÏ´Â ¸Ş¼Òµå
    public void AccSave()
    {
        if (isPrivacyChk)
        {
            Param param = new Param();
            param.Add("NICKNAME", uNickName);
            param.Add("BIRTH", uBirth);
            param.Add("PARENTSNO", uParentsNo);


            var bro = Backend.GameData.Insert("ACC_INFO", param);

            if (bro.IsSuccess())
            {
                Debug.Log("°èÁ¤ Á¤º¸ ¼³Á¤ ¿Ï·á!");

                Save_Basic.PlayerInfoInit();    //°èÁ¤ ±âº» Á¤º¸ ÀúÀå(·¹º§, ÀçÈ­ µî)
                Save_Basic.SaveBasicClothes(); //±âº» ÀÇ»ó ¾ÆÀÌÅÛ ÀúÀå 
            }
            else
            {
                Debug.Log("°èÁ¤ Á¤º¸ ¼³Á¤ ½ÇÆĞ!");
                //todo: ¿À·ù ¹®ÀÇ ¾È³» ¹®±¸ ¶ç¿ì±â
            }
        }
    }
}

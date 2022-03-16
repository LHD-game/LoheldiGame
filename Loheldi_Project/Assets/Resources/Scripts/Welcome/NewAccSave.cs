using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using BackEnd;
using System;

public class NewAccSave : MonoBehaviour
{
    [SerializeField]
    private GameObject NickNameField;   //´Ğ³×ÀÓ ÀÔ·Â È­¸é
    [SerializeField]
    private GameObject BirthField;   //»ı³â¿ùÀÏ ÀÔ·Â È­¸é
    [SerializeField]
    private GameObject ResultField;   //ÀÓ½Ã °ª È®ÀÎ¿ë

    [SerializeField]
    private InputField InputNickName;   //°èÁ¤ ´Ğ³×ÀÓ
    [SerializeField]
    private Dropdown[] InputBirth = new Dropdown[3];      //°èÁ¤ÁÖ »ı³â,¿ù,ÀÏ

    string uNickName;   // ¼­¹ö¿¡ ÀúÀåµÇ±â Àü °ªÀ» ´ã¾Æ³õ´Â º¯¼ö
    DateTime uBirth;    // À§¿Í °°À½

    public Text nick;
    public Text birth;

    void Start()
    {
        NickNameField.SetActive(true);
        BirthField.SetActive(false);
        ResultField.SetActive(false);
    }

    private void Update()
    {
        //ÀÔ·Â È®ÀÎ¿ë ÀÓ½Ã °á°ú Ãâ·Â
        if (ResultField.activeSelf)
        {
            nick.text = "´Ğ³×ÀÓ: " + uNickName;
            birth.text = "»ı³â¿ùÀÏ: " + uBirth.ToString("yyyy³â M¿ù dÀÏ");
        }
    }

    public void SaveNickName()  //´Ğ³×ÀÓ ÀÔ·Â ÈÄ ¹öÆ°À» ´­·¶À» °æ¿ì ½ÇÇà
    {
        Regex regex = new Regex(@"[a-zA-Z°¡-ÆR0-9]{2,8}$"); //´Ğ³×ÀÓ Á¤±Ô½Ä. ¿µ´ë¼Ò¹®ÀÚ, ÇÑ±Û 2~8ÀÚ °¡´É

        if ((regex.IsMatch(InputNickName.text))) //Á¤±Ô½Ä ÀÏÄ¡½Ã,
        {
            uNickName = InputNickName.text; //uNickName º¯¼ö¿¡ ÀÔ·Â°ªÀ» ÀúÀåÇÏ°í,
            
            ShowNHide(BirthField, NickNameField);   //´Ğ³×ÀÓ ÀÔ·Â ºñÈ°¼ºÈ­, »ıÀÏ ÀÔ·Â È°¼ºÈ­
        }
        else    //Á¤±Ô½Ä ºÒÀÏÄ¡½Ã
        {
            //¿À·ù ÆË¾÷ È°¼ºÈ­
            Transform t = NickNameField.transform.Find("ErrorPop");
            t.gameObject.SetActive(true);
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
            }
        }

        if (isOK)   //¸ğµÎ Á¤±Ô½Ä ÀÏÄ¡ÇÏ¸é
        {
            string str = InputBirth[0].options[InputBirth[0].value].text + "/";
            str += InputBirth[1].options[InputBirth[1].value].text + "/";
            str += InputBirth[2].options[InputBirth[2].value].text; //yyyy/MM/dd
            Debug.Log(str);
            uBirth = Convert.ToDateTime(str);   //uBirth º¯¼ö¿¡ ÀÔ·Â°ª ÀúÀå
            ShowNHide(ResultField, BirthField);
        }
        else    //Á¤±Ô½Ä ºÒÀÏÄ¡½Ã
        {
            //¿À·ù ÆË¾÷ È°¼ºÈ­
            Transform t = BirthField.transform.Find("ErrorPop");
            t.gameObject.SetActive(true);
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

    //todo: ¼­¹ö¿¡ ´Ğ³×ÀÓ°ú »ı³â¿ùÀÏÀ» ÀúÀåÇÏ´Â ¸Ş¼Òµå
    public void AccSave()
    {
        Param param = new Param();
        param.Add("BIRTH", uBirth);
        param.Add("NICKNAME", uNickName);


        var bro = Backend.GameData.Insert("ACC_INFO", param);

        if (bro.IsSuccess())
        {
            Debug.Log("°èÁ¤ Á¤º¸ ¼³Á¤ ¿Ï·á!");
            SceneLoader.instance.GotoGameMove();
        }
        else
        {
            Debug.Log("°èÁ¤ Á¤º¸ ¼³Á¤ ½ÇÆĞ!");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class SignupCheck : MonoBehaviour
{
    private static SignupCheck _instance;
    public static SignupCheck instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SignupCheck>();
            }
            return _instance;
        }
    }

    [SerializeField]
    private Transform[] ErrorLine = new Transform[5]; //¼ø¼­´ë·Î ÀÌ¸§, ID, PW, ÀçÀÔ·ÂPW, ÀÌ¸ÞÀÏ
    [SerializeField]
    private Transform[] ErrorTxt = new Transform[7];    //¿À·ù ¹®±¸ ¹è¿­, Áßº¹id, Áßº¹ email Æ÷ÇÔ

    public void Start()
    {
        for (int i = 0; i < ErrorLine.Length; i++)
        {
            ErrorLine[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < ErrorTxt.Length; i++)
        {
            ErrorTxt[i].gameObject.SetActive(false);
        }
        Debug.Log(ErrorLine.Length);  //¿ÀºêÁ§Æ® ÀÎÁöO
        Debug.Log(ErrorLine[1]);  //¿ÀºêÁ§Æ® ÀÎÁöO
    }



    public bool ChkName(string uName = "")   //ÀÌ¸§ 2~6ÀÚ ÀÌ³» ÇÑ±Û/¿µ¾î, °ø¹é ¹ÌÆ÷ÇÔ
    {
        Regex regex = new Regex(@"[a-zA-Z°¡-ÆR]{2,6}$"); //ÀÌ¸§ Á¤±Ô½Ä. ¿µ´ë¼Ò¹®ÀÚ, ÇÑ±Û 2~6ÀÚ °¡´É
        bool isCorrect = true; //ÇÑ±Û, ¿µ¾î·Î¸¸ ÀÌ·ç¾îÁü+°ø¹é ¹ÌÆ÷ÇÔÀÏ ½Ã true

        if ((regex.IsMatch(uName)))    //Á¤±Ô½Ä ÀÏÄ¡ ½Ã
        {
            Debug.Log("ÀÌ¸§ÀÌ ¾ç½Ä°ú ÀÏÄ¡ÇÕ´Ï´Ù.");
            ErrorLine[0].gameObject.SetActive(false);
            ErrorTxt[0].gameObject.SetActive(false);
            isCorrect = true;
        }
        else
        {
            Debug.Log("ÀÌ¸§ÀÌ ¾ç½Ä°ú ÀÏÄ¡ÇÏÁö ¾Ê½À´Ï´Ù.");
            Debug.Log(ErrorLine[1]);  //¿ÀºêÁ§Æ® ÀÎÁöX
            ErrorLine[0].gameObject.SetActive(true);
            ErrorTxt[0].gameObject.SetActive(true);
            isCorrect = false;
        }

        return isCorrect;
    }

    public bool ChkID(string uID = "")     //ID 5~20ÀÚ ÀÌ³» ¿µ¾î, ¼ýÀÚ
    {
        Regex regex = new Regex(@"[a-zA-Z0-9]$"); //ID Á¤±Ô½Ä. ¿µ´ë¼Ò¹®ÀÚ, ¼ýÀÚ 0~8ÀÚ ÀÌ³» °¡´É
        bool isCorrect = true; //Á¤±Ô½Ä ¸¸Á· ½Ã, true

        if(uID.Length <= 8)
        {
            if ((regex.IsMatch(uID)))    //Á¤±Ô½Ä ºÒÀÏÄ¡ ½Ã
            {
                Debug.Log("ID°¡ ¾ç½Ä°ú ÀÏÄ¡ÇÕ´Ï´Ù.");
                ErrorLine[1].gameObject.SetActive(false);
                ErrorTxt[1].gameObject.SetActive(false);
                isCorrect = true;
            }
            else
            {
                Debug.Log("ID°¡ ¾ç½Ä°ú ÀÏÄ¡ÇÏÁö ¾Ê½À´Ï´Ù.");
                ErrorLine[1].gameObject.SetActive(true);
                ErrorTxt[1].gameObject.SetActive(true);
                isCorrect = false;
            }
        }
        else
        {
            Debug.Log("ID°¡ ¾ç½Ä°ú ÀÏÄ¡ÇÏÁö ¾Ê½À´Ï´Ù.(ÀÚ¸®¼ö ºÒÀÏÄ¡)");
            ErrorLine[1].gameObject.SetActive(true);
            ErrorTxt[1].gameObject.SetActive(true);
            isCorrect = false;
        }
        return isCorrect;
    }

    public bool ChkPW(string uPW = "")     //ºñ¹Ð¹øÈ£ 20ÀÚ ÀÌ³» ¿µ¾î+¼ýÀÚ+Æ¯¼ö¹®ÀÚ Á¶ÇÕ
    {
        bool isCorrect = true; //Á¤±Ô½Ä ¸¸Á· ½Ã, true
        char[] upw = uPW.ToCharArray();  //ÀÔ·Â¹ÞÀº°Å ¹è¿­·Î ÀúÀå
        int Num = 0; //¼ýÀÚ ¿¬¼Ó Ã¼Å©
        int Num2 = 0;  //¹Ýº¹ ¼ýÀÚ Ã¼Å©
        int Num3 = 0;  //¿¬¼Ó ¼ýÀÚ Ã¼Å©
        int[] Numupw = new int[uPW.Length];
        bool numupw = true; //ºñ¹ø ¿¬¼Ó, ¹Ýº¹ 4ÀÌ»ó ¾Æ´Ò½Ã, ture
        
        //¼ýÀÚ1ÀÌ»ó, ¿µ¹®1ÀÌ»ó, Æ¯¼ö¹®ÀÚ1ÀÌ»ó
        Regex regex = new Regex(@"^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{5,}$", RegexOptions.IgnorePatternWhitespace);

        if (uPW.Length >= 6 && uPW.Length <= 10)
        {
            foreach(char c in upw) //upw¾È¿¡ ¾Öµé ÇÑ¹ø¾¿ ½ÇÇà
            {
                if (char.IsNumber(c))
                {
                    Numupw[Num] = (int)c; //¼ýÀÚ µû·Î ¹è¿­·Î »­
                    Num++;
                    if (Num == 4)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (Numupw[i] == Numupw[i + 1]) //¹Ýº¹ÀÏ¶§
                                Num2++;
                            else if (Numupw[i] + 1 == Numupw[i + 1]) //¿¬¼ÓÀÏ¶§ (+Çü)
                                Num3++;
                            else if (Numupw[i] - 1 == Numupw[i + 1]) //¿¬¼ÓÀÏ¶¼(-Çü)
                                Num3++;
                            else
                                continue;
                            Debug.Log("¿¬¼Ó ¼ýÀÚ:" + Num3);
                            Debug.Log("i:" + i);

                        }
                        if(Num2==4^Num3==4)
                            numupw = false;
                        Debug.Log("¹Ýº¹ ¼ýÀÚ:" + Num2);
                        
                        break;
                    }
                }
                else
                    Num = 0;

            }
            
            if (!numupw)
            {
                ErrorLine[2].gameObject.SetActive(true);
                ErrorTxt[2].gameObject.SetActive(true);
                isCorrect = false;
                Debug.Log("PWÁß ¿¬¼Ó/¹Ýº¹µÇ´Â ¼ýÀÚ°¡ 4°³ÀÌ»ó ÀÖ½À´Ï´Ù.");
            }
            else if (regex.IsMatch(uPW))
            {
                ErrorLine[2].gameObject.SetActive(false);
                ErrorTxt[2].gameObject.SetActive(false);
                isCorrect = true;
                Debug.Log("PW°¡ ¾ç½Ä°ú ÀÏÄ¡ÇÕ´Ï´Ù.");
            }
            else
            {
                ErrorLine[2].gameObject.SetActive(true);
                ErrorTxt[2].gameObject.SetActive(true);
                isCorrect = false;
                Debug.Log("PW°¡ ¾ç½Ä°ú ÀÏÄ¡ÇÏÁö ¾Ê½À´Ï´Ù.(Á¤±Ô½Ä ºÒ¸¸Á·)");
                
            }

        }
        else
        {
            ErrorLine[2].gameObject.SetActive(true);
            ErrorTxt[2].gameObject.SetActive(true);
            isCorrect = false;
            Debug.Log("PW°¡ ¾ç½Ä°ú ÀÏÄ¡ÇÏÁö ¾Ê½À´Ï´Ù.(ÀÚ¸´¼ö ºÒÀÏÄ¡)");
        }
        return isCorrect;
    }
        
    

    public bool RePW(string PW = "", string rePW = "")      //ºñ¹Ð¹øÈ£ Áßº¹ È®ÀÎ
    {
        bool isCorrect = true;

        if (PW.Equals(rePW))
        {
            ErrorLine[3].gameObject.SetActive(false);
            ErrorTxt[3].gameObject.SetActive(false);
            isCorrect = true;
            Debug.Log("pw°¡ ÀÏÄ¡ÇÕ´Ï´Ù.");
        }
        else
        {
            ErrorLine[3].gameObject.SetActive(true);
            ErrorTxt[3].gameObject.SetActive(true);
            isCorrect = false;
            Debug.Log("pw°¡ ÀÏÄ¡ÇÏÁö ¾Ê½À´Ï´Ù.");
        }

        return isCorrect;
    }

    public bool ChkEmail(string uEmail = "")  //e-mail ¾ç½Ä È®ÀÎ
    {
        Regex regex = new Regex(@"[a-zA-Z0-9]{1,20}@[a-zA-Z0-9]{1,20}\.[a-zA-Z]{1,5}$"); //ÀÌ¸ÞÀÏ Á¤±Ô½Ä
        bool isCorrect = true; //Á¤±Ô½Ä ¸¸Á· ½Ã, true

        if ((regex.IsMatch(uEmail)))    //Á¤±Ô½Ä ºÒÀÏÄ¡ ½Ã
        {
            Debug.Log("emailÀÌ ¾ç½Ä°ú ÀÏÄ¡ÇÕ´Ï´Ù.");
            ErrorLine[4].gameObject.SetActive(false);
            ErrorTxt[4].gameObject.SetActive(false);
            isCorrect = true;
        }
        
        else
        {
            Debug.Log("emailÀÌ ¾ç½Ä°ú ÀÏÄ¡ÇÏÁö ¾Ê½À´Ï´Ù.");
            ErrorLine[4].gameObject.SetActive(true);
            ErrorTxt[4].gameObject.SetActive(true);
            isCorrect = false;
        }
        return isCorrect;

    }


//ÀÌ¹Ì Á¸ÀçÇÏ´Â ID, Email popup

    public void ExistID(bool isExist)   
    {
        if (isExist)
        {
            Debug.Log("»ç¿ë °¡´ÉÇÑ id ÀÔ´Ï´Ù.");
            ErrorTxt[5].gameObject.SetActive(false);
            ErrorLine[1].gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("ÀÌ¹Ì Á¸ÀçÇÏ´Â id ÀÔ´Ï´Ù.");
            ErrorTxt[5].gameObject.SetActive(true);
            ErrorLine[1].gameObject.SetActive(true);
            
        }
        
    }

    public void ExistEmail()
    {
        ErrorTxt[6].gameObject.SetActive(true);
    }

}

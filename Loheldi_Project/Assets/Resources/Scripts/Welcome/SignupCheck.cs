using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class SignupCheck : MonoBehaviour
{
    [SerializeField]
    private Transform[] ErrorLine = new Transform[5]; //º¯º≠¥Î∑Œ ¿Ã∏ß, ID, PW, ¿Á¿‘∑¬PW, ¿Ã∏ﬁ¿œ

    public void Start()
    {
        for (int i = 0; i < ErrorLine.Length; i++)  // 1, 3, 5, 7, 9
        {
            ErrorLine[i].gameObject.SetActive(false);
        }
        //Debug.Log(ErrorLine[1]);  //ø¿∫Í¡ß∆Æ ¿Œ¡ˆO
    }



    public bool ChkName(string uName = "")   //¿Ã∏ß 2~6¿⁄ ¿Ã≥ª «—±€/øµæÓ, ∞¯πÈ πÃ∆˜«‘
    {
        Regex regex = new Regex(@"[a-zA-Z∞°-∆R]{2,6}$"); //¿Ã∏ß ¡§±‘Ωƒ. øµ¥Îº“πÆ¿⁄, «—±€ 2~6¿⁄ ∞°¥…
        bool isCorrect = true; //«—±€, øµæÓ∑Œ∏∏ ¿Ã∑ÁæÓ¡¸+∞¯πÈ πÃ∆˜«‘¿œ Ω√ true

        if ((regex.IsMatch(uName)))    //¡§±‘Ωƒ ¿œƒ° Ω√
        {
            Debug.Log("¿Ã∏ß¿Ã æÁΩƒ∞˙ ¿œƒ°«’¥œ¥Ÿ.");
            ErrorLine[1].gameObject.SetActive(false);
            isCorrect = true;
        }
        else
        {
            Debug.Log("¿Ã∏ß¿Ã æÁΩƒ∞˙ ¿œƒ°«œ¡ˆ æ Ω¿¥œ¥Ÿ.");
            //Debug.Log(ErrorLine[1]);  //ø¿∫Í¡ß∆Æ ¿Œ¡ˆX
            ErrorLine[1].gameObject.SetActive(true);
            isCorrect = false;
        }

        return isCorrect;
    }

    public bool ChkID(string uID = "")     //ID 5~20¿⁄ ¿Ã≥ª øµæÓ, º˝¿⁄
    {
        Regex regex = new Regex(@"[a-zA-Z0-9]{5,20}$"); //ID ¡§±‘Ωƒ. øµ¥Îº“πÆ¿⁄, º˝¿⁄ 5~20¿⁄ ¿Ã≥ª ∞°¥…
        bool isCorrect = true; //¡§±‘Ωƒ ∏∏¡∑ Ω√, true

        if ((regex.IsMatch(uID)))    //¡§±‘Ωƒ ∫“¿œƒ° Ω√
        {
            Debug.Log("ID∞° æÁΩƒ∞˙ ¿œƒ°«’¥œ¥Ÿ.");
            ErrorLine[3].gameObject.SetActive(false);
            isCorrect = true;
        }
        else
        {
            Debug.Log("ID∞° æÁΩƒ∞˙ ¿œƒ°«œ¡ˆ æ Ω¿¥œ¥Ÿ.");
            ErrorLine[3].gameObject.SetActive(true);
            isCorrect = false;
        }
        return isCorrect;
    }

    public bool ChkPW(string uPW = "")     //∫Òπ–π¯»£ 20¿⁄ ¿Ã≥ª øµæÓ+º˝¿⁄+∆ØºˆπÆ¿⁄ ¡∂«’
    {
        bool isCorrect = true; //¡§±‘Ωƒ ∏∏¡∑ Ω√, true

        //º˝¿⁄1¿ÃªÛ, øµπÆ1¿ÃªÛ, ∆ØºˆπÆ¿⁄1¿ÃªÛ
        Regex regex = new Regex(@"^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{5,}$", RegexOptions.IgnorePatternWhitespace);

        if (uPW.Length >= 5 && uPW.Length <= 20)
        {
            if (regex.IsMatch(uPW))
            {
                ErrorLine[5].gameObject.SetActive(false);
                isCorrect = true;
                Debug.Log("PW∞° æÁΩƒ∞˙ ¿œƒ°«’¥œ¥Ÿ.");
            }
            else
            {
                ErrorLine[5].gameObject.SetActive(true);
                isCorrect = false;
                Debug.Log("PW∞° æÁΩƒ∞˙ ¿œƒ°«œ¡ˆ æ Ω¿¥œ¥Ÿ.(¡§±‘Ωƒ ∫“∏∏¡∑)");
            }
        }
        else
        {
            ErrorLine[5].gameObject.SetActive(true);
            isCorrect = false;
            Debug.Log("PW∞° æÁΩƒ∞˙ ¿œƒ°«œ¡ˆ æ Ω¿¥œ¥Ÿ.(¿⁄∏¥ºˆ ∫“¿œƒ°)");
        }

        return isCorrect;
    }

    public bool RePW(string PW = "", string rePW = "")      //∫Òπ–π¯»£ ¡ﬂ∫π »Æ¿Œ
    {
        bool isCorrect = true;
        if (PW.Equals(rePW))
        {
            ErrorLine[5].gameObject.SetActive(false);
            ErrorLine[7].gameObject.SetActive(false);
            isCorrect = true;
            Debug.Log("pw∞° ¿œƒ°«’¥œ¥Ÿ.");
        }
        else
        {
            ErrorLine[5].gameObject.SetActive(true);
            ErrorLine[7].gameObject.SetActive(true);
            isCorrect = false;
            Debug.Log("pw∞° ¿œƒ°«œ¡ˆ æ Ω¿¥œ¥Ÿ.");
        }

        return isCorrect;
    }

    public bool ChkEmail(string uEmail = "")  //e-mail æÁΩƒ »Æ¿Œ
    {
        Regex regex = new Regex(@"[a-zA-Z0-9]{1,20}@[a-zA-Z0-9]{1,20}.[a-zA-Z]{1,5}$"); //¿Ã∏ﬁ¿œ ¡§±‘Ωƒ
        bool isCorrect = true; //¡§±‘Ωƒ ∏∏¡∑ Ω√, true

        if ((regex.IsMatch(uEmail)))    //¡§±‘Ωƒ ∫“¿œƒ° Ω√
        {
            Debug.Log("email¿Ã æÁΩƒ∞˙ ¿œƒ°«’¥œ¥Ÿ.");
            ErrorLine[9].gameObject.SetActive(false);
            isCorrect = true;
        }
        else
        {
            Debug.Log("email¿Ã æÁΩƒ∞˙ ¿œƒ°«œ¡ˆ æ Ω¿¥œ¥Ÿ.");
            ErrorLine[9].gameObject.SetActive(true);
            isCorrect = false;
        }
        return isCorrect;

    }

    /*
//¡ﬂ∫π »Æ¿Œ ID, Email

public bool ExistID()   
{

}

public bool ExistEmail()
{

}*/

}

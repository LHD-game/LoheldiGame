using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public Text text;                               //점프버튼에 글자를 선언
    public bool NearNPC = false;                    //NPC근처에 있는지 확인하는 함수 선언
    public string NameNPC;
    

    void OnTriggerEnter(Collider other)             //다른 콜리더와 부딛혔을때
    {
        if (other.gameObject.tag == "NPC")          //콜리더의 Tag가 NPC라면
        {
            NearNPC = true;
            text.text = "대화";

            if (other.gameObject.name == "tiger")
            {
                NameNPC = "tiger";
            }
            else if (other.gameObject.name == "cat")
            {
                NameNPC = "cat";
            }
            else if (other.gameObject.name == "chick")
            {
                NameNPC = "chick";
            }
            else if (other.gameObject.name == "rabbit")
            {
                NameNPC = "rabbit";
            }
            else if (other.gameObject.name == "squirrel")
            {
                NameNPC = "squirrel";
            }
            else if (other.gameObject.name == "goat")
            {
                NameNPC = "goat";
            }
            else if (other.gameObject.name == "fox2")
            {
                NameNPC = "fox2";
            }
            else if (other.gameObject.name == "fox1")
            {
                NameNPC = "fox1";
            }
            else if (other.gameObject.name == "dog")
            {
                NameNPC = "dog";
            }
        }
    }

    void OnTriggerStay(Collider other)              //다른 콜리더와 겹쳐있을때
    {

    }

    void OnTriggerExit(Collider other)              //다른 콜리더와 떨어졌을때
    {
        if (other.gameObject.tag == "NPC")          //콜리더의 Tag가 NPC라면
        {
            NearNPC = false;
            text.text = "점프";
        }
    }
}
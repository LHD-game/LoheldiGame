using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailLoad : MonoBehaviour
{
    public Text RTitleText;                                           //우측에 표시되는 메일 제목
    public Text RDetailText;
    public GameObject ThisTitle;                                      //메일 제목
    public GameObject ThisSent;                                       //보낸 사람
    string Detail1 = "A";                                             //메일 내용 (나중에 서버에서 불러오는방식으로 변경)
    string Detail2 = "B";
    string Detail3 = "C";
    string Detail4 = "D";

     void Start()
    {
        RTitleText = GameObject.Find("RTitleText").GetComponent<Text>();    //RTitleText라는 이름에 Text오브젝트를 찾아 RTitleText라고 지정함
        RDetailText = GameObject.Find("RDetailText").GetComponent<Text>();  //RDetailText이라는 이름에 Text오브젝트를 찾아 RDetailText라고 지정함
    } 

    public void MailLoading()
    {
        ThisTitle = this.transform.GetChild(1).gameObject;                  //메일에 제목을 지정함 (나중에 DB에서 불러오는 스크립트 필요)
        ThisSent = this.transform.GetChild(2).gameObject;                   //메일 쓴 사람을 지정함(                ''                )

        RTitleText.text = ThisTitle.GetComponent<Text>().text;              //우측에 표시되는 제목을 선택한 제목과 같게 함
        RDetailText.text = Detail1;                                         //내용을 Detail1으로 바꿈
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LodingTxt : MonoBehaviour
{
    public Transform Player;
    public Transform Nari;

    public Camera MainCamera;
    public Camera QuizCamera;

    private Text Txt;
    public Text Name;
    public Text chatName;
    public Text QuizName;
    public Text chatTxt;
    public Text QuizTxt;
    public Text[] QuizButton = new Text[3];

    public GameObject[] SelecButton = new GameObject[5];
    public Text[] SelecButtonTxt = new Text[5];

    public GameObject Main_UI;
    public GameObject Button;
    public GameObject NPCButtons;
    public GameObject OImage;
    public GameObject XImage;
    public GameObject Quiz;
    public Material[] Quiz_material;
    [SerializeField]
    private Material[] material;

    public GameObject Arrow;
    public GameObject block;        //넘김방지 맨앞 블럭
    public Color color;
    public GameObject Chat;
    public GameObject ChatWin;
    public GameObject QuizeWin;
    public GameObject chatCanvus;
    public GameObject shopCanvus;
    public GameObject MailCanvus;

    public GameObject movie;
    public GameObject DrawUI;
    public GameObject Note;
    public GameObject Value;
    public GameObject IMessage;
    public GameObject KeyToDream;
    public GameObject AppleTree;
    public GameObject MasterOfMtLife;

    public int NPCButton = 0;
    public string LoadTxt;

    public List<Dictionary<string, object>> data_Dialog = new List<Dictionary<string, object>>();
    public string FileAdress;                // 스크립트 파일 위치
    public string cuttoonFileAdress;         // 컷툰 파일 위치

    public string Num;                       //스크립트 번호
    public int j;                                  //data_Dialog 줄갯수
    public int c = 0;                              //컷툰 이미지 번호
    //public static int k;                    //npc
    public int l;                            //뜨는 이미지 번호
    string Answer;               //누른 버튼 인식

    public bool tutoEnd = false;  //튜토리얼 완전 끝
    public bool tutoFinish = false;
    public bool tuto;
    public bool move = false; //캐릭터 순간이동
    public static GameObject CCImage;     //캐릭터 이미지
    public static Sprite[] CCImageList;
    static Image spriteR;

    public GameObject cuttoon;        //컷툰 이미지
    public Sprite[] cuttoonImageList;
    static Image cuttoonspriteR;

    //public Fadeln fade_in_out;
    public UIButton JumpButtons;
    tutorial tu;
    public Interaction Inter;

    int m = 0;                                  //카메라 무빙
    int o = 1;                                  //m서포터
    int MataNum = 0;                        //메터리얼 번호

    public QuestDontDestroy DontDestroy;
    private QuestScript Quest;
    private VideoScript video;
    public Drawing Draw;

    private void Awake()
    {
        Draw = GameObject.Find("QuestManager").GetComponent<Drawing>();
        video = GameObject.Find("QuestManager").GetComponent<VideoScript>();
        Quest = GameObject.Find("chatManager").GetComponent<QuestScript>();
        DontDestroy = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        JumpButtons = GameObject.Find("EventSystem").GetComponent<UIButton>();
        tu = GameObject.Find("chatManager").GetComponent<tutorial>();
        Inter = GameObject.Find("Player").GetComponent<Interaction>();

        Quiz_material = Quiz.GetComponent<MeshRenderer>().materials;
        color = block.GetComponent<Image>().color;
        ChatWin.SetActive(true);
        //if (SceneManager.GetActiveScene().name == "MainField")     //메인 필드에 있을 떄만 사용

        //fade_in_out = GameObject.Find("EventSystem").GetComponent<Fadeln>();
        CCImage = GameObject.Find("CCImage"); //이미지 띄울 곳
        Debug.Log("이미지=" + CCImage);
        CCImageList = Resources.LoadAll<Sprite>("Sprites/CCImage/"); //이미지 경로

        cuttoon = GameObject.Find("chatUI").transform.Find("Cuttoon").gameObject;
        cuttoon.SetActive(false);
        ChatWin.SetActive(false);
        QuizeWin.SetActive(false);
    }
    public void NewChat()
    {
        //cuttoonImageList = Resources.LoadAll<Sprite>(cuttoonFileAdress);
        //Debug.Log("이미지 리스트 갯수" + cuttoonImageList.Length);
        //Debug.Log("이미지 스프라이트 오브젝트: " + CCImage.name);
        Debug.Log("컷툰 파일 주소:"+ cuttoonFileAdress);
        Debug.Log("Num=" + Num);
        /*        if (DontDestroy.QuestMail)    //이거 없어도 될듯 뭐하는 애지 (아마 순간이동 할 떄 쓰이는 애 근데 이거 없어도 알아서 잘 하는데..? 이거 이미 0으로 할당 해두고 진행시킴. 혹시 모르니 남겨둬야지)
                    DontDestroy.QuestSubNum = Int32.Parse(data_Dialog[j]["scriptNumber"].ToString().Substring(0, data_Dialog[j]["scriptNumber"].ToString().IndexOf("_"))); //앞쪽 퀘스트 넘버만 자르기*/
        //if (SceneManager.GetActiveScene().name == "MainField")
        Main_UI.SetActive(false);
        data_Dialog = CSVReader.Read(FileAdress);
        for (int k = 0; k <= data_Dialog.Count; k++)
        {
            //Debug.Log(data_Dialog[k]["scriptNumber"].ToString());
            if (data_Dialog[k]["scriptNumber"].ToString().Equals(Num))
            {
                j = k;
                if (DontDestroy.tutorialLoading)
                {
                    j += 1;
                }
                chatCanvus.SetActive(true);
                ChatTime();
                Line();
                break;
            }
            else
            {
                continue;
            }
        }

    }

    public void changeMoment()  //플레이어 이동, 카메라 무브
    {
        if (o != m)
        {
            if ((o == 3 || o == 4 || o == 5 || o == 6 || o == 7 || o == 8 || o == 9 || o == 10 || o == 11 || o == 12))
            {
                switch (o)
                {
                    case 3:
                        Player.transform.position = new Vector3(-145.334457f, 17.3483009f, -32.4463768f);
                        break;
                    case 4:
                        if (DontDestroy.tutorialLoading)
                            Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                        Player.transform.position = new Vector3(45.25f, 5.2f, 49.5f);
                        DontDestroy.tutorialLoading = false;
                        break;
                    case 5:
                        Player.transform.position = new Vector3(102.449997f, 16.0599995f, 163.380005f);
                        Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                        break;
                    case 6:
                        Player.transform.position = new Vector3(-43.25f, 5.78999996f, 81.6999969f);
                        Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                        break;
                    case 7:
                        Player.transform.position = new Vector3(273.381134f, 5.6500001f, 100.656158f);
                        Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                        break;
                    case 8:
                        Player.transform.position = new Vector3(257.940002f, 5.6500001f, 100.656158f);
                        Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                        break;
                    case 9:
                        Player.transform.position = new Vector3(71.1548233f, 5.98000002f, -20.8635712f);
                        Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                        break;
                    case 10:
                        Player.transform.position = new Vector3(-46f, 5.57700014f, -13.6999998f);
                        Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                        break;
                    case 11:
                        Player.transform.position = new Vector3(327.879333f, 5.67999983f, 19.1537189f);
                        Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                        break;
                    case 12:
                        Player.transform.position = new Vector3(46.8151436f, 5.57000017f, 55.7096672f);
                        Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                        break;
                    default:
                        break;
                }
                m = o;
            }
        }
        o = Int32.Parse(data_Dialog[j]["scriptNumber"].ToString().Substring(data_Dialog[j]["scriptNumber"].ToString().IndexOf("_") + 1));

    } 

    private void QuestSubChoice()
    {
        if (data_Dialog[j]["scriptType"].ToString().Equals("quiz"))  //퀴즈시작
        {
            MataNum = Int32.Parse(data_Dialog[j]["QuizNumber"].ToString());
            QuizTIme();
            scriptLine();
            for (int i = 0; i < QuizButton.Length; i++)
            {
                QuizButton[i].text = data_Dialog[j]["select" + (i + 1)].ToString();
                //string selecNumber = "select" + (i + 1).ToString();
            }
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("choice"))  //선택지
        {
            j--;
            /*for (int i = 0; i < QuizButton.Length; i++)
            {
                QuizButton[i].text = data_Dialog[j]["select" + (i + 1)].ToString();
                //string selecNumber = "select" + (i + 1).ToString();
            }*/
            QuizCho();
            //Button.SetActive(true); //유니티에서 버튼 위치 옮김
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("over"))  //카메라 시점 원상복귀로 변경
        {
            ChatTime();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("cuttoon"))
        {
            Cuttoon();
            ChatWin.SetActive(false);
            Invoke("scriptLine", 2f);   //딜레이 후 스크립트 띄움
        } //컷툰 보이기
        else if (data_Dialog[j]["scriptType"].ToString().Equals("Dcuttoon"))
        {
            scriptLine();
        } //컷툰 보이기
        else if (data_Dialog[j]["scriptType"].ToString().Equals("tutorial"))//튜토리얼
            scriptLine();
        else if (data_Dialog[j]["scriptType"].ToString().Equals("video"))//동영상 실행
        { 
            video.OnPlayVideo();
            Main_UI.SetActive(false);
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("videoEnd")) //동영상 실행 중지       영상에 몇초 뒤 버튼을 추가시켜 그걸 누르면 확인창으로 넘어가게끔
        {
            video.OnResetVideo();
            Main_UI.SetActive(false);
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("KeepC"))
        {
            c = Int32.Parse(data_Dialog[j]["cuttoon"].ToString());
            RectTransform rectTran = cuttoon.GetComponent<RectTransform>();
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 8208);
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 3888);
            Vector3 position = cuttoon.transform.localPosition;
            if (c == 1)
            {
                position.x = -778;
                position.y = -42;
                cuttoon.transform.Rotate(0, 0, -35);
            }
            else if (c == 2)
            {
                position.x = -2029;
                position.y = -233;
                cuttoon.transform.Rotate(0, 0, 35);
            }
            else if (c == 3)
            {
                position.x = -1913;
                position.y = 685;
                cuttoon.transform.Rotate(0, 0, 0);
            }
            else if (c == 4)
            {
                position.x = -493;
                position.y = 685;
                cuttoon.transform.Rotate(0, 0, 0);
            }
            cuttoon.transform.localPosition = position;
            ChatWin.SetActive(false);
            Invoke("scriptLine", 2f);   //딜레이 후 스크립트 띄움
        }   //컷툰 계속 (회전)
        else if (data_Dialog[j]["scriptType"].ToString().Equals("KeepC_Over"))  //컷툰 계속 하는거 끝 (회전)
        {
            RectTransform rectTran = cuttoon.GetComponent<RectTransform>();
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 3040);
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1440);
            Vector3 position = cuttoon.transform.localPosition;

            position.x = 0;
            position.y = 0;
            cuttoon.transform.localPosition = position;
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("note"))        //퀘스트중간애들
        {
            ChatWin.SetActive(false);
            Note.SetActive(true);
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("noteEnd"))
        {
            Note.SetActive(false);
            //ChatWin.SetActive(true);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("i-message"))
        {
            ChatWin.SetActive(false);
            IMessage.SetActive(true);
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("i-messageEnd"))
        {
            //ChatWin.SetActive(true);
            IMessage.SetActive(false);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("train"))
        {
            ChatWin.SetActive(false);
            Value.SetActive(true);
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("trainEnd"))
        {
            //ChatWin.SetActive(true);
            Value.SetActive(false);
            scriptLine();

        }/*
        else if (data_Dialog[j]["scriptType"].ToString().Equals("e"))  //뭐하는 애지
        {
            ChatWin.SetActive(false);
            Draw.ChangeDrawCamera();
        }*/
        else if (data_Dialog[j]["scriptType"].ToString().Equals("drawFinish"))
        {
            scriptLine();
            Debug.Log("다그려땡");
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("Screenshot"))
        {
            Value.SetActive(false);
            scriptLine();
            Debug.Log("스샷 어케하는경");
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("drawEnd"))
        {
            Value.SetActive(false);
            //scriptLine();
            Invoke("scriptLine", 1f);   //딜레이 후 스크립트 띄움
            Draw.ChangeDrawCamera();
        }
    }
    public void Line()  //줄넘김
    {
        //튜토리얼 스크립트 이어가는 애
        //Debug.Log("튜툐:"+tuto);
        //Debug.Log("튜툐피니:"+tutoFinish);
        Debug.Log("무브=" + move);
        if (tuto && tutoFinish)
        {
            chatCanvus.SetActive(true);
            color.a = 0;
            block.GetComponent<Image>().color = color;
            tuto = false;
            tutoFinish = false;
        }
        if (data_Dialog[j]["scriptType"].ToString().Equals("end")) //대화 끝
        {
            ChatEnd();
            if (data_Dialog[j]["name"].ToString().Equals("end"))
                QuestEnd();
        }
        else
        {
            if (!data_Dialog[j]["scriptType"].ToString().Equals("nomal"))
                QuestSubChoice();
            else
            {
                cuttoon.SetActive(false);
                scriptLine();
            }
            if (data_Dialog[j]["scriptNumber"].ToString().Equals("0_1"))
                Main_UI.SetActive(false);

        }
        if (move)
            changeMoment();
    }

    
    public void scriptLine()  //스크립트 띄우는 거
    {
        spriteR = CCImage.GetComponent<Image>();
        l = Int32.Parse(data_Dialog[j]["image"].ToString());
        //Debug.Log("j=" + j);
        //Debug.Log("이미지번호=" + l);
        spriteR.sprite = CCImageList[l];

        if (ChatWin.activeSelf == false)
            ChatWin.SetActive(true);
        

        LoadTxt = data_Dialog[j]["dialog"].ToString().Replace("P_name","이름");
        if (data_Dialog[j]["name"].ToString().Equals("주인공"))
            Name.text = "이름";
        else
            Name.text = data_Dialog[j]["name"].ToString();
        
        StartCoroutine(_typing());
        Arrow.SetActive(false);
        block.SetActive(true);

        j++;
    }

    public void Cuttoon()
    {
        c= Int32.Parse(data_Dialog[j]["cuttoon"].ToString());
        cuttoon.SetActive(true);
        //Debug.Log("컷툰갯수=" + cuttoonImageList.Length);
        cuttoonspriteR = cuttoon.GetComponent<Image>();
        cuttoonspriteR.sprite = cuttoonImageList[c];
    }

    public void ChatEnd() //리셋
    {
        //if (SceneManager.GetActiveScene().name == "MainField")
        JumpButtons.Main_UI.SetActive(true);
        chatCanvus.SetActive(false);
        ChatWin.SetActive(false);
        QuizeWin.SetActive(false);
        Arrow.SetActive(false);
        NPCButtons.SetActive(false);
        Name.text = " ";
        Main_UI.SetActive(true);
        c = 0;
        for (int i = 0; i < NPCButton; i++)
        {
            string selecNumber = "select" + (i + 1).ToString();
            SelecButton[i].SetActive(false);
            SelecButtonTxt[i].text = data_Dialog[j - 1][selecNumber].ToString();
        }
        NPCButton = 0;
    }

    public void Buttons()      //npc대화 상호작용 선택지 수
    {
        //Debug.Log("인터NPC:" + Inter.NameNPC);
        if (Inter.NameNPC.Equals(DontDestroy.ButtonPlusNpc))
            NPCButton += 1;

        NPCButtons.SetActive(true);
        for (int i= 0; i < NPCButton;i++)
        {
            string selecNumber = "select"+(i+1).ToString();
            SelecButton[i].SetActive(true);
            SelecButtonTxt[i].text = data_Dialog[j-1][selecNumber].ToString();
        }
    }

    public void QuizTIme() //카메라 시점 변경으로 변경
    {
        QuizMate();
        MainCamera.enabled = false;
        QuizCamera.enabled = true;
        Draw.Dcam.enabled = false;
    }
    public void QuizMate() //전광판 메테리얼 설정
    {
        Quiz_material[1] = material[MataNum]; //0에 메테리얼 번호
        Quiz.GetComponent<MeshRenderer>().materials = Quiz_material;
        //Debug.Log("응애"+ material[MataNum]);

    }

    public void ChatTime() //시작에 합체시킴
    {
        
        MainCamera.enabled = true;
        QuizCamera.enabled = false;
        //k = 0;
        Txt = chatTxt;
        Name=chatName;
        //ChatWin.SetActive(true);
        //QuizeWin.SetActive(false);
    }

    IEnumerator _typing()  //타이핑 효과
    {
        
        Txt.text = " ";
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < LoadTxt.Length + 1; i++)
        {
            Txt.text = LoadTxt.Substring(0, i);
            yield return new WaitForSeconds(0.01f);
        }
        
        //Arrow.SetActive(true);

        /*if (data_Dialog[j-1]["scriptType"].ToString().Equals("choice"))  //선택지
        {
            j--;
            for (int i = 0;i<QuizButton.Length; i++)
            {
                QuizButton[i].text = data_Dialog[j]["select" + (i + 1)].ToString();
                //string selecNumber = "select" + (i + 1).ToString();
            }
            Invoke("QuizCho", 1f);
            //Button.SetActive(true); //유니티에서 버튼 위치 옮김
        }*/


        if (data_Dialog[j - 1]["scriptType"].ToString().Equals("tutorial") || tuto)
        {
            Debug.Log("튜토리얼 실핻ㅇ");
            Invoke("Tutorial_", 2f);
        }
        else
            block.SetActive(false);
    }

    void QuizCho()
    {
        Chat.SetActive(false);
        Button.SetActive(true); //유니티에서 버튼 위치 옮김
    }

    void Tutorial_()
    {
        tu.Tutorial();
        tuto = true;
    }

    public void Answer1()
    {
        Answer = "1";
        QuizeAnswer();
    }
    public void Answer2()
    {
        Answer = "2";
        QuizeAnswer();
    }
    public void Answer3()
    {
        Answer = "3";
        QuizeAnswer();
    }
    public void Answer4()
    {
        Answer = "4";
        QuizeAnswer();
    }
    public void Answer5()
    {
        Answer = "5";
        QuizeAnswer();
    }
    public void QuizeAnswer()
    {
        if (data_Dialog[j]["answer"].ToString().Equals(Answer))
        {
            Chat.SetActive(true);
            O();
            scriptLine();
            //Line();
        }

        else
        {
            Chat.SetActive(true);
            X();
        }
    }

    public void O()//정답 골랐을 때
    {
        block.SetActive(true);
        OImage.SetActive(true); 
        Button.SetActive(false);
        j++;

    }
    public void X()//틀린 정답 골랐을 때
    {
        block.SetActive(true);
        XImage.SetActive(true);
        Button.SetActive(false);
        LoadTxt = "다시 생각해보자!";
        StartCoroutine(_typing());

    }

    public void Xback()//X이미지 버튼
    {
        XImage.SetActive(false);
        Button.SetActive(true);
    }
    private void QuestEnd()
    {
        DontDestroy.ButtonPlusNpc = "";
        Quest.Load.QuestMail = false;
        DontDestroy.QuestIndex++;
       // Quest.Load.Quest = true;

        DontDestroy.LastDay = DontDestroy.ToDay;

    }

}

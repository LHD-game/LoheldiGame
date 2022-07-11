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

    public Text videocheckTxT;
    public string parentscheckTxTNum;
    public GameObject ErrorWin;

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
    public GameObject screenShot;

    public GameObject Bike;
    public GameObject BikeNPC;

    public int NPCButton = 0;
    public string LoadTxt;
    private string[] Xdialog = {"다시 한번 생각해봐.","아쉽지만 틀렸어.","땡!","다시 도전해봐"};

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
    int QBikeSpeed;
    bool BikeQ = false;
    float timer=0.0f;
    bool bikerotate = false;
    Vector3 NPCBike;

    public QuestDontDestroy DontDestroy;
    private QuestScript Quest;
    public VideoScript video;
    public Drawing Draw;
    public BicycleRide bicycleRide; 
    public ChangColor badgeList; 
    [SerializeField]
    private ParticleSystem hairPs;

    public Animator JumpAnimator;
    public Animator NPCJumpAnimator;
    public Animator JumpAnimatorRope;
    public Animator NPCJumpAnimatorRope;
    public GameObject PlayerRope;
    public GameObject NPCRope;

    public Animator CleanT;
    string PlayerName;
    private void Awake()
    {
        Draw = GameObject.Find("QuestManager").GetComponent<Drawing>();
        video = GameObject.Find("QuestManager").GetComponent<VideoScript>();
        Quest = GameObject.Find("chatManager").GetComponent<QuestScript>();
        DontDestroy = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        JumpButtons = GameObject.Find("EventSystem").GetComponent<UIButton>();
        badgeList = GameObject.Find("EventSystem").GetComponent<ChangColor>();
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

        GameObject.Find("Player").transform.position = DontDestroy.LastPlayerTransform.transform.position;
        Debug.Log("플레이어 위치 설정" + DontDestroy.LastPlayerTransform.transform.position);
    }

    void FixedUpdate()
    {
        if (BikeQ)
        {
            if(QBikeSpeed != 12)
            BikeNPC.transform.position = Player.position + NPCBike;
            BikeNPC.transform.rotation = Player.rotation;
            if (timer > 5)
            {
                Debug.Log(QBikeSpeed);
                if (bikerotate)
                {
                    JumpButtons.Playerrb.velocity = JumpButtons.Playerrb.velocity.normalized * 0;
                    NPCBike = new Vector3(-4, 0, 0);
                    Player.rotation = Quaternion.Euler(0, 90, 0);
                    bikerotate = false;
                }
                else
                {
                    JumpButtons.Playerrb.velocity = JumpButtons.Playerrb.velocity.normalized * 0;
                    NPCBike = new Vector3(4, 0, 0);
                    Player.rotation = Quaternion.Euler(0, -90, 0);
                    bikerotate = true;
                }
                timer = 0;
            }

            if (QBikeSpeed == 3 && JumpButtons.Playerrb.velocity.magnitude > QBikeSpeed)
            {
                JumpButtons.Playerrb.velocity = JumpButtons.Playerrb.velocity.normalized * QBikeSpeed;
            }
            else if (QBikeSpeed == 12 && JumpButtons.Playerrb.velocity.magnitude > QBikeSpeed)
                JumpButtons.Playerrb.velocity = JumpButtons.Playerrb.velocity.normalized * (QBikeSpeed + 2);
            timer += Time.deltaTime;
            JumpButtons.Playerrb.AddRelativeForce(Vector3.forward * 1000f); //앞 방향으로 밀기 (방향 * 힘)
            





        }
    }

    public void skip()
    {
        j = 82;
        move = false;
        scriptLine();
    }
    public void NewChat()
    {
        //cuttoonImageList = Resources.LoadAll<Sprite>(cuttoonFileAdress);
        //Debug.Log("이미지 리스트 갯수" + cuttoonImageList.Length);
        //Debug.Log("이미지 스프라이트 오브젝트: " + CCImage.name);
        //Debug.Log("컷툰 파일 주소:"+ cuttoonFileAdress);
        //Debug.Log("Num=" + Num);
        /*        if (DontDestroy.QuestMail)    //이거 없어도 될듯 뭐하는 애지 (아마 순간이동 할 떄 쓰이는 애 근데 이거 없어도 알아서 잘 하는데..? 이거 이미 0으로 할당 해두고 진행시킴. 혹시 모르니 남겨둬야지)
                    DontDestroy.QuestSubNum = Int32.Parse(data_Dialog[j]["scriptNumber"].ToString().Substring(0, data_Dialog[j]["scriptNumber"].ToString().IndexOf("_"))); //앞쪽 퀘스트 넘버만 자르기*/
        //if (SceneManager.GetActiveScene().name == "MainField")
        PlayerName=PlayerPrefs.GetString("Nickname");
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
                        Player.transform.position = new Vector3(-145.300003f, 12.6158857f, -21.80023f);
                        break;
                    case 4:
                        if (DontDestroy.tutorialLoading)
                            Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                        Player.transform.position = new Vector3(45.1500015f, 5.31948805f, 50.0898895f);
                        DontDestroy.tutorialLoading = false;
                        break;
                    case 5:
                        Player.transform.position = new Vector3(103.51342f, 15.7201061f, 165.103439f);
                        Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                        break;
                    case 6:
                        Player.transform.position = new Vector3(-44.7900009f, 5.319489f, 79.5400085f);
                        Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                        break;
                    case 7:
                        Player.transform.position = new Vector3(288.572632f, 5.31948948f, 98.3887405f);
                        Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                        break;
                    case 8:
                        Player.transform.position = new Vector3(255, 5.31949139f, 101.299973f);
                        Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                        break;
                    case 9:
                        Player.transform.position = new Vector3(69.9799881f, 5.67073011f, -16.2484417f);
                        Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                        break;
                    case 10:
                        Player.transform.position = new Vector3(-46f, 5.57700014f, -13.6999998f);
                        Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                        break;
                    case 11:
                        Player.transform.position = new Vector3(317.426666f, 5.67073059f, 25.669136f);
                        Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                        break;
                    case 12:
                        Player.transform.position = new Vector3(45.1500015f, 5.31948805f, 50.0898895f);
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

    public void QuestSubChoice()
    {
        if (data_Dialog[j]["scriptType"].ToString().Equals("quiz"))  //퀴즈시작
        {
            MataNum = Int32.Parse(data_Dialog[j]["QuizNumber"].ToString());
            QuizTIme();
            scriptLine();
            for (int i = 0; i < QuizButton.Length; i++)
            {
                if (data_Dialog[j]["select" + (i + 1)].ToString().Equals("빈칸"))
                    QuizButton[i].transform.parent.gameObject.SetActive(false);
                else
                    QuizButton[i].transform.parent.gameObject.SetActive(true);
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
            scriptLine();
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
        { 
            scriptLine(); 
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("video"))//동영상 실행
        {
            video.videoClip.clip = video.VideoClip[Int32.Parse(data_Dialog[j]["cuttoon"].ToString())];
            movie.SetActive(true);
            video.OnPlayVideo();
            Chat.SetActive(false);
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("videoEnd")) //동영상 실행 중지       영상에 몇초 뒤 버튼을 추가시켜 그걸 누르면 확인창으로 넘어가게끔
        {
            if (videocheckTxT.text.Equals(parentscheckTxTNum))
            {
                movie.SetActive(false);
                Chat.SetActive(true);
                scriptLine();
            }
            else
            {
                ErrorWin.SetActive(true);
            }
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("Bike"))
        {
            cuttoon.SetActive(false);
            Chat.SetActive(false);
            Bike.SetActive(true);
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("Bicycle"))
        {
            if (!BikeQ)
            {
                NPCBike = new Vector3(-4, 0, 0);
                bicycleRide.RideOn();
                BikeNPC = GameObject.Find(Inter.NameNPC);
                Destroy(GameObject.Find("Qbicycle(Clone)"));
                Player.rotation = Quaternion.Euler(0, 90, 0);
                QBikeSpeed = 3;
                BikeQ = true;
            }
            else if (QBikeSpeed == 12)
            {
                BikeQ = false; 
                Vector3 targetPositionNPC;
                Vector3 targetPositionPlayer;
                targetPositionNPC = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
                targetPositionPlayer = new Vector3(BikeNPC.transform.position.x, BikeNPC.transform.position.y, BikeNPC.transform.position.z);
                BikeNPC.transform.LookAt(targetPositionNPC);
                Player.transform.LookAt(targetPositionPlayer);
            }
            else if (BikeQ)
            {
                //페이드 인 페이드 아웃하면서 화면에 한시간 후... 띄우기
                QBikeSpeed = 12;
                Player.position = new Vector3(37, 6, -14);
                BikeNPC.transform.position = Player.position + new Vector3(10, 0, 10);
                BikeNPC.transform.rotation = Quaternion.Euler(0, 0, 0);
                Player.rotation = Player.rotation = Quaternion.Euler(0, 90, 0);
                bikerotate = true;
                timer = 0;
            }
            
            scriptLine();

        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("hair"))
        {
            Chat.SetActive(false);
            hairFX(GameObject.Find("Player"));
            j++;
            Invoke("clearHair", 1f);
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("note"))        //퀘스트중간애들
        {
            j++;
            ChatWin.SetActive(false);
            Note.SetActive(true);
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("noteEnd"))
        {
            Note.SetActive(false);
            ChatWin.SetActive(true);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("train"))
        {
            ChatWin.SetActive(false);
            Value.SetActive(true);
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("trainEnd"))
        {
            //ChatWin.SetActive(true);
            Value.SetActive(false);
            scriptLine();

        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("draw"))
        {
            ChatWin.SetActive(false);
            Draw.ChangeDrawCamera();
            //scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("drawFinish"))
        {
            Draw.ForDraw = false;
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("Screenshot"))
        {
            screenShot.SetActive(true);
            Chat.SetActive(false);
            //scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("drawEnd"))
        {
            Draw.ChangeDrawCamera();
            //scriptLine();
            Invoke("scriptLine", 1f);   //딜레이 후 스크립트 띄움
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("song"))
        {
            //웃어봐 송 틀기
            Invoke("scriptLine", 10f);   //딜레이 후 스크립트 띄움
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("songend"))
        {
            //원래 노래로 바꾸기
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("JumpRope"))
        {
            //줄넘기
            NPCJumpAnimator.SetBool("JumpRope", true);
            Invoke("scriptLine", 2f);
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("JumpRopeEnd"))
        {
            //줄넘기
            scriptLine();
            NPCJumpAnimator.SetBool("JumpRope", false);
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("MasterOfMtLife"))
        {
            //나에게 편지쓰기
            MasterOfMtLife.SetActive(true);
            Chat.SetActive(false);
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("MasterOfMtLifeEnd"))
        {
            MasterOfMtLife.SetActive(false);
            Chat.SetActive(true);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("AppleTree"))
        {
            AppleTree.SetActive(true);
            Chat.SetActive(true);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("AppleTreeEnd"))
        {
            MasterOfMtLife.SetActive(false);
            Chat.SetActive(true);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("MakeAppleTree"))
        {
            //사과 나무 프리펩 쩌구저꾸저
            Instantiate(Resources.Load<GameObject>("Prefabs/Q/Qbicycle"), new Vector3(65.1100006f, 5.41002083f, -17.799999f), Quaternion.Euler(0, 51.4773521f, 0));
            scriptLine();
        }
        
    }
    public void Line()  //줄넘김 (scriptType이 뭔지 걸러냄)
    {
        //튜토리얼 스크립트 이어가는 애
        //Debug.Log("튜툐:"+tuto);
        //Debug.Log("튜툐피니:"+tutoFinish);
        //Debug.Log("무브=" + move);
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

    
    public void scriptLine()  //스크립트 띄우는 거 (어굴 이미지+ 이름+ 뜨는 텍스트)
    {
        spriteR = CCImage.GetComponent<Image>();
        l = Int32.Parse(data_Dialog[j]["image"].ToString());
        //Debug.Log("j=" + j);
        //Debug.Log("이미지번호=" + l);
        spriteR.sprite = CCImageList[l];

        if (ChatWin.activeSelf == false)
            ChatWin.SetActive(true);
        
        
        LoadTxt = data_Dialog[j]["dialog"].ToString().Replace("P_name",PlayerName); //로컬값 가져오긴
        if (data_Dialog[j]["name"].ToString().Equals("주인공"))
            Name.text = PlayerName;
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
        Debug.Log("컷툰갯수=" + cuttoonImageList.Length);
        cuttoonspriteR = cuttoon.GetComponent<Image>();
        cuttoonspriteR.sprite = cuttoonImageList[c];
    }

    public void ChatEnd() //리셋
    {
        ChatTime();
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
        int x = UnityEngine.Random.Range(0,3);
        block.SetActive(true);
        XImage.SetActive(true);
        Button.SetActive(false);
        LoadTxt = Xdialog[x];
        StartCoroutine(_typing());

    }

    public void Xback()//X이미지 버튼
    {
        XImage.SetActive(false);
        Button.SetActive(true);
    }

    int Questbadge;
    private void QuestEnd()
    {
        DontDestroy.ButtonPlusNpc = "";
        Quest.Load.QuestMail = false;
        DontDestroy.QuestIndex++;
        // Quest.Load.Quest = true;
        //아래가 몇번째 퀘스트인지에 따라서 어느 뱃지를 잠금 해제해야하는지 해놓은거입니다!
        //저는 풀리는 뱃지의 list번호를 넣어두고 상태창을 열면 for문을 돌려서 setActive(false)하는 식으로
        //짜뒀었는데 혹시 이거 안필요한거면 말씀해주세요! 수정해두겠습니다.
        if(DontDestroy.QuestIndex ==4)
            badgeList.Ride.SetActive(true);
        /*switch(DontDestroy.QuestIndex)
         {
             case 2:
                 DontDestroy.badgeList.Add(2);
                 break;
             case 3:
                 DontDestroy.badgeList.Add(9);
                 break;
             case 4:
                 DontDestroy.badgeList.Add(4);
                 badgeList.Ride.SetActive(true);
                 break;
             case 6:
                 DontDestroy.badgeList.Add(7);
                 break;
             case 8:
                 DontDestroy.badgeList.Add(10);
                 break;
             case 9:
                 DontDestroy.badgeList.Add(5);
                 break;
             case 14:
                 DontDestroy.badgeList.Add(6);
                 break;
             case 15:
                 DontDestroy.badgeList.Add(11);
                 break;
             case 16:
                 DontDestroy.badgeList.Add(8);
                 break;
             case 17:
                 DontDestroy.badgeList.Add(3);
                 break;
             case 20:
                 DontDestroy.badgeList.Add(12);
                 break;
         }
         badgeList.color = true;*/
        DontDestroy.LastDay = DontDestroy.ToDay;
    }
    public void hairFX(GameObject go)    //머리 반짝!하는 파티클
    {
        Debug.Log("반짝");
        ParticleSystem newfx = Instantiate(hairPs);
        newfx.transform.position = go.transform.position+new Vector3(0,10,0);
        newfx.transform.SetParent(go.transform);

        newfx.Play();
    }

    void clearHair()
    {
        Chat.SetActive(true);
        scriptLine();
    }

}

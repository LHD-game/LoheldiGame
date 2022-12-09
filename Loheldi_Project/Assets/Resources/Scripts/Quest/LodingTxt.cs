using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class LodingTxt : MonoBehaviour
{
    public Transform Player;
    public Transform Nari;
    //public GameObject PlayerCamera;
    //public Camera MainCamera;
    //public Camera QuizCamera;

    public Text chatName;
    //public Text QuizName;
    public Text chatTxt;
    //메일 내용 뜨는거
    public Text Mailcontent;
    //public Text QuizTxt;
    public Text[] QuizButton = new Text[3];

    public GameObject[] SelecButton = new GameObject[5];
    public Text[] SelecButtonTxt = new Text[5];

    public InputField videocheckTxT;
    public InputField ParentscheckTxt;     
    public string parentscheckTxTNum;
    public GameObject ErrorWin;      //홈트인증실패
    public GameObject ClearWin;     //인증클리어
    public GameObject ParentsErrorWin;  //인증실패
    public GameObject ParentscheckUI;  //인증창

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
    public GameObject chatCanvus;
    public GameObject shopCanvus;
    public GameObject MailCanvus;

    public GameObject movie;
    public GameObject DrawUI;
    public GameObject Note;
    public GameObject Value;
    //public GameObject IMessage;
    public GameObject KeyToDream;
    public GameObject AppleTree;
    public GameObject MasterOfMtLife;
    public GameObject screenShot;
    public GameObject screenShotExit;
    public GameObject nutrient;
    public GameObject LoveNature;

    public GameObject Ride;
    public GameObject Bike;
    public GameObject BikeNPC;

    public GameObject AppleTreeObj;

    public GameObject SoundEffectManager;
    GameObject SoundManager;

    public int NPCButton = 0;
    public string LoadTxt;
    private string[] Xdialog = {"다시 한번 생각해봐.","아쉽지만 틀렸어.","땡!","다시 도전해봐"};

    public List<Dictionary<string, object>> data_Dialog = new List<Dictionary<string, object>>();
    public string FileAdress;                // 스크립트 파일 위치
    public string cuttoonFileAdress;         // 컷툰 파일 위치

    public string Num;                       //스크립트 번호
    public int j;                                  //data_Dialog 줄갯수
    public int c = 0;                              //컷툰 이미지 번호
    public int l;                            //뜨는 이미지 번호
    string Answer;               //누른 버튼 인식

    public bool tutoFinish = false;
    public bool tuto;
    public bool tutoclick;

    public GameObject CCImage;     //캐릭터 이미지
    public static Sprite[] CCImageList;
    static Image spriteR;

    public GameObject cuttoon;        //컷툰 이미지
    public Sprite[] cuttoonImageList;
    static Image cuttoonspriteR;

    public TMP_InputField KeyToDreamInput;
    //public Fadeln fade_in_out;
    public UIButton JumpButtons;
    public tutorial tu;
    public Interaction Inter;



    int m = 0;                                  //카메라 무빙
    public int o = 0;                                  //m서포터
    int MataNum = 0;                        //메터리얼 번호
    int QBikeSpeed;
    bool BikeQ = false;
    float timer=0.0f;
    float Maxtime;
    bool bikerotate = false;
    Vector3 NPCBike;

    public QuestDontDestroy DontDestroy;
    public QuestScript Quest;
    public VideoScript video;
    public Drawing Draw;
    public BicycleRide bicycleRide;
    public QuestLoad QuestLoad;
    public NpcButtonClick NpcButton;
    [SerializeField]
    private ParticleSystem hairPs;

    public Animator JumpAnimator;
    public Animator NPCJumpAnimator;
    public Animator JumpAnimatorRope;
    public Animator NPCJumpAnimatorRope;
    public GameObject PlayerRope;
    public GameObject NPCRope;

    string PlayerName;

    Animator ToothAnimator;
    private void Awake()
    {
        color = block.GetComponent<Image>().color;
        ChatWin.SetActive(true);
        
        //fade_in_out = GameObject.Find("EventSystem").GetComponent<Fadeln>();
        CCImageList = Resources.LoadAll<Sprite>("Sprites/CCImage/CImage"); //이미지 경로

        parentscheckTxTNum = PlayerPrefs.GetString("ParentsNo");
        PlayerName = PlayerPrefs.GetString("Nickname");

        DontDestroy = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();


        DontDestroy.LastDay = PlayerPrefs.GetInt("LastQTime");

        if (SceneManager.GetActiveScene().name == "MainField")     //메인 필드에 있을 떄만 사용
        {
            Player.position = DontDestroy.gameObject.transform.position;
            DontDestroy.LastDay = PlayerPrefs.GetInt("LastQTime");

            string[] QQ = PlayerPrefs.GetString("QuestPreg").Split('_');
            string[] qq = PlayerPrefs.GetString("WeeklyQuestPreg").Split('_');

            string[] q_qid = DontDestroy.QuestIndex.Split('_');
            if (Int32.Parse(QQ[0]) > 3)
                Ride.SetActive(true);
            else
                Ride.SetActive(false);
            if (Int32.Parse(QQ[0]) > 12)
                AppleTreeObj.SetActive(true);
            else
                AppleTreeObj.SetActive(false);
            //주말체크
            DateTime nowDT = DateTime.Now;
            if (nowDT.DayOfWeek == DayOfWeek.Saturday)
                DontDestroy.SDA = true;
            else if (nowDT.DayOfWeek == DayOfWeek.Sunday)
                DontDestroy.weekend = true;
            else
                DontDestroy.weekend = false;
            DontDestroy.ToDay = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));   //퀘스트용 오늘날짜 저장 */

            if (Int32.Parse(QQ[0]) == 0)
            {
                QuestLoad.QuestLoadStart();
            }
            else if (DontDestroy.SDA)
            {
                return;
            }
            else
            {
                if (!DontDestroy.weekend)
                {
                    if (Int32.Parse(QQ[0]) < 21)
                    {
                        if (DontDestroy.ToDay != DontDestroy.LastDay)
                            QuestLoad.QuestLoadStart();
                    }
                }
                else if(DontDestroy.weekend)
                {
                    if (Int32.Parse(qq[0]) < 25)
                    {
                        if (DontDestroy.ToDay != DontDestroy.LastDay)
                            QuestLoad.QuestLoadStart();
                    }
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "Quiz")
        {
            Quiz_material = Quiz.GetComponent<MeshRenderer>().materials;
        }
        else if (SceneManager.GetActiveScene().name == "Game_Tooth")
        {
            CCImage = GameObject.Find("CCImage");
            SoundEffectManager = GameObject.Find("GameManager");
        }
        
    }
    public void QuestTest()
    {
        PlayerPrefs.SetInt("LastQTime", 0);
        DontDestroy.LastDay = 0;
        string QuestType = null;
        if (!DontDestroy.weekend)
        {
            QuestType = "QuestPreg";
        }
        else
            QuestType = "WeeklyQuestPreg";
        DontDestroy.QuestIndex = QuestType;
        QuestLoad.QuestLoadStart();
    }
    public void QuestMoveTest()
    {
        PlayerPrefs.SetInt("LastQTime", 0);
        DontDestroy.LastDay = 0;
        string[] q_qid = DontDestroy.QuestIndex.Split('_');
        string QuestType = null;
        if (Int32.Parse(q_qid[0]) < 22)
        {
            QuestType = "QuestPreg";
        }
        else
            QuestType = "WeeklyQuestPreg";
        PlayerPrefs.SetString(QuestType, DontDestroy.QuestIndex);
        PlayInfoManager.GetQuestPreg();
        QuestLoad.QuestLoadStart();
    }

    public void NotWeekend()
    {
        if (DontDestroy.weekend)
            DontDestroy.weekend = false;
        else
            DontDestroy.weekend = true;
    }
    public void ToothQuest()
    {
        ToothAnimator = GameObject.Find("ToothBrush").transform.Find("Armature").gameObject.GetComponent<Animator>();
        Num = "22_2";
        FileAdress = "Scripts/Quest/script";
        NewChat();
    }


    IEnumerator QBikeLoop()
    {
        while (true)
        {
            if (JumpButtons.Playerrb.velocity.magnitude > QBikeSpeed)
            {
                if (timer > Maxtime)
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
                JumpButtons.Playerrb.velocity = JumpButtons.Playerrb.velocity.normalized * QBikeSpeed;
                if (Maxtime == 8)
                {
                    BikeNPC.transform.position = Player.position + NPCBike;
                    BikeNPC.transform.rotation = Player.rotation;
                }
                else
                {
                    Vector3 targetPositionNPC;
                    targetPositionNPC = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
                    BikeNPC.transform.LookAt(targetPositionNPC);
                }
            }
            timer += Time.deltaTime;
            JumpButtons.Playerrb.AddRelativeForce(Vector3.forward * 1000f); //앞 방향으로 밀기 (방향 * 힘)

            yield return null;
        }
    }
    
    public void skip()
    {
        Num = "0_2";
        o = 11;
        NewChat();
    }
    public void NewChat()
    {
        Input.multiTouchEnabled = false;
        //PlayerCamera.SetActive(true);
        //Debug.Log("퀴즈3");
        data_Dialog = CSVReader.Read(FileAdress);
        for (int k = 0; k <= data_Dialog.Count; k++)
        {
            if (data_Dialog[k]["scriptNumber"].ToString().Equals(Num))
            {
                j = k; 
                if (DontDestroy.tutorialLoading)
                    j=14;
                chatCanvus.SetActive(true);
                Line();
                //Debug.Log("퀴즈4");
                break;
            }
            else
            {
                continue;
            }
        }
        Main_UI.SetActive(false);
    }

    public void changeMoment()  //플레이어 이동, 카메라 무브
    {
        switch (o)
        {
            case 1:
                Player.transform.position = new Vector3(-145.300003f, 12.6158857f, -21.80023f);
                break;
            case 2:
                cuttoon.SetActive(false);
                Player.transform.position = new Vector3(45f, 5f, 40f);
                break;
            case 3:
                if (DontDestroy.tutorialLoading)
                    Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                DontDestroy.tutorialLoading = false;
                break;
            case 4:
                Player.transform.position = new Vector3(103.51342f, 15.7201061f, 165.103439f);
                Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                break;
            case 5:
                Player.transform.position = new Vector3(-44.7900009f, 5.319489f, 79.5400085f);
                Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                break;
            case 6:
                Player.transform.position = new Vector3(288.572632f, 5.31948948f, 98.3887405f);
                Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                break;
            case 7:
                Player.transform.position = new Vector3(255, 5.31949139f, 101.299973f);
                Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                break;
            case 8:
                Player.transform.position = new Vector3(69.9799881f, 5.67073011f, -16.2484417f);
                Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                break;
            case 9:
                Player.transform.position = new Vector3(-46f, 5.57700014f, -13.6999998f);
                Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                break;
            case 10:
                Player.transform.position = new Vector3(317.426666f, 5.67073059f, 25.669136f);
                Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                break;
            case 11:
                Player.transform.position = new Vector3(45f, 5f, 40f);
                Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                break;
            case 12:
                PlayerPrefs.SetString("QuestPreg", DontDestroy.QuestIndex);
                PlayInfoManager.GetQuestPreg();
                //Save_Log.instance.SaveQEndLog();    //퀘스트 종료 로그 기록
                SceneLoader.instance.GotoMainField();
                break;
            case 13:
                Player.transform.position = new Vector3(-139.300003f, 12.6158857f, -21.80023f);
                Player.transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
                GameObject Parents = GameObject.Find("parents(Clone)");
                Parents.transform.position = Player.transform.position + new Vector3(-6, 0, 0);
                Parents.transform.rotation = Quaternion.Euler(new Vector3(0, 150, 0));
                break;
            default:
                break;
        }
    }
    GameObject mouth; //양치겜 입
    public void QuestSubChoice()
    {
        Debug.Log("타입"+data_Dialog[j]["scriptType"].ToString());
        if (data_Dialog[j]["scriptType"].ToString().Equals("quiz"))  //퀴즈시작
        {
            MataNum = Int32.Parse(data_Dialog[j]["QuizNumber"].ToString());
            scriptLine();
            for (int i = 0; i < QuizButton.Length; i++)
            {
                if (data_Dialog[j]["select" + (i + 1)].ToString().Equals("빈칸"))
                    QuizButton[i].transform.parent.gameObject.SetActive(false);
                else
                    QuizButton[i].transform.parent.gameObject.SetActive(true);
                QuizButton[i].text = data_Dialog[j]["select" + (i + 1)].ToString();
            }
            QuizMate();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("choice"))  //선택지
        {
            block.SetActive(false);
            j--;
            QuizCho();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("Move"))  //선택지
        {
            if (data_Dialog[j]["scriptNumber"].ToString().Equals("24_1"))
                o = 13;
            else if (DontDestroy.tutorialLoading)
                o = 3;
            else
                o += 1;

            changeMoment();
            if (o == 12)
                return;
            else
            {
                j += 1;
                Line();
            }

        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("ReloadEnd"))  //main으로
        {
            QuestEnd();
            StartCoroutine(reload());
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("over"))  //main으로
        {
            PlayerPrefs.SetString("QuestPreg", DontDestroy.QuestIndex);
            SceneLoader.instance.GotoMainField();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("cuttoonE"))
        {
            Cuttoon();
            ChatWin.SetActive(false);
            Invoke("ChatEnd", 2f);
            Invoke("QuestEnd", 2f);
        } //컷툰 보이기
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
        else if (data_Dialog[j]["scriptType"].ToString().Equals("Panorama"))
        {
            c = 0;
            ChatWin.SetActive(false);
            InvokeRepeating("Panorama", 0f, 2f);
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("Panoramas"))
        {
            c = 0;
            ChatWin.SetActive(false);
            InvokeRepeating("Panorama", 0f, 1f);
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
            ChatWin.SetActive(false); 
            SoundManager = GameObject.Find("SoundManager");
            SoundManager.SetActive(false);
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("videoEnd")) //동영상 실행 중지       영상에 몇초 뒤 버튼을 추가시켜 그걸 누르면 확인창으로 넘어가게끔
        {
            if (videocheckTxT.text.Equals(parentscheckTxTNum))
            {
                GameObject.Find("checkUI").SetActive(false);
                videocheckTxT.text = null;
                movie.SetActive(false);
                video.OnFinishVideo();
                ChatWin.SetActive(true);
                SoundManager.SetActive(true);
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
            ChatWin.SetActive(false);
            Bike.SetActive(true);
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("Bicycle"))
        {
            if (!BikeQ)
            {
                NPCBike = new Vector3(-4, 0, 0);
                bicycleRide.RideOn();
                Destroy(GameObject.Find("Qbicycle(Clone)"));
                Player.position = new Vector3(36, 5, -23);
                Player.rotation = Quaternion.Euler(0, 90, 0);
                QBikeSpeed = 3;
                Maxtime = 8;
                BikeQ = true;
                StartCoroutine("QBikeLoop");
                NPCJumpAnimator.SetBool("BikeWalk", true);
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
                JumpButtons.Playerrb.velocity = JumpButtons.Playerrb.velocity.normalized * 0;
                StopCoroutine("QBikeLoop");
                Ride.SetActive(true);
            }
            else if (BikeQ)
            {
                
                //페이드 인 페이드 아웃하면서 화면에 한시간 후... 띄우기
                QBikeSpeed = 12;
                Maxtime = 3;
                Player.position = new Vector3(36, 5, -23);
                BikeNPC.transform.position = Player.position + new Vector3(10, 0, 10);
                Player.rotation = Player.rotation = Quaternion.Euler(0, 90, 0);
                bikerotate = false;
                timer = 0;
                NPCJumpAnimator.SetBool("BikeWalk", false);
            }
            
            scriptLine();

        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("hair"))
        {
            ChatWin.SetActive(false);
            hairFX(GameObject.Find("Player"));
            j++;
            Invoke("clearHair", 1f);
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("note"))        //퀘스트중간애들
        {
            if (data_Dialog[j]["cuttoon"].ToString().Equals("0"))
            {
                j++;
                ChatWin.SetActive(false);
                Note.SetActive(true);
            }
            else
            {
                j++;
                ChatWin.SetActive(false);
            }
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("noteFinish"))        //퀘스트중간애들
        {
            Note.transform.Find("Button").gameObject.SetActive(false);
            Draw.FinishWrite();
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("noteEnd"))
        {
            Note.SetActive(false);
            //ChatWin.SetActive(true);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("nutrient"))        //퀘스트중간애들
        {
            cuttoon.SetActive(false);
            j++;
            ChatWin.SetActive(false);
            nutrient.SetActive(true);
            Debug.Log("nut");
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("nutrientEnd"))
        {
            j++;
            nutrient.SetActive(false);
            Cuttoon();
            ChatWin.SetActive(false);
            Invoke("scriptLine", 2f);   //딜레이 후 스크립트 띄움
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("train"))
        {
            ChatWin.SetActive(false);
            Value.SetActive(true);
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("trainEnd"))
        {
            Value.SetActive(false);
            scriptLine();

        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("draw"))
        {
            ChatWin.SetActive(false);
            Draw.ChangeDrawCamera();
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("drawFinish"))
        {
            GameObject.Find("DrawUI").transform.Find("Button").gameObject.SetActive(false);
            Draw.Draw = false;
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("Screenshot"))
        {
            screenShot.SetActive(true);
            screenShotExit.SetActive(true);
            ChatWin.SetActive(false);
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("drawEnd"))
        {
            screenShot.SetActive(false);
            Draw.ChangeDrawCamera();
            Main_UI.SetActive(false);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("listen"))
        {
            ChatWin.SetActive(false);
            c = 0;
            InvokeRepeating("QSound", 0f, 4f);
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("song"))
        {
            //웃어봐 송 틀기
            SoundManager SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            SoundManager.Sound("HaHasong");
            Invoke("scriptLine", 20f);   //딜레이 후 스크립트 띄움
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("songend"))
        {
            //원래 노래로 바꾸기
            SoundManager SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            SoundManager.Sound("BGMField");
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("JumpRope"))
        {
            //줄넘기
            if (data_Dialog[j]["cuttoon"].ToString().Equals("0"))
            {
                GameObject.Find("Himchan").transform.rotation = Quaternion.Euler(0, 180, 0);
                PlayerRope.SetActive(true);
                NPCJumpAnimator.SetBool("JumpRope", true);
                NPCJumpAnimatorRope.SetBool("JumpRope", true);
            }
            else if (data_Dialog[j]["cuttoon"].ToString().Equals("1"))
            {
                Player.position = new Vector3(54, 5, -14);
                Player.rotation = Quaternion.Euler(0, 180, 0);
                JumpAnimator.SetBool("JumpRope", true);
                JumpAnimatorRope.SetBool("JumpRope", true);
                JumpAnimator.speed = 0.3f;
                JumpAnimatorRope.speed = 0.3f;
            }
            else if (data_Dialog[j]["cuttoon"].ToString().Equals("2"))
            {
                JumpAnimator.speed = 1f; 
                JumpAnimatorRope.speed = 1f;
            }
            else if (data_Dialog[j]["cuttoon"].ToString().Equals("3"))
            {
                NPCJumpAnimator.SetBool("JumpRopeSide", true);
                NPCJumpAnimatorRope.SetBool("JumpRopeSide", true);
            }
            else if (data_Dialog[j]["cuttoon"].ToString().Equals("4"))
            {
                NPCRope.transform.rotation = Quaternion.Euler(new Vector3(-180, 180, -180));
                NPCJumpAnimator.SetBool("JumpRope", true);
                NPCJumpAnimatorRope.SetBool("JumpRope", true);
            }
            else if (data_Dialog[j]["cuttoon"].ToString().Equals("5"))
            {
                NPCRope.transform.rotation = Quaternion.Euler(-180, 0, -180);
                NPCJumpAnimator.SetBool("JumpRope", true);
                NPCJumpAnimatorRope.SetBool("JumpRope", true);
                NPCJumpAnimatorRope.speed = 2f;
            }
            else if (data_Dialog[j]["cuttoon"].ToString().Equals("6"))
            {
                PlayerRope.SetActive(false);
                NPCRope.SetActive(false);
            }
            else if (data_Dialog[j]["cuttoon"].ToString().Equals("7"))
            {
                PlayerRope.SetActive(true);

                GameObject NPC = GameObject.Find(Inter.NameNPC);

                Player.transform.position = new Vector3(54, 5, -15);
                NPC.transform.position = Player.transform.position+ new Vector3(5, 0, 0);
                NPCRope.SetActive(true);

                Vector3 targetPositionNPC;
                Vector3 targetPositionPlayer;
                targetPositionNPC = new Vector3(Player.transform.position.x, NPC.transform.position.y, Player.transform.position.z);
                NPC.transform.LookAt(targetPositionNPC);
                targetPositionPlayer = new Vector3(NPC.transform.position.x, Player.transform.position.y, NPC.transform.position.z);
                Player.transform.LookAt(targetPositionPlayer);
            }
            Invoke("scriptLine", 2f);
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("JumpRopeEnd"))
        {
            //줄넘기
            NPCJumpAnimator.SetBool("JumpRope", false);
            NPCJumpAnimator.SetBool("JumpRopeSide", false);
            NPCJumpAnimator.SetBool("JumpRopeBack", false);
            NPCJumpAnimatorRope.SetBool("JumpRope", false);
            NPCJumpAnimatorRope.SetBool("JumpRopeSide", false);
            NPCJumpAnimatorRope.SetBool("JumpRopeBack", false);
            JumpAnimator.SetBool("JumpRope", false);
            JumpAnimator.SetBool("JumpRopeSide", false);
            JumpAnimator.SetBool("JumpRopeBack", false);
            JumpAnimatorRope.SetBool("JumpRope", false);
            JumpAnimatorRope.SetBool("JumpRopeSide", false);
            JumpAnimatorRope.SetBool("JumpRopeBack", false);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("MasterOfMtLife"))
        {
            //나에게 편지쓰기
            MasterOfMtLife.SetActive(true);
            ChatWin.SetActive(false);
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("MasterOfMtLifeEnd"))
        {
            MasterOfMtLife.SetActive(false);
            ChatWin.SetActive(true);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("LoveNature"))
        {
            LoveNature.SetActive(true);
            ChatWin.SetActive(false);
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("LoveNatureEnd"))
        {
            LoveNature.SetActive(false);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("AppleTree"))
        {
            AppleTree.SetActive(true);
            ChatWin.SetActive(false);
            //scriptLine();
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("AppleTreeEnd"))
        {
            ChatWin.SetActive(true);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("MakeAppleTree"))
        {
            AppleTree.SetActive(false);
            AppleTreeObj.SetActive(true);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("KeyToDream"))
        {
            KeyToDream.SetActive(true);
            ChatWin.SetActive(false);
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("KeyToDreamEnd"))
        {
            KeyToDreamInput.text = null;
            KeyToDream.SetActive(false);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("Tooth"))
        {
            if (Int32.Parse(data_Dialog[j]["cuttoon"].ToString())==0)
            {
                DontDestroy.QuestIndex = "22";
                SceneLoader.instance.GotoToothGame();
            }
            else if (SceneManager.GetActiveScene().name == "Game_Tooth")
            {
                ChatWin.SetActive(false);
                GameObject QToothBrush = GameObject.Find("QToothBrush(Clone)");
                GameObject Maincam = GameObject.Find("Main Camera");
                if (Int32.Parse(data_Dialog[j]["cuttoon"].ToString()) == 1)
                {
                    mouth = GameObject.Find("mouth");
                    mouth.SetActive(false);
                    Instantiate(Resources.Load<GameObject>("Prefabs/Tooth/QTooth/AH"), new Vector3(0, -13, 17), Quaternion.Euler(new Vector3(0, 90, 0)));
                    QToothBrush.transform.position = new Vector3(15, 8, 10);
                    QToothBrush.transform.rotation = Quaternion.Euler(new Vector3(1, 294, 272));
                    Maincam.transform.position = new Vector3(26, 25, 0);
                    Maincam.transform.rotation = Quaternion.Euler(new Vector3(36, 332, 356));


                    ToothAnimator.SetBool("1", true);
                }
                else if (Int32.Parse(data_Dialog[j]["cuttoon"].ToString()) == 2)
                {
                    mouth.SetActive(true);
                    GameObject.Find("AH(Clone)").SetActive(false);
                    QToothBrush.transform.position = new Vector3(-13, -10, 18);
                    QToothBrush.transform.rotation = Quaternion.Euler(new Vector3(52, 176, 89));
                    Maincam.transform.position = new Vector3(2, 6, 29);
                    Maincam.transform.rotation = Quaternion.Euler(new Vector3(63, 180, 0));
                    ToothAnimator.SetBool("1", true);
                }
                else if (Int32.Parse(data_Dialog[j]["cuttoon"].ToString()) == 3)
                {
                    QToothBrush.transform.position = new Vector3(16, -6, 15);
                    QToothBrush.transform.rotation = Quaternion.Euler(new Vector3(63, -9, 347));
                    Maincam.transform.position = new Vector3(15, 4, 4);
                    Maincam.transform.rotation = Quaternion.Euler(new Vector3(16, 0, 0));
                    ToothAnimator.SetBool("2", true);
                    ToothAnimator.SetBool("1", false);
                }
                else if (Int32.Parse(data_Dialog[j]["cuttoon"].ToString()) == 4)
                {
                    QToothBrush.transform.position = new Vector3(20, 0, 26);
                    QToothBrush.transform.rotation = Quaternion.Euler(new Vector3(63, -3, 84));
                    Maincam.transform.position = new Vector3(0, 5, 2);
                    Maincam.transform.rotation = Quaternion.Euler(new Vector3(20, 0, 0));
                    ToothAnimator.SetBool("1", true);
                    ToothAnimator.SetBool("2", false);
                }
                else if (Int32.Parse(data_Dialog[j]["cuttoon"].ToString()) == 5)
                {
                    QToothBrush.transform.position = new Vector3(13.3f, -11, 19.7f);
                    QToothBrush.transform.rotation = Quaternion.Euler(new Vector3(350, 299, 4));
                    Maincam.transform.position = new Vector3(11.5f, -3, -1);
                    GameObject.Find("QToothBrush(Clone)").transform.Find("Dentalfloss").gameObject.SetActive(true);
                    GameObject.Find("ToothBrush").SetActive(false);
                    ToothAnimator.SetBool("finish", true);
                    ToothAnimator.SetBool("2", false);
                }
                else if (Int32.Parse(data_Dialog[j]["cuttoon"].ToString()) == 6)
                {
                    QToothBrush.SetActive(false);
                    Maincam.transform.position = new Vector3(0, 16.6f, -27);
                    Maincam.transform.rotation = Quaternion.Euler(new Vector3(7.7f, 0, 0));
                    scriptLine();
                    return;
                }
                else if (Int32.Parse(data_Dialog[j]["cuttoon"].ToString()) == 7)
                {
                    DontDestroy.ToothQ = true;
                    DontDestroy.QuestIndex = "22_1";
                    PlayerPrefs.SetString("WeeklyQuestPreg", DontDestroy.QuestIndex); //주말
                    SceneLoader.instance.GotoMainField();
                    return;
                }

                Invoke("scriptLine", 3f);   //딜레이 후 스크립트 띄움
            }
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("LookAt"))
        {
            Transform NPC = GameObject.Find(Inter.NameNPC).transform;
            Vector3 targetPositionNPC;
            targetPositionNPC = new Vector3(Player.transform.position.x, NPC.position.y, Player.transform.position.z);
            StartCoroutine(JumpButtons.NPCturn(NPC, targetPositionNPC));
            if (Quest.note)
            {
                    Quest.note = false;
                    GameObject[] objs = GameObject.FindGameObjectsWithTag("note");
                    for (int i = 0; i < objs.Length; i++)
                        Destroy(objs[i]);
            }
            else if(data_Dialog[j]["scriptNumber"].ToString().Equals("19_1"))
            {
                Transform NPC2 = GameObject.Find("Nari").transform;
                StartCoroutine(JumpButtons.NPCturn(NPC2, targetPositionNPC));
            }
            Invoke("stopCorou", 1f);
               //딜레이 후 스크립트 띄움
        }
    }
    void stopCorou()
    {
        JumpButtons.Nstop = false;
        scriptLine();
    }
    void stopCorout()
    {
        JumpButtons.Nstop = false;
    }

    public void QSound()
    {
        string SoundName=null;
        if (c == 0)
        {
            SoundName = "QWind";
        }
        else if (c == 1)
        {
            SoundName = "QWater";
        }
        else if (c == 2)
        {
            SoundName = "QBird";
        }
        else
        {
            Invoke("scriptLine", 4f);
            CancelInvoke("QSound");
            return;
        }
        SoundEffectManager.GetComponent<SoundEffect>().Sound(SoundName);
        c++;
    }
    public void TQ()
    {
        GameObject ToothBrush = GameObject.Find("ToothBrush");
        GameObject QToothBrush = GameObject.Find("QToothBrush(Clone)");
        if (c == 0)
        {
            QToothBrush.transform.position = new Vector3(19, 1, 19);
        }
    }
    public void Line()  //줄넘김 (scriptType이 뭔지 걸러냄)
    {
        if (data_Dialog[j]["SoundEffect"].ToString().Equals("Null"))
        { }
        else
        {
            string SoundName = data_Dialog[j]["SoundEffect"].ToString();
            SoundEffectManager.GetComponent<SoundEffect>().Sound(SoundName);
        }
        block.SetActive(true);
        //Debug.Log(data_Dialog[j]["scriptNumber"].ToString());
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
            if (data_Dialog[j]["name"].ToString().Equals("end"))
            {
                QuestEnd();
            }
            ChatEnd();
            
        }
        else
        {
            if (!data_Dialog[j]["scriptType"].ToString().Equals("nomal"))
            {
                //Debug.Log(data_Dialog[j]["scriptType"].ToString());
                QuestSubChoice(); 
            }
            else
            {
                cuttoon.SetActive(false);
                scriptLine();
            }
            if (data_Dialog[j]["scriptNumber"].ToString().Equals("0_1"))
                Main_UI.SetActive(false);

        }
        //if (move)
        //    changeMoment();
    }

    
    public void scriptLine()  //스크립트 띄우는 거 (어굴 이미지+ 이름+ 뜨는 텍스트)
    {
        spriteR = CCImage.GetComponent<Image>();
        l = Int32.Parse(data_Dialog[j]["image"].ToString());
        if (l == 9)
            CCImage.SetActive(false);
        else
        {
            CCImage.SetActive(true);
            spriteR.sprite = CCImageList[l];
        }

        LoadTxt = data_Dialog[j]["dialog"].ToString().Replace("P_name",PlayerName);//로컬값 가져오긴
        LoadTxt = LoadTxt.Replace("<n>", "\n");
        if (data_Dialog[j]["name"].ToString().Equals("주인공"))
            chatName.text = PlayerName;
        else
            chatName.text = data_Dialog[j]["name"].ToString();
        
        StartCoroutine(_typing());
        Arrow.SetActive(false);
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
        Input.multiTouchEnabled = true;
        chatCanvus.SetActive(false);
        ChatWin.SetActive(false);
        Arrow.SetActive(false);
        NPCButtons.SetActive(false);
        chatName.text = " ";
        Main_UI.SetActive(true);
        c = 0;
        //PlayerCamera.SetActive(false);
        NPCButton = 0;
        ButtonsFalse();
        if (Inter.NameNPC.Equals("WallMirror") || Inter.NameNPC.Equals("GachaMachine")|| Inter.NameNPC.Equals("ThankApplesTree"))
        { stopCorou(); }
        else if (DontDestroy.QuestIndex.Equals("8_1") && Inter.NameNPC.Equals("Mei"))
        { stopCorou(); }
        else if (DontDestroy.QuestIndex.Equals("13_1") && Inter.NameNPC.Equals("Suho"))
        { stopCorou(); }
        else
        {
            Vector3 targetPositionNPC;
            targetPositionNPC = new Vector3(JumpButtons.Ntransform.transform.position.x, JumpButtons.NPC.position.y, JumpButtons.Ntransform.transform.position.z - 1);

            StartCoroutine(JumpButtons.NPCturn(JumpButtons.NPC, targetPositionNPC));
            Invoke("stopCorout",1f);
        }
    }


    public void Buttons()      //npc대화 상호작용 선택지 수
    {
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
    public void ButtonsFalse()      //npc대화 상호작용 선택지 수
    {
        NPCButtons.SetActive(false);
        for (int i= 0; i < SelecButton.Length; i++)
        {
            string selecNumber = "select"+(i+1).ToString();
            SelecButton[i].SetActive(false);
        }
    }

    public void QuizMate() //전광판 메테리얼 설정
    {
        Quiz_material[1] = material[MataNum]; //0에 메테리얼 번호
        Quiz.GetComponent<MeshRenderer>().materials = Quiz_material;

    }

    public bool typingSkip = true;
    public void typingSkip_()
    {
        if (chatTxt.text.Length > 3)
        {
            typingSkip = false;
        }
    }
    IEnumerator _typing()  //타이핑 효과
    {
        typingSkip = true;
        if (!ChatWin.activeSelf)
            ChatWin.SetActive(true);

        chatTxt.text = " ";
        yield return new WaitForSecondsRealtime(0.1f);
        for (int i = 0; i < LoadTxt.Length + 1; i++)
        {
            if (typingSkip)
            {
                chatTxt.text = LoadTxt.Substring(0, i);
                yield return new WaitForSecondsRealtime(0.03f);
            }
            else
                break;
        }
        chatTxt.text = LoadTxt;

        yield return new WaitForSecondsRealtime(0.2f);
        Arrow.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        if (data_Dialog[j - 1]["scriptType"].ToString().Equals("tutorial") || tuto)
        {
            Debug.Log("튜토리얼 실행ㅇ");
            Invoke("Tutorial_", 1f);
        }
        else
        {
            block.SetActive(false);
        }
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

    private IEnumerator reload()
    {
        yield return new WaitForEndOfFrame();
        SceneLoader.instance.GotoMainField();
    }
    public void QuestEnd()
    {
        if (SceneManager.GetActiveScene().name == "Quiz") ;
        else
            Save_Log.instance.SaveQEndLog();    //퀘스트 종료 로그 기록

        DontDestroy.ButtonPlusNpc = "";
        //Quest.Load.QuestMail = false;

        if (DontDestroy.weekend)
            PlayerPrefs.SetString("WeeklyQuestPreg", DontDestroy.QuestIndex); //주말
        else
            PlayerPrefs.SetString("QuestPreg", DontDestroy.QuestIndex);


        if (data_Dialog[j]["dialog"].ToString().Equals("end"))
        {
            PlayerPrefs.SetInt("LastQTime", DontDestroy.ToDay);
            NpcButton.Chat.EPin.SetActive(false);
            DontDestroy.LastDay = DontDestroy.ToDay;
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "MainField")
                QuestLoad.QuestLoadStart();
        }

        PlayInfoManager.GetQuestPreg();
    }

    public void ParentsCheck()
    {
        if (ParentscheckTxt.text.Equals(parentscheckTxTNum))
        {
            if (DontDestroy.weekend)
                PlayerPrefs.SetString("WeeklyQuestPreg", DontDestroy.QuestIndex); //주말
            else
                PlayerPrefs.SetString("QuestPreg", DontDestroy.QuestIndex);
            PlayerPrefs.SetInt("LastQTime", DontDestroy.ToDay);
            DontDestroy.LastDay = DontDestroy.ToDay;
            DontDestroy.From = " ";
            ParentscheckUI.SetActive(false);
            ParentscheckTxt.text = null;
            DontDestroy.ButtonPlusNpc = "";
            PlayInfoManager.GetQuestPreg();
            NpcButton.CheckQuest();
            ClearWin.SetActive(true);
            NpcButton.Chat.EPin.SetActive(false);
        }
        else
        {
            ParentsErrorWin.SetActive(true);
        }
    }
    public void hairFX(GameObject go)    //머리 반짝!하는 파티클
    {
        Debug.Log("반짝");
        ParticleSystem newfx = Instantiate(hairPs);
        newfx.transform.position = go.transform.position+new Vector3(0,5,0);
        newfx.transform.SetParent(go.transform);

        newfx.Play();
    }

    
    void clearHair()
    {
        Chat.SetActive(true);
        scriptLine();
    }

    void Panorama()
    {
        cuttoon.SetActive(true);
        cuttoonspriteR = cuttoon.GetComponent<Image>();
        cuttoonspriteR.sprite = cuttoonImageList[c];
        Debug.Log("c=" + c);
        if(c == Int32.Parse(data_Dialog[j]["cuttoon"].ToString()))
        {
            CancelInvoke("Panorama");
            Invoke("scriptLine", 2f);
        }
        c++;
    }

}

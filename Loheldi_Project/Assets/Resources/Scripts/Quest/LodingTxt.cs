using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;
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

    public InputField videocheckTxT;
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
    public GameObject block;        //�ѱ���� �Ǿ� ��
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
    public GameObject nutrient;

    public GameObject Bike;
    public GameObject BikeNPC;

    public GameObject AppleTreeObj;

    public int NPCButton = 0;
    public string LoadTxt;
    private string[] Xdialog = {"�ٽ� �ѹ� �����غ�.","�ƽ����� Ʋ�Ⱦ�.","��!","�ٽ� �����غ�"};

    public List<Dictionary<string, object>> data_Dialog = new List<Dictionary<string, object>>();
    public string FileAdress;                // ��ũ��Ʈ ���� ��ġ
    public string cuttoonFileAdress;         // ���� ���� ��ġ

    public string Num;                       //��ũ��Ʈ ��ȣ
    public int j;                                  //data_Dialog �ٰ���
    public int c = 0;                              //���� �̹��� ��ȣ
    public int l;                            //�ߴ� �̹��� ��ȣ
    string Answer;               //���� ��ư �ν�

    public bool tutoEnd = false;  //Ʃ�丮�� ���� ��
    public bool tutoFinish = false;
    public bool tuto;
    public bool move = false; //ĳ���� �����̵�
    public static GameObject CCImage;     //ĳ���� �̹���
    public static Sprite[] CCImageList;
    static Image spriteR;

    public GameObject cuttoon;        //���� �̹���
    public Sprite[] cuttoonImageList;
    static Image cuttoonspriteR;

    public TMP_InputField KeyToDreamInput;
    //public Fadeln fade_in_out;
    public UIButton JumpButtons;
    tutorial tu;
    public Interaction Inter;



    int m = 0;                                  //ī�޶� ����
    int o = 0;                                  //m������
    int MataNum = 0;                        //���͸��� ��ȣ
    int QBikeSpeed;
    bool BikeQ = false;
    float timer=0.0f;
    float Maxtime;
    bool bikerotate = false;
    Vector3 NPCBike;

    public QuestDontDestroy DontDestroy;
    private QuestScript Quest;
    public VideoScript video;
    public Drawing Draw;
    public BicycleRide bicycleRide;
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
        DontDestroy = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        if (SceneManager.GetActiveScene().name == "MainField")     //���� �ʵ忡 ���� ���� ���
        {
            Draw = GameObject.Find("QuestManager").GetComponent<Drawing>();
            video = GameObject.Find("QuestManager").GetComponent<VideoScript>();
            JumpButtons = GameObject.Find("EventSystem").GetComponent<UIButton>();
            tu = GameObject.Find("chatManager").GetComponent<tutorial>();
            Inter = GameObject.Find("Player").GetComponent<Interaction>();
            Quest = GameObject.Find("chatManager").GetComponent<QuestScript>();

            if (DontDestroy.weekend)
                DontDestroy.QuestIndex = PlayerPrefs.GetString("QuestPreg");  //�ָ�
            else
                DontDestroy.QuestIndex = PlayerPrefs.GetString("QuestPreg");  //���ð� ��������
            DontDestroy.LastDay = PlayerPrefs.GetInt("LastQTime");
        }
        
        if (SceneManager.GetActiveScene().name == "Quiz")
            Quiz_material = Quiz.GetComponent<MeshRenderer>().materials;
        color = block.GetComponent<Image>().color;
        ChatWin.SetActive(true);
        //if (SceneManager.GetActiveScene().name == "MainField")     //���� �ʵ忡 ���� ���� ���

        //fade_in_out = GameObject.Find("EventSystem").GetComponent<Fadeln>();
        CCImage = GameObject.Find("CCImage"); //�̹��� ��� ��
        Debug.Log("�̹���=" + CCImage);
        CCImageList = Resources.LoadAll<Sprite>("Sprites/CCImage/"); //�̹��� ���

        cuttoon = GameObject.Find("chatUI").transform.Find("Cuttoon").gameObject;
        cuttoon.SetActive(false);
        ChatWin.SetActive(false);
        QuizeWin.SetActive(false);

        parentscheckTxTNum = PlayerPrefs.GetString("ParentsNo");
        PlayerName = PlayerPrefs.GetString("Nickname");

        Quest.QuestStart();
    }

    void FixedUpdate()
    {
        
        if (BikeQ)
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
            JumpButtons.Playerrb.AddRelativeForce(Vector3.forward * 1000f); //�� �������� �б� (���� * ��)
        }
    }

    public void skip()
    {
        j = 93;
        o = 11;
        //GameObject SoundManager = GameObject.Find("SoundManager");
        //SoundManager.GetComponent<SoundManager>().Sound("BGMField");
        scriptLine();
    }
    public void NewChat()
    {
        //Debug.Log("����3");
        Txt = chatTxt;
        Name = chatName;
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
                //Debug.Log("����4");
                break;
            }
            else
            {
                continue;
            }
        }
        Main_UI.SetActive(false);
        
    }

    public void changeMoment()  //�÷��̾� �̵�, ī�޶� ����
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
                QuestEnd();
                SceneLoader.instance.GotoMainField();
                break;
            default:
                break;
        }
    } 

    public void QuestSubChoice()
    {
        if (data_Dialog[j]["scriptType"].ToString().Equals("quiz"))  //�������
        {
            MataNum = Int32.Parse(data_Dialog[j]["QuizNumber"].ToString());
            scriptLine();
            for (int i = 0; i < QuizButton.Length; i++)
            {
                if (data_Dialog[j]["select" + (i + 1)].ToString().Equals("��ĭ"))
                    QuizButton[i].transform.parent.gameObject.SetActive(false);
                else
                    QuizButton[i].transform.parent.gameObject.SetActive(true);
                QuizButton[i].text = data_Dialog[j]["select" + (i + 1)].ToString();
            }
            QuizMate();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("choice"))  //������
        {
            j--;
            QuizCho();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("Move"))  //������
        {
            if (DontDestroy.tutorialLoading)
                o = 3;
            else
                o += 1;
            changeMoment();
            j +=1;
            Line();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("over"))  //main����
        {
            DontDestroy.QuestIndex = data_Dialog[j + 1]["scriptNumber"].ToString();
            PlayerPrefs.SetString("QuestPreg", DontDestroy.QuestIndex);
            SceneLoader.instance.GotoMainField();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("cuttoon"))
        {
            Cuttoon();
            ChatWin.SetActive(false);
            Invoke("scriptLine", 2f);   //������ �� ��ũ��Ʈ ���
        } //���� ���̱�
        else if (data_Dialog[j]["scriptType"].ToString().Equals("Dcuttoon"))
        {
            scriptLine();
        } //���� ���̱�
        else if (data_Dialog[j]["scriptType"].ToString().Equals("tutorial"))//Ʃ�丮��
        { 
            scriptLine(); 
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("video"))//������ ����
        {
            video.videoClip.clip = video.VideoClip[Int32.Parse(data_Dialog[j]["cuttoon"].ToString())];
            movie.SetActive(true);
            video.OnPlayVideo();
            ChatWin.SetActive(false); 
            GameObject SoundManager = GameObject.Find("SoundManager");
            SoundManager.GetComponent<AudioSource>().volume = 0f;
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("videoEnd")) //������ ���� ����       ���� ���� �� ��ư�� �߰����� �װ� ������ Ȯ��â���� �Ѿ�Բ�
        {
            if (videocheckTxT.text.Equals(parentscheckTxTNum))
            {
                GameObject.Find("checkUI").SetActive(false);
                videocheckTxT.text = null;
                movie.SetActive(false);
                video.OnFinishVideo();
                ChatWin.SetActive(true);
                GameObject SoundManager = GameObject.Find("SoundManager");
                SoundManager.GetComponent<AudioSource>().volume = 1f;
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
                BikeNPC = GameObject.Find(Inter.NameNPC);
                Destroy(GameObject.Find("Qbicycle(Clone)"));
                Player.position = new Vector3(36, 5, -23);
                Player.rotation = Quaternion.Euler(0, 90, 0);
                QBikeSpeed = 3;
                Maxtime = 8;
                BikeQ = true;
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

                Main_UI.transform.Find("Ride").gameObject.SetActive(true);
            }
            else if (BikeQ)
            {
                
                //���̵� �� ���̵� �ƿ��ϸ鼭 ȭ�鿡 �ѽð� ��... ����
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
        else if (data_Dialog[j]["scriptType"].ToString().Equals("note"))        //����Ʈ�߰��ֵ�
        {
            j++;
            ChatWin.SetActive(false);
            Note.SetActive(true);
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("noteEnd"))
        {
            Note.SetActive(false);
            //ChatWin.SetActive(true);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("nutrient"))        //����Ʈ�߰��ֵ�
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
            Invoke("scriptLine", 2f);
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
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("drawFinish"))
        {
            Draw.ForDraw = false;
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("Screenshot"))
        {
            screenShot.SetActive(true);
            ChatWin.SetActive(false);
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("drawEnd"))
        {
            Draw.ChangeDrawCamera();
            Invoke("scriptLine", 1f);   //������ �� ��ũ��Ʈ ���
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("song"))
        {
            //����� �� Ʋ��
            SoundManager SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            SoundManager.Sound("HaHasong");
            Invoke("scriptLine", 20f);   //������ �� ��ũ��Ʈ ���
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("songend"))
        {
            //���� �뷡�� �ٲٱ�
            SoundManager SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            SoundManager.Sound("BGMField");
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("JumpRope"))
        {
            //�ٳѱ�
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
            }
            Invoke("scriptLine", 2f);
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("JumpRopeEnd"))
        {
            //�ٳѱ�
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
            //������ ��������
            MasterOfMtLife.SetActive(true);
            ChatWin.SetActive(false);
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("MasterOfMtLifeEnd"))
        {
            MasterOfMtLife.SetActive(false);
            ChatWin.SetActive(true);
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
        
    }
    public void Line()  //�ٳѱ� (scriptType�� ���� �ɷ���)
    {
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
        if (data_Dialog[j]["scriptType"].ToString().Equals("end")) //��ȭ ��
        {
            ChatEnd();
            if (data_Dialog[j]["name"].ToString().Equals("end"))
            { QuestEnd(); }
        }
        else
        {
            if (!data_Dialog[j]["scriptType"].ToString().Equals("nomal"))
            {
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

    
    public void scriptLine()  //��ũ��Ʈ ���� �� (� �̹���+ �̸�+ �ߴ� �ؽ�Ʈ)
    {
        //block.SetActive(true);
        if (SceneManager.GetActiveScene().name == "MainField")
        {
            if (Quest.note)
            {
                if (data_Dialog[j]["scriptNumber"].ToString().Equals("8_2"))
                {
                    GameObject NPC = GameObject.Find(Inter.NameNPC);
                    Vector3 targetPositionNPC;
                    targetPositionNPC = new Vector3(Player.transform.position.x, NPC.transform.position.y, Player.transform.position.z);
                    NPC.transform.LookAt(targetPositionNPC);
                    Quest.note = false;
                    GameObject[] objs = GameObject.FindGameObjectsWithTag("note");
                    for (int i = 0; i < objs.Length; i++)
                        Destroy(objs[i]);

                }
            }
            spriteR = CCImage.GetComponent<Image>();
            l = Int32.Parse(data_Dialog[j]["image"].ToString());
            spriteR.sprite = CCImageList[l];
        }

        LoadTxt = data_Dialog[j]["dialog"].ToString().Replace("P_name",PlayerName); //���ð� ��������
        if (data_Dialog[j]["name"].ToString().Equals("���ΰ�"))
            Name.text = PlayerName;
        else
            Name.text = data_Dialog[j]["name"].ToString();
        
        StartCoroutine(_typing());
        Arrow.SetActive(false);
        j++;
    }

    public void Cuttoon()
    {
        c= Int32.Parse(data_Dialog[j]["cuttoon"].ToString());
        cuttoon.SetActive(true);
        //Debug.Log("��������=" + cuttoonImageList.Length);
        cuttoonspriteR = cuttoon.GetComponent<Image>();
        cuttoonspriteR.sprite = cuttoonImageList[c];
    }

    public void ChatEnd() //����
    {
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

    public void Buttons()      //npc��ȭ ��ȣ�ۿ� ������ ��
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

    public void QuizMate() //������ ���׸��� ����
    {
        Quiz_material[1] = material[MataNum]; //0�� ���׸��� ��ȣ
        Quiz.GetComponent<MeshRenderer>().materials = Quiz_material;

    }

    public void ChatTime() //���ۿ� ��ü��Ŵ
    {
        Txt = chatTxt;
        Name=chatName;
    }

    IEnumerator _typing()  //Ÿ���� ȿ��
    {
        if (!ChatWin.activeSelf)
            ChatWin.SetActive(true);

        Txt.text = " ";
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < LoadTxt.Length + 1; i++)
        {
            Txt.text = LoadTxt.Substring(0, i);
            yield return new WaitForSeconds(0.01f);
        }
        
        if (data_Dialog[j - 1]["scriptType"].ToString().Equals("tutorial") || tuto)
        {
            Debug.Log("Ʃ�丮�� ���P��");
            Invoke("Tutorial_", 2f);
        }
        else
            block.SetActive(false);
    }

    void QuizCho()
    {
        Chat.SetActive(false);
        Button.SetActive(true); //����Ƽ���� ��ư ��ġ �ű�
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

    public void O()//���� ����� ��
    {
        block.SetActive(true);
        OImage.SetActive(true); 
        Button.SetActive(false);
        j++;

    }
    public void X()//Ʋ�� ���� ����� ��
    {
        int x = UnityEngine.Random.Range(0,3);
        block.SetActive(true);
        XImage.SetActive(true);
        Button.SetActive(false);
        LoadTxt = Xdialog[x];
        StartCoroutine(_typing());

    }

    public void Xback()//X�̹��� ��ư
    {
        XImage.SetActive(false);
        Button.SetActive(true);
    }

    int Questbadge;
    private void QuestEnd()
    {
        DontDestroy.ButtonPlusNpc = "";
        Quest.Load.QuestMail = false;
        DontDestroy.QuestIndex = data_Dialog[j]["scriptNumber"].ToString();
        if (data_Dialog[j]["dialog"].ToString().Equals("end"))
        {
            PlayerPrefs.SetInt("LastQTime", DontDestroy.ToDay);
            DontDestroy.LastDay = DontDestroy.ToDay;
        }
        if (DontDestroy.weekend)
            PlayerPrefs.SetString("QuestPreg", DontDestroy.QuestIndex); //�ָ�
        else
            PlayerPrefs.SetString("QuestPreg", DontDestroy.QuestIndex);
        Debug.Log(PlayerPrefs.GetString("QuestPreg", DontDestroy.QuestIndex));
    }
    public void hairFX(GameObject go)    //�Ӹ� ��¦!�ϴ� ��ƼŬ
    {
        Debug.Log("��¦");
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

}

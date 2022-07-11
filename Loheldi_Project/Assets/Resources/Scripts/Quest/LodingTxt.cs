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

    public GameObject Bike;
    public GameObject BikeNPC;

    public int NPCButton = 0;
    public string LoadTxt;
    private string[] Xdialog = {"�ٽ� �ѹ� �����غ�.","�ƽ����� Ʋ�Ⱦ�.","��!","�ٽ� �����غ�"};

    public List<Dictionary<string, object>> data_Dialog = new List<Dictionary<string, object>>();
    public string FileAdress;                // ��ũ��Ʈ ���� ��ġ
    public string cuttoonFileAdress;         // ���� ���� ��ġ

    public string Num;                       //��ũ��Ʈ ��ȣ
    public int j;                                  //data_Dialog �ٰ���
    public int c = 0;                              //���� �̹��� ��ȣ
    //public static int k;                    //npc
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

    //public Fadeln fade_in_out;
    public UIButton JumpButtons;
    tutorial tu;
    public Interaction Inter;



    int m = 0;                                  //ī�޶� ����
    int o = 1;                                  //m������
    int MataNum = 0;                        //���͸��� ��ȣ
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
        //if (SceneManager.GetActiveScene().name == "MainField")     //���� �ʵ忡 ���� ���� ���

        //fade_in_out = GameObject.Find("EventSystem").GetComponent<Fadeln>();
        CCImage = GameObject.Find("CCImage"); //�̹��� ��� ��
        Debug.Log("�̹���=" + CCImage);
        CCImageList = Resources.LoadAll<Sprite>("Sprites/CCImage/"); //�̹��� ���

        cuttoon = GameObject.Find("chatUI").transform.Find("Cuttoon").gameObject;
        cuttoon.SetActive(false);
        ChatWin.SetActive(false);
        QuizeWin.SetActive(false);

        GameObject.Find("Player").transform.position = DontDestroy.LastPlayerTransform.transform.position;
        Debug.Log("�÷��̾� ��ġ ����" + DontDestroy.LastPlayerTransform.transform.position);
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
            JumpButtons.Playerrb.AddRelativeForce(Vector3.forward * 1000f); //�� �������� �б� (���� * ��)
            





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
        //Debug.Log("�̹��� ����Ʈ ����" + cuttoonImageList.Length);
        //Debug.Log("�̹��� ��������Ʈ ������Ʈ: " + CCImage.name);
        //Debug.Log("���� ���� �ּ�:"+ cuttoonFileAdress);
        //Debug.Log("Num=" + Num);
        /*        if (DontDestroy.QuestMail)    //�̰� ��� �ɵ� ���ϴ� ���� (�Ƹ� �����̵� �� �� ���̴� �� �ٵ� �̰� ��� �˾Ƽ� �� �ϴµ�..? �̰� �̹� 0���� �Ҵ� �صΰ� �����Ŵ. Ȥ�� �𸣴� ���ܵ־���)
                    DontDestroy.QuestSubNum = Int32.Parse(data_Dialog[j]["scriptNumber"].ToString().Substring(0, data_Dialog[j]["scriptNumber"].ToString().IndexOf("_"))); //���� ����Ʈ �ѹ��� �ڸ���*/
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

    public void changeMoment()  //�÷��̾� �̵�, ī�޶� ����
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
        if (data_Dialog[j]["scriptType"].ToString().Equals("quiz"))  //�������
        {
            MataNum = Int32.Parse(data_Dialog[j]["QuizNumber"].ToString());
            QuizTIme();
            scriptLine();
            for (int i = 0; i < QuizButton.Length; i++)
            {
                if (data_Dialog[j]["select" + (i + 1)].ToString().Equals("��ĭ"))
                    QuizButton[i].transform.parent.gameObject.SetActive(false);
                else
                    QuizButton[i].transform.parent.gameObject.SetActive(true);
                QuizButton[i].text = data_Dialog[j]["select" + (i + 1)].ToString();
                //string selecNumber = "select" + (i + 1).ToString();
            }
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("choice"))  //������
        {
            j--;
            /*for (int i = 0; i < QuizButton.Length; i++)
            {
                QuizButton[i].text = data_Dialog[j]["select" + (i + 1)].ToString();
                //string selecNumber = "select" + (i + 1).ToString();
            }*/
            QuizCho();
            //Button.SetActive(true); //����Ƽ���� ��ư ��ġ �ű�
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("over"))  //ī�޶� ���� ���󺹱ͷ� ����
        {
            ChatTime();
            scriptLine();
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
            Chat.SetActive(false);
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("videoEnd")) //������ ���� ����       ���� ���� �� ��ư�� �߰����� �װ� ������ Ȯ��â���� �Ѿ�Բ�
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
                //���̵� �� ���̵� �ƿ��ϸ鼭 ȭ�鿡 �ѽð� ��... ����
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
        else if (data_Dialog[j]["scriptType"].ToString().Equals("note"))        //����Ʈ�߰��ֵ�
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
            Invoke("scriptLine", 1f);   //������ �� ��ũ��Ʈ ���
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("song"))
        {
            //����� �� Ʋ��
            Invoke("scriptLine", 10f);   //������ �� ��ũ��Ʈ ���
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("songend"))
        {
            //���� �뷡�� �ٲٱ�
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("JumpRope"))
        {
            //�ٳѱ�
            NPCJumpAnimator.SetBool("JumpRope", true);
            Invoke("scriptLine", 2f);
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("JumpRopeEnd"))
        {
            //�ٳѱ�
            scriptLine();
            NPCJumpAnimator.SetBool("JumpRope", false);
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("MasterOfMtLife"))
        {
            //������ ��������
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
            //��� ���� ������ ¼��������
            Instantiate(Resources.Load<GameObject>("Prefabs/Q/Qbicycle"), new Vector3(65.1100006f, 5.41002083f, -17.799999f), Quaternion.Euler(0, 51.4773521f, 0));
            scriptLine();
        }
        
    }
    public void Line()  //�ٳѱ� (scriptType�� ���� �ɷ���)
    {
        //Ʃ�丮�� ��ũ��Ʈ �̾�� ��
        //Debug.Log("Ʃ��:"+tuto);
        //Debug.Log("Ʃ���Ǵ�:"+tutoFinish);
        //Debug.Log("����=" + move);
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

    
    public void scriptLine()  //��ũ��Ʈ ���� �� (� �̹���+ �̸�+ �ߴ� �ؽ�Ʈ)
    {
        spriteR = CCImage.GetComponent<Image>();
        l = Int32.Parse(data_Dialog[j]["image"].ToString());
        //Debug.Log("j=" + j);
        //Debug.Log("�̹�����ȣ=" + l);
        spriteR.sprite = CCImageList[l];

        if (ChatWin.activeSelf == false)
            ChatWin.SetActive(true);
        
        
        LoadTxt = data_Dialog[j]["dialog"].ToString().Replace("P_name",PlayerName); //���ð� ��������
        if (data_Dialog[j]["name"].ToString().Equals("���ΰ�"))
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
        Debug.Log("��������=" + cuttoonImageList.Length);
        cuttoonspriteR = cuttoon.GetComponent<Image>();
        cuttoonspriteR.sprite = cuttoonImageList[c];
    }

    public void ChatEnd() //����
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

    public void Buttons()      //npc��ȭ ��ȣ�ۿ� ������ ��
    {
        //Debug.Log("����NPC:" + Inter.NameNPC);
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

    public void QuizTIme() //ī�޶� ���� �������� ����
    {
        QuizMate();
        MainCamera.enabled = false;
        QuizCamera.enabled = true;
        Draw.Dcam.enabled = false;
    }
    public void QuizMate() //������ ���׸��� ����
    {
        Quiz_material[1] = material[MataNum]; //0�� ���׸��� ��ȣ
        Quiz.GetComponent<MeshRenderer>().materials = Quiz_material;
        //Debug.Log("����"+ material[MataNum]);

    }

    public void ChatTime() //���ۿ� ��ü��Ŵ
    {
        
        MainCamera.enabled = true;
        QuizCamera.enabled = false;
        //k = 0;
        Txt = chatTxt;
        Name=chatName;
        //ChatWin.SetActive(true);
        //QuizeWin.SetActive(false);
    }

    IEnumerator _typing()  //Ÿ���� ȿ��
    {
        
        Txt.text = " ";
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < LoadTxt.Length + 1; i++)
        {
            Txt.text = LoadTxt.Substring(0, i);
            yield return new WaitForSeconds(0.01f);
        }
        
        //Arrow.SetActive(true);

        /*if (data_Dialog[j-1]["scriptType"].ToString().Equals("choice"))  //������
        {
            j--;
            for (int i = 0;i<QuizButton.Length; i++)
            {
                QuizButton[i].text = data_Dialog[j]["select" + (i + 1)].ToString();
                //string selecNumber = "select" + (i + 1).ToString();
            }
            Invoke("QuizCho", 1f);
            //Button.SetActive(true); //����Ƽ���� ��ư ��ġ �ű�
        }*/


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
            //Line();
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
        DontDestroy.QuestIndex++;
        // Quest.Load.Quest = true;
        //�Ʒ��� ���° ����Ʈ������ ���� ��� ������ ��� �����ؾ��ϴ��� �س������Դϴ�!
        //���� Ǯ���� ������ list��ȣ�� �־�ΰ� ����â�� ���� for���� ������ setActive(false)�ϴ� ������
        //¥�׾��µ� Ȥ�� �̰� ���ʿ��ѰŸ� �������ּ���! �����صΰڽ��ϴ�.
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
    public void hairFX(GameObject go)    //�Ӹ� ��¦!�ϴ� ��ƼŬ
    {
        Debug.Log("��¦");
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

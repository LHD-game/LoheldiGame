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

    public int NPCButton = 0;
    public string LoadTxt;

    public List<Dictionary<string, object>> data_Dialog = new List<Dictionary<string, object>>();
    public string FileAdress;                // ��ũ��Ʈ ���� ��ġ
    public string cuttoonFileAdress;         // ���� ���� ��ġ

    public string Num;                       //��ũ��Ʈ ��ȣ
    public int j;                                  //data_Dialog �ٰ���
    public int c=0;                              //���� �̹��� ��ȣ
    //public static int k;                    //npc
    public int l;                            //�ߴ� �̹��� ��ȣ
    public int tutoi;                            //Ʃ�丮�� ���̶���Ʈ �̹�����
    string Answer;               //���� ��ư �ν�

    public bool tutoEnd=false;  //Ʃ�丮�� ���� ��
    public bool tutoFinish=false;
    public bool tuto=false;
    public bool move = false; //ĳ���� �����̵�
    public static GameObject CCImage;     //ĳ���� �̹���
    public static Sprite[] CCImageList;
    static Image spriteR;

    public GameObject cuttoon;        //���� �̹���
    public Sprite[] cuttoonImageList;
    static Image cuttoonspriteR;

    public Fadeln fade_in_out;
    public UIButton JumpButtons;
    tutorial tu;
    public Interaction Inter;

    int m;                                  //ī�޶� ����
    int o = 0;                                  //m������
    int MataNum = 0;                        //���͸��� ��ȣ

    public QuestDontDestroy DontDestroy;
    private QuestScript Quest;
    private VideoScript video;

    private void Awake()
    {
        video = GameObject.Find("QuestManager").GetComponent<VideoScript>();
        Quest = GameObject.Find("chatManager").GetComponent<QuestScript>();
        DontDestroy = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        //Quiz_material = Quiz.GetComponent<MeshRenderer>().materials;  //���߿� �ּ� Ǯ��� ��

        Inter = GameObject.Find("Player").GetComponent<Interaction>();
        color = block.GetComponent<Image>().color;
        cuttoon.SetActive(true);
        ChatWin.SetActive(true);
        QuizeWin.SetActive(true);
        //if (SceneManager.GetActiveScene().name == "MainField")     //���� �ʵ忡 ���� ���� ���
        JumpButtons = GameObject.Find("EventSystem").GetComponent<UIButton>();
        fade_in_out = GameObject.Find("EventSystem").GetComponent<Fadeln>();
        tu = GameObject.Find("chatManager").GetComponent<tutorial>();

        CCImage = GameObject.Find("CCImage"); //�̹��� ��� ��
                                              //Debug.Log("�̹���=" + CCImage);
        CCImageList = Resources.LoadAll<Sprite>("Sprites/CCImage/"); //�̹��� ���

        //cuttoon = GameObject.Find("Cutton");
        //cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/tutorial");
        cuttoon.SetActive(false);
        ChatWin.SetActive(false);
        QuizeWin.SetActive(false);
        
        //Debug.Log("�̹��� ����Ʈ ����"+CCImageList.Length);
        Debug.Log("�̹��� ��������Ʈ ������Ʈ: "+CCImage.name);

    }
    public void NewChat()
    {
        Debug.Log(FileAdress);
        Debug.Log("Num="+Num);
        if (DontDestroy.QuestMail)
            DontDestroy.QuestSubNum = Int32.Parse(data_Dialog[j]["scriptNumber"].ToString().Substring(0, data_Dialog[j]["scriptNumber"].ToString().IndexOf("-"))); //���� ����Ʈ �ѹ��� �ڸ���
        Debug.Log("�̰� �����ε�");
        //if (SceneManager.GetActiveScene().name == "MainField")
        Main_UI.SetActive(false);
        data_Dialog = CSVReader.Read(FileAdress);
        for (int k=0;k<= data_Dialog.Count;k++)
        {
            if (data_Dialog[k]["scriptNumber"].ToString().Equals(Num))
            {
                j = k;
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
        if (move)
        { //Debug.Log("o=" +o+ "m=" + m);
            if (o != m )
            {
                if ((o == 4 || o == 5 || o == 6 || o == 7 || o == 8 || o == 9 || o == 10 || o == 11 || o == 12) && (DontDestroy.QuestSubNum == 0))
                {
                    switch (o)
                    {
                        case 4:
                            Player.transform.position = new Vector3(-39.69f, 5.577f, -74.88f);
                            Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
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
                }
                m = o;
                o = Int32.Parse(data_Dialog[j]["scriptNumber"].ToString().Substring(data_Dialog[j]["scriptNumber"].ToString().IndexOf("-") + 1));
            }
        }
        
    }
    public void Line()  //�ٳѱ�
    {
        //Ʃ�丮�� ��ũ��Ʈ �̾�� ��
        //Debug.Log("Ʃ��:"+tuto);
        //Debug.Log("Ʃ���Ǵ�:"+tutoFinish);
        if (tuto && tutoFinish)
        {
            chatCanvus.SetActive(true);
            color.a = 0;
            block.GetComponent<Image>().color = color;
            tuto = false;
            tutoFinish = false;
            if (tutoi == 6)
            {
                j = 33;
                o++;
            }
            if (tutoi == 12)
            {
                Main_UI.SetActive(true);
                j = 80;
                tutoEnd = true;
            }
            tutoi = 0;
        }
        else if (tutoi == 5)
        {
            chatCanvus.SetActive(true);
            ++j;
        }

        if (data_Dialog[j]["scriptType"].ToString().Equals("end")) //��ȭ ��
        {
            //Debug.Log("end����");
            ChatEnd();
            if(data_Dialog[j]["name"].ToString().Equals("end"))
            QuestEnd();
        }
        else
        {
            if (data_Dialog[j]["scriptNumber"].ToString().Equals("0-1"))
                Main_UI.SetActive(false);
            spriteR = CCImage.GetComponent<Image>();     
            l = Int32.Parse(data_Dialog[j]["image"].ToString());
            //Debug.Log("�̹�����ȣ="+l);
            spriteR.sprite = CCImageList[l];
            //Debug.Log("��:" +data_Dialog[j]["cuttoon"].ToString());
            if (data_Dialog[j]["scriptType"].ToString().Equals("cuttoon"))
            {
                Cuttoon();
                ChatWin.SetActive(false);
                Invoke("scriptLine", 2f);   //������ �� ��ũ��Ʈ ���
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
                Invoke("scriptLine", 2f);   //������ �� ��ũ��Ʈ ���
            }
            else if (data_Dialog[j]["scriptType"].ToString().Equals("KeepC_Over"))
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
            else
            {
                //Debug.Log("�׳ɽ���");
                cuttoon.SetActive(false);
                scriptLine();
            }
        }
        /*if(data_Dialog[j-1]["scriptType"].ToString().Equals("movie"))
        {
            chatCanvus.SetActive(false);
            video.OnPlayVideo();
        }
        else if (data_Dialog[j - 1]["scriptType"].ToString().Equals("movieEnd"))
        {
            chatCanvus.SetActive(true);
            video.OnResetVideo();
            Invoke("scriptLine", 1f);   //������ �� ��ũ��Ʈ ���
        }*/

        if(data_Dialog[j-1]["scriptType"].ToString().Equals("note"))
        {
            chatCanvus.SetActive(false);
            Note.SetActive(true);
        }
        else if (data_Dialog[j - 1]["scriptType"].ToString().Equals("noteEnd"))
        {
            chatCanvus.SetActive(true);
            Note.SetActive(false);
            Invoke("scriptLine", 1f);   //������ �� ��ũ��Ʈ ���
        }

        if(data_Dialog[j-1]["scriptType"].ToString().Equals("i-message"))
        {
            chatCanvus.SetActive(false);
            IMessage.SetActive(true);
        }
        else if (data_Dialog[j - 1]["scriptType"].ToString().Equals("i-messageEnd"))
        {
            chatCanvus.SetActive(true);
            IMessage.SetActive(false);
            Invoke("scriptLine", 1f);   //������ �� ��ũ��Ʈ ���
        }

        if(data_Dialog[j-1]["scriptType"].ToString().Equals("train"))
        {
            chatCanvus.SetActive(false);
            Value.SetActive(true);
        }
        else if (data_Dialog[j - 1]["scriptType"].ToString().Equals("trainEnd"))
        {
            chatCanvus.SetActive(true);
            Value.SetActive(false);
            Invoke("scriptLine", 1f);   //������ �� ��ũ��Ʈ ���
        }

        if(data_Dialog[j-1]["scriptType"].ToString().Equals("draw"))
        {
            Value.SetActive(true);
        }
        else if (data_Dialog[j - 1]["scriptType"].ToString().Equals("drawFinish"))
        {
            Value.SetActive(false);
            Debug.Log("�ٱ׷���");
        }
        else if (data_Dialog[j - 1]["scriptType"].ToString().Equals("Screenshot"))
        {
            Value.SetActive(false);
            Debug.Log("���� �����ϴ°�");
        }
        else if (data_Dialog[j - 1]["scriptType"].ToString().Equals("drawEnd"))
        {
            Value.SetActive(false);
            Invoke("scriptLine", 1f);   //������ �� ��ũ��Ʈ ���
        }
    }

    
    public void scriptLine()  //��ũ��Ʈ ���� ��
    {
        if (ChatWin.activeSelf == false)
            ChatWin.SetActive(true);
        if (data_Dialog[j]["scriptType"].ToString().Equals("quiz"))  //�������
        {
            MataNum = Int32.Parse(data_Dialog[j]["QuizNumber"].ToString());
            QuizTIme();
            Debug.Log("j:" + j + "MataNum=" + MataNum);
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("over"))  //ī�޶� ���� ���󺹱ͷ� ����
        {
            ChatTime();
        }

        //Debug.Log("j=" + j);
        LoadTxt = data_Dialog[j]["dialog"].ToString();
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
        //Debug.Log("��������=" + cuttoonImageList.Length);
        cuttoonspriteR = cuttoon.GetComponent<Image>();
        cuttoonspriteR.sprite = cuttoonImageList[c];
    }

    public void ChatEnd() //����
    {
        //if (SceneManager.GetActiveScene().name == "MainField")
        JumpButtons.JumpButtons.SetActive(true);
        chatCanvus.SetActive(false);
        ChatWin.SetActive(false);
        QuizeWin.SetActive(false);
        Arrow.SetActive(false);
        NPCButtons.SetActive(false);
        Name.text = " ";
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
        /*for(int i = 0; i < ButtonPlusNpc.Length; i++)
        {
            if (Inter.NameNPC.Equals(DontDestroy.ButtonPlusNpc))
                NPCButton += 1;
            else
                continue;
        }*/

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
        //k = 1;
        //Txt = QuizTxt;
        //Name=QuizName;
        //ChatWin.SetActive(false);
        //QuizeWin.SetActive(true);
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

        if (data_Dialog[j-1]["scriptType"].ToString().Equals("choice"))  //������
        {
            j--;
            for (int i = 0;i<QuizButton.Length; i++)
            {
                QuizButton[i].text = data_Dialog[j]["select" + (i + 1)].ToString();
                //string selecNumber = "select" + (i + 1).ToString();
            }
            Invoke("QuizCho", 1f);
            //Button.SetActive(true); //����Ƽ���� ��ư ��ġ �ű�
        }

        block.SetActive(false);

        if (data_Dialog[j-1]["scriptType"].ToString().Equals("tutorial")||tuto)
        {
            changeMoment();
            //Debug.Log("Ʃ�丮�� ���P��");
            block.SetActive(true);

            Invoke("Tutorial_", 2f);
            
        }
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
    public void QuizeAnswer()
    {
        if (data_Dialog[j]["answer"].ToString().Equals(Answer))
        {
            Chat.SetActive(true);
            O();
            Line();
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
        block.SetActive(true);
        XImage.SetActive(true);
        Button.SetActive(false);
        LoadTxt = "�ٽ� �����غ���!";
        StartCoroutine(_typing());

    }

    public void Xback()//X�̹��� ��ư
    {
        XImage.SetActive(false);
        Button.SetActive(true);
    }
    private void QuestEnd()
    {
        if (DontDestroy.ButtonPlusNpc == Inter.NameNPC)
        {
            DontDestroy.ButtonPlusNpc = "";
            Quest.Load.QuestMail = false;
            Quest.Load.Quest = true;

        }
        /*for (int i = 0; i < ButtonPlusNpc.Length; i++)
        {
            if (ButtonPlusNpc == Inter.NameNPC)
            {
                ButtonPlusNpc = "";
                break;
            }
        }*/
        
    }
}

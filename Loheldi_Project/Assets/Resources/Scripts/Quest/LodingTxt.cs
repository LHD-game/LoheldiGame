using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

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
    public GameObject ChatWin;
    public GameObject QuizeWin;
    public GameObject chatCanvus;

    public int NPCButton = 0;
    public string LoadTxt;
    public string[] ButtonPlusNpc = new string[9]{"","","","","","","","",""};

    public List<Dictionary<string, object>> data_Dialog = new List<Dictionary<string, object>>();
    public string FileAdress;                // ��ũ��Ʈ ���� ��ġ
    public string cuttoonFileAdress;         // ���� ���� ��ġ

    public string Num;                       //��ũ��Ʈ ��ȣ
    public int j;                                  //data_Dialog �ٰ���
    public int c=0;                              //���� �̹��� ��ȣ
    private int h;                          //�̹��� ���� ��ȣ
    private int n;                          //�ߴ� �̹��� ��ȣ(��ũ��Ʈ ��)
    public static int k;                    //npc
    public int l;                            //�ߴ� �̹��� ��ȣ(�⺻ ��ȭ)
    public int tutoi;                            //Ʃ�丮�� ���̶���Ʈ �̹�����
    string Answer;               //���� ��ư �ν�

    public bool tutoEnd=false;  //Ʃ�丮�� ���� ��
    public bool tutoFinish=false;
    public bool tuto=false;
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

    private void Awake()
    {
        Quiz_material = Quiz.GetComponent<MeshRenderer>().materials;

        Inter = GameObject.Find("Player").GetComponent<Interaction>(); //�Ⱦ��°�
        color = block.GetComponent<Image>().color;
        cuttoon.SetActive(true);
        ChatWin.SetActive(true);
        QuizeWin.SetActive(true);
        //if (SceneManager.GetActiveScene().name == "MainField")     //���� �ʵ忡 ���� ���� ���
        JumpButtons = GameObject.Find("EventSystem").GetComponent<UIButton>();
        fade_in_out = GameObject.Find("EventSystem").GetComponent<Fadeln>();
        tu = GameObject.Find("chatManager").GetComponent<tutorial>();

        CCImage = GameObject.Find("CCImage"); //���� �±� ����
        Debug.Log("�̹���=" + CCImage);
        CCImageList = Resources.LoadAll<Sprite>("Sprites/CCImage/"); //�̹��� ���

        //cuttoon = GameObject.Find("Cutton");
        cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/tutorial");

        cuttoon.SetActive(false);
        ChatWin.SetActive(false);
        QuizeWin.SetActive(false);
        /*
        Debug.Log("�̹��� ����Ʈ ����"+CCImageList.Length);
        Debug.Log("�̹��� ��������Ʈ ������Ʈ: "+CCImage.Length);*/

    }
    public void NewChat()
    {
        //h = Int32.Parse(data_Dialog[j]["scriptNumber"].ToString()); //�̹��� ���� �� ����Ʈ ��ȣ
        //n = Int32.Parse(data_Dialog[j]["scriptNumber"].ToString()); //�̹��� ��ȣ(�⺻��ȭ)
        //if (SceneManager.GetActiveScene().name == "MainField")
        JumpButtons.JumpButtons.SetActive(false);
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
        //Debug.Log("o=" +o+ "m=" + m);
        if(o!=m)
        {
            if ((o == 4 || o == 5 || o == 6 || o == 7 || o == 8 || o == 9 || o == 10 || o == 11 || o == 12) && (n == 0))
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
        }
        m = o;
        o = Int32.Parse(data_Dialog[j]["scriptNumber"].ToString().Substring(data_Dialog[j]["scriptNumber"].ToString().IndexOf("_") + 1));
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
        }
        else
        {
            if (data_Dialog[j]["scriptNumber"].ToString().Equals("0_1"))
                Main_UI.SetActive(false);
            /*h = Int32.Parse(data_Dialog[j]["scriptNumber"].ToString()); //�̹��� ���� �� ����Ʈ ��ȣ
            n = Int32.Parse(data_Dialog[j]["image"].ToString()); //�̹��� ��ȣ(�⺻��ȭ)*/
            spriteR = CCImage.GetComponent<Image>();     //�̹��� ���� �� 0���� h�ֱ�
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
            else
            {
                //Debug.Log("�׳ɽ���");
                cuttoon.SetActive(false);
                scriptLine();
            }
        }
        if(data_Dialog[j]["scriptType"].ToString().Equals("choice"))
        {
            MataNum = Int32.Parse(data_Dialog[j]["QuizNumber"].ToString());
            QuizMate();
        }
    }

    
    public void scriptLine()  //��ũ��Ʈ ���� ��
    {
        if (ChatWin.activeSelf == false)
            ChatWin.SetActive(true);
        if (data_Dialog[j]["scriptType"].ToString().Equals("quiz"))  //�������
        {
            QuizTIme();
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
        Debug.Log("����NPC:" + Inter.NameNPC);
        for(int i = 0; i < ButtonPlusNpc.Length; i++)
        {
            if (Inter.NameNPC.Equals(ButtonPlusNpc[i]))
                NPCButton += 1;
            else
                continue;
        }
        
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
        Debug.Log("����"+ material[MataNum]);

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
            Button.SetActive(true); //����Ƽ���� ��ư ��ġ �ű�
        }
        block.SetActive(false);

        if (data_Dialog[j-1]["scriptType"].ToString().Equals("tutorial")||tuto)
        {
            //Debug.Log("Ʃ�丮�� ���P��");
            block.SetActive(true);
            //tuto = true;
            /*if (!tuto)
            {
                Debug.Log("tuto0�����");
                tutoi = 0;
            }*/

            Invoke("Tutorial_", 2f);
            
        }
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
            O();
            Line();
        }

        else
        {
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

}


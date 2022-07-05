using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;


public class Drawing : MonoBehaviour
{
    public Camera cam;  //Gets Main Camera
    public Camera Dcam;  //Gets Draw Camera
    public Material[] Material; //Material for Line Renderer

    //[SerializeField]
    private GameObject SkechBook;
    private LineRenderer curLine;  //Line which draws now
    private int positionCount = 2;  //Initial start and end position
    private Vector3 PrevPos = Vector3.zero; // 0,0,0 position variable
    private Transform ForErase;
    int layerMask;
    public bool ForDraw = false;
    private bool Erase = false;

    int i=0;  //메테리얼 번호
    private LodingTxt chat;
    void Start()
    {
        chat = GameObject.Find("chatManager").GetComponent<LodingTxt>();
    }
    void Update()
    {
        if (ForDraw)
        {
            DrawMouse();
        }
        
    }
    void DrawMouse()
    {
        Vector3 mousePos = Dcam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
        if (!Erase)
        {
            if (Input.GetMouseButtonDown(0))
                createLine(mousePos);
            else if (Input.GetMouseButton(0))
                connectLine(mousePos);
        }
        else if (Erase)
        {
            if (Input.GetMouseButtonDown(0)) 
                EraseLine(mousePos);
            else if (Input.GetMouseButton(0))
                EraseLine(mousePos);
        }
        
    }

    void createLine(Vector3 mousePos)
    {
        positionCount = 2;
        GameObject line = new GameObject("Line");
        //LineRenderer lineRend = line.GetComponent<LineRenderer>();
        LineRenderer lineRend = line.AddComponent<LineRenderer>();
        
        line.transform.parent = Dcam.transform;
        line.transform.position = mousePos;
        line.layer = 12;
        lineRend.startWidth = 0.01f;
        lineRend.endWidth = 0.01f;
        lineRend.numCornerVertices = 50;
        lineRend.numCapVertices = 50;
        lineRend.material = Material[i];//Resources.Load<Material>("Resources/Fonts/Materials/Furniture/Quiz/pen"); ;
        lineRend.SetPosition(0, mousePos);
        lineRend.SetPosition(1, mousePos);
        curLine = lineRend;
        ForErase = line.GetComponent<Transform>();
    }

    void connectLine(Vector3 mousePos)
    {
        if (PrevPos != null && Mathf.Abs(Vector3.Distance(PrevPos, mousePos)) >= 0.003f)
        {
            PrevPos = mousePos;
            positionCount++;
            curLine.positionCount = positionCount;
            curLine.SetPosition(positionCount - 1, mousePos);
            GameObject child = Instantiate(Resources.Load<GameObject>("Prefabs/Q/EraseLine"), curLine.transform)as GameObject;
            child.transform.position = curLine.GetPosition(positionCount - 1);
            child.transform.parent = ForErase;
            child.tag = "Eraser";
        }
    }

    void EraseLine(Vector3 mousePos)
    {
        Ray ray = Dcam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray,out hit))
        {
            if (hit.collider.gameObject.tag.Equals("Eraser"))
            {
                GameObject target = hit.collider.transform.parent.gameObject;
                Destroy(target);
            }
            //Destroy(target);
        }
    }

    public void changeColor()
    {
        GameObject click = EventSystem.current.currentSelectedGameObject;
        if (click.name.Equals("red"))
            i = 1;
        else if (click.name.Equals("yellow"))
            i = 2;
        else if (click.name.Equals("green"))
            i = 3;
        else if (click.name.Equals("blue"))
            i = 4;
        else if (click.name.Equals("black"))
            i = 0;
        Erase = false;
    }
    
    public void ChangeDrawCamera()
    {
        GameObject mainUI = GameObject.Find("Canvas").transform.Find("mainUI").gameObject;
        SkechBook = GameObject.Find("QuestEventUI").transform.Find("DrawUI").gameObject;
        Debug.Log(SkechBook);
        if (cam.enabled)
        {
            mainUI.SetActive(false);
            SkechBook.SetActive(true);
            ForDraw = true;
            cam.enabled=false;
            Dcam.enabled = true;
        }
        else
        {
            mainUI.SetActive(true);
            SkechBook.SetActive(false);
            ForDraw = false;
            Dcam.enabled = false;
            cam.enabled = true;
        }
    }
    
    public void Eraser()
    {
        if (!Erase)
            Erase = true;
    }

    //kkkkk노트버리기kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk


    public GameObject[] notes;
    public Animator NoteAnimator;
    //[SerializeField]
    private GameObject Destroyed;
    int Length;

    public void FinishWrite()
    {
        for (int i = 0; i < notes.Length; i++)
        {
            Destroy(notes[i].GetComponent<InputField>());
        }
        Invoke("AddButton",0.1f);
    }
    public void AddButton()
    {
        for (int i = 0; i < notes.Length; i++)
        {
            notes[i].AddComponent<Button>().onClick.AddListener(GotoWastebasket);
        }
    }
    public void GotoWastebasket()
    {
        GameObject click = EventSystem.current.currentSelectedGameObject;
        if (click.name.Equals("note"))
        {
            NoteAnimator.SetTrigger("NoteTrigger");
            Destroy(notes[0].GetComponent<Button>());
        }
        else if (click.name.Equals("note (1)"))
        {
            NoteAnimator.SetTrigger("NoteTrigger1");
            Destroy(notes[1].GetComponent<Button>());
        }
        else if (click.name.Equals("note (2)"))
        { 
            NoteAnimator.SetTrigger("NoteTrigger2");
            Destroy(notes[2].GetComponent<Button>());
        }
        else if (click.name.Equals("note (3)"))
        {
            NoteAnimator.SetTrigger("NoteTrigger3");
            Destroy(notes[3].GetComponent<Button>());
        }
        Destroyed = click.gameObject;
    }

    ///////////////가치관 카드//////////////////////////////////////////

    private int ValueLevel=0;
    private int ValueLength=0;
    private int MaxValueLength=10;
    int j;
    private static GameObject ValueCardBack;
    static Image spriteR;
    public Sprite ValueCardBackImage;
    public GameObject ValueButton;
    public void NextLevel()
    {
        j = 0;
        if (ValueLength < MaxValueLength)
        {
            Debug.Log(MaxValueLength + "개의 카드를 선택하세요");
        }
        else
        {
            if (ValueLevel==1)
            {
                ValueCardBack = GameObject.Find("ValueCardBack");
                spriteR = ValueCardBack.GetComponent<Image>();
                spriteR.sprite = ValueCardBackImage;
                ValueButton.SetActive(false);
            }
            ValueLevel++;
            RectTransform RectTransform;
            MaxValueLength = 5;
            GameObject parentsObject = GameObject.Find("ValueCards").gameObject;
            for (int i = 0; i < parentsObject.transform.childCount; i++)
            {
                GameObject gameObject = GameObject.Find("ValueCards").transform.GetChild(i).gameObject;
                RectTransform = gameObject.GetComponent<RectTransform>();
                if (gameObject.tag.Equals("DestroyCard"))
                {
                    Destroy(gameObject);
                }
                else
                {
                    ValueLength = 0;
                    j++;
                    RectTransform.transform.localScale = new Vector2(4f, 3f);
                    gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    gameObject.tag = "DestroyCard";
                    
                    if (ValueLevel == 1)
                    {
                        switch (j)
                        {
                            case 10:
                                RectTransform.anchoredPosition = new Vector2(-1089.614f, 82.99998f);
                                break;
                            case 1:
                                RectTransform.anchoredPosition = new Vector2(-583.024f, 82.99998f);
                                break;
                            case 2:
                                RectTransform.anchoredPosition = new Vector2(-583.024f, -317.27f);
                                break;
                            case 3:
                                RectTransform.anchoredPosition = new Vector2(-82.29398f, 82.99998f);
                                break;
                            case 4:
                                RectTransform.anchoredPosition = new Vector2(-1089.614f, -317.27f);
                                break;
                            case 5:
                                RectTransform.anchoredPosition = new Vector2(-82.2937f, -317.2701f);
                                break;
                            case 6:
                                RectTransform.anchoredPosition = new Vector2(432f, 83f);
                                break;
                            case 7:
                                RectTransform.anchoredPosition = new Vector2(432f, -317.27f);
                                break;
                            case 8:
                                RectTransform.anchoredPosition = new Vector2(918.9661f, 82.99998f);
                                break;
                            case 9:
                                RectTransform.anchoredPosition = new Vector2(918.9661f, -317.27f);
                                break;
                        }
                    }
                    else if (ValueLevel == 2)
                    {
                        switch (j)
                        {
                            case 1:
                                RectTransform.anchoredPosition = new Vector2(-781f, -215f);
                                break;
                            case 2:
                                RectTransform.anchoredPosition = new Vector2(-299f, -215f);
                                break;
                            case 3:
                                RectTransform.anchoredPosition = new Vector2(174f, -215f);
                                break;
                            case 4:
                                RectTransform.anchoredPosition = new Vector2(678f, -215f);
                                break;
                            case 5:
                                RectTransform.anchoredPosition = new Vector2(1157f, -215f);
                                Debug.Log("끝!");
                                Invoke("scriptLine", 1f);   //딜레이 후 스크립트 띄움
                                break;
                        }
                    }
                    
                }
            }
        }
    }

    private void scriptLine()
    {
        chat.scriptLine();
    }

    public void Select()
    {
        GameObject click = EventSystem.current.currentSelectedGameObject;
        if (click.tag.Equals("DestroyCard"))
        {
            if (ValueLength < MaxValueLength)
            {
                click.gameObject.tag = "SaveCard";
                click.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                ValueLength++;
            }
        }
        else
        {
            click.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            click.gameObject.tag = "DestroyCard";
            ValueLength--;
        }

    }

}

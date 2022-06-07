using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Drawing : MonoBehaviour
{
    public Camera cam;  //Gets Main Camera
    public Camera Dcam;  //Gets Draw Camera
    public Material[] Material; //Material for Line Renderer

    private LineRenderer curLine;  //Line which draws now
    private int positionCount = 2;  //Initial start and end position
    private Vector3 PrevPos = Vector3.zero; // 0,0,0 position variable
    private Transform ForErase;
    int layerMask;
    private bool ForDraw = false;
    private bool Erase = false;

    int i=0;  //메테리얼 번호

    private void Awake()
    {
        layerMask = 1 << LayerMask.NameToLayer("Draw");
    }
    // Update is called once per frame
    void Update()
    {
        if (ForDraw)
        {
            RaycastHit hit;
            if (Input.GetMouseButton(0) && Physics.Raycast(Dcam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layerMask))
            {
                DrawMouse();
            }
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
            //Debug.Log(curLine.GetPosition(positionCount-1));
        }
    }

    void EraseLine(Vector3 mousePos)
    {
        Debug.Log("지우개");
        Ray ray = Dcam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray,out hit))
        {
            Debug.Log(hit.collider.gameObject.name);
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
        Debug.Log(click.name);
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
        Debug.Log(i);
    }
    
    public void ChangeDrawCamera()
    {
        if(cam.enabled)
        {
            ForDraw = true;
            cam.enabled=false;
            Dcam.enabled = true;
        }
        else
        {
            ForDraw = false;
            Dcam.enabled = false;
            cam.enabled = true;
        }
    }
    
    public void Eraser()
    {
        if (!Erase)
            Erase = true;
        else
            Erase = false;
    }
}

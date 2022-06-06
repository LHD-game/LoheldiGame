using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawing : MonoBehaviour
{
    public Camera cam;  //Gets Main Camera
    public Camera Dcam;  //Gets Draw Camera
    public Material defaultMaterial; //Material for Line Renderer

    private LineRenderer curLine;  //Line which draws now
    private int positionCount = 2;  //Initial start and end position
    private Vector3 PrevPos = Vector3.zero; // 0,0,0 position variable
    private Transform ForErase;
    int layerMask;
    private bool ForDraw = false;
    private bool Erase = false;

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
        lineRend.material = defaultMaterial;//Resources.Load<Material>("Resources/Fonts/Materials/Furniture/Quiz/pen"); ;
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
        Debug.Log("Áö¿ì°³");
        Ray ray = Dcam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray,out hit))
        {
            Debug.Log(hit.collider.gameObject.name);

            GameObject target = hit.collider.transform.parent.gameObject;
            Destroy(target);

            //Destroy(target);
        }
    }
/*
    RaycastHit2D layerChk(string layerName)
    {
        Vector3 pos = Dcam.ScreenToWorldPoint(Input.mousePosition);

        int layerMask =1 <<LayerMask.NameToLayer(layerName);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f, layerMask);
        return hit;
    }*/
    
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

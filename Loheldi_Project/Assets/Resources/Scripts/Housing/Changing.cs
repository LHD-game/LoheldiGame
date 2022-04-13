using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changing : MonoBehaviour
{
    public GameObject TempObject;

    public GameObject bed;
    public MeshFilter bedmesh;

    private bool i = false;

    public void ButtonClick()
    {
        bedmesh = bed.GetComponent<MeshFilter>();

        if (i)
        {
            TempObject = Resources.Load<GameObject>("Models/Furniture/Bed_01_Single");  //해당 주소에 오브젝트를 물러옴.
            bedmesh.mesh = TempObject.GetComponent<MeshFilter>().sharedMesh;            //오브젝트에 메쉬를 추출해서 넣음.
            i = false;
        }
        else
        {
            TempObject = Resources.Load<GameObject>("Models/Furniture/Bed_03_Double");  //해당 주소에 오브젝트를 물러옴.
            bedmesh.mesh = TempObject.GetComponent<MeshFilter>().sharedMesh;            //오브젝트에 메쉬를 추출해서 넣음.
            i = true;
        }
    }
}
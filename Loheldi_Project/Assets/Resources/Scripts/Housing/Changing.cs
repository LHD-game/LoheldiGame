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
            TempObject = Resources.Load<GameObject>("Models/Furniture/Bed_01_Single");  //�ش� �ּҿ� ������Ʈ�� ������.
            bedmesh.mesh = TempObject.GetComponent<MeshFilter>().sharedMesh;            //������Ʈ�� �޽��� �����ؼ� ����.
            i = false;
        }
        else
        {
            TempObject = Resources.Load<GameObject>("Models/Furniture/Bed_03_Double");  //�ش� �ּҿ� ������Ʈ�� ������.
            bedmesh.mesh = TempObject.GetComponent<MeshFilter>().sharedMesh;            //������Ʈ�� �޽��� �����ؼ� ����.
            i = true;
        }
    }
}
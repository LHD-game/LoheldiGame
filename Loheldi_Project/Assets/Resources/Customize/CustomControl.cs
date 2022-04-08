using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomControl : MonoBehaviour
{
    Texture tSkin;
    Texture tEyes;
    Texture tMouth;

    Mesh meEyes;
    Mesh meMouth;

    //player's material
    public Material p_mSkin;
    public Material p_mEyes;
    public Material p_mMouth;

    public GameObject p_Eyes;
    public GameObject p_Mouth;
    /*Param param = new Param();*/
    //public GameObject p_Eyes;

    // Start is called before the first frame update
    void Start()
    {
        nowCustom();
        PlayerLook();
    }

    void Update()
    {
        PlayerLook();   // ó������ SelectCustom() ���� �ٿ��µ�, ������ �׷����ϸ� material �ν��� ���Ѵ�...
    }

    void nowCustom()    //���� Ŀ������ DB���� �޾ƿ�, NowSettings�� ����
    {
        NowSettings.u_skin_name = "skin1";   //!�ӽ� ����!
        NowSettings.u_skin_texture = NowSettings.u_skin_name+"_texture";   //!�ӽ� ����!
        NowSettings.u_eyes_name = "eyes1";   //!�ӽ� ����!
        NowSettings.u_eyes_color = "gray";   //!�ӽ� ����!
        NowSettings.u_eyes_texture =  NowSettings.u_eyes_name + "_texture_" + NowSettings.u_eyes_color;   //!�ӽ� ����!
        NowSettings.u_mouth_name = "mouthI";   //!�ӽ� ����!
        NowSettings.u_mouth_texture = "mouth1_texture";   //!�ӽ� ����!

        tSkin = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_skin_texture));
        tEyes = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_eyes_texture));
        tMouth = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_mouth_texture));
    }

    public void SelectCustom(GameObject go) //Ŀ���� ������ ���� �� ���� �޼ҵ�
    {
        //�ش� Ŀ������ itemname ��������, 
        string itemName = go.transform.Find("ItemName").gameObject.GetComponent<Text>().text;
        /*string itemName2 = go.transform.Find("ItemName").gameObject.GetComponent<Text>().text;
        string itemName3 = go.transform.Find("ItemName").gameObject.GetComponent<Text>().text;*/
        //print(itemName + "�޼ҵ� ���� ����.");

        //data_dialog���� ������ row ã��. 
        List<Dictionary<string, object>> d_dialog = new List<Dictionary<string, object>>();
        d_dialog = CommonField.GetDataDialog();
        for (int i = 0; i < d_dialog.Count; i++)
        {
            if (d_dialog[i][CommonField.nName].ToString().Equals(itemName)) //itemName�� ������ ������ �̸��� ���� ������ db���� ã��
            {
                if (d_dialog[i][CommonField.nModel].ToString().Equals(CommonField.m_skin))//�װ� skin�̸�
                {
                    NowSettings.u_skin_name = d_dialog[i][CommonField.nName].ToString();
                    NowSettings.u_skin_texture = d_dialog[i][CommonField.nTexture].ToString();
                }
                else if (d_dialog[i][CommonField.nModel].ToString().Equals(CommonField.m_eyes))
                {
                    NowSettings.u_eyes_name = d_dialog[i][CommonField.nName].ToString();
                    NowSettings.u_eyes_texture = d_dialog[i][CommonField.nTexture].ToString() + "_" + NowSettings.u_eyes_color;
                }
                else if (d_dialog[i][CommonField.nModel].ToString().Equals(CommonField.m_mouth))    //�װ� ���̸�,
                {
                    NowSettings.u_mouth_name = d_dialog[i][CommonField.nName].ToString();
                    NowSettings.u_mouth_texture = d_dialog[i][CommonField.nTexture].ToString();
                }

            }
        }
        
        /*{
            param.Add("Skin", itemName);
            param.Add("Eyes", itemName2);
            param.Add("Mouth", itemName3);
            *//*param.Add("Model", rows[itemnum = 1][itemnum = 2][itemnum = 3]["Model"][0][0][0]);
            param.Add("Meterial", rows[itemnum = 1][itemnum = 2][itemnum = 3]["Meterial"][0][0][0]);
            param.Add("Texture", rows[itemnum = 1][itemnum = 2][itemnum = 3]["Texture"][0][0][0]);*//*
            Backend.GameData.Insert("USER_CUSTOM", param);
        }*/
        //�ش� ������ row ã����, �ش��ϴ� texture������ �������ش�.

    }
    /*public void SelectCustom2(GameObject go)
    {
        string itemName = go.transform.Find("ItemName").gameObject.GetComponent<Text>().text;
        param.Add("Eyes", itemName);
        Backend.GameData.Insert("USER_CUSTOM", param);
    }
    public void SelectCustom3(GameObject go)
    {
        string itemName = go.transform.Find("ItemName").gameObject.GetComponent<Text>().text;
        param.Add("Mouth", itemName);
        Backend.GameData.Insert("USER_CUSTOM", param);
    }*/
    public void SelectColor(GameObject go)   //�� �̸�, ������ ����
    {
        string color = go.transform.Find("ColorTxt").gameObject.GetComponent<Text>().text;
        string part = go.transform.Find("part").gameObject.GetComponent<Text>().text;
        
        print(color);
        print(part);
        

        if (part.Equals("eyes"))
        {
            NowSettings.u_eyes_color = color;
            NowSettings.u_eyes_texture = NowSettings.u_eyes_name + "_texture_" + NowSettings.u_eyes_color;
            //print("���� ��"+NowSettings.u_eyes_texture);
        }
        else if (part.Equals("mouth"))
        {
            //todo
        }
        //PlayerLook(); <- �ְԵǸ� UnassignedReferenceException ������ �߻��մϴ�;; ���� update() ������ �۵��˴ϴ�.
    }

    /*public void ConfirmColor(GameObject go)
    {
        *//*string color = go.transform.Find("ColorTxt").gameObject.GetComponent<Text>().text;*//*
        string part = go.transform.Find("part").gameObject.GetComponent<Text>().text;

        
    }*/

    private void PlayerLook()   //�ܰ� Ŀ���� ������Ʈ
    {
        tSkin = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_skin_texture));
        tEyes = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_eyes_texture));
        tMouth = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_mouth_texture));

        meEyes = Resources.Load<Mesh>(("Customize/" + NowSettings.u_eyes_name + "_mesh"));
        meMouth = Resources.Load<Mesh>(("Customize/" + NowSettings.u_mouth_name + "_mesh"));



        p_mSkin.SetTexture("_MainTex", tSkin);    //_MainTex: Material�� Albedo texture�Դϴ�. 
        p_mEyes.SetTexture("_MainTex", tEyes);     
        p_mMouth.SetTexture("_MainTex", tMouth);

        MeshFilter e_mesh = p_Eyes.GetComponent<MeshFilter>();  //�� �޽� ����(�𵨸�)
        MeshFilter m_mesh = p_Mouth.GetComponent<MeshFilter>();  //�� �޽� ����(�𵨸�)
        e_mesh.sharedMesh = meEyes;
        m_mesh.sharedMesh = meMouth;
    }
}

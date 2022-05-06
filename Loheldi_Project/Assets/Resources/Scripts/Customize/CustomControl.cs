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
    Texture tHair;

    Mesh meEyes;
    Mesh meMouth;
    Mesh meHair;

    //player's material
    public Material p_mSkin;
    public Material p_mEyes;
    public Material p_mMouth;
    public Material p_mHair;

    public GameObject p_Eyes;
    public GameObject p_Mouth;
    public GameObject p_Hair;
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

    void nowCustom()    //�������� ������ Ŀ���͸���¡ ����� �޾ƿ� PreviousSettings�� ����.
    {
        var bro = Backend.GameData.GetMyData("USER_CUSTOM", new Where());

        if (bro.IsSuccess() == false)
        {
            Debug.Log("��û ����");
            return;
        }
        if (bro.GetReturnValuetoJSON()["rows"].Count <= 0)
        {
            // ��û�� �����ص� where ���ǿ� �����ϴ� �����Ͱ� ���� �� �ֱ� ������
            // �����Ͱ� �����ϴ��� Ȯ��
            // ���� ���� new Where() ������ ��� ���̺� row�� �ϳ��� ������ Count�� 0 ���� �� �� �ִ�.
            Debug.Log("��û ���������� ���̺� row�� �ϳ��� ����");
            return;
        }
        JsonData rows = bro.GetReturnValuetoJSON()["rows"];

        PreviousSettings.u_skin_name = rows[0]["Skin"]["S"].ToString();
        PreviousSettings.u_eyes_name = rows[0]["Eyes"]["S"].ToString();
        PreviousSettings.u_eyes_color = rows[0]["EColor"]["S"].ToString();
        PreviousSettings.u_mouth_name = rows[0]["Mouth"]["S"].ToString();
        PreviousSettings.u_hair_name = rows[0]["Hair"]["S"].ToString();
        PreviousSettings.u_hair_color = rows[0]["HColor"]["S"].ToString();

        Debug.Log("previous settings" + PreviousSettings.u_skin_name);

        ResetCustom();
    }

    public void ResetCustom()  //���� Ŀ���͸���¡�� �ʱ� Ŀ���͸���¡���� �ʱ�ȭ
    {
        NowSettings.u_skin_name = PreviousSettings.u_skin_name;
        NowSettings.u_skin_texture = FindTexture(NowSettings.u_skin_name);
        NowSettings.u_eyes_name = PreviousSettings.u_eyes_name;
        NowSettings.u_eyes_color = PreviousSettings.u_eyes_color;
        NowSettings.u_eyes_texture = NowSettings.u_eyes_name + "_texture_" + NowSettings.u_eyes_color;
        NowSettings.u_mouth_name = PreviousSettings.u_mouth_name;
        NowSettings.u_mouth_texture = FindTexture(NowSettings.u_mouth_name);
        NowSettings.u_hair_name = PreviousSettings.u_hair_name;
        NowSettings.u_hair_color = PreviousSettings.u_hair_color;
        NowSettings.u_hair_texture = "texture_" + NowSettings.u_hair_color;

        tSkin = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_skin_texture));
        tEyes = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_eyes_texture));
        //tMouth = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_mouth_texture));
        tHair = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_hair_texture));
    }

    public void SaveCustom()    //���� Ŀ���͸���¡�� ������ ����
    {
        Param param = new Param();
        param.Add("Skin", NowSettings.u_skin_name);
        param.Add("Eyes", NowSettings.u_eyes_name);
        param.Add("EColor", NowSettings.u_eyes_color);
        param.Add("Mouth", NowSettings.u_mouth_name);
        param.Add("Hair", NowSettings.u_hair_name);
        param.Add("HColor", NowSettings.u_hair_color);

        //���� ���� ���� ����� row �˻�
        var bro = Backend.GameData.Get("USER_CUSTOM", new Where());
        string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

        //�ش� row�� ���� update
        Backend.GameData.UpdateV2("USER_CUSTOM", rowIndate, Backend.UserInDate, param);
        print("SaveCustom");

        SceneLoader.instance.GotoMainField();
    }

    //���� ���� ������ ��Ͽ��� �������� Texture ��ȸ �޼ҵ�
    string FindTexture(string item_name)
    {
        Where where = new Where();
        where.Equal("IName", item_name);

        var bro = Backend.GameData.GetMyData("ACC_CUSTOM", where);

        if (bro.IsSuccess() == false)
        {
            Debug.Log("��û ����");
            return null;
        }
        if (bro.GetReturnValuetoJSON()["rows"].Count <= 0)
        {
            // ��û�� �����ص� where ���ǿ� �����ϴ� �����Ͱ� ���� �� �ֱ� ������
            // �����Ͱ� �����ϴ��� Ȯ��
            // ���� ���� new Where() ������ ��� ���̺� row�� �ϳ��� ������ Count�� 0 ���� �� �� �ִ�.
            Debug.Log("��û ���������� ���̺� row�� �ϳ��� ����");
            return null;
        }
        JsonData rows = bro.GetReturnValuetoJSON()["rows"];
        string item_texture = rows[0]["Texture"]["S"].ToString();

        Debug.Log("FindTexture: " + item_texture);

        return item_texture;
    }


    public void SelectCustom(GameObject go) //Ŀ���� ������ ���� �� ���� �޼ҵ�
    {
        //�ش� Ŀ������ itemname ��������, 
        string itemName = go.transform.Find("ItemName").gameObject.GetComponent<Text>().text;
        
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
                else if (d_dialog[i][CommonField.nModel].ToString().Equals(CommonField.m_hair))    //�װ� hair�̸�,
                {
                    NowSettings.u_hair_name = d_dialog[i][CommonField.nName].ToString();
                    NowSettings.u_hair_texture = d_dialog[i][CommonField.nTexture].ToString() + "_" + NowSettings.u_hair_color;
                }

            }
        }
    }

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
        else if (part.Equals("hair"))
        {
            NowSettings.u_hair_color = color;
            NowSettings.u_hair_texture = "texture_" + NowSettings.u_hair_color;
            print("���� ��"+NowSettings.u_hair_texture);
        }
        //PlayerLook(); <- �ְԵǸ� UnassignedReferenceException ������ �߻��մϴ�;; ���� update() ������ �۵��˴ϴ�.
    }

    private void PlayerLook()   //�ܰ� Ŀ���� ������Ʈ
    {
        tSkin = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_skin_texture));
        tEyes = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_eyes_texture));
        tMouth = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_mouth_texture));
        tHair = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_hair_texture));

        meEyes = Resources.Load<Mesh>(("Customize/" + NowSettings.u_eyes_name + "_mesh"));
        meMouth = Resources.Load<Mesh>(("Customize/" + NowSettings.u_mouth_name + "_mesh"));
        meHair = Resources.Load<Mesh>(("Customize/" + NowSettings.u_hair_name + "_mesh"));



        p_mSkin.SetTexture("_MainTex", tSkin);    //_MainTex: Material�� Albedo texture�Դϴ�. 
        p_mEyes.SetTexture("_MainTex", tEyes);     
        p_mMouth.SetTexture("_MainTex", tMouth);
        p_mHair.SetTexture("_MainTex", tHair);

        MeshFilter e_mesh = p_Eyes.GetComponent<MeshFilter>();  //�� �޽� ����(�𵨸�)
        MeshFilter m_mesh = p_Mouth.GetComponent<MeshFilter>();  //�� �޽� ����(�𵨸�)
        MeshFilter h_mesh = p_Hair.GetComponent<MeshFilter>();  //�Ӹ� �޽� ����(�𵨸�)
        e_mesh.sharedMesh = meEyes;
        m_mesh.sharedMesh = meMouth;
        h_mesh.sharedMesh = meHair;
    }
}

using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCustom : MonoBehaviour
{
    protected Texture tSkin;
    protected Texture tEyes;
    protected Texture tMouth;
    protected Texture tHair;

    protected Mesh meEyes;
    protected Mesh meMouth;
    protected Mesh meHair;

    //player's material
    protected Material p_mSkin;
    protected Material p_mEyes;
    protected Material p_mMouth;
    protected Material p_mHair;

    public GameObject p_Eyes;
    public GameObject p_Mouth;
    public GameObject p_Hair;

    protected static bool newAcc = false;

    public void nowCustom()    //�������� ������ Ŀ���͸���¡ ����� �޾ƿ� PreviousSettings�� ����.
    {
        var bro = Backend.GameData.GetMyData("USER_CUSTOM", new Where());

        if (bro.IsSuccess() == false)
        {
            Debug.Log("��û ����");
            return;
        }
        if (bro.GetReturnValuetoJSON()["rows"].Count <= 0)  // �⺻ Ŀ�� �������� ���� ���
        {
            newAcc = true;  // �� ������ �� �����Դϴ�.
            Save_BasicCustom.SaveBasicCustom(); //�⺻ Ŀ���� ������ ����
            nowCustom();//�����
            return;
        }
        JsonData rows = bro.GetReturnValuetoJSON()["rows"];

        PreviousSettings.u_skin_name = rows[0]["Skin"]["S"].ToString();
        PreviousSettings.u_eyes_name = rows[0]["Eyes"]["S"].ToString();
        PreviousSettings.u_eyes_color = rows[0]["EColor"]["S"].ToString();
        PreviousSettings.u_mouth_name = rows[0]["Mouth"]["S"].ToString();
        PreviousSettings.u_hair_name = rows[0]["Hair"]["S"].ToString();
        PreviousSettings.u_hair_color = rows[0]["HColor"]["S"].ToString();
    }

    public void ResetCustom()  //���� Ŀ���͸���¡�� �ʱ� Ŀ���͸���¡���� �ʱ�ȭ
    {
        NowSettings.u_skin_name = PreviousSettings.u_skin_name;
        NowSettings.u_skin_texture = FindTexture(NowSettings.u_skin_name);
        NowSettings.u_eyes_name = PreviousSettings.u_eyes_name;
        NowSettings.u_eyes_color = PreviousSettings.u_eyes_color;
        NowSettings.u_eyes_texture = FindTexture(NowSettings.u_eyes_name) + "_" + NowSettings.u_eyes_color;
        NowSettings.u_mouth_name = PreviousSettings.u_mouth_name;
        NowSettings.u_mouth_texture = FindTexture(NowSettings.u_mouth_name);
        NowSettings.u_hair_name = PreviousSettings.u_hair_name;
        NowSettings.u_hair_color = PreviousSettings.u_hair_color;
        NowSettings.u_hair_texture = "texture_" + NowSettings.u_hair_color;

        tSkin = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_skin_texture));
        tEyes = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_eyes_texture));
        tMouth = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_mouth_texture));
        tHair = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_hair_texture));
    }

    public void PlayerLook()   //�ܰ� Ŀ���� ������Ʈ
    {
        NowSettings.u_skin_texture = FindTexture(NowSettings.u_skin_name);
        NowSettings.u_eyes_texture = FindTexture(NowSettings.u_eyes_name) + "_" + NowSettings.u_eyes_color;
        NowSettings.u_mouth_texture = FindTexture(NowSettings.u_mouth_name);
        NowSettings.u_hair_texture = "texture_" + NowSettings.u_hair_color;

        tSkin = Resources.Load<Texture>("Customize/Textures/" + NowSettings.u_skin_texture);
        tEyes = Resources.Load<Texture>("Customize/Textures/" + NowSettings.u_eyes_texture);
        tMouth = Resources.Load<Texture>("Customize/Textures/" + NowSettings.u_mouth_texture);
        tHair = Resources.Load<Texture>("Customize/Textures/" + NowSettings.u_hair_texture);

        meEyes = Resources.Load<Mesh>("Customize/Mesh/" + NowSettings.u_eyes_name + "_mesh");
        meMouth = Resources.Load<Mesh>("Customize/Mesh/" + NowSettings.u_mouth_name + "_mesh");
        meHair = Resources.Load<Mesh>("Customize/Mesh/" + NowSettings.u_hair_name + "_mesh");

        p_mSkin = Resources.Load<Material>("Customize/Materials/Skin");
        p_mEyes = Resources.Load<Material>("Customize/Materials/Eyes");
        p_mMouth = Resources.Load<Material>("Customize/Materials/Mouth");
        p_mHair = Resources.Load<Material>("Customize/Materials/Hair");

        p_mSkin.SetTexture("_MainTex", tSkin);    //_MainTex: Material�� Albedo texture�Դϴ�. 
        p_mEyes.SetTexture("_MainTex", tEyes);
        p_mMouth.SetTexture("_MainTex", tMouth);
        p_mHair.SetTexture("_MainTex", tHair);

        //MeshFilter e_mesh = p_Eyes.GetComponent<MeshFilter>();  //�� �޽� ����(�𵨸�)
        SkinnedMeshRenderer e_mesh = p_Eyes.GetComponent<SkinnedMeshRenderer>();
        SkinnedMeshRenderer m_mesh = p_Mouth.GetComponent<SkinnedMeshRenderer>();
        SkinnedMeshRenderer h_mesh = p_Hair.GetComponent<SkinnedMeshRenderer>();

        e_mesh.sharedMesh = meEyes;
        m_mesh.sharedMesh = meMouth;
        h_mesh.sharedMesh = meHair;
    }

    //���� ���� ������ ��Ͽ��� �������� Texture ��ȸ �޼ҵ�
    protected string FindTexture(string item_name)
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
}

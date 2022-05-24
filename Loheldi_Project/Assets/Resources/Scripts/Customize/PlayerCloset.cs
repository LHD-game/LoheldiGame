using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCloset : MonoBehaviour
{
    protected Texture tUpper;
    protected Texture tLower;
    protected Texture tSocks;
    protected Texture tShoes;

    protected Mesh meLower;

    //player's material
    protected Material p_mUpper;
    protected Material p_mLower;
    protected Material p_mSocks;
    protected Material p_mShoes;

    public GameObject p_Lower;


    public void nowClothes()    //�������� ������ Ŀ���͸���¡ ����� �޾ƿ� PreviousSettings�� ����.
    {
        var bro = Backend.GameData.GetMyData("USER_CLOTHES", new Where());

        if (bro.IsSuccess() == false)
        {
            Debug.Log("��û ����");
            return;
        }
        if (bro.GetReturnValuetoJSON()["rows"].Count <= 0)  // �⺻ �ǻ� �������� ���� ���
        {
            Save_BasicCustom.SaveBasicClothes(); //�⺻ �ǻ� ������ ����
            nowClothes();//�����
            return;
        }
        JsonData rows = bro.GetReturnValuetoJSON()["rows"];

        PreviousSettings.u_upper_name = rows[0]["Upper"]["S"].ToString();
        PreviousSettings.u_upper_color = rows[0]["UColor"]["S"].ToString();
        PreviousSettings.u_lower_name = rows[0]["Lower"]["S"].ToString();
        PreviousSettings.u_lower_color = rows[0]["LColor"]["S"].ToString();
        PreviousSettings.u_socks_name = rows[0]["Socks"]["S"].ToString();
        PreviousSettings.u_socks_color = rows[0]["SColor"]["S"].ToString();
        PreviousSettings.u_shoes_name = rows[0]["Shoes"]["S"].ToString();
        PreviousSettings.u_shoes_color = rows[0]["ShColor"]["S"].ToString();
    }

    public void ResetClothes()  //���� Ŀ���͸���¡�� �ʱ� Ŀ���͸���¡���� �ʱ�ȭ
    {
        //����
        NowSettings.u_upper_name = PreviousSettings.u_upper_name;
        NowSettings.u_upper_color = PreviousSettings.u_upper_color;
        NowSettings.u_upper_texture = NowSettings.u_upper_name + "_texture_" + NowSettings.u_upper_color;
        //����
        NowSettings.u_lower_name = PreviousSettings.u_lower_name;
        NowSettings.u_lower_color = PreviousSettings.u_lower_color;
        NowSettings.u_lower_texture = NowSettings.u_lower_name + "_texture_" + NowSettings.u_lower_color;
        //�縻
        NowSettings.u_socks_name = PreviousSettings.u_socks_name;
        NowSettings.u_socks_color = PreviousSettings.u_socks_color;
        NowSettings.u_socks_texture = NowSettings.u_socks_name + "_texture_" + NowSettings.u_socks_color;
        //�Ź�
        NowSettings.u_shoes_name = PreviousSettings.u_shoes_name;
        NowSettings.u_shoes_color = PreviousSettings.u_shoes_color;
        NowSettings.u_shoes_texture = NowSettings.u_shoes_name + "_texture_" + NowSettings.u_shoes_color;
        //texture �ش� ��ο��� �ҷ�����
        tUpper = Resources.Load<Texture>(("Customize/Textures/Upper/" + NowSettings.u_upper_texture));
        tLower = Resources.Load<Texture>(("Customize/Textures/Lower/" + NowSettings.u_lower_texture));
        tSocks = Resources.Load<Texture>(("Customize/Textures/Socks/" + NowSettings.u_socks_texture));
        tShoes = Resources.Load<Texture>(("Customize/Textures/Shoes/" + NowSettings.u_shoes_texture));
    }

    public void PlayerLook()   //�ܰ� Ŀ���� ������Ʈ
    {
        NowSettings.u_upper_texture = NowSettings.u_upper_name + "_texture_" + NowSettings.u_upper_color;
        NowSettings.u_lower_texture = NowSettings.u_lower_name + "_texture_" + NowSettings.u_lower_color;
        NowSettings.u_socks_texture = NowSettings.u_socks_name + "_texture_" + NowSettings.u_socks_color;
        NowSettings.u_shoes_texture = NowSettings.u_shoes_name + "_texture_" + NowSettings.u_shoes_color;

        tUpper = Resources.Load<Texture>(("Customize/Textures/Upper/" + NowSettings.u_upper_texture));
        tLower = Resources.Load<Texture>(("Customize/Textures/Lower/" + NowSettings.u_lower_texture));
        tSocks = Resources.Load<Texture>(("Customize/Textures/Socks/" + NowSettings.u_socks_texture));
        tShoes = Resources.Load<Texture>(("Customize/Textures/Shoes/" + NowSettings.u_shoes_texture));

        //meLower = Resources.Load<Mesh>(("Customize/Mesh/" + NowSettings.u_lower_name + "_mesh"));

        p_mUpper = Resources.Load<Material>("Customize/Materials/Upper");
        p_mLower = Resources.Load<Material>("Customize/Materials/Lower");
        p_mSocks = Resources.Load<Material>("Customize/Materials/Socks");
        p_mShoes = Resources.Load<Material>("Customize/Materials/Shoes");

        p_mUpper.SetTexture("_MainTex", tUpper);    //_MainTex: Material�� Albedo texture�Դϴ�. 
        p_mLower.SetTexture("_MainTex", tLower);
        p_mSocks.SetTexture("_MainTex", tSocks);
        p_mShoes.SetTexture("_MainTex", tShoes);
  
        //SkinnedMeshRenderer l_mesh = p_Lower.GetComponent<SkinnedMeshRenderer>();

        //l_mesh.sharedMesh = meLower;

    }
}

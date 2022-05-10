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
    public Material p_mSkin;
    public Material p_mEyes;
    public Material p_mMouth;
    public Material p_mHair;

    public GameObject p_Eyes;
    public GameObject p_Mouth;
    public GameObject p_Hair;

    protected void nowCustom()    //서버에서 유저의 커스터마이징 목록을 받아와 PreviousSettings에 저장.
    {
        var bro = Backend.GameData.GetMyData("USER_CUSTOM", new Where());

        if (bro.IsSuccess() == false)
        {
            Debug.Log("요청 실패");
            return;
        }
        if (bro.GetReturnValuetoJSON()["rows"].Count <= 0)
        {
            // 요청이 성공해도 where 조건에 부합하는 데이터가 없을 수 있기 때문에
            // 데이터가 존재하는지 확인
            // 위와 같은 new Where() 조건의 경우 테이블에 row가 하나도 없으면 Count가 0 이하 일 수 있다.
            Debug.Log("요청 성공했지만 테이블에 row가 하나도 없음");
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
    }

    public void ResetCustom()  //현재 커스터마이징을 초기 커스터마이징으로 초기화
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

    protected void PlayerLook()   //외관 커스텀 업데이트
    {
        tSkin = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_skin_texture));
        tEyes = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_eyes_texture));
        tMouth = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_mouth_texture));
        tHair = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_hair_texture));

        meEyes = Resources.Load<Mesh>(("Customize/" + NowSettings.u_eyes_name + "_mesh"));
        meMouth = Resources.Load<Mesh>(("Customize/" + NowSettings.u_mouth_name + "_mesh"));
        meHair = Resources.Load<Mesh>(("Customize/" + NowSettings.u_hair_name + "_mesh"));



        p_mSkin.SetTexture("_MainTex", tSkin);    //_MainTex: Material의 Albedo texture입니다. 
        p_mEyes.SetTexture("_MainTex", tEyes);
        p_mMouth.SetTexture("_MainTex", tMouth);
        p_mHair.SetTexture("_MainTex", tHair);

        MeshFilter e_mesh = p_Eyes.GetComponent<MeshFilter>();  //눈 메시 변경(모델링)
        MeshFilter m_mesh = p_Mouth.GetComponent<MeshFilter>();  //입 메시 변경(모델링)
        MeshFilter h_mesh = p_Hair.GetComponent<MeshFilter>();  //머리 메시 변경(모델링)
        e_mesh.sharedMesh = meEyes;
        m_mesh.sharedMesh = meMouth;
        h_mesh.sharedMesh = meHair;
    }

    //서버 보유 아이템 목록에서 아이템의 Texture 조회 메소드
    protected string FindTexture(string item_name)
    {
        Where where = new Where();
        where.Equal("IName", item_name);

        var bro = Backend.GameData.GetMyData("ACC_CUSTOM", where);

        if (bro.IsSuccess() == false)
        {
            Debug.Log("요청 실패");
            return null;
        }
        if (bro.GetReturnValuetoJSON()["rows"].Count <= 0)
        {
            // 요청이 성공해도 where 조건에 부합하는 데이터가 없을 수 있기 때문에
            // 데이터가 존재하는지 확인
            // 위와 같은 new Where() 조건의 경우 테이블에 row가 하나도 없으면 Count가 0 이하 일 수 있다.
            Debug.Log("요청 성공했지만 테이블에 row가 하나도 없음");
            return null;
        }
        JsonData rows = bro.GetReturnValuetoJSON()["rows"];
        string item_texture = rows[0]["Texture"]["S"].ToString();

        Debug.Log("FindTexture: " + item_texture);

        return item_texture;
    }
}

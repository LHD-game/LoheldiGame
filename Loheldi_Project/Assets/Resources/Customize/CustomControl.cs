using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomControl : MonoBehaviour
{
    Texture tSkin;
    Texture tEyes;
    Texture tMouth;

    //player's material
    public Material p_mSkin;
    public Material p_mEyes;
    public Material p_mMouth;

    // Start is called before the first frame update
    void Start()
    {
        nowCustom();
        PlayerLook();
    }

    void Update()
    {
        PlayerLook();   // 처음에는 SelectCustom() 끝에 붙였는데, 왜인지 그렇게하면 material 인식을 못한다...
    }

    void nowCustom()    //현재 커스텀을 DB에서 받아옴, NowSettings에 저장
    {
        NowSettings.u_skin_name = "skin1";   //!임시 세팅!
        NowSettings.u_skin_texture = NowSettings.u_skin_name+"_texture";   //!임시 세팅!
        NowSettings.u_eyes_name = "eyes1";   //!임시 세팅!
        NowSettings.u_eyes_texture = NowSettings.u_skin_name+"_"+ NowSettings.u_eyes_name + "_texture";   //!임시 세팅!
        NowSettings.u_mouth_name = "mouth1";   //!임시 세팅!
        NowSettings.u_mouth_texture = NowSettings.u_skin_name+"_"+ NowSettings.u_mouth_name + "_texture";   //!임시 세팅!

        tSkin = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_skin_texture));
        tEyes = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_eyes_texture));
        tMouth = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_mouth_texture));
    }

    public void SelectCustom(GameObject go) //커스텀 아이템 선택 시 실행 메소드
    {
        //해당 커스텀의 itemname 가져오고, 
        string itemName = go.transform.Find("ItemName").gameObject.GetComponent<Text>().text;
        //print(itemName + "메소드 실행 성공.");

        //data_dialog에서 아이템 row 찾기. 
        List<Dictionary<string, object>> d_dialog = new List<Dictionary<string, object>>();
        d_dialog = CommonField.GetDataDialog();
        for (int i = 0; i < d_dialog.Count; i++)
        {
            if (d_dialog[i][CommonField.nName].ToString().Equals(itemName)) //itemName과 동일한 아이템 이름을 가진 아이템 db에서 찾음
            {
                if (d_dialog[i][CommonField.nModel].ToString().Equals(CommonField.m_skin))//그게 skin이면
                {
                    NowSettings.u_skin_name = d_dialog[i][CommonField.nName].ToString();
                    NowSettings.u_skin_texture = d_dialog[i][CommonField.nTexture].ToString();
                    NowSettings.u_eyes_texture = NowSettings.u_skin_name + "_" + NowSettings.u_eyes_name + "_texture";
                    NowSettings.u_mouth_texture = NowSettings.u_skin_name + "_" + NowSettings.u_mouth_name + "_texture";
                }
                else if (d_dialog[i][CommonField.nModel].ToString().Equals(CommonField.m_eyes))
                {
                    NowSettings.u_eyes_name = d_dialog[i][CommonField.nName].ToString();
                    NowSettings.u_eyes_texture = NowSettings.u_skin_name + "_" + d_dialog[i][CommonField.nTexture].ToString();
                }
                else if (d_dialog[i][CommonField.nModel].ToString().Equals(CommonField.m_mouth))
                {
                    NowSettings.u_mouth_name = d_dialog[i][CommonField.nName].ToString();
                    NowSettings.u_mouth_texture = NowSettings.u_skin_name + "_" + d_dialog[i][CommonField.nTexture].ToString();
                }

            }
        }

        //해당 아이템 row 찾으면, 해당하는 texture등으로 변경해준다.
        
    }

    private void PlayerLook()   //외관 커스텀 업데이트
    {
        tSkin = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_skin_texture));
        tEyes = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_eyes_texture));
        tMouth = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_mouth_texture));

        p_mSkin.SetTexture("_MainTex", tSkin);    //_MainTex: Material의 Albedo texture입니다. 
        p_mEyes.SetTexture("_MainTex", tEyes);     
        p_mMouth.SetTexture("_MainTex", tMouth);     
    }
}

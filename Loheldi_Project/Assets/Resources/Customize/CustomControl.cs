using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomControl : MonoBehaviour
{
    
    Texture tSkin;

    public Material mSkin;

    // Start is called before the first frame update
    void Start()
    {
        InitMaterial();
        nowCustom();
    }

    void Update()
    {
        tSkin = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_skin_texture));
        mSkin.SetTexture("_MainTex", tSkin);    //_MainTex: Material의 Albedo texture입니다. 
        //mSkin.SetTexture("Skin", tSkin);
        print(tSkin);
    }

    void InitMaterial()
    {

    }

    void nowCustom()    //현재 커스텀을 DB에서 받아옴, NowSettings에 저장
    {
        NowSettings.u_skin_texture = "1";   //!임시 세팅!
        print(NowSettings.u_skin_texture);
        tSkin = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_skin_texture));
        print(tSkin);
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
                    NowSettings.u_skin_texture = d_dialog[i][CommonField.nTexture].ToString();
                }
                
            }
        }
        //Dictionary<string, object> itemRow = 

        //해당 아이템 row 찾으면, 해당하는 texture등으로 변경해준다.
    }

    private void PlayerLook()   //외관 커스텀 업데이트
    {

    }
}

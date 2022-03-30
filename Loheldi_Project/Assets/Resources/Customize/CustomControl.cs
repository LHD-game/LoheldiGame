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
        PlayerLook();   // ó������ SelectCustom() ���� �ٿ��µ�, ������ �׷����ϸ� material �ν��� ���Ѵ�...
    }

    void nowCustom()    //���� Ŀ������ DB���� �޾ƿ�, NowSettings�� ����
    {
        NowSettings.u_skin_name = "skin1";   //!�ӽ� ����!
        NowSettings.u_skin_texture = NowSettings.u_skin_name+"_texture";   //!�ӽ� ����!
        NowSettings.u_eyes_name = "eyes1";   //!�ӽ� ����!
        NowSettings.u_eyes_texture = NowSettings.u_skin_name+"_"+ NowSettings.u_eyes_name + "_texture";   //!�ӽ� ����!
        NowSettings.u_mouth_name = "mouth1";   //!�ӽ� ����!
        NowSettings.u_mouth_texture = NowSettings.u_skin_name+"_"+ NowSettings.u_mouth_name + "_texture";   //!�ӽ� ����!

        tSkin = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_skin_texture));
        tEyes = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_eyes_texture));
        tMouth = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_mouth_texture));
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

        //�ش� ������ row ã����, �ش��ϴ� texture������ �������ش�.
        
    }

    private void PlayerLook()   //�ܰ� Ŀ���� ������Ʈ
    {
        tSkin = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_skin_texture));
        tEyes = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_eyes_texture));
        tMouth = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_mouth_texture));

        p_mSkin.SetTexture("_MainTex", tSkin);    //_MainTex: Material�� Albedo texture�Դϴ�. 
        p_mEyes.SetTexture("_MainTex", tEyes);     
        p_mMouth.SetTexture("_MainTex", tMouth);     
    }
}

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
        mSkin.SetTexture("_MainTex", tSkin);    //_MainTex: Material�� Albedo texture�Դϴ�. 
        //mSkin.SetTexture("Skin", tSkin);
        print(tSkin);
    }

    void InitMaterial()
    {

    }

    void nowCustom()    //���� Ŀ������ DB���� �޾ƿ�, NowSettings�� ����
    {
        NowSettings.u_skin_texture = "1";   //!�ӽ� ����!
        print(NowSettings.u_skin_texture);
        tSkin = Resources.Load<Texture>(("Customize/Textures/" + NowSettings.u_skin_texture));
        print(tSkin);
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
                    NowSettings.u_skin_texture = d_dialog[i][CommonField.nTexture].ToString();
                }
                
            }
        }
        //Dictionary<string, object> itemRow = 

        //�ش� ������ row ã����, �ش��ϴ� texture������ �������ش�.
    }

    private void PlayerLook()   //�ܰ� Ŀ���� ������Ʈ
    {

    }
}

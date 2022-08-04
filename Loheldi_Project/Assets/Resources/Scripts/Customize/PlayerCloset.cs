using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCloset : MonoBehaviour
{
    GameObject playerPrefab;
    //PlaterSet.cs ���� �����մϴ�.
    protected Texture tUpper;
    protected Texture tLower;
    protected Texture tSocks;
    protected Texture tShoes;

    protected Mesh meLower_L;
    protected Mesh meLower_R;

    protected Mesh meShoes_L;
    protected Mesh meShoes_R;

    //player's material
    protected Material p_mUpper;
    protected Material p_mLower;
    protected Material p_mSocks;
    protected Material p_mShoes;

    public GameObject p_Lower_L;
    public GameObject p_Lower_R;

    public GameObject p_Shoes_L;
    public GameObject p_Shoes_R;

    public BackendReturnObject closet_chart = null;


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
            Save_Basic.SaveBasicClothes(); //�⺻ �ǻ� ������ ����
            nowClothes();//�����
            return;
        }
        JsonData rows = bro.GetReturnValuetoJSON()["rows"];

        PreviousSettings.u_upper_id = rows[0]["Upper"]["S"].ToString();
        PreviousSettings.u_upper_color = rows[0]["UColor"]["S"].ToString();
        PreviousSettings.u_lower_id = rows[0]["Lower"]["S"].ToString();
        PreviousSettings.u_lower_color = rows[0]["LColor"]["S"].ToString();
        PreviousSettings.u_socks_id = rows[0]["Socks"]["S"].ToString();
        PreviousSettings.u_socks_color = rows[0]["SColor"]["S"].ToString();
        PreviousSettings.u_shoes_id = rows[0]["Shoes"]["S"].ToString();
        PreviousSettings.u_shoes_color = rows[0]["ShColor"]["S"].ToString();
        PreviousSettings.u_hat_id = rows[0]["Hat"]["S"].ToString();
        PreviousSettings.u_glasses_id = rows[0]["Glasses"]["S"].ToString();
        PreviousSettings.u_bag_id = rows[0]["Bag"]["S"].ToString();
    }

    public void ResetClothes()  //���� Ŀ���͸���¡�� �ʱ� Ŀ���͸���¡���� �ʱ�ȭ
    {
        //����
        NowSettings.u_upper_id = PreviousSettings.u_upper_id;
        NowSettings.u_upper_color = PreviousSettings.u_upper_color;
        NowSettings.u_upper_texture = FindTexture(NowSettings.u_upper_id) + "_" + NowSettings.u_upper_color;
        //����
        NowSettings.u_lower_id = PreviousSettings.u_lower_id;
        NowSettings.u_lower_color = PreviousSettings.u_lower_color;
        NowSettings.u_lower_texture = FindTexture(NowSettings.u_lower_id) + "_" + NowSettings.u_lower_color;
        //�縻
        NowSettings.u_socks_id = PreviousSettings.u_socks_id;
        NowSettings.u_socks_color = PreviousSettings.u_socks_color;
        NowSettings.u_socks_texture = FindTexture(NowSettings.u_socks_id) + "_" + NowSettings.u_socks_color;
        //�Ź�
        NowSettings.u_shoes_id = PreviousSettings.u_shoes_id;
        NowSettings.u_shoes_color = PreviousSettings.u_shoes_color;
        NowSettings.u_shoes_texture = FindTexture(NowSettings.u_shoes_id) + "_" + NowSettings.u_shoes_color;
        //����,�Ȱ�,����
        NowSettings.u_hat_id = PreviousSettings.u_hat_id;
        NowSettings.u_glasses_id = PreviousSettings.u_glasses_id;
        NowSettings.u_bag_id = PreviousSettings.u_bag_id;
        //texture �ش� ��ο��� �ҷ�����
        tUpper = Resources.Load<Texture>(("Customize/Textures/Upper/" + NowSettings.u_upper_texture));
        tLower = Resources.Load<Texture>(("Customize/Textures/Lower/" + NowSettings.u_lower_texture));
        tSocks = Resources.Load<Texture>(("Customize/Textures/Socks/" + NowSettings.u_socks_texture));
        tShoes = Resources.Load<Texture>(("Customize/Textures/Shoes/" + NowSettings.u_shoes_texture));
    }

    public void PlayerLook()   //�ܰ� Ŀ���� ������Ʈ
    {
        NowSettings.u_upper_texture = FindTexture(NowSettings.u_upper_id) + "_" + NowSettings.u_upper_color;
        NowSettings.u_lower_texture = FindTexture(NowSettings.u_lower_id) + "_" + NowSettings.u_lower_color;
        NowSettings.u_socks_texture = FindTexture(NowSettings.u_socks_id) + "_" + NowSettings.u_socks_color;
        NowSettings.u_shoes_texture = FindTexture(NowSettings.u_shoes_id) + "_" + NowSettings.u_shoes_color;

        tUpper = Resources.Load<Texture>(("Customize/Textures/Upper/" + NowSettings.u_upper_texture));
        tLower = Resources.Load<Texture>(("Customize/Textures/Lower/" + NowSettings.u_lower_texture));
        tSocks = Resources.Load<Texture>(("Customize/Textures/Socks/" + NowSettings.u_socks_texture));
        tShoes = Resources.Load<Texture>(("Customize/Textures/Shoes/" + NowSettings.u_shoes_texture));

        meLower_L = Resources.Load<Mesh>(("Customize/Mesh/" + NowSettings.u_lower_id + "_L_mesh"));
        meLower_R = Resources.Load<Mesh>(("Customize/Mesh/" + NowSettings.u_lower_id + "_R_mesh"));

        meShoes_L = Resources.Load<Mesh>(("Customize/Mesh/" + NowSettings.u_shoes_id + "_L_mesh"));
        meShoes_R = Resources.Load<Mesh>(("Customize/Mesh/" + NowSettings.u_shoes_id + "_R_mesh"));

        //�÷��̾� �𵨸� �޽�
        p_mUpper = Resources.Load<Material>("Customize/Materials/Upper");
        p_mLower = Resources.Load<Material>("Customize/Materials/Lower");
        p_mSocks = Resources.Load<Material>("Customize/Materials/Socks");
        p_mShoes = Resources.Load<Material>("Customize/Materials/Shoes");

        p_mUpper.SetTexture("_MainTex", tUpper);    //_MainTex: Material�� Albedo texture�Դϴ�. 
        p_mLower.SetTexture("_MainTex", tLower);
        p_mSocks.SetTexture("_MainTex", tSocks);
        p_mShoes.SetTexture("_MainTex", tShoes);
  
        SkinnedMeshRenderer l_mesh_L = p_Lower_L.GetComponent<SkinnedMeshRenderer>();
        SkinnedMeshRenderer l_mesh_R = p_Lower_R.GetComponent<SkinnedMeshRenderer>();

        l_mesh_L.sharedMesh = meLower_L;
        l_mesh_R.sharedMesh = meLower_R;

        SkinnedMeshRenderer sh_mesh_L = p_Shoes_L.GetComponent<SkinnedMeshRenderer>();
        SkinnedMeshRenderer sh_mesh_R = p_Shoes_R.GetComponent<SkinnedMeshRenderer>();

        sh_mesh_L.sharedMesh = meShoes_L;
        sh_mesh_R.sharedMesh = meShoes_R;

        //todo: �Ǽ�����
        playerPrefab = GameObject.Find("Player");
        Transform parents_folder = playerPrefab.transform.Find("player_body");

        //hat init
        Transform hat_folder = parents_folder.transform.Find("Hat");
        int hat_cnt = hat_folder.childCount;
        for (int i = 0; i < hat_cnt; i++)
        {
            hat_folder.GetChild(i).gameObject.SetActive(false);
        }
        //hat�� id�� null�� �ƴ� ���,
        if (!NowSettings.u_hat_id.Equals("null"))
        {
            Transform active_hat = hat_folder.Find(NowSettings.u_hat_id+"_hat");
            active_hat.gameObject.SetActive(true);
        }
        //glasses
        Transform glasses_folder = parents_folder.transform.Find("Glasses");
        int glasses_cnt = glasses_folder.childCount;
        for (int i = 0; i < glasses_cnt; i++)
        {
            glasses_folder.GetChild(i).gameObject.SetActive(false);
        }
        if (!NowSettings.u_glasses_id.Equals("null"))
        {
            Transform active_glasses = glasses_folder.Find(NowSettings.u_glasses_id + "_glasses");
            active_glasses.gameObject.SetActive(true);
        }
        //bag
        Transform bag_folder = parents_folder.transform.Find("Bag");
        int bag_cnt = bag_folder.childCount;

        for (int i = 0; i < bag_cnt; i++)
        {
            bag_folder.GetChild(i).gameObject.SetActive(false);
        }
        if (!NowSettings.u_bag_id.Equals("null"))
        {
            Transform active_bag = bag_folder.Find(NowSettings.u_bag_id + "_bag");
            active_bag.gameObject.SetActive(true);
        }

    }


    //���� ���� ������ ��Ͽ��� �������� Texture ��ȸ �޼ҵ�
    protected string FindTexture(string item_code)
    {
        string item_texture = "null";
        string chart = ChartNum.ClothesItemChart;
        if (closet_chart == null)
        {
            closet_chart = Backend.Chart.GetChartContents(chart);
        }

        JsonData all_rows = closet_chart.GetReturnValuetoJSON()["rows"];
        ParsingJSON pj = new ParsingJSON();

        for (int i = 0; i < all_rows.Count; i++)
        {
            CustomStoreItem data = pj.ParseBackendData<CustomStoreItem>(all_rows[i]);
            if (data.ICode.Equals(item_code))
            {
                item_texture = data.Texture;
            }
        }

        //Debug.Log("FindTexture: " + item_texture);

        return item_texture;
    }
}

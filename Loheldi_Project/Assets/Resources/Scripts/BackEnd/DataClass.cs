using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//정보 저장 클래스

//차트 번호
public static class ChartNum
{
    public static string BasicCustomItemChart = "53501";
    public static string BasicClothesItemChart = "53497";
    public static string CustomItemChart = "53553";
    public static string ClothesItemChart = "54323";
    public static string BadgeChart = "54709";

}

//계정 정보
public class AccInfo
{
    public string NICKNAME;
    public DateTime BIRTH;
    public string PARENTSNO;
}

public class PlayInfo
{
    public int Wallet;
    public int Level;
    public float NowExp;
    public float MaxExp;
    public string QuestPreg;
    public int LastQTime;
}

public class MyItem
{
    public string ICode;
    public int Amount;
}

public class MyCustomItem
{
    public string ICode;
}

//상점 아이템 정보
public class StoreItem
{
    public string ICode;
    public string IName;
    public string Price;
    public string Category;
    public string ItemType;
}
public class CustomStoreItem
{
    public string ICode;
    public string IName;
    public string Price;
    public string Category;
    public string ItemType;
    public string Texture;
}

public class CustomItem
{
    public string ICode;
    public string IName;
    public string Model;
    public string Material;
    public string Texture;
}

public class Badge
{
    public string BCode;
    public string BName;
    public string Bcontent;
    public string Category;
}

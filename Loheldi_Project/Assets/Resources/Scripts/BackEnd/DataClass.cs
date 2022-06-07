using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//정보 저장 클래스


//계정 정보
public class AccInfo
{
    public string NICKNAME;
    public DateTime BIRTH;
}

public class MyItem
{
    public string ICode;
    public int Amount;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSprite2 : MonoBehaviour
{
    Sprite[] sprite;
    public void LoadSprite()
    {
        sprite = Resources.LoadAll<Sprite>("Sprites/Image");
        if (sprite == null) Debug.Log("null");
        for (int i = 0; i < sprite.Length; i++)
        {
            //�� �迭 �� ��ŭ �ݺ��Ͽ� �̸� �ܼ� â�� ���;
            Debug.Log(sprite[i].name);
        }
    }
}

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
            //들어간 배열 수 만큼 반복하여 이름 콘솔 창에 띄움;
            Debug.Log(sprite[i].name);
        }
    }
}

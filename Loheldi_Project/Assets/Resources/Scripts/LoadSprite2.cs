using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSprite2 : MonoBehaviour
{
    public static Sprite sprites;
    public void ResourceLoad()
    {
        string PATH = "Sprites/Image";
        sprites = Resources.Load(PATH, typeof(Sprite)) as Sprite;
        ShopCategorySelect.Buttons1 = LoadSprite.imageObj;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

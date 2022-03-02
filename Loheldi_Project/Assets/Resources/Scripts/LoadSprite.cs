using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSprite : MonoBehaviour
{
    public static SpriteRenderer spriteRenderer;
    public static GameObject imageObj;
            
    // Start is called before the first frame update
    void Start()
    {
        ShopCategorySelect.mokitem = GameObject.FindGameObjectsWithTag("mokitem");

        ShopCategorySelect.itemList = Resources.LoadAll<Sprite>("Sprites/Image");

        /*sprites = Resources.LoadAll<Sprite>("Sprites/Image");
        if (sprites == null) Debug.Log("null");
        for (int i = 0; i < sprites.Length; i++)
        {
            Debug.Log(sprites[i].name);
        }*/
        /*Sprite image = null;
        Image = Resources.LoadAll<Sprite>("Sprites/Image");
        if (Image == null) Debug.Log("null");
        for (int i = 0; i < Image.Length; i++)
        {
            Debug.Log(Image[i].name);
        }
        image = Resources.Load("Sprites/Image", typeof(Sprite)) as Sprite;
        ShopCategorySelect.Buttons1.GetComponent<SpriteRenderer>().sprite = image;*/
        /*ItemImage.Item = GameObject.FindGameObjectsWithTag("Item");
        ItemImage.ItemList = Resources.LoadAll<Sprite>("Sprites/Image");*/
        /*imageObj = GameObject.FindGameObjectWithTag("mokitem");
        image = imageObj.GetComponent<Image>();*/
        /*spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];*/
        /*SpriteRenderer spriteR = gameObject.GetComponent<SpriteRenderer>();
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/Image");
        spriteR.sprite = sprites[0];*/
    }
    private void Awake()
    {
        
    }
    

    /* public void OnClickBox1()
     {
         print("Å¬¸¯");
         foreach (var t in sprites)
         {
             Debug.Log(t);
         }
     }*/

    // Update is called once per frame
    void Update()
    {
        //Func();
    }
    /*void Func()
    {
        sprites = Resources.LoadAll<Sprite>("Sprites/Image") as Sprite;
    }*/
}

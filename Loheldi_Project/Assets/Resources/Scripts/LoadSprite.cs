using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSprite : MonoBehaviour
{
    public GameObject imageObj;
    public Image myImage;
   /* Sprite[] sprites;
    public GameObject pushImage;
    public GameObject pullImage;
    static Image itemImage;*/

    // Start is called before the first frame update
    void Start()
    {
        imageObj = GameObject.FindGameObjectWithTag("mokview");
        myImage = imageObj.GetComponent<Image>();
        
        /*ShopCategorySelect.mokitem = GameObject.FindGameObjectsWithTag("mokitem");

        ShopCategorySelect.itemList = Resources.LoadAll<Sprite>("Sprites/Image");*/

        /*sprites = Resources.LoadAll<Sprite>("Sprites/Image");
        */
        /*Sprite image = null;
        Image = Resources.LoadAll<Sprite>("Sprites/Image");
        
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
    void Awake()
    {
        
    }

    public void clickChangeImage()
    {
        Func();
    }
    /*public void clickImage()
    {
        sprites = Resources.LoadAll<Sprite>("Sprites/Image");

        GameObject imageObj = GameObject.Find("Button");
        itemImage = pushImage.GetComponent<Image>();



    


    }*/

    void Func()
    {
        myImage.sprite = Resources.Load<Sprite>("Sprites/Image");
        /*if (myImage.sprite == null) Debug.Log("null");
        for (int i = 0; i < myImage.sprite.Length; i++)
        {
            Debug.Log(myImage.name);
        }*/
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
        
        
        
    }
    /*void Func()
    {
        sprites = Resources.LoadAll<Sprite>("Sprites/Image") as Sprite;
    }*/
}

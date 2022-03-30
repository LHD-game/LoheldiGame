using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSprite : MonoBehaviour
{
    public GameObject imageObj1;
    public GameObject imageObj2;
    public GameObject imageObj3;
    public GameObject imageObj4;
    public GameObject imageObj5;
    public GameObject imageObj6;

    public Image myImage1;
    public Image myImage2;
    public Image myImage3;
    public Image myImage4;
    public Image myImage5;
    public Image myImage6;
    /* Sprite[] sprites;
     public GameObject pushImage;
     public GameObject pullImage;
     static Image itemImage;*/

    // Start is called before the first frame update
    void Start()
    {
        
        
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

    public void clickChangeImage1()
    {
        Func1();
    }
    public void clickChangeImage2()
    {
        Func2();
    }
    public void clickChangeImage3()
    {
        Func3();
    }
    public void clickChangeImage4()
    {
        Func4();
    }
    public void clickChangeImage5()
    {
        Func5();
    }
    public void clickChangeImage6()
    {
        Func6();
    }

    void Func1()
    {
        imageObj1 = GameObject.FindGameObjectWithTag("mokview");
        myImage1 = imageObj1.GetComponent<Image>();
        myImage1.sprite = Resources.Load<Sprite>("Sprites/Image/Bed");
        
        /*if (myImage.sprite == null) Debug.Log("null");
        for (int i = 0; i < myImage.sprite.Length; i++)
        {
            Debug.Log(myImage.name);
        }*/
    }
    void Func2()
    {
        imageObj2 = GameObject.FindGameObjectWithTag("mokview");
        myImage2 = imageObj2.GetComponent<Image>();
        myImage2.sprite = Resources.Load<Sprite>("Sprites/Image/closet");
    }
    void Func3()
    {
        imageObj3 = GameObject.FindGameObjectWithTag("mokview");
        myImage3 = imageObj3.GetComponent<Image>();
        myImage3.sprite = Resources.Load<Sprite>("Sprites/Image/wash");
    }
    void Func4()
    {
        imageObj4 = GameObject.FindGameObjectWithTag("mokview");
        myImage4 = imageObj4.GetComponent<Image>();
        myImage2.sprite = Resources.Load<Sprite>("Sprites/Image/test2");
    }
    void Func5()
    {
        imageObj5 = GameObject.FindGameObjectWithTag("mokview");
        myImage5 = imageObj5.GetComponent<Image>();
        myImage2.sprite = Resources.Load<Sprite>("Sprites/Image/test2");
    }
    void Func6()
    {
        imageObj6 = GameObject.FindGameObjectWithTag("mokview");
        myImage6 = imageObj6.GetComponent<Image>();
        myImage2.sprite = Resources.Load<Sprite>("Sprites/Image/test2");
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

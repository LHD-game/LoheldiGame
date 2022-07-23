using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedSelect : MonoBehaviour //¾¾¾Ñ ¹öÆ°¿¡ ´Þ¸®µµ·Ï ÇÕ´Ï´Ù.
{
    public Text FarmNum;

    public void Selected()  //¾¾¾ÑÀ» ¼±ÅÃÇÑ´Ù. ¾¾¾ÑÀº ºó ÅÔ¹ç¿¡ ½É°ÜÁöµµ·Ï ÇÑ´Ù.
    {
        for(int i=0; i< GardenControl.empty_ground.Length; i++)
        {
            if (GardenControl.empty_ground[i])  //ºó ÅÔ¹çÀÌ¶ó¸é
            {
                GameObject item_code = this.transform.Find("ItemCode").gameObject;
                Text item_code_txt = item_code.GetComponent<Text>();
                string i_code = item_code_txt.text;

                string ground_num = "G1";
                switch (i){
                    case 0:
                        ground_num = "G1";
                        break;
                    case 1:
                        ground_num = "G2";
                        break;
                    case 2:
                        ground_num = "G3";
                        break;
                    case 3:
                        ground_num = "G4";
                        break;
                    default:
                        break;
                }

                PlayerPrefs.SetString(ground_num, i_code);  //ÇØ´çÇÏ´Â ÅÔ¹ç¿¡, ÇØ´çÇÏ´Â ¾¾¾Ñ ÄÚµå ÀúÀå
                Debug.Log(ground_num);
                Debug.Log(i_code);
                DateTime datetime = DateTime.Now;
                PlayerPrefs.SetString(ground_num + "Time",datetime.ToString("g"));  //ÇØ´çÇÏ´Â ÅÔ¹ç¿¡, ½ÉÀº ½Ã°¢(ÇöÀç½Ã°¢)À» ÀúÀå
                GardenControl.instance.GroundIsUpdated();
                break;
            }
        }
        
    }
}

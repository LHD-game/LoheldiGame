using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedSelect : MonoBehaviour
{
    GameObject TempObject;
    FarmingMaster FarmingMaster;
    public Text FarmNum;

    public Text ItemCode;

    public void Selected()
    {
        if (FarmingMaster.GetComponent<FarmingMaster>().ChosenFarm != null)
            if (FarmingMaster.GetComponent<FarmingMaster>().ChosenFarm.transform.childCount == 0)
            {
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Crops/Crops_GreenPlants"));
                TempObject.transform.SetParent(FarmingMaster.GetComponent<FarmingMaster>().ChosenFarm.transform);
                TempObject.transform.GetChild(0).GetComponent<Text>().text = ItemCode.text;
                TempObject.transform.localPosition = new Vector3(0, 0, 0);
                Debug.Log(FarmingMaster.GetComponent<FarmingMaster>().ChosenFarm);
            }
    }
    public void SelectedAuto(string ItemCode1, string ItemCode2, string ItemCode3, string ItemCode4, GameObject Farm)
    {
        if (ItemCode1 != null)
        {
            TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Crops/Crops_GreenPlants"));
            TempObject.transform.SetParent(Farm.transform.GetChild(0).transform);
            TempObject.transform.GetChild(0).GetComponent<Text>().text = ItemCode1;
            TempObject.transform.localPosition = new Vector3(0, 0, 0);
        }
        if (ItemCode2 != null)
        {
            TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Crops/Crops_GreenPlants"));
            TempObject.transform.SetParent(Farm.transform.GetChild(1).transform);
            TempObject.transform.GetChild(0).GetComponent<Text>().text = ItemCode2;
            TempObject.transform.localPosition = new Vector3(0, 0, 0);
        }
        if (ItemCode3 != null)
        {
            TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Crops/Crops_GreenPlants"));
            TempObject.transform.SetParent(Farm.transform.GetChild(2).transform);
            TempObject.transform.GetChild(0).GetComponent<Text>().text = ItemCode3;
            TempObject.transform.localPosition = new Vector3(0, 0, 0);
        }
        if (ItemCode4 != null)
        {
            TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Crops/Crops_GreenPlants"));
            TempObject.transform.SetParent(Farm.transform.GetChild(3).transform);
            TempObject.transform.GetChild(0).GetComponent<Text>().text = ItemCode4;
            TempObject.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
}

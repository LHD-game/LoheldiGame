using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedSelect : MonoBehaviour
{
    GameObject TempObject;
    GameObject FarmingMaster;

    public Text ItemCode;

    public void Selected()
    {
        FarmingMaster = GameObject.Find("InventoryManager");
        TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Crops/Crops_GreenPlants"));
        TempObject.transform.SetParent(FarmingMaster.GetComponent<FarmingMaster>().ChosenFarm.transform);
        TempObject.transform.GetChild(0).GetComponent<Text>().text = ItemCode.text;
        TempObject.transform.localPosition = new Vector3 (0, 0, 0);
        Debug.Log(FarmingMaster.GetComponent<FarmingMaster>().ChosenFarm);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "HudController", menuName = "Scripts/ScriptableObjects/HudController", order = 1)]
public class HudController : ScriptableObject
{
    public GameObject lightWeaponSelected;
    public GameObject heavyWeaponSelected;

    public void LightHUDActivate()
    {   
        Debug.Log("Light Selected Image");
        lightWeaponSelected.transform.GetChild(0).GetComponent<Image>().enabled = true;
        heavyWeaponSelected.transform.GetChild(0).GetComponent<Image>().enabled = false;
    }

    public void HeavyHUDActivate()
    {
        Debug.Log("Heavy Selected Image");
        lightWeaponSelected.transform.GetChild(0).GetComponent<Image>().enabled = false;
        heavyWeaponSelected.transform.GetChild(0).GetComponent<Image>().enabled = true;
    }
}

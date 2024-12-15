using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoidChecking : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject EnergyBar1;
    [SerializeField] GameObject EnergyBar2;
    [SerializeField] GameObject EnergyBar3;
    [SerializeField] GameObject EnergyBar4;
    [SerializeField] GameObject EnergyBar5;
    [SerializeField] GameObject VoidButton;
    void Awake()
    {
        VoidGauge();
    }

    private Color HexToColor(string hexColor)
    {
        hexColor = hexColor.Replace("#", "");
        byte r = byte.Parse(hexColor.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hexColor.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hexColor.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);   

        return new Color32(r, g, b, 255);
    }
    // Update is called once per frame
    public void VoidGauge(){
        if(Data_Player.VoidAccess > 0)
            EnergyBar1.GetComponent<Image>().color = HexToColor("#FF1000");
        else
            EnergyBar1.GetComponent<Image>().color = HexToColor("#D6D6D6");
        
        if(Data_Player.VoidAccess > 1)
            EnergyBar2.GetComponent<Image>().color = HexToColor("#FF1000");
        else
            EnergyBar2.GetComponent<Image>().color = HexToColor("#D6D6D6");

        if(Data_Player.VoidAccess > 2)
            EnergyBar3.GetComponent<Image>().color = HexToColor("#FF1000");
        else
            EnergyBar3.GetComponent<Image>().color = HexToColor("#D6D6D6");

        if(Data_Player.VoidAccess > 3)
            EnergyBar4.GetComponent<Image>().color = HexToColor("#FF1000");
        else
            EnergyBar4.GetComponent<Image>().color = HexToColor("#D6D6D6");

        if(Data_Player.VoidAccess > 4)
            EnergyBar5.GetComponent<Image>().color = HexToColor("#FF1000");
        else
            EnergyBar5.GetComponent<Image>().color = HexToColor("#D6D6D6");
        
        if(Data_Player.VoidAccess >= 5)
                VoidButton.GetComponent<Button>().interactable = true;
    }
}

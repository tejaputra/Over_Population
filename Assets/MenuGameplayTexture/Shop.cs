using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] float timerBuyColor = 2f;
    private float timerRemainder = 2f;
    bool BuySuccess = false;
    bool AwakeOnce = true;
    [SerializeField] GameObject OtherworlderBody;
    [SerializeField] GameObject OtherworlderWeapon;
    [SerializeField] GameObject OtherworlderAbility;
    [SerializeField] GameObject BodyShop;
    [SerializeField] GameObject WeaponShop;
    [SerializeField] GameObject AbilityShop;

    [Header("BodyShop")]

    [SerializeField] GameObject HealthText;
    [SerializeField] GameObject HealthDetailText;
    [SerializeField] GameObject HealthExplanationText;

    [SerializeField] GameObject EnergyRecoveryText;
    [SerializeField] GameObject EnergyRecoveryDetailText;
    [SerializeField] GameObject EnergyRecoveryExplanationText;

    [SerializeField] GameObject MovementSpeedText;
    [SerializeField] GameObject MovementSpeedDetailText;
    [SerializeField] GameObject MovementSpeedExplanationText;

    [Header("WeaponShop")]
    [SerializeField] GameObject SwordAttackText;
    [SerializeField] GameObject SwordAttackDetailText;
    [SerializeField] GameObject SwordAttackExplanationText;

    [SerializeField] GameObject SwordAttackSpeedText;
    [SerializeField] GameObject SwordAttackSpeedDetailText;
    [SerializeField] GameObject SwordAttackSpeedExplanationText;

    [SerializeField] GameObject ScorpioAtackText;
    [SerializeField] GameObject ScorpioAtackDetailText;
    [SerializeField] GameObject ScorpioAtackExplanationText;

    [SerializeField] GameObject ScorpioAttackSpeedText;
    [SerializeField] GameObject ScorpioAttackSpeedDetailText;
    [SerializeField] GameObject ScorpioAttackSpeedExplanationText;

    [SerializeField] GameObject WandAttackText;
    [SerializeField] GameObject WandAttackDetailText;
    [SerializeField] GameObject WandAttackExplanationText;

    [SerializeField] GameObject WandAttackRadiusText;
    [SerializeField] GameObject WandAttackRadiusDetailText;
    [SerializeField] GameObject WandAttackRadiusExplanationText;

    [SerializeField] GameObject WandAttackSpeedText;
    [SerializeField] GameObject WandAttackSpeedDetailText;
    [SerializeField] GameObject WandAttackSpeedExplanationText;

    [Header("AbilityShop")]
    [SerializeField] GameObject Ability1Text;
    [SerializeField] GameObject Ability2Text;
    [SerializeField] GameObject Ability3Text;
    
    Setting MenuSetting;

    void Awake()
    {
        timerRemainder = 0;
        UdpdateShop();
    }

    // Update is called once per frame
    void Start()
    {
        MenuSetting = GameObject.FindGameObjectWithTag("SettingObject").GetComponent<Setting>();
    }

    void FixedUpdate()
    {
        if(BuySuccess && timerRemainder < timerBuyColor)
        {
            timerRemainder += Time.deltaTime;
        }
        else if (BuySuccess && timerRemainder > timerBuyColor)
        {
            timerRemainder = 0;
            GameObject.FindGameObjectWithTag("Money").GetComponent<TextMeshProUGUI>().color = Color.white;
            BuySuccess = false;
        }

    }
    private void BuyConfirm(){
        BuySuccess = true;
        GameObject.FindGameObjectWithTag("Money").GetComponent<TextMeshProUGUI>().color = Color.red;
    }


    private void UdpdateShop(){
        GameObject.FindGameObjectWithTag("Money").GetComponent<TextMeshProUGUI>().text = "$ "+ Data_Player.Point.ToString();
        if(AwakeOnce)
            AwakeOnce = false;
        else
            BuyConfirm();
        //body

        HealthDetailText.GetComponent<TextMeshProUGUI>().text = "Health";
        if(Data_Player.Shop_Health_QuantityRemainder < Data_Player.Shop_Health_Quantity)
        {
            HealthExplanationText.GetComponent<TextMeshProUGUI>().text = Data_Player.MaxHealth.ToString("F2") + " >>> " + (Data_Player.MaxHealth + Data_Player.Shop_Health_Increased).ToString("F2");
            HealthText.GetComponent<TextMeshProUGUI>().text = "$ " + Data_Player.Shop_Health_Cost + "    ";
        }
        else
        {
            HealthExplanationText.GetComponent<TextMeshProUGUI>().text = "Max Upgraded";
            HealthText.GetComponent<TextMeshProUGUI>().text = "-";
        }

        EnergyRecoveryDetailText.GetComponent<TextMeshProUGUI>().text = "Energy Recovery";
        if(Data_Player.Shop_EnergyRecovery_QuantityRemainder < Data_Player.Shop_EnergyRecovery_Quantity)
        {
            EnergyRecoveryExplanationText.GetComponent<TextMeshProUGUI>().text = Data_Player.EnergyRecovery.ToString("F2") + "/s >>> " + (Data_Player.EnergyRecovery + Data_Player.Shop_EnergyRecovery_Increased).ToString("F2") + "/s";
            EnergyRecoveryText.GetComponent<TextMeshProUGUI>().text = "$ " + Data_Player.Shop_EnergyRecovery_Cost + "    ";
        }
        else
        {
            EnergyRecoveryExplanationText.GetComponent<TextMeshProUGUI>().text = "Max Upgraded";
            EnergyRecoveryText.GetComponent<TextMeshProUGUI>().text = "-";
        }

        MovementSpeedDetailText.GetComponent<TextMeshProUGUI>().text = "Movement Speed";
        if(Data_Player.Shop_MovementSpeed_QuantityRemainder < Data_Player.Shop_MovementSpeed_Quantity)
        {
            MovementSpeedExplanationText.GetComponent<TextMeshProUGUI>().text = Data_Player.MovementSpeed.ToString("F2") + " >>> " + (Data_Player.MovementSpeed + Data_Player.Shop_MovementSpeed_Increased).ToString("F2");
            MovementSpeedText.GetComponent<TextMeshProUGUI>().text = "$ " + Data_Player.Shop_MovementSpeed_Cost + "    ";
        }
        else
        {
            MovementSpeedExplanationText.GetComponent<TextMeshProUGUI>().text = "Max Upgraded";
            MovementSpeedText.GetComponent<TextMeshProUGUI>().text = "-";
        }

        //weapon

        SwordAttackDetailText.GetComponent<TextMeshProUGUI>().text = "Sword Attack";
        if(Data_Player.Shop_SwordAttack_QuantityRemainder < Data_Player.Shop_SwordAttack_Quantity)
        {
            SwordAttackExplanationText.GetComponent<TextMeshProUGUI>().text = Data_Player.SwordAttack.ToString("F2") + " >>> " + (Data_Player.SwordAttack + Data_Player.Shop_SwordAttack_Increased).ToString("F2");
            SwordAttackText.GetComponent<TextMeshProUGUI>().text = "$ " + Data_Player.Shop_SwordAttack_Cost + "    ";
        }
        else
        {
            SwordAttackExplanationText.GetComponent<TextMeshProUGUI>().text = "Max Upgraded";
            SwordAttackText.GetComponent<TextMeshProUGUI>().text = "-";
        }

        SwordAttackSpeedDetailText.GetComponent<TextMeshProUGUI>().text = "Sword Attack Speed";
        if(Data_Player.Shop_SwordAttackSpeed_QuantityRemainder < Data_Player.Shop_SwordAttackSpeed_Quantity)
        {
            SwordAttackSpeedExplanationText.GetComponent<TextMeshProUGUI>().text = Data_Player.SwordAttackSpeed.ToString("F2") + "/s >>> " + (Data_Player.SwordAttackSpeed - Data_Player.Shop_SwordAttackSpeed_Increased).ToString("F2") + "/s";
            SwordAttackSpeedText.GetComponent<TextMeshProUGUI>().text = "$ " + Data_Player.Shop_SwordAttackSpeed_Cost + "    ";
        }
        else
        {
            SwordAttackSpeedExplanationText.GetComponent<TextMeshProUGUI>().text = "Max Upgraded";
            SwordAttackSpeedText.GetComponent<TextMeshProUGUI>().text = "-";
        }

        ScorpioAtackDetailText.GetComponent<TextMeshProUGUI>().text = "Scorpio Attack";
        if(Data_Player.Shop_ScorpionAttack_QuantityRemainder < Data_Player.Shop_ScorpionAttack_Quantity)
        {
            ScorpioAtackExplanationText.GetComponent<TextMeshProUGUI>().text = Data_Player.ScorpionAttack.ToString("F2") + " >>> " + (Data_Player.ScorpionAttack + Data_Player.Shop_ScorpionAttack_Increased).ToString("F2");
            ScorpioAtackText.GetComponent<TextMeshProUGUI>().text = "$ " + Data_Player.Shop_ScorpionAttack_Cost + "    ";
        }
        else
        {
            ScorpioAtackExplanationText.GetComponent<TextMeshProUGUI>().text = "Max Upgraded";
            ScorpioAtackText.GetComponent<TextMeshProUGUI>().text = "-";
        }

        ScorpioAttackSpeedDetailText.GetComponent<TextMeshProUGUI>().text = "Scorpio Attack Speed";
        if(Data_Player.Shop_ScorpionAttackSpeed_QuantityRemainder < Data_Player.Shop_ScorpionAttackSpeed_Quantity)
        {
            ScorpioAttackSpeedExplanationText.GetComponent<TextMeshProUGUI>().text = Data_Player.ScorpionAttackSpeed.ToString("F2") + "/s >>> " + (Data_Player.ScorpionAttackSpeed - Data_Player.Shop_ScorpionAttackSpeed_Increased).ToString("F2") + "/s";
            ScorpioAttackSpeedText.GetComponent<TextMeshProUGUI>().text = "$ " + Data_Player.Shop_ScorpionAttackSpeed_Cost + "    ";
        }
        else
        {
            ScorpioAttackSpeedExplanationText.GetComponent<TextMeshProUGUI>().text = "Max Upgraded";
            ScorpioAttackSpeedText.GetComponent<TextMeshProUGUI>().text = "-";
        }

        WandAttackDetailText.GetComponent<TextMeshProUGUI>().text = "Wand Attack";
        if(Data_Player.Shop_WandAttack_QuantityRemainder < Data_Player.Shop_WandAttack_Quantity)
        {
            WandAttackExplanationText.GetComponent<TextMeshProUGUI>().text = Data_Player.WandAttack.ToString("F2") + " >>> " + (Data_Player.WandAttack + Data_Player.Shop_WandAttack_Increased).ToString("F2");
            WandAttackText.GetComponent<TextMeshProUGUI>().text = "$ " + Data_Player.Shop_WandAttack_Cost + "    ";
        }
        else
        {
            WandAttackExplanationText.GetComponent<TextMeshProUGUI>().text = "Max Upgraded";
            WandAttackText.GetComponent<TextMeshProUGUI>().text = "-";
        }

        WandAttackRadiusDetailText.GetComponent<TextMeshProUGUI>().text = "Wand Attack Radius";
        if(Data_Player.Shop_WandAttackRadius_QuantityRemainder < Data_Player.Shop_WandAttackRadius_Quantity)
        {
            WandAttackRadiusExplanationText.GetComponent<TextMeshProUGUI>().text = Data_Player.WandAttackRadius.ToString("F2") + " >>> " + (Data_Player.WandAttackRadius + Data_Player.Shop_WandAttackRadius_Increased).ToString("F2");
            WandAttackRadiusText.GetComponent<TextMeshProUGUI>().text = "$ " + Data_Player.Shop_WandAttackRadius_Cost + "    ";
        }
        else
        {
            WandAttackRadiusExplanationText.GetComponent<TextMeshProUGUI>().text = "Max Upgraded";
            WandAttackRadiusText.GetComponent<TextMeshProUGUI>().text = "-";
        }

        WandAttackSpeedDetailText.GetComponent<TextMeshProUGUI>().text = "Wand Attack Speed";
        if(Data_Player.Shop_WandAttackSpeed_QuantityRemainder < Data_Player.Shop_WandAttackSpeed_Quantity)
        {
            WandAttackSpeedExplanationText.GetComponent<TextMeshProUGUI>().text = Data_Player.WandAttackSpeed.ToString("F2") + "/s >>> " + (Data_Player.WandAttackSpeed - Data_Player.Shop_WandAttackSpeed_Increased).ToString("F2") + "/s";
            WandAttackSpeedText.GetComponent<TextMeshProUGUI>().text = "$ " + Data_Player.Shop_WandAttackSpeed_Cost + "    ";
        }
        else
        {
            WandAttackSpeedExplanationText.GetComponent<TextMeshProUGUI>().text = "Max Upgraded";
            WandAttackSpeedText.GetComponent<TextMeshProUGUI>().text = "-";
        }

        //Ability

        Ability1Text.GetComponent<TextMeshProUGUI>().text = "Ability1 " + 0;
        Ability2Text.GetComponent<TextMeshProUGUI>().text = "Ability2 " + 0;
        Ability3Text.GetComponent<TextMeshProUGUI>().text = "Ability3" + 0;
    }

    // function shop menu
    public void OpenBodyShop(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);

        OtherworlderBody.GetComponent<Button>().interactable = false;
        OtherworlderWeapon.GetComponent<Button>().interactable = true;
        OtherworlderAbility.GetComponent<Button>().interactable = true;

        BodyShop.SetActive(true);
        WeaponShop.SetActive(false);
        AbilityShop.SetActive(false);
    }
    public void OpenWeaponShop(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);
        OtherworlderBody.GetComponent<Button>().interactable = true;
        OtherworlderWeapon.GetComponent<Button>().interactable = false;
        OtherworlderAbility.GetComponent<Button>().interactable = true;

        BodyShop.SetActive(false);
        WeaponShop.SetActive(true);
        AbilityShop.SetActive(false);
    }
    public void OpenAbilityShop(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);

        OtherworlderBody.GetComponent<Button>().interactable = true;
        OtherworlderWeapon.GetComponent<Button>().interactable = true;
        OtherworlderAbility.GetComponent<Button>().interactable = false;

        BodyShop.SetActive(false);
        WeaponShop.SetActive(false);
        AbilityShop.SetActive(true);
    }
    //Body Shop Button
    public void BuyHealth(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);

        if(Data_Player.Point >= Data_Player.Shop_Health_Cost && Data_Player.Shop_Health_QuantityRemainder < Data_Player.Shop_Health_Quantity)
        {
            Data_Player.MaxHealth += Data_Player.Shop_Health_Increased;
            Data_Player.Point -= Data_Player.Shop_Health_Cost;
            
            if(Data_Player.Shop_Health_QuantityRemainder == 0)
            {
                Data_Player.Shop_Health_Cost = Data_Player.Shop_Health_Cost * 2;
                Data_Player.Shop_Health_QuantityRemainder++;
            }
            else
            {
            Data_Player.Shop_Health_Cost = Data_Player.Shop_Health_Cost / Data_Player.Shop_Health_QuantityRemainder;
            Data_Player.Shop_Health_QuantityRemainder++;
            Data_Player.Shop_Health_Cost = Data_Player.Shop_Health_Cost * Data_Player.Shop_Health_QuantityRemainder;
            }

            UdpdateShop();
        }
    }
    public void BuyEnergy(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);
        
        if(Data_Player.Point >= Data_Player.Shop_EnergyRecovery_Cost && Data_Player.Shop_EnergyRecovery_QuantityRemainder < Data_Player.Shop_EnergyRecovery_Quantity)
        {
            Data_Player.EnergyRecovery += Data_Player.Shop_EnergyRecovery_Increased;
            Data_Player.Point -= Data_Player.Shop_EnergyRecovery_Cost;
            
            if(Data_Player.Shop_EnergyRecovery_QuantityRemainder == 0)
            {
                Data_Player.Shop_EnergyRecovery_Cost = Data_Player.Shop_EnergyRecovery_Cost * 2;
                Data_Player.Shop_EnergyRecovery_QuantityRemainder++;
            }
            else
            {
            Data_Player.Shop_EnergyRecovery_Cost = Data_Player.Shop_EnergyRecovery_Cost / Data_Player.Shop_EnergyRecovery_QuantityRemainder;
            Data_Player.Shop_EnergyRecovery_QuantityRemainder++;
            Data_Player.Shop_EnergyRecovery_Cost = Data_Player.Shop_EnergyRecovery_Cost * Data_Player.Shop_EnergyRecovery_QuantityRemainder;
            }
            UdpdateShop();
        }
    }
    public void BuyMovement(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);
        
        if(Data_Player.Point >= Data_Player.Shop_MovementSpeed_Cost && Data_Player.Shop_MovementSpeed_QuantityRemainder < Data_Player.Shop_MovementSpeed_Quantity)
        {
            Data_Player.MovementSpeed += Data_Player.Shop_MovementSpeed_Increased;
            Data_Player.Point -= Data_Player.Shop_MovementSpeed_Cost;
            
            if(Data_Player.Shop_MovementSpeed_QuantityRemainder == 0)
            {
                Data_Player.Shop_MovementSpeed_Cost = Data_Player.Shop_MovementSpeed_Cost * 2;
                Data_Player.Shop_MovementSpeed_QuantityRemainder++;
            }
            else
            {
            Data_Player.Shop_MovementSpeed_Cost = Data_Player.Shop_MovementSpeed_Cost / Data_Player.Shop_MovementSpeed_QuantityRemainder;
            Data_Player.Shop_MovementSpeed_QuantityRemainder++;
            Data_Player.Shop_MovementSpeed_Cost = Data_Player.Shop_MovementSpeed_Cost * Data_Player.Shop_MovementSpeed_QuantityRemainder;
            }

            UdpdateShop();
        }
    }
    //Weapon Shop Button
    public void BuySwordAttack(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);
        
        if(Data_Player.Point >= Data_Player.Shop_SwordAttack_Cost && Data_Player.Shop_SwordAttack_QuantityRemainder < Data_Player.Shop_SwordAttack_Quantity)
        {
            Data_Player.SwordAttack += Data_Player.Shop_SwordAttack_Increased;
            Data_Player.Point -= Data_Player.Shop_SwordAttack_Cost;
            
            if(Data_Player.Shop_SwordAttack_QuantityRemainder == 0)
            {
                Data_Player.Shop_SwordAttack_Cost = Data_Player.Shop_SwordAttack_Cost * 2;
                Data_Player.Shop_SwordAttack_QuantityRemainder++;
            }
            else
            {
            Data_Player.Shop_SwordAttack_Cost = Data_Player.Shop_SwordAttack_Cost / Data_Player.Shop_SwordAttack_QuantityRemainder;
            Data_Player.Shop_SwordAttack_QuantityRemainder++;
            Data_Player.Shop_SwordAttack_Cost = Data_Player.Shop_SwordAttack_Cost * Data_Player.Shop_SwordAttack_QuantityRemainder;
            }

            UdpdateShop();
        }
    }
    public void BuySwordAtackSpeed(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);
        
        if(Data_Player.Point >= Data_Player.Shop_SwordAttackSpeed_Cost && Data_Player.Shop_SwordAttackSpeed_QuantityRemainder < Data_Player.Shop_SwordAttackSpeed_Quantity)
        {
            Data_Player.SwordAttackSpeed -= Data_Player.Shop_SwordAttackSpeed_Increased;
            Data_Player.Point -= Data_Player.Shop_SwordAttackSpeed_Cost;
            
            if(Data_Player.Shop_SwordAttackSpeed_QuantityRemainder == 0)
            {
                Data_Player.Shop_SwordAttackSpeed_Cost = Data_Player.Shop_SwordAttackSpeed_Cost * 2;
                Data_Player.Shop_SwordAttackSpeed_QuantityRemainder++;
            }
            else
            {
            Data_Player.Shop_SwordAttackSpeed_Cost = Data_Player.Shop_SwordAttackSpeed_Cost / Data_Player.Shop_SwordAttackSpeed_QuantityRemainder;
            Data_Player.Shop_SwordAttackSpeed_QuantityRemainder++;
            Data_Player.Shop_SwordAttackSpeed_Cost = Data_Player.Shop_SwordAttackSpeed_Cost * Data_Player.Shop_SwordAttackSpeed_QuantityRemainder;
            }

            UdpdateShop();
        }
    }
    public void BuyScorpionAttack(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);

        if(Data_Player.Point >= Data_Player.Shop_ScorpionAttack_Cost && Data_Player.Shop_ScorpionAttack_QuantityRemainder < Data_Player.Shop_ScorpionAttack_Quantity)
        {
            Data_Player.ScorpionAttack += Data_Player.Shop_ScorpionAttack_Increased;
            Data_Player.Point -= Data_Player.Shop_ScorpionAttack_Cost;
            
            if(Data_Player.Shop_ScorpionAttack_QuantityRemainder == 0)
            {
                Data_Player.Shop_ScorpionAttack_Cost = Data_Player.Shop_ScorpionAttack_Cost * 2;
                Data_Player.Shop_ScorpionAttack_QuantityRemainder++;
            }
            else
            {
            Data_Player.Shop_ScorpionAttack_Cost = Data_Player.Shop_ScorpionAttack_Cost / Data_Player.Shop_ScorpionAttack_QuantityRemainder;
            Data_Player.Shop_ScorpionAttack_QuantityRemainder++;
            Data_Player.Shop_ScorpionAttack_Cost = Data_Player.Shop_ScorpionAttack_Cost * Data_Player.Shop_ScorpionAttack_QuantityRemainder;
            }

            UdpdateShop();
        }
    }
    public void BuyScorpionAttackSpeed(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);
        
        if(Data_Player.Point >= Data_Player.Shop_ScorpionAttackSpeed_Cost && Data_Player.Shop_ScorpionAttackSpeed_QuantityRemainder < Data_Player.Shop_ScorpionAttackSpeed_Quantity)
        {
            Data_Player.ScorpionAttackSpeed -= Data_Player.Shop_ScorpionAttackSpeed_Increased;
            Data_Player.Point -= Data_Player.Shop_ScorpionAttackSpeed_Cost;
            
            if(Data_Player.Shop_ScorpionAttackSpeed_QuantityRemainder == 0)
            {
                Data_Player.Shop_ScorpionAttackSpeed_Cost = Data_Player.Shop_ScorpionAttackSpeed_Cost * 2;
                Data_Player.Shop_ScorpionAttackSpeed_QuantityRemainder++;
            }
            else
            {
            Data_Player.Shop_ScorpionAttackSpeed_Cost = Data_Player.Shop_ScorpionAttackSpeed_Cost / Data_Player.Shop_ScorpionAttackSpeed_QuantityRemainder;
            Data_Player.Shop_ScorpionAttackSpeed_QuantityRemainder++;
            Data_Player.Shop_ScorpionAttackSpeed_Cost = Data_Player.Shop_ScorpionAttackSpeed_Cost * Data_Player.Shop_ScorpionAttackSpeed_QuantityRemainder;
            }

            UdpdateShop();
        }
    }
    public void BuyWandAttack(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);
        
        if(Data_Player.Point >= Data_Player.Shop_WandAttack_Cost && Data_Player.Shop_WandAttack_QuantityRemainder < Data_Player.Shop_WandAttack_Quantity)
        {
            Data_Player.WandAttack += Data_Player.Shop_WandAttack_Increased;
            Data_Player.Point -= Data_Player.Shop_WandAttack_Cost;
            
            if(Data_Player.Shop_WandAttack_QuantityRemainder == 0)
            {
                Data_Player.Shop_WandAttack_Cost = Data_Player.Shop_WandAttack_Cost * 2;
                Data_Player.Shop_WandAttack_QuantityRemainder++;
            }
            else
            {
            Data_Player.Shop_WandAttack_Cost = Data_Player.Shop_WandAttack_Cost / Data_Player.Shop_WandAttack_QuantityRemainder;
            Data_Player.Shop_WandAttack_QuantityRemainder++;
            Data_Player.Shop_WandAttack_Cost = Data_Player.Shop_WandAttack_Cost * Data_Player.Shop_WandAttack_QuantityRemainder;
            }

            UdpdateShop();
        }
    }
    public void BuyWandAttackRadius(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);
        
        if(Data_Player.Point >= Data_Player.Shop_WandAttackRadius_Cost && Data_Player.Shop_WandAttackRadius_QuantityRemainder < Data_Player.Shop_WandAttackRadius_Quantity)
        {
            Data_Player.WandAttackRadius += Data_Player.Shop_WandAttackRadius_Increased;
            Data_Player.Point -= Data_Player.Shop_WandAttackRadius_Cost;
            
            if(Data_Player.Shop_WandAttackRadius_QuantityRemainder == 0)
            {
                Data_Player.Shop_WandAttackRadius_Cost = Data_Player.Shop_WandAttackRadius_Cost * 2;
                Data_Player.Shop_WandAttackRadius_QuantityRemainder++;
            }
            else
            {
            Data_Player.Shop_WandAttackRadius_Cost = Data_Player.Shop_WandAttackRadius_Cost / Data_Player.Shop_WandAttackRadius_QuantityRemainder;
            Data_Player.Shop_WandAttackRadius_QuantityRemainder++;
            Data_Player.Shop_WandAttackRadius_Cost = Data_Player.Shop_WandAttackRadius_Cost * Data_Player.Shop_WandAttackRadius_QuantityRemainder;
            }

            UdpdateShop();
        }
    }
    public void BuyWandAttackSpeed(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);
        
        if(Data_Player.Point >= Data_Player.Shop_WandAttackSpeed_Cost && Data_Player.Shop_WandAttackSpeed_QuantityRemainder < Data_Player.Shop_WandAttackSpeed_Quantity)
        {
            Data_Player.WandAttackSpeed -= Data_Player.Shop_WandAttackSpeed_Increased;
            Data_Player.Point -= Data_Player.Shop_WandAttackSpeed_Cost;
            
            if(Data_Player.Shop_WandAttackSpeed_QuantityRemainder == 0)
            {
                Data_Player.Shop_WandAttackSpeed_Cost = Data_Player.Shop_WandAttackSpeed_Cost * 2;
                Data_Player.Shop_WandAttackSpeed_QuantityRemainder++;
            }
            else
            {
            Data_Player.Shop_WandAttackSpeed_Cost = Data_Player.Shop_WandAttackSpeed_Cost / Data_Player.Shop_WandAttackSpeed_QuantityRemainder;
            Data_Player.Shop_WandAttackSpeed_QuantityRemainder++;
            Data_Player.Shop_WandAttackSpeed_Cost = Data_Player.Shop_WandAttackSpeed_Cost * Data_Player.Shop_WandAttackSpeed_QuantityRemainder;
            }

            UdpdateShop();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Player : MonoBehaviour
{
    void Start() {
    }
    public static int mode = 2;

    public static int VoidAccess = 0;
    
    //body
        [SerializeField] public static float MaxHealth = 15;
        [SerializeField] public static float CurrentEnergy = 100;
        [SerializeField] public static float MaxEnergy = 100;
        [SerializeField] public static float EnergyRecovery = 0.1f;
        [SerializeField] public static float MovementSpeed = 4f;
        [SerializeField] public static float Point = 3000f;

    //weapon

        //Sword
        [SerializeField] public static float SwordAttack = 5;
        [SerializeField] public static float SwordAttackSpeed = 1;
        [SerializeField] public static float SwordAttackEnergy = 2;

        [SerializeField] public static float LSwordAttack = 2;
        [SerializeField] public static float LSwordAttackSpeed = 0.04f;
        
        //Scorpion
        [SerializeField] public static float ScorpionAttack = 2;
        [SerializeField] public static float ScorpionAttackSpeed = 0.5f;
        [SerializeField] public static float ScorpionAttackEnergy = 1;

        
        [SerializeField] public static float LScorpionAttack = 0.4f;
        [SerializeField] public static float LScorpionAttackSpeed = 0.04f;

        //Wand
        [SerializeField] public static float WandAttack = 5;
        [SerializeField] public static float WandAttackRadius = 2;
        [SerializeField] public static float WandAttackSpeed = 3;
        [SerializeField] public static float WandAttackEnergy = 4;

        [SerializeField] public static float LWandAttack = 1.5f;
        [SerializeField] public static float LWandAttackRadius = 0.4f;
        [SerializeField] public static float LWandAttackSpeed = 0.1f;

        //settings
        public static bool _status_Tutorial = false;

    //=============================================================================================
    //Shop
        //Shop Body
        [SerializeField] public static float Shop_Health_Increased = 1.5f;
        [SerializeField] public static float Shop_Health_Cost = 1.5f;
        [SerializeField] public static int Shop_Health_Quantity = 10;
        public static int Shop_Health_QuantityRemainder = 0;
    
        [SerializeField] public static float Shop_EnergyRecovery_Increased = 0.5f;
        [SerializeField] public static float Shop_EnergyRecovery_Cost = 10f;
        [SerializeField] public static int Shop_EnergyRecovery_Quantity = 5;
        public static int Shop_EnergyRecovery_QuantityRemainder = 0;

        [SerializeField] public static float Shop_MovementSpeed_Increased = 1f;
        [SerializeField] public static float Shop_MovementSpeed_Cost = 4f;
        [SerializeField] public static int Shop_MovementSpeed_Quantity = 3;
        public static int Shop_MovementSpeed_QuantityRemainder = 0;

        //Shop Sword
        [SerializeField] public static float Shop_SwordAttack_Increased = 0.4f;
        [SerializeField] public static float Shop_SwordAttack_Cost = 4f;
        [SerializeField] public static int Shop_SwordAttack_Quantity = 10;
        public static int Shop_SwordAttack_QuantityRemainder = 0;

        [SerializeField] public static float Shop_SwordAttackSpeed_Increased = 0.04f;
        [SerializeField] public static float Shop_SwordAttackSpeed_Cost = 5f;
        [SerializeField] public static int Shop_SwordAttackSpeed_Quantity = 7;
        public static int Shop_SwordAttackSpeed_QuantityRemainder = 0;

        //Shop Scorpion
        [SerializeField] public static float Shop_ScorpionAttack_Increased = 0.5f;
        [SerializeField] public static float Shop_ScorpionAttack_Cost = 4f;
        [SerializeField] public static int Shop_ScorpionAttack_Quantity = 10;
        public static int Shop_ScorpionAttack_QuantityRemainder = 0;

        [SerializeField] public static float Shop_ScorpionAttackSpeed_Increased = 0.02f;
        [SerializeField] public static float Shop_ScorpionAttackSpeed_Cost = 5f;
        [SerializeField] public static int Shop_ScorpionAttackSpeed_Quantity = 5;
        public static int Shop_ScorpionAttackSpeed_QuantityRemainder = 0;

        //Shop Wand
        [SerializeField] public static float Shop_WandAttack_Increased = 0.8f;
        [SerializeField] public static float Shop_WandAttack_Cost = 4f;
        [SerializeField] public static int Shop_WandAttack_Quantity = 10;
        public static int Shop_WandAttack_QuantityRemainder = 0;

        [SerializeField] public static float Shop_WandAttackRadius_Increased = 0.2f;
        [SerializeField] public static float Shop_WandAttackRadius_Cost = 10f;
        [SerializeField] public static int Shop_WandAttackRadius_Quantity = 5;
        public static int Shop_WandAttackRadius_QuantityRemainder = 0;

        [SerializeField] public static float Shop_WandAttackSpeed_Increased = 0.1f;
        [SerializeField] public static float Shop_WandAttackSpeed_Cost = 5f;
        [SerializeField] public static int Shop_WandAttackSpeed_Quantity = 5;
        public static int Shop_WandAttackSpeed_QuantityRemainder = 0;

        public static int HistoryPlay = 0;

        public static List<string[]> Achievement_Prairie = new List<string[]>();
        public static List<string[]> Achievement_Forest = new List<string[]>();
        public static List<string[]> Achievement_Graveyard = new List<string[]>();
        public static List<string[]> Achievement_Void = new List<string[]>();
}

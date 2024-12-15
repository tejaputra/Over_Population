using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Player_Script : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Stats")]
    [SerializeField] private float CurrentHealth = 10;
    private float MaxHealth = 0;
    [SerializeField] private GameObject HealthBarSlider;
    [SerializeField] private GameObject HealthBarText;

    [SerializeField] private float CurrentEnergy = 100;
    private float MaxEnergy = 0;
    [SerializeField] private float EnergyRecovery = 0;
    [SerializeField] private GameObject EnergyhBarSlider;
    [SerializeField] private GameObject EnergyBarText;
    private int Level = 0;
    [SerializeField] private float [] Level_Exp;
    private float Exp = 0;
    public static float Point = 0;
    [SerializeField] private GameObject LevelExphBarSlider;
    [SerializeField] private GameObject LevelExpBarText;
    [SerializeField] private float CurrentSpeed = 10;
    [SerializeField] private InputActionReference moveAction; 
    private float Angle_PLayer = 0;
    private Vector3 Angle_Projectile = new Vector3 (1.0f,0f,0f);
    private bool flipped = false;
    [SerializeField] private GameObject KillCountText;

    [Header("Weapon Sword")]
    private float Time_Attack_Speed = 10f;
    [SerializeField] private GameObject Weapon;
    [SerializeField] private GameObject Sword_Sprite;
    [SerializeField] private GameObject Sword_Animation;
    [SerializeField] private float Sword_Attack;
    [SerializeField] private float Sword_Attack_Speed;
    [SerializeField] private float Sword_Attack_Energy_Cost;
    [SerializeField] private float LSword_Attack = 1f;
    [SerializeField] private float LSword_Attack_Speed = 0.04f;
    private bool Sword_Rotation = true;
    public bool F_Sword_Rotation(){
        return Sword_Rotation;
    }
    [Header("Weapon Scorpion")]
    [SerializeField] private GameObject Scorpion_Sprite;
    [SerializeField] private GameObject Scorpion_Pivot;
    [SerializeField] private GameObject Scorpion_Bullet;
    [SerializeField] private float Scorpion_Attack;
    [SerializeField] private float Scorpion_Attack_Speed;
    [SerializeField] private float Scorpion_Attack_Energy_Cost;
    [SerializeField] private float LScorpion_Attack = 0.6f;
    [SerializeField] private float LScorpion_Attack_Speed = 0.04f;
    [SerializeField] private GameObject Smoke_Pivot;
    [SerializeField] private GameObject Scorpio_Skill;

    [Header("Weapon Wand")]
    [SerializeField] private GameObject Wand_Sprite;
    [SerializeField] private GameObject Wand_Pivot;
    [SerializeField] private GameObject Wand_Magic;
    [SerializeField] private float Wand_Attack;
    [SerializeField] private float Wand_Attack_Area;
    [SerializeField] private float Wand_Attack_Speed;
    [SerializeField] private float Wand_Attack_Energy_Cost;

    [SerializeField] private float LWand_Attack = 1.5f;
    [SerializeField] private float LWand_Attack_Area = 0.2f;
    [SerializeField] private float LWand_Attack_Speed = 0.1f;

    Setting MenuSetting;
    void Awake()
    {
        //body
        MaxHealth = Data_Player.MaxHealth;
        CurrentHealth = MaxHealth;
        MaxEnergy = Data_Player.CurrentEnergy;
        CurrentEnergy = MaxEnergy;
        EnergyRecovery = Data_Player.EnergyRecovery;
        CurrentSpeed = Data_Player.MovementSpeed;

        //weapon Sword
        Sword_Attack = Data_Player.SwordAttack;
        Sword_Attack_Speed = Data_Player.SwordAttackSpeed;
        Sword_Attack_Energy_Cost = Data_Player.SwordAttackEnergy;

        LSword_Attack = Data_Player.LSwordAttack;
        LSword_Attack_Speed = Data_Player.LSwordAttackSpeed;

        //Weapon Scorpion
        Scorpion_Attack = Data_Player.ScorpionAttack;
        Scorpion_Attack_Speed = Data_Player.ScorpionAttackSpeed;
        Scorpion_Attack_Energy_Cost = Data_Player.ScorpionAttackEnergy;

        LScorpion_Attack = Data_Player.LScorpionAttack;
        LScorpion_Attack_Speed = Data_Player.LScorpionAttackSpeed;

        //Weapon Wand
        Wand_Attack = Data_Player.WandAttack;
        Wand_Attack_Area = Data_Player.WandAttackRadius;
        Wand_Attack_Speed = Data_Player.WandAttackSpeed;
        Wand_Attack_Energy_Cost = Data_Player.WandAttackEnergy;

        LWand_Attack = Data_Player.LWandAttack;
        LWand_Attack_Area = Data_Player.LWandAttackRadius;
        LWand_Attack_Speed = Data_Player.LWandAttackSpeed;

        Point = 0;
    }
    void Start()
    {
        MaxHealth = CurrentHealth;
        MaxEnergy = CurrentEnergy;

        MenuSetting = GameObject.FindGameObjectWithTag("SettingObject").GetComponent<Setting>();
    }

    float timeCheck = 0;

    private float _atkDelayCheck = 0;
    private bool _atk = false;
    private void FixedUpdate() {
        //pergerakan
        MovementCharacter();

        //rotasi
        RotationWeapon();

        //hitung waktu buat attack speed
        Time_Attack_Speed += Time.deltaTime;

        AttackForScorpionandWand();

        /*if (Input.GetKeyDown("AttackButton"))
        {
            // Button was just pressed
            Debug.Log("Button pressed down");
        }*/

        //testing function
        EnergyRecharge();

        Update_Health_Bar_Text();
        Update_Energy_Bar_Text();
        Update_Level_Exp_Bar_Text();
        Update_Kill_Count_Text();
        CheckDeath();
        //Debug.Log("Exp : "  + Exp);
        if(Sword_Rotation){
            Weapon.transform.eulerAngles += new Vector3(0, 0, 225 *  10 *Time.deltaTime);
            timeCheck += 225 * 10 *Time.deltaTime;
            if(timeCheck >= 360){
                timeCheck = 0;
                Sword_Rotation = false;
            }
        }
        if(Skill){
            DurationSkill();
        }
        if(TimeColSkill != 0){
            CooldownSkill();
        }
        if(Giddy || Slow || NegativeVision){
            NegativeEffect();
        }

        //jelek, attack delay kalau energy abis
        _atkDelayCheck += Time.deltaTime;
        if(0.1f < _atkDelayCheck)
        {
            _atkDelayCheck = 0;
            _atk = true;
        }
    }
    private Vector3 direction;

    public Vector3 F_Get_Direction(){
        return direction;
    }
    private int Move = Animator.StringToHash("Move");
    private void MovementCharacter(){
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        direction = new Vector3 (horizontalMovement,verticalMovement,0);
        direction = moveAction.action.ReadValue<Vector2>();
        //animasi bergerak
        if(Giddy){
            Vector3 temporary = direction;
            direction.y = -temporary.y;
            direction.x = -temporary.x;
        }
        if(direction.x != 0 || direction.y != 0)
        {
            gameObject.GetComponent<Animator>().SetBool(Move,true);
            if(flipped == false && direction.x < -0.03){
                flipped = true;
                //gameObject.GetComponent<SpriteRenderer>().flipX = true;
                transform.localScale = new Vector2(
                    -transform.localScale.x ,
                    transform.localScale.y
                );
            }
            else if(flipped == true && direction.x > 0.03){
                flipped = false;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                transform.localScale = new Vector2(
                    -transform.localScale.x ,
                    transform.localScale.y
                );
            }
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool(Move,false);
        }
        Vector3 directionMovement = direction.normalized * CurrentSpeed*50 * Time.deltaTime;
        if((BoundBotLeft.transform.position.x > gameObject.transform.position.x || gameObject.transform.position.x > BoundTopRight.transform.position.x) ||
            (BoundBotLeft.transform.position.y > gameObject.transform.position.y || gameObject.transform.position.y > BoundTopRight.transform.position.y)
            )
        {
            BoundariesLevelSet();
        }
        else if(direction.x != 0 || direction.y != 0)
        {
            if (Slow && dodge)
            {
                directionMovement = directionMovement * (dodge_Speed - (1f - SlowSpeed));
                gameObject.GetComponent<Rigidbody2D>().velocity = directionMovement;
            }
            else if(Slow)
            {
                directionMovement = directionMovement * SlowSpeed;
                gameObject.GetComponent<Rigidbody2D>().velocity = directionMovement;
            }
            else if (dodge)
            {
                directionMovement = directionMovement * dodge_Speed;
                gameObject.GetComponent<Rigidbody2D>().velocity = directionMovement;
            }
            else
                gameObject.GetComponent<Rigidbody2D>().velocity = directionMovement;
        } 
        else 
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = directionMovement;
        }
    }

    [SerializeField] GameObject BoundBotLeft;
    [SerializeField] GameObject BoundTopRight;

    private void BoundariesLevelSet() {
        Vector3 limitPlayerPosition = gameObject.transform.position;
        limitPlayerPosition.x = Mathf.Clamp(limitPlayerPosition.x, BoundBotLeft.transform.position.x, BoundTopRight.transform.position.x);
        limitPlayerPosition.y = Mathf.Clamp(limitPlayerPosition.y, BoundBotLeft.transform.position.y, BoundTopRight.transform.position.y);
        gameObject.transform.position = limitPlayerPosition;

        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    
    [SerializeField] private InputActionReference Attack_Keypad;
    private void RotationWeapon(){
        Vector3 Attack_Coordinate = new Vector3(0f,0f,0f);
        if(AttackButton.activeSelf == true)
            Attack_Coordinate = moveAction.action.ReadValue<Vector2>();
        else 
            Attack_Coordinate = Attack_Keypad.action.ReadValue<Vector2>();
        if(!Sword_Rotation){
            if(Attack_Coordinate.x != 0 && Attack_Coordinate.y != 0)
            {
                Vector3 vec = Camera.main.ScreenToWorldPoint(Attack_Coordinate);
                vec.z = 0f;
                Vector3 aimDirection = Attack_Coordinate.normalized;
                float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg ;
                Weapon.transform.eulerAngles = new Vector3 (0,0,angle);

                Angle_PLayer = angle;
                Angle_Projectile = aimDirection;
            }
        }
    }
    [SerializeField] private GameObject AttackButton;
    [SerializeField] private GameObject AttackKeypad;

    
    public void ChangeWeapon(){
        GameObject.FindGameObjectWithTag("Game_Information").GetComponent<SystemGameplay>().F_Tutorial_Check_Change_Weapon();
        GameObject.FindGameObjectWithTag("Game_Information").GetComponent<SystemGameplay>().F_Tutorial_4_Weapon_Order();

        if(Sword_Sprite.activeSelf == true){
            MenuSetting.playSFX(MenuSetting.SFX_Change_Scorpio,"");

            Sword_Sprite.SetActive(false);
            Scorpion_Sprite.SetActive(true);
            AttackButton.SetActive(false);
            AttackKeypad.SetActive(true);
        }
        else if(Scorpion_Sprite.activeSelf == true){
            MenuSetting.playSFX(MenuSetting.SFX_Change_Wand,"");

            Scorpion_Sprite.SetActive(false);
            Wand_Sprite.SetActive(true);
            AttackButton.SetActive(false);
            AttackKeypad.SetActive(true);
        }
        else if(Wand_Sprite.activeSelf == true){
            MenuSetting.playSFX(MenuSetting.SFX_Change_Sword,"");

            Wand_Sprite.SetActive(false);
            Sword_Sprite.SetActive(true);
            AttackButton.SetActive(true);
            AttackKeypad.SetActive(false);
        }
    }
    private bool dodge = false;

    public bool F_Get_Dodge(){
        return dodge;
    }

    [SerializeField] float dodge_Speed = 1.4f;
    [SerializeField] float dodgeEnergy = 30;
    private int Dodge = Animator.StringToHash("Dodge");
    public void DodgeStart(){

        //for tutorial
        float _energyDodge = 0;
        if(GameObject.FindGameObjectWithTag("Game_Information").GetComponent<SystemGameplay>().F_Get_Tutorial_adventure())
            _energyDodge = dodgeEnergy;

        if(CurrentEnergy >= _energyDodge)
        {
            gameObject.GetComponent<Animator>().SetBool(Dodge,true);
            CurrentEnergy -= _energyDodge;
            EnergyBarText.GetComponent<TextMeshProUGUI>().color = Color.black;
            GameObject.FindWithTag("Energy").GetComponent<Image>().color = Color.white;

            MenuSetting.playSFX(MenuSetting.SFX_Player_Dodge);
            dodge = true;
        }
        else
        {
            MenuSetting.playSFX(MenuSetting.SFX_Energy_Empty, "");
            EnergyBarText.GetComponent<TextMeshProUGUI>().color = Color.red;
            GameObject.FindWithTag("Energy").GetComponent<Image>().color = Color.yellow;
        }
    }
    private void DodgeStop(){
            gameObject.GetComponent<Animator>().SetBool(Dodge,false);
            dodge = false;
    }

    private Vector3 _attack_Coordinate = new Vector3(0f,0f,0f);
    public Vector3 F_get_attack_Coordinate(){
        return _attack_Coordinate;
    }
    private void AttackForScorpionandWand(){
         _attack_Coordinate = Attack_Keypad.action.ReadValue<Vector2>();
        if(_attack_Coordinate.x != 0 && _attack_Coordinate.y != 0)
        {
            Attack_Pattern();
        }
    }
    private int Sword_Atk = Animator.StringToHash("Sword_Atk");
    public void Attack_Pattern(){

        if(_atk)
        {
            _atk = false;

            //sword
            if(Sword_Sprite.activeSelf == true && Time_Attack_Speed > Sword_Attack_Speed && checkAttackEnergy(Sword_Attack_Energy_Cost))
            {
                //serangan masih belum ada animasi memutar pemain #
                Time_Attack_Speed = 0;
                Sword_Animation.GetComponent<Animator>().SetBool(Sword_Atk,true);
                Sword_Animation.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
                Sword_Animation.GetComponent<Transform>().localPosition = new Vector3(-0.1f, -0.7f, 1f);
                GameObject.FindWithTag("Player_Melee").GetComponent<AtkSwordHolder>().SetDamage(Sword_Attack);
                Sword_Rotation = true;

                MenuSetting.playSFX(MenuSetting.SFX_Sword,"");

            }

            //scorpion
            float Scorpion_Total_Attack_Speed;
            float Scorpion_Total_Attack_Energy_Cost;
            if(WhichSkill == 2)
            {
                Scorpion_Total_Attack_Speed = Scorpion_Attack_Speed * 0.4f;
                Scorpion_Total_Attack_Energy_Cost = Scorpion_Attack_Energy_Cost * 0.7f;
            }
            else 
            {
                Scorpion_Total_Attack_Speed = Scorpion_Attack_Speed;
                Scorpion_Total_Attack_Energy_Cost = Scorpion_Attack_Energy_Cost;
            }
            if(Scorpion_Sprite.activeSelf == true && Time_Attack_Speed > Scorpion_Total_Attack_Speed && checkAttackEnergy(Scorpion_Attack_Energy_Cost))
            {
                Time_Attack_Speed = 0;
                GameObject bullet = Instantiate(Scorpion_Bullet);
                bullet.GetComponent<Projectile>().getDirection(Angle_Projectile);
                bullet.transform.position = Scorpion_Pivot.transform.position;
                bullet.transform.eulerAngles = new Vector3 (0,0,Angle_PLayer);

                if(WhichSkill == 2)
                {
                    MenuSetting.playSFX(MenuSetting.SFX_Scorpio_Skill,"");
                    
                    bullet.GetComponent<SpriteRenderer>().color = new Color(0, 255/255, 244/255);

                    GameObject smoke = Instantiate(Scorpio_Skill);
                    smoke.transform.position = Smoke_Pivot.transform.position;
                    smoke.transform.eulerAngles = new Vector3 (0,0,Angle_PLayer);
                } 
                else 
                {
                    MenuSetting.playSFX(MenuSetting.SFX_Scorpio,"");
                }

                bullet.GetComponent<Projectile>().SetAtkDamageProjetile(Scorpion_Attack);
                bullet.GetComponent<Projectile>().SetTypeBullet(1);
            }

            //wand
            float Wand_Total_Attack;
            if(WhichSkill == 3)
            {
                Wand_Total_Attack = Wand_Attack * 1.5f;
            }
            else 
            {
                Wand_Total_Attack = Wand_Attack;
            }
            if(Wand_Sprite.activeSelf == true && Time_Attack_Speed > Wand_Attack_Speed && checkAttackEnergy(Wand_Attack_Energy_Cost))
            {
                Time_Attack_Speed = 0;
                
                MenuSetting.playSFX(MenuSetting.SFX_Fireball,"");

                GameObject magic = Instantiate(Wand_Magic);
                magic.GetComponent<Projectile>().getDirection(Angle_Projectile);
                magic.transform.position = Wand_Pivot.transform.position;
                magic.transform.eulerAngles = new Vector3 (0,0,Angle_PLayer);
                magic.GetComponent<Projectile>().SetTypeBullet(2);
                if(WhichSkill == 3)
                {
                    magic.GetComponent<SpriteRenderer>().color = new Color(255f/255f, 166f/255f, 88f/255f);

                    magic.GetComponent<Projectile>().OnSkill();
                    magic.GetComponentInChildren<CircleCollider2D>().radius = Wand_Attack_Area * 1.5f;
                    magic.GetComponent<Projectile>().SetAtkDamageProjetile(Wand_Total_Attack * 1.5f);
                }
                else
                {
                    magic.GetComponentInChildren<CircleCollider2D>().radius = Wand_Attack_Area;
                    magic.GetComponent<Projectile>().SetAtkDamageProjetile(Wand_Total_Attack);
                }
            }

        }
    }
    private bool checkAttackEnergy(float EnergyCost){
        float _energyCost = 0;
        if(GameObject.FindGameObjectWithTag("Game_Information").GetComponent<SystemGameplay>().F_Get_Tutorial_adventure())
            _energyCost = EnergyCost;

        if(CurrentEnergy >= _energyCost) 
        {
            CurrentEnergy -= _energyCost;
            EnergyBarText.GetComponent<TextMeshProUGUI>().color = Color.black;
            GameObject.FindWithTag("Energy").GetComponent<Image>().color = Color.white;
            return true;
        } 
        else 
        {
            MenuSetting.playSFX(MenuSetting.SFX_Energy_Empty, "");
            EnergyBarText.GetComponent<TextMeshProUGUI>().color = Color.red;
            GameObject.FindWithTag("Energy").GetComponent<Image>().color = Color.yellow;
            return false;
        }
    }
    private float Time_Energy = 0f;
    private void EnergyRecharge(){
        Time_Energy += Time.deltaTime;
        if(1 < Time_Energy){
            CurrentEnergy += EnergyRecovery;
            //check kelebihan energi
            if(CurrentEnergy > MaxEnergy){
                CurrentEnergy = MaxEnergy;
            }
            Time_Energy = 0;
        }
    }
    [Header("Status Effect")]
    [SerializeField] Button SkillButton;
    [SerializeField] GameObject Shield;
    [SerializeField] CapsuleCollider2D Player_Area;
    [SerializeField] GameObject Status_PowerUp;
    [SerializeField] GameObject Status_PowerUp_Shield;
    [SerializeField] GameObject Status_PowerUp_BulletHell;
    [SerializeField] GameObject Status_PowerUp_LitThemUp;
    [SerializeField] float Duration_Shield = 3;
    [SerializeField] float Duration_BulletHell = 5;
    [SerializeField] float Duration_LitThemUp = 15;
    private bool Skill = false;
    private int WhichSkill = 0;

    public int F_Get_WhichSkill(){
        return WhichSkill;
    }
    //0 inactive, 1 sword, 2 scorpion, 3 wand
    private float timeDurSkill = 0;
    private float remainder4 = 0;

    public void F_ResetSkill(){
        if(WhichSkill == 1)
        {
            Shield.SetActive(false);
            Player_Area.enabled = true;
        }
        WhichSkill = 0;

        remainder4 = 0;
            Status_PowerUp.SetActive(false);
            Skill = false;

        TimeColSkill = 0;
            remainder5 = 0;
            SkillButton.interactable = true;
    }

    private float Cooldown_Shield = 15;
    private float Cooldown_BulletHell = 15;
    private float Cooldown_LitThemUp = 30;

    private float TimeColSkill = 0;
    private float remainder5 = 0;
    public void SkillUse(){
        if(!Skill && TimeColSkill == 0)
        {
            Skill = true;
            SkillButton.interactable = false;
            Status_PowerUp.SetActive(true);
            if(Sword_Sprite.activeSelf == true)
            {
                Status_PowerUp_Shield.SetActive(true);
                Status_PowerUp_BulletHell.SetActive(false);
                Status_PowerUp_LitThemUp.SetActive(false);
                Shield.SetActive(true);
                Player_Area.enabled = false;
                timeDurSkill = Duration_Shield;
                WhichSkill = 1;
                TimeColSkill = Cooldown_Shield;
            }
            else if (Scorpion_Sprite.activeSelf == true)
            {
                Status_PowerUp_Shield.SetActive(false);
                Status_PowerUp_BulletHell.SetActive(true);
                Status_PowerUp_LitThemUp.SetActive(false);
                timeDurSkill = Duration_BulletHell;
                WhichSkill = 2;
                TimeColSkill = Cooldown_BulletHell;
            }
            else if (Wand_Sprite.activeSelf == true)
            {
                Status_PowerUp_Shield.SetActive(false);
                Status_PowerUp_BulletHell.SetActive(false);
                Status_PowerUp_LitThemUp.SetActive(true);
                timeDurSkill = Duration_LitThemUp;
                WhichSkill = 3;
                TimeColSkill = Cooldown_LitThemUp;
            }
        }
    }
    private void DurationSkill(){
        remainder4 += Time.deltaTime;
        if(remainder4 > timeDurSkill)
        {
            Status_PowerUp.SetActive(false);
            Skill = false;
            if(WhichSkill == 1)
            {
                Shield.SetActive(false);
                Player_Area.enabled = true;
            }
            WhichSkill = 0;
            remainder4 = 0;
        }
    }
    private void CooldownSkill(){
        remainder5 += Time.deltaTime;
        if(remainder5 > TimeColSkill)
        {
            TimeColSkill = 0;
            remainder5 = 0;
            SkillButton.interactable = true;
        }
    }

    private void Update_Health_Bar_Text(){
        HealthBarText.GetComponent<TextMeshProUGUI>().text = CurrentHealth.ToString("F0") + " / "+ MaxHealth;
        HealthBarSlider.GetComponent<Slider>().value = 100 - CurrentHealth/MaxHealth*100;
    }
    private void Update_Energy_Bar_Text(){
        EnergyBarText.GetComponent<TextMeshProUGUI>().text = ((int)CurrentEnergy).ToString("F0")  + " / " + MaxEnergy;
        EnergyhBarSlider.GetComponent<Slider>().value = 100 - CurrentEnergy/MaxEnergy*100;
    }
    private void Update_Level_Exp_Bar_Text(){
        if(Level < Level_Exp.Length)
        {
            LevelExpBarText.GetComponent<TextMeshProUGUI>().text = Level + " ( " + Mathf.Round(Exp/Level_Exp[Level]*100) + "% ) ";
            LevelExphBarSlider.GetComponent<Slider>().value = Mathf.Round(Exp/Level_Exp[Level]*100);
        }
        else
        {
            LevelExpBarText.GetComponent<TextMeshProUGUI>().text = Level + " ( 100% ) ";
            LevelExphBarSlider.GetComponent<Slider>().value = 100;
        }
    }
    private void Update_Kill_Count_Text(){
        KillCountText.GetComponent<TextMeshProUGUI>().text = "Count : " + SpawnController.CounterDeath;
    }

    public void TakeDamage(float damage){
        if(WhichSkill == 1 || dodge)
        {
            return;
        }

        MenuSetting.playSFX(MenuSetting.SFX_Player_Hurt);

        CurrentHealth -= damage;
        if(CurrentHealth < 0)
            CurrentHealth = 0;
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        Invoke("backColor", 0.1f);
    }
    private void backColor(){
        if(Giddy)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        } 
        else if(Slow)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    public void setExp(float exp){
        Exp += exp;
        while (Level < Level_Exp.Length && Exp >= Level_Exp[Level])
        {
            Exp -= Level_Exp[Level];
            Level++;

            Sword_Attack += LSword_Attack;
            Sword_Attack_Speed -= LSword_Attack_Speed;

            Scorpion_Attack += LScorpion_Attack;
            Scorpion_Attack_Speed -= LScorpion_Attack_Speed;

            Wand_Attack += LWand_Attack;
            Wand_Attack_Area += LWand_Attack_Area;
            Wand_Attack_Speed -= LWand_Attack_Speed;

            //print(Level_Exp.Length);
            //##belum tambahin semua stats senjata tiap level.
        }
    }

    public void setPoint(float point){
        Point += point;
    }
    public float getExp(){
            return Point;
    }


    private bool Giddy = false;
    private float GiddyTime = 2;
    private float remainder1 = 0;
    [SerializeField] GameObject Status_Giddy;
    public void setGiddy(float Time){
        Giddy = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        Status_Giddy.SetActive(true);
        GiddyTime = Time;
    }
    private bool Slow = false;
    private float SlowTime;
    private float SlowSpeed;
    private float remainder2 = 0;
    [SerializeField] GameObject Status_Slow;
    public void setSlow(float Time, float slowEffect){
        Slow = true;
        SlowSpeed = slowEffect;
        Status_Slow.SetActive(true);
        gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
        SlowTime = Time;
    }
    private bool NegativeVision = false;
    private float NegativeVisionTime = 0;
    private float remainder3 = 0;
    [SerializeField] GameObject Status_NegativeVision;
    [SerializeField] GameObject Background_Effect_NegativeVision;

    public void setNegativeVision(float Time){
        NegativeVision = true;
        Status_NegativeVision.SetActive(true);
        Background_Effect_NegativeVision.SetActive(true);
        NegativeVisionTime = Time;
    }
    private void NegativeEffect(){   
        remainder1 += Time.deltaTime;
        remainder2 += Time.deltaTime;
        remainder3 += Time.deltaTime;
        if(remainder1 > GiddyTime){
            Giddy = false;
            Status_Giddy.SetActive(false);
            remainder1 = 0;
            backColor();
        }
        if(remainder2 > SlowTime){
            Slow = false;
            Status_Slow.SetActive(false);
            remainder2 = 0;
            backColor();
        }
        if(remainder3 > NegativeVisionTime){
            NegativeVision = false;
            Status_NegativeVision.SetActive(false);
            Background_Effect_NegativeVision.SetActive(false);
            remainder3 = 0;
            backColor();
        }
    }
    private void CheckDeath(){
        if(CurrentHealth <= 0){
            CurrentHealth = 0;
            Destroy(gameObject);
        } 
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.gameObject.GetComponentInParent<Enemy>() != null && target.gameObject.GetComponentInParent<Enemy>().tag == "Enemies")
        {
            target.gameObject.GetComponentInParent<Enemy>().meleeAtk = true;
        }
        if(!dodge)
        {
            if(target.GetComponent<Projectile>() != null && target.GetComponent<Projectile>().tag == "Enemies_Projectile")
            {
                TakeDamage(target.gameObject.GetComponentInParent<Projectile>().GetAtkDamageProjetile());
                if(target.GetComponent<Projectile>() != null && target.GetComponent<Projectile>().GetTypeBullet() == 5){
                    if(target.GetComponent<Projectile>().getNegativeName() == "Negative_Vision"){
                        setNegativeVision(target.GetComponent<Projectile>().getNegative_Time());
                    }
                }
            }
        }
    }
    void OnTriggerStay2D(Collider2D target)
    {
        //print("bersentuhan" + target.name + " & " + target.tag);
        if(target.gameObject.GetComponentInParent<Enemy>() != null && target.gameObject.GetComponentInParent<Enemy>().tag == "Enemies")
        {
            target.gameObject.GetComponentInParent<Enemy>().meleeAtk = true;
        }
    }
    void OnTriggerExit2D(Collider2D target)
    {
        if(target.gameObject.GetComponentInParent<Enemy>() != null && target.gameObject.GetComponentInParent<Enemy>().tag == "Enemies" )
        {
            target.gameObject.GetComponentInParent<Enemy>().meleeAtk = false;
        }
    }
}

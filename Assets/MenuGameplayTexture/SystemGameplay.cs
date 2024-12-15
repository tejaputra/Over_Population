using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SystemGameplay : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] bool DebugTesting = false;
    [SerializeField] GameObject Player;

    [SerializeField] GameObject Early_Game_Detail;

    [SerializeField] GameObject End_Game_Detail;

    [SerializeField] GameObject Prairie;
    [SerializeField] GameObject Forest;
    [SerializeField] GameObject Graveyard;
    [SerializeField] GameObject Void;
    static int GameplayMode;

    [SerializeField] float TimeCountdown;
    private float TimeRemainder;
    private float TimePlay = 0;

    private bool once = true;

    Setting MenuSetting;

    public float getTimePlay(){
        return TimePlay;
    }

    //1 = Prairie, 2 = Forest, 3 = Graveyard
    void Awake() {
        TimePlay -= TimeCountdown;
        Tutorial = Data_Player._status_Tutorial;
    }
    void Start()
    {
        GameplayMode = Data_Player.mode;
        if(GameplayMode == 1)
        {
            Prairie.SetActive(true);
            Forest.SetActive(false);
            Graveyard.SetActive(false);
            Void.SetActive(false);
        }
        else if(GameplayMode == 2)
        {
            Prairie.SetActive(false);
            Forest.SetActive(true);
            Graveyard.SetActive(false);
            Void.SetActive(false);
        }
        else if(GameplayMode == 3)
        {
            Prairie.SetActive(false);
            Forest.SetActive(false);
            Graveyard.SetActive(true);
            Void.SetActive(false);
        }
        else if(GameplayMode == 4)
        {
            Prairie.SetActive(false);
            Forest.SetActive(false);
            Graveyard.SetActive(false);
            Void.SetActive(true);

            //ngurangin energy akses void.
            Data_Player.VoidAccess -= 5;
            if(Data_Player.VoidAccess < 0)
                Data_Player.VoidAccess = 0;
        }
        MenuSetting = GameObject.FindGameObjectWithTag("SettingObject").GetComponent<Setting>();

        if(GameplayMode == 4)
        {
            GameObject.FindWithTag("Objective").GetComponent<TextMeshProUGUI>().text = "survived as long as you can, good luck!";
        }
        else
        {
            string text = "";
            if(GameplayMode == 1)
            {
                text = "Prairie Boss";
            } 
            else if (GameplayMode == 2)
            {
                text = "Forest Boss";
            } 
            else if (GameplayMode == 3)
            {
                text = "Graveyard Boss";
            } 
            GameObject.FindWithTag("Objective").GetComponent<TextMeshProUGUI>().text = "Kill " + SpawnController.TotalKillForBoss + " and " + text;
        
        }
    }

    // Update is called once per frame
    bool Tutorial = false;

    public bool F_Get_Tutorial_adventure()
    {
        return Tutorial;
    }
    void FixedUpdate()
    {
        if(!DebugTesting && Tutorial)
        {
            TimeRemainder += Time.deltaTime;
            if(TimeCountdown > TimeRemainder){
                //GameObject.FindWithTag("Countdown").GetComponent<TextMeshProUGUI>().text = (TimeCountdown - TimeRemainder).ToString("F0");
                var CountdownTimerText = GameObject.FindGameObjectsWithTag("Countdown");
                for (int i = 0; i < CountdownTimerText.Length; i++) 
                {
                    CountdownTimerText[i].GetComponent<TextMeshProUGUI>().text = (((int)(TimeCountdown - TimeRemainder))).ToString("F0");
                }
            }
            else {
                gameObject.GetComponent<SpawnController>().enabled = true;
                Early_Game_Detail.SetActive(false);
            }

            if(End_Game_Detail.activeSelf == false)
                TimePlay += Time.deltaTime;
            if(Player == null)
            {
                On_End_Game(0);
            }
            else if(SpawnController.CounterDeath >= SpawnController.TotalKillForBoss && !SpawnController.onceSpawnBoss && !GameObject.FindWithTag("Boss")){
                On_End_Game(1000);
            }
        } else {
            Early_Game_Detail.SetActive(false);
            F_Tutorial_Phase();
        }
    }
    private int phaseTutorial = 1;
    private int remainderPhase = -1;
    private bool OncePerPhase = true;

    [Header("Tutorial Setting")]

    [SerializeField] GameObject Hightlight_Tutorial;
    [SerializeField] float DelayEachTutorial = 3f;
    [SerializeField] GameObject Tutorial_1;
    [SerializeField] string Tutorial_Intro_Objective = "";
    [SerializeField] string Tutorial_1_Objective = "";
    [SerializeField] string Tutorial_2_Objective = "";
    [SerializeField] string Tutorial_3_Objective = "";
    private bool Tutorial_3 = false;
    private int Tutorial_Order_weapon = 1;
    [SerializeField] string Tutorial_4_Objective = "";
    private bool Tutorial_4 = false;
    [SerializeField] string Tutorial_5_Objective = "";
    [SerializeField] string Tutorial_6_Objective = "";
    [SerializeField] string Tutorial_7_Objective = "";
    private void F_Tutorial_Phase(){

        //Debug.Log("phaseTutorial : " + phaseTutorial);
    
        switch(phaseTutorial)
        {
            case 1: 

                //tutorial Intro tampilan
                if(OncePerPhase)
                {
                    OncePerPhase = false;
                    Tutorial_1.SetActive(true);
                    Tutorial_1.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = Tutorial_Intro_Objective;
                    Vector2 Area = new Vector2(-2000,-2000);
                    Hightlight_Tutorial.transform.localPosition = Area;
                }
                if(remainderPhase != phaseTutorial)
                {
                    remainderPhase = phaseTutorial;
                    Invoke("F_Change_Tutorial", DelayEachTutorial);

                    //Tutorial_1.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = Color.green;
                }
                break;

            case 2: 

                //tutorial 1 tampilan
                if(OncePerPhase)
                {
                    OncePerPhase = false;
                    Tutorial_1.SetActive(true);
                    Tutorial_1.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = Tutorial_1_Objective;
                    Vector2 Area = new Vector2(-709,-239);
                    Hightlight_Tutorial.transform.localPosition = Area;
                }

                //tutorial 1 kriteria gerakin movementcharacter
                Vector3 MovementPlayer = Player.GetComponent<Player_Script>().F_Get_Direction();
                //Debug.Log(MovementPlayer);
                if((MovementPlayer.x != 0 || MovementPlayer.y != 0) && remainderPhase != phaseTutorial)
                {
                    remainderPhase = phaseTutorial;
                    Invoke("F_Change_Tutorial", DelayEachTutorial);
                    
                    MenuSetting.playSFX(MenuSetting.SFX_Tutorial_Complete);

                    Tutorial_1.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = Color.green;
                }
                break;

            case 3: 

                //tutorial 2 tampilan
                if(OncePerPhase)
                {
                    OncePerPhase = false;
                    Tutorial_1.SetActive(true);
                    Tutorial_1.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = Tutorial_2_Objective;
                    Vector2 Area = new Vector2(821, -300);
                    Hightlight_Tutorial.transform.localPosition = Area;
                }

                if(Tutorial_Order_weapon == 1)
                {
                    Vector2 Area = new Vector2(821,-300);
                    Hightlight_Tutorial.transform.localPosition = Area;
                }
                 else
                {
                    Vector2 Area = new Vector2(899,-59);
                    Hightlight_Tutorial.transform.localPosition = Area;
                }

                //tutorial 2 kriteria using melee attack
                bool MeleeAttack = Player.GetComponent<Player_Script>().F_Sword_Rotation();
                //Debug.Log("MeleeAttack " + MeleeAttack);
                if(MeleeAttack && remainderPhase != phaseTutorial)
                {
                    remainderPhase = phaseTutorial;
                    Invoke("F_Change_Tutorial", DelayEachTutorial);
                   
                    MenuSetting.playSFX(MenuSetting.SFX_Tutorial_Complete);

                    Tutorial_1.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = Color.green;
                }
                break;
            case 4: 
                
                //tutorial 3 tampilan
                if(OncePerPhase)
                {
                    OncePerPhase = false;
                    Tutorial_1.SetActive(true);
                    Tutorial_1.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = Tutorial_3_Objective;
                    Vector2 Area = new Vector2(899,-59);
                    Hightlight_Tutorial.transform.localPosition = Area;
                }

                //tutorial 3 kriteria check change weapon
                if(Tutorial_3 && remainderPhase != phaseTutorial)
                {
                    remainderPhase = phaseTutorial;
                    Invoke("F_Change_Tutorial", DelayEachTutorial);
                    
                    MenuSetting.playSFX(MenuSetting.SFX_Tutorial_Complete);

                    Tutorial_1.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = Color.green;
                }
                break;
            case 5: 

                //tutorial 4 tampilan
                if(OncePerPhase)
                {
                    OncePerPhase = false;
                    Tutorial_1.SetActive(true);
                    Tutorial_1.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = Tutorial_4_Objective;
                    Vector2 Area = new Vector2(818,-300);
                    Hightlight_Tutorial.transform.localPosition = Area;
                }

                if(Tutorial_Order_weapon == 2 || Tutorial_Order_weapon == 3)
                {
                    Vector2 Area = new Vector2(818,-300);
                    Hightlight_Tutorial.transform.localPosition = Area;
                }
                 else
                {
                    Vector2 Area = new Vector2(899,-59);
                    Hightlight_Tutorial.transform.localPosition = Area;
                }

                Vector3 RangeAtkData = Player.GetComponent<Player_Script>().F_get_attack_Coordinate();
                //Debug.Log("RangeAtkData " + RangeAtkData);
                //tutorial 4 kriteria attack range weapon
                if((RangeAtkData.x != 0 || RangeAtkData.y != 0) && remainderPhase != phaseTutorial)
                {
                    remainderPhase = phaseTutorial;
                    Invoke("F_Change_Tutorial", DelayEachTutorial);

                    MenuSetting.playSFX(MenuSetting.SFX_Tutorial_Complete);

                    Tutorial_1.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = Color.green;
                }
                break;
            case 6: 
                
                //tutorial 5 tampilan
                if(OncePerPhase)
                {
                    OncePerPhase = false;
                    Tutorial_1.SetActive(true);
                    Tutorial_1.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = Tutorial_5_Objective;
                    Vector2 Area = new Vector2(537,-312);
                    Hightlight_Tutorial.transform.localPosition = Area;
                }

                bool Dodge = Player.GetComponent<Player_Script>().F_Get_Dodge();
                //tutorial 5 kriteria Dodge
                if(Dodge && remainderPhase != phaseTutorial)
                {
                    remainderPhase = phaseTutorial;
                    Invoke("F_Change_Tutorial", DelayEachTutorial);
                    
                    MenuSetting.playSFX(MenuSetting.SFX_Tutorial_Complete);

                    Tutorial_1.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = Color.green;
                }
                break;
            case 7: 
                //tutorial 6 tampilan
                if(OncePerPhase)
                {
                    OncePerPhase = false;
                    Tutorial_1.SetActive(true);
                    Tutorial_1.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = Tutorial_6_Objective;
                    Vector2 Area = new Vector2(655,-74);
                    Hightlight_Tutorial.transform.localPosition = Area;
                }

                int skillType = Player.GetComponent<Player_Script>().F_Get_WhichSkill();
                //tutorial 6 kriteria Dodge
                if((skillType == 1 || skillType == 2 || skillType == 3) && remainderPhase != phaseTutorial)
                {
                    remainderPhase = phaseTutorial;
                    Invoke("F_Change_Tutorial", DelayEachTutorial);
                    
                    MenuSetting.playSFX(MenuSetting.SFX_Tutorial_Complete);

                    if(skillType == 1)
                        Tutorial_1.GetComponentInChildren<TMPro.TextMeshProUGUI>().text += " (Sword)";
                    else if (skillType == 2)
                        Tutorial_1.GetComponentInChildren<TMPro.TextMeshProUGUI>().text += " (Scorpio)";
                    else if (skillType == 3)
                        Tutorial_1.GetComponentInChildren<TMPro.TextMeshProUGUI>().text += " (Wand)";

                    Tutorial_1.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = Color.green;
                }
                break;
            case 8: 
                if(OncePerPhase)
                    {
                        OncePerPhase = false;
                        Tutorial_1.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = Tutorial_7_Objective;
                        Tutorial_1.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = Color.green;
                        Invoke("F_Change_Tutorial", DelayEachTutorial);

                        MenuSetting.playSFX(MenuSetting.SFX_Tutorial_Complete);

                        Hightlight_Tutorial.SetActive(false);
                    }
                break;
            case 9:
                F_Resume_after_Tutorial();
                break;
        }
    }
    private void F_Change_Tutorial(){
        phaseTutorial++;
        OncePerPhase = true;
        Tutorial_1.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = Color.white;
        Player.GetComponent<Player_Script>().F_ResetSkill();
    }

    private void F_Resume_after_Tutorial(){
        Tutorial = true;
        //Debug.Log("tutorial selesai");
        Early_Game_Detail.SetActive(true);
        Tutorial_1.SetActive(false);
        Player.GetComponent<Player_Script>().F_ResetSkill();
    }

    //function check change weapon
    public void F_Tutorial_Check_Change_Weapon(){
        if(phaseTutorial == 4)
        {
            Tutorial_3 = true;
        } 
    }
    public void F_Tutorial_Check_Range_Weapon(){
        if(phaseTutorial == 5)
        {
            Tutorial_4 = true;
            
        }
    }
    public void F_Tutorial_4_Weapon_Order(){
        Tutorial_Order_weapon++;
        if(Tutorial_Order_weapon > 3)
            Tutorial_Order_weapon = 1;
    }
    public void F_Tutorial_Skip(){
        phaseTutorial = 9;
        F_Resume_after_Tutorial();
        Hightlight_Tutorial.SetActive(false);
    }

    [SerializeField] GameObject Escape;
    public void On_End_Game(){
        Escape.SetActive(false);

        //untuk skor bonus 500    
        //Debug.Log("huh point 500");
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);

        point_player = Player_Script.Point;
        int skor = 0;
        End_Game_Detail.SetActive(true);

        if(Player != null)
        {
            //Debug.Log(point_player + "sebelum");
            if(Data_Player.VoidAccess >= 5)
            {
                Data_Player.Point += point_player / 2;
                point_player /= 2;
                GameObject.FindWithTag("Money").GetComponent<TextMeshProUGUI>().color = Color.red;
            }
             else
            {
                Data_Player.Point += point_player;
            }
            GameObject.FindWithTag("Money").GetComponent<TextMeshProUGUI>().text = "$ " + point_player.ToString();
            //Debug.Log(point_player + "sesudah");
        } 
        else
        {
            GameObject.FindWithTag("Money").GetComponent<TextMeshProUGUI>().text = "$ 0";
        }

        if(GameplayMode != 4)
        {
            skor = SpawnController.CounterDeath * 10;
            if(TimeCountdown <= TimeRemainder)
                skor += 500;

            GameObject.FindWithTag("Score").GetComponent<TextMeshProUGUI>().text = skor + " poin";
            GameObject.FindWithTag("Kill").GetComponent<TextMeshProUGUI>().text = SpawnController.CounterDeath.ToString();
            if(TimePlay < 0)
                TimePlay = 0;
    
            Data_Player.VoidAccess += 2;
        } 
        else
        {
            skor = SpawnController.CounterDeath * 30;
            skor += SpawnController.CounterDeath * 300;

            int calculate = ((int)((TimePlay / 60) * 100));
            skor += calculate;
            skor += 500;
        }

        Update_End_Game(skor);
    }
    public void On_End_Game(int skorStats){
        //Debug.Log("huh point 0 dan 1000");
        //untuk skor bonus 0 dan 1000
        int skor = 0;
        End_Game_Detail.SetActive(true);
        point_player = Player_Script.Point;

        if(Player != null)
        {
            //Debug.Log(point_player + "sebelum");
            if(Data_Player.VoidAccess >= 5)
            {
                Data_Player.Point += point_player / 2;
                point_player /= 2;
                GameObject.FindWithTag("Money").GetComponent<TextMeshProUGUI>().color = Color.red;
            }
             else
            {
                Data_Player.Point += point_player;
            }
            GameObject.FindWithTag("Money").GetComponent<TextMeshProUGUI>().text = "$ " + point_player.ToString();
            //Debug.Log(point_player + "sesudah");
        } 
        else
        {
            GameObject.FindWithTag("Money").GetComponent<TextMeshProUGUI>().text = "$ 0";
        }

        if(GameplayMode != 4)
        {
            skor = SpawnController.CounterDeath * 10;
            skor += skorStats;
            if(skorStats == 1000)
            {
                int calculate = ((int)((TimePlay / 60) - 5));
                if(calculate < 0)
                    calculate = 0;
                skor += 5000 - (calculate * 100);
                Data_Player.VoidAccess -= 1;
            }
            Data_Player.VoidAccess += 2;
        }
        else
        {
            skor = SpawnController.CounterDeath * 30;
            skor += SpawnController.CounterDeath * 300;

            int calculate = ((int)((TimePlay / 60) * 100));
            skor += calculate;
        }

        Update_End_Game(skor);
    }
    float point_player;
    private void Update_End_Game(int skor){

        Time.timeScale = 0;
        GameObject.FindWithTag("Score").GetComponent<TextMeshProUGUI>().text = skor.ToString();
        GameObject.FindWithTag("Kill").GetComponent<TextMeshProUGUI>().text = SpawnController.CounterDeath.ToString();
        if(TimePlay < 0)
            TimePlay = 0;
        GameObject.FindWithTag("Time").GetComponent<TextMeshProUGUI>().text = (TimePlay).ToString("F0") + " S";

        AddAchievement(skor);
    }
    public bool CheckEarlyInfoActive(){
        if(TimeCountdown > TimeRemainder)
        {
            return true;
        } 
        else
        {
            return false;
        }
    }
    private void AddAchievement(float skor){
        if(once)
        {
            Data_Gameplay gameplay = new Data_Gameplay(skor,SpawnController.CounterDeath,TimePlay, ++Data_Player.HistoryPlay);
            string [] data = new string[4];
            
            data[0] = skor.ToString();
            data[1] = SpawnController.CounterDeath.ToString();
            data[2] = ((int)TimePlay).ToString();
            data[3] = Data_Player.HistoryPlay.ToString();
            
            if(GameplayMode == 1)
            {
                Data_Player.Achievement_Prairie.Add(data);
            }
            else if(GameplayMode == 2)
            {
                Data_Player.Achievement_Forest.Add(data);
            }
            else if(GameplayMode == 3)
            {
                Data_Player.Achievement_Graveyard.Add(data);
            }
            else if(GameplayMode == 4)
            {
                Data_Player.Achievement_Void.Add(data);
            }
            once = false;
        }
    }
}

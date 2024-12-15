using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Pause_Module;
    [SerializeField] GameObject Early_Game_Detail;
    [SerializeField] GameObject Gameplay_Button;
    [SerializeField] GameObject Gameplay_Info;
    [SerializeField] GameObject Gameplay_Tutorial;
    [SerializeField] GameObject Confirm_Exit;
    [SerializeField] GameObject Confirm_Escape;
    [SerializeField] GameObject BlackCurtain;

    [SerializeField] GameObject NegativeVisionPause;

    [SerializeField] GameObject Tutorial_Hightlight;
    [SerializeField] GameObject Tutorial_UI;

    [SerializeField] GameObject RestBar;
    [SerializeField] GameObject EnergyDetail;

    Setting MenuSetting;

    public static bool SettingBool = false;

    [SerializeField] int Energy_Used_Each_Play = 30;
    [SerializeField] int Energy_Rest = 50;

    private void Awake() {
        if(RestBar != null){
            RestBar.GetComponent<Slider>().value = Data_Player.CurrentEnergy / Data_Player.MaxEnergy * 100;
            EnergyDetail.GetComponent<TextMeshProUGUI>().text = Data_Player.CurrentEnergy + " / " +Data_Player.MaxEnergy;
        }
    }

    private void Start() {
        if(GameObject.FindGameObjectWithTag("SettingObject") != null)
            MenuSetting = GameObject.FindGameObjectWithTag("SettingObject").GetComponent<Setting>();
        print("oioioiioiioioiii");
    }

    public void Change_Scene(string scene) {
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);
        SceneManager.LoadScene(scene);
    }
    public bool By = false;
    public void Change_Scene2(string scene) {
        if(!By)
            SceneManager.LoadScene(scene);
    }
    public void Change_Scene2_By(string scene) {
        SceneManager.LoadScene(scene);
    }
    public void Change_Scene_ByPass(string scene) {
        if(gameObject.GetComponent<Trailer_Setting>() != null)
        {
            By = true;
            gameObject.GetComponent<Trailer_Setting>().FastFoward = true;
        }

        //SceneManager.LoadScene(scene);
    }
    public void Select_Level(string scene){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);
        string[] split_data = scene.Split(","[0]);
        //mode & scene
        Data_Player.mode = int.Parse(split_data[1]);
        SceneManager.LoadScene(split_data[0]);
    }
    public void Escape(string scene) {
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);

        Time.timeScale = 1;
        
        SceneManager.LoadScene(scene);
        Pause_Module.SetActive(false);
        Gameplay_Button.SetActive(true);
        Gameplay_Info.SetActive(true);

        if(GameObject.FindWithTag("Game_Information").GetComponent<SystemGameplay>().CheckEarlyInfoActive()){
            Early_Game_Detail.SetActive(false);
        }

        //untuk pemakaian energy setiap permainan
        if(Data_Player.CurrentEnergy - Energy_Used_Each_Play < 0)
            Data_Player.CurrentEnergy = 0;
        else if(Data_Player.mode != 4){
            Data_Player.CurrentEnergy -= Energy_Used_Each_Play;
        }
    }
    public void SettingOn(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);
        GameObject.FindWithTag("SettingObject").GetComponent<Setting>().SettingPlayOn();

    }
    public void SettingOff(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);
        GameObject.FindWithTag("SettingObject").GetComponent<Setting>().SettingPlayOff();
    }

    bool NegativeOn = false;
    public void Pause(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);

        Time.timeScale = 0;
        if(NegativeVisionPause.activeSelf == true)
        {
            NegativeOn = true;
            NegativeVisionPause.SetActive(false);
        }
        Pause_Module.SetActive(true);
        Gameplay_Button.SetActive(false);
        Gameplay_Info.SetActive(false);
        if(!GameObject.FindWithTag("Game_Information").GetComponent<SystemGameplay>().F_Get_Tutorial_adventure())
        {
            Tutorial_Hightlight.SetActive(false);
            Tutorial_UI.SetActive(false);
        }
        if(GameObject.FindWithTag("Game_Information").GetComponent<SystemGameplay>().CheckEarlyInfoActive()){
            Early_Game_Detail.SetActive(false);
        }
    }
    public void TutorialOn(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);

        Gameplay_Tutorial.SetActive(true);
    }

    public void TutorialOff(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);

        Gameplay_Tutorial.SetActive(false);
    }

    public void Resume(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);

        Time.timeScale = 1;
        if(NegativeOn == true)
        {
            NegativeOn = false;
            NegativeVisionPause.SetActive(true);
        }
        Pause_Module.SetActive(false);
        Gameplay_Button.SetActive(true);
        Gameplay_Info.SetActive(true);
        if(!GameObject.FindWithTag("Game_Information").GetComponent<SystemGameplay>().F_Get_Tutorial_adventure())
        {
            Tutorial_Hightlight.SetActive(true);
            Tutorial_UI.SetActive(true);
        }
        if(GameObject.FindWithTag("Game_Information").GetComponent<SystemGameplay>().CheckEarlyInfoActive()){
            Early_Game_Detail.SetActive(true);
        }
    }
    public void ExitGame() {
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);

        Application.Quit();
    }
    
    public void ConfirmExitOn(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);
        Confirm_Exit.SetActive(true);
    }
    public void ConfirmExitOff(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);
        Confirm_Exit.SetActive(false);
    }

    public void F_ConfirmEscapeOn(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);
        Confirm_Escape.SetActive(true);
    }
    public void F_ConfirmEscapeOff(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);
        Confirm_Escape.SetActive(false);
    }

    public void RestEffect(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);

        Data_Player.CurrentEnergy += Energy_Rest;
        if(Data_Player.CurrentEnergy > Data_Player.MaxEnergy)
            Data_Player.CurrentEnergy = Data_Player.MaxEnergy;
        RestBar.GetComponent<Slider>().value = Data_Player.CurrentEnergy / Data_Player.MaxEnergy * 100;
        EnergyDetail.GetComponent<TextMeshProUGUI>().text = Data_Player.CurrentEnergy + " / " +Data_Player.MaxEnergy;

        Data_Player.VoidAccess += 3;
        if(Data_Player.VoidAccess > 5){
            Data_Player.VoidAccess = 5;
        }
        if(RestBar != null){
            gameObject.GetComponent<VoidChecking>().VoidGauge();
        }
    }
}

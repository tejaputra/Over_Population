using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio; 
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource Intro_AS_BackgroundMusic;
    public AudioSource AS_BackgroundMusic;
    public AudioSource AS_SoundEffect;

    [SerializeField] private GameObject CanvasSound;
    [SerializeField] private GameObject CanvasBackgorund;

    [SerializeField] private AudioMixer MyAudioMixer;

    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;


    [Header("Background Sound")]
    public AudioClip BackgroundMusic_1;
    public AudioClip BackgroundMusic_2;
    //[SerializeField] float delay_Time_to_BackgroundMusic_2 = 7.4f;

    [Header("SFX Sound")]
    public AudioClip SFX_ClickingButton;

    public AudioClip SFX_Player_Hurt;
    public AudioClip SFX_Player_Dodge;

    public AudioClip SFX_Change_Sword;
    public AudioClip SFX_Sword;

    public AudioClip SFX_Change_Scorpio;
    public AudioClip SFX_Scorpio;
    public AudioClip SFX_Scorpio_Skill;

    public AudioClip SFX_Change_Wand;
    public AudioClip SFX_Fireball;
    public AudioClip SFX_Fireball_Impact;
    public AudioClip SFX_Fireball_Impact_Skill;

    public AudioClip SFX_Tutorial_Complete;

    public AudioClip SFX_Energy_Empty;

    public static Setting instance;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        OnBGM();
    }
    private void FixedUpdate() {

        //harus dibenerin ini gk guna code na looping berkali2
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Settings")
        {
            CanvasSound.SetActive(true);
            Tutorial_Settings.SetActive(true);
            About_Us_Button.SetActive(true);
        } 
        else
        {
            Tutorial_Settings.SetActive(false);
            CanvasSound.SetActive(false);
            About_Us_Button.SetActive(false);
        } 
    }
    public void SettingPlayOn(){
        CanvasSound.SetActive(true);
        CanvasBackgorund.SetActive(true);
    }
    public void SettingPlayOff(){
        CanvasSound.SetActive(false);
        CanvasBackgorund.SetActive(false);
    }

    public void OnBGM(){
        Intro_AS_BackgroundMusic.clip = BackgroundMusic_1;
        Intro_AS_BackgroundMusic.Play();

        AS_BackgroundMusic.clip = BackgroundMusic_2;
        AS_BackgroundMusic.PlayDelayed(Intro_AS_BackgroundMusic.clip.length - 0.00f);
    }
    public void playSFX(AudioClip clip){
        MyAudioMixer.SetFloat("pitch_sfx", 1f);
        AS_SoundEffect.PlayOneShot(clip);
    }

    public void playSFX(AudioClip clip, string test){
        float random = Random.Range(0.85f,1.15f);
        MyAudioMixer.SetFloat("pitch_sfx", random);
        AS_SoundEffect.PlayOneShot(clip);
    }

    public void SetMusicVolume(){
        float volume = MusicSlider.value;
        MyAudioMixer.SetFloat("music", Mathf.Log10(volume)*20);
    }

    public void SetSFXVolume(){
        float volume = SFXSlider.value;
        MyAudioMixer.SetFloat("sfx", Mathf.Log10(volume)*20);
    }

    [Header("Other Settings")]
    [SerializeField] GameObject Tutorial_Settings;
    public void F_Tutorial_Status_Check(){
        if(Data_Player._status_Tutorial == true)
        {
            Tutorial_Settings.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Tutorial ( On )";
            Data_Player._status_Tutorial = false;
        }
        else
        {
            Tutorial_Settings.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Tutorial ( Off )";
            Data_Player._status_Tutorial = true;
        }
    }

    [SerializeField] GameObject About_Us;
    [SerializeField] GameObject About_Us_Button;
    [SerializeField] GameObject About_Us_BGM;
    [SerializeField] GameObject About_Us_SFX;
    [SerializeField] GameObject About_Us_Tut_Play_Text;
    [SerializeField] GameObject About_Us_Tut_Play_Toggle;

    public void F_About_Us_On(){
        playSFX(SFX_ClickingButton);
        About_Us.SetActive(true);
        About_Us_BGM.SetActive(false);
        About_Us_SFX.SetActive(false);
        About_Us_Tut_Play_Text.SetActive(false);
        About_Us_Tut_Play_Toggle.SetActive(false);
    }
    public void F_About_Us_Off(){
        playSFX(SFX_ClickingButton);
        About_Us.SetActive(false);
        About_Us_BGM.SetActive(true);
        About_Us_SFX.SetActive(true);
        About_Us_Tut_Play_Text.SetActive(true);
        About_Us_Tut_Play_Toggle.SetActive(true);
    }
}

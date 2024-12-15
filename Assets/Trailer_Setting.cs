using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; 
using UnityEngine.UI;

public class Trailer_Setting : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource AS_SoundEffect;
    [SerializeField] AudioSource Ba_SoundEffect;

    [SerializeField] AudioClip Falling;
    [SerializeField] AudioClip FallDownHurt;

    [SerializeField] AudioClip T_Background;

    [SerializeField] GameObject T_Change;

    [SerializeField] GameObject Smoke;
    private int reverse = 1;
    public bool FastFoward = false;

    [SerializeField] GameObject FastForward_BG;

    private void FixedUpdate() {
        if(T_Change != null)
        {
            if(reverse == 1)
            {
                Color data = T_Change.GetComponent<Image>().color;
                data.a -= 0.012f;
                if(data.a < 0)
                    data.a = 0;
                T_Change.GetComponent<Image>().color = data;
            }
            else if(reverse == 2){
                Color data = T_Change.GetComponent<Image>().color;
                data.a += 0.05f;
                if(data.a > 1)
                    data.a = 1;
                T_Change.GetComponent<Image>().color = data;
            }
            else if(reverse == 3)
            {
                Color data = T_Change.GetComponent<Image>().color;
                data.a -= 0.05f;
                T_Change.GetComponent<Image>().color = data;
            }
        }
        if(FastFoward)
        {
            reverse = 2;
            Ba_SoundEffect.volume -= 0.02f;
            AS_SoundEffect.volume -= 0.02f;
            Invoke("reverse3On",1.5f);
        }
    }
    private void reverse3On(){
        FastFoward = false;
        
        Color data = FastForward_BG.GetComponent<Image>().color;
        data.a = 1;
        FastForward_BG.GetComponent<Image>().color = data;

        Invoke("ChangeSceneHelp", 0.6f);

        reverse = 3;
    }
    private void ChangeSceneHelp(){
        if(gameObject.GetComponent<ChangeScene>() != null)
            gameObject.GetComponent<ChangeScene>().Change_Scene2_By("TrailerExit");
    }

    public void F_playFall(){
        AS_SoundEffect.PlayOneShot(Falling);
    }

    public void F_playFallDownHurt(){
        AS_SoundEffect.PlayOneShot(FallDownHurt);
    }

    public void F_background(){
        Ba_SoundEffect.PlayOneShot(T_Background);
    }

    public void SmokeOn(){
        Smoke.SetActive(true);
    }
    public void ChangeSceneOn(){
        reverse = 2;
    }
    public void ChangeSceneOff(){
        reverse = 3;
    }
}

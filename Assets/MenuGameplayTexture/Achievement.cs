using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Achievement : MonoBehaviour
{
    [SerializeField] Vector2 [] TableChildArea;
    [SerializeField] GameObject CellTable;

    [SerializeField] GameObject ButtonPrairie;
    [SerializeField] GameObject ButtonForest;
    [SerializeField] GameObject ButtonGraveyard;
    [SerializeField] GameObject ButtonVoid;

    [SerializeField] GameObject PrairieDetail;
    [SerializeField] GameObject ForestDetail;
    [SerializeField] GameObject GraveyardDetail;
    [SerializeField] GameObject VoidDetail;

    [SerializeField] GameObject PrairieLocation;
    [SerializeField] GameObject ForestLocation;
    [SerializeField] GameObject GraveyardLocation;
    [SerializeField] GameObject VoidLocation;

    //[SerializeField] GameObject Canvas;
    //private int row = 10;
    private float padEachRow = -70;
    //private float yTableStart = -30;

    Setting MenuSetting;

    void Awake()
    {
        MenuSetting = GameObject.FindGameObjectWithTag("SettingObject").GetComponent<Setting>();

        CreatePrairieAchievementTable();
        CreateForestAchievementTable();
        CreateGraveyardAchievementTable();
        CreateVoidAchievementTable();
        OnPrairieFirst();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPrairieFirst(){
        PrairieDetail.SetActive(true);
        ForestDetail.SetActive(false);
        GraveyardDetail.SetActive(false);
        VoidDetail.SetActive(false);
        ButtonPrairie.GetComponent<Button>().interactable = false;
        ButtonForest.GetComponent<Button>().interactable = true;
        ButtonGraveyard.GetComponent<Button>().interactable = true;
        ButtonVoid.GetComponent<Button>().interactable = true;
    }
    public void OnPrairie(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);

        PrairieDetail.SetActive(true);
        ForestDetail.SetActive(false);
        GraveyardDetail.SetActive(false);
        VoidDetail.SetActive(false);

        ButtonPrairie.GetComponent<Button>().interactable = false;
        ButtonForest.GetComponent<Button>().interactable = true;
        ButtonGraveyard.GetComponent<Button>().interactable = true;
        ButtonVoid.GetComponent<Button>().interactable = true;
    }
    private void CreatePrairieAchievementTable()
    {
    for(int i=0; i < Data_Player.Achievement_Prairie.Count; i++)
    {
        string [] data_achievement = Data_Player.Achievement_Prairie[i];
        for(int j = 0; j < TableChildArea.Length; j++)
            {
                GameObject cell = Instantiate(CellTable,PrairieLocation.transform.parent);
                Vector3 dataPos = TableChildArea[j];
                dataPos.y += padEachRow *i;
                cell.transform.position += dataPos;
                
                cell.GetComponentInChildren<TextMeshProUGUI>().text = data_achievement[j];
            }
        }
    }

    public void OnForest(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);

        PrairieDetail.SetActive(false);
        ForestDetail.SetActive(true);
        GraveyardDetail.SetActive(false);
        VoidDetail.SetActive(false);

        ButtonPrairie.GetComponent<Button>().interactable = true;
        ButtonForest.GetComponent<Button>().interactable = false;
        ButtonGraveyard.GetComponent<Button>().interactable = true;
        ButtonVoid.GetComponent<Button>().interactable = true;
    }
    private void CreateForestAchievementTable()
    {
    for(int i=0; i < Data_Player.Achievement_Forest.Count; i++)
    {
        string [] data_achievement = Data_Player.Achievement_Forest[i];
        for(int j = 0; j < TableChildArea.Length; j++)
            {
                GameObject cell = Instantiate(CellTable,ForestLocation.transform.parent);
                Vector3 dataPos = TableChildArea[j];
                dataPos.y += padEachRow *i;
                cell.transform.position += dataPos;
                
                cell.GetComponentInChildren<TextMeshProUGUI>().text = data_achievement[j];
            }
        }
    }
    public void OnGraveyard(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);

        PrairieDetail.SetActive(false);
        ForestDetail.SetActive(false);
        GraveyardDetail.SetActive(true);
        VoidDetail.SetActive(false);

        ButtonPrairie.GetComponent<Button>().interactable = true;
        ButtonForest.GetComponent<Button>().interactable = true;
        ButtonGraveyard.GetComponent<Button>().interactable = false;
        ButtonVoid.GetComponent<Button>().interactable = true;
    }
    private void CreateGraveyardAchievementTable()
    {
    for(int i=0; i < Data_Player.Achievement_Graveyard.Count; i++)
    {
        string [] data_achievement = Data_Player.Achievement_Graveyard[i];
        for(int j = 0; j < TableChildArea.Length; j++)
            {
                GameObject cell = Instantiate(CellTable,GraveyardLocation.transform.parent);
                Vector3 dataPos = TableChildArea[j];
                dataPos.y += padEachRow *i;
                cell.transform.position += dataPos;
                
                cell.GetComponentInChildren<TextMeshProUGUI>().text = data_achievement[j];
            }
        }
    }
    public void OnVoid(){
        MenuSetting.playSFX(MenuSetting.SFX_ClickingButton);

        PrairieDetail.SetActive(false);
        ForestDetail.SetActive(false);
        GraveyardDetail.SetActive(false);
        VoidDetail.SetActive(true);

        ButtonPrairie.GetComponent<Button>().interactable = true;
        ButtonForest.GetComponent<Button>().interactable = true;
        ButtonGraveyard.GetComponent<Button>().interactable = true;
        ButtonVoid.GetComponent<Button>().interactable = false;
    }
    private void CreateVoidAchievementTable()
    {
    for(int i=0; i < Data_Player.Achievement_Void.Count; i++)
    {
        string [] data_achievement = Data_Player.Achievement_Void[i];
        for(int j = 0; j < TableChildArea.Length; j++)
            {
                GameObject cell = Instantiate(CellTable,VoidLocation.transform.parent);
                Vector3 dataPos = TableChildArea[j];
                dataPos.y += padEachRow *i;
                cell.transform.position += dataPos;
                
                cell.GetComponentInChildren<TextMeshProUGUI>().text = data_achievement[j];
            }
        }
    }

}



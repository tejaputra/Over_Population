using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject[] PrairieEnemy;
    [SerializeField] private GameObject[] ForestEnemy;
    [SerializeField] private GameObject[] GraveyardEnemy;
    [SerializeField] private GameObject[] VoidEnemy;
    [SerializeField] private GameObject [] arrayBoss;
    
    [SerializeField] private GameObject BossSlimeKing;
    [SerializeField] private GameObject BossOrcKing;
    [SerializeField] private GameObject BossLich;

     [SerializeField] private GameObject Prairie;
    [SerializeField] private GameObject Forest;
    [SerializeField] private GameObject Graveyard;
    [SerializeField] private GameObject Void;
    
    public static float RespawnTime;
    [SerializeField] int TotalKill = 0;
    public static int TotalKillForBoss = 0;
    public static int CounterDeath = 0;
    public static int BossCounterDeath = 0;
    private GameObject playerObject;
    public static bool onceSpawnBoss = true;

    [SerializeField] bool Debug = false;
    private int Mode = 1;
    // Start is called before the first frame update
    private void Awake() {
        onceSpawnBoss = true;
        CounterDeath = 0;
        RespawnTime = 0f;
        TotalKillForBoss = TotalKill;
    }
    void Start()
    {
        TotalKillForBoss = TotalKill;
        Mode = Data_Player.mode;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        if(Mode == 1)
        {
            Prairie.SetActive(true);
        } 
        else if(Mode == 2)
        {
            Forest.SetActive(true);
        } 
        else if(Mode == 3)
        {
            Graveyard.SetActive(true);
        } 
        else if(Mode == 4)
        {
            Void.SetActive(true);
        }

    }
    // Update is called once per frame
    private float howManyBoss = 1;
    [SerializeField] float timeInSec = 10;
    void FixedUpdate()
    {
        if(Debug)
            return;
            
        if(playerObject != null)
        {
            //khusus adventure
            if(CounterDeath < TotalKillForBoss && Mode != 4)
                WaveController();
            else if(onceSpawnBoss == true && Mode != 4)
            {
                onceSpawnBoss = false;
                if(Mode == 1)
                    SpawnBossSlimeKing();
                if(Mode == 2)
                    SpawnBossOrc();
                if(Mode == 3)
                    SpawnBossLich();
            }
            //khusus void
            if(Mode == 4)
            {
                WaveController();
                if( GameObject.FindWithTag("Game_Information").GetComponent<SystemGameplay>().getTimePlay() > (timeInSec * howManyBoss)){
                    if(howManyBoss > 3)
                    {
                        SpawnBoss(arrayBoss);
                    }
                    howManyBoss++;
                    SpawnBoss(arrayBoss);
                }
            }
        }
    }

    private void WaveController(){
        RespawnTime -= Time.deltaTime;
        //print("Respawm : " + RespawnTime);
        if(RespawnTime <= 0)
        {
            if(Mode == 1)
                SpawnEnemy(PrairieEnemy);
            if(Mode == 2)
                SpawnEnemy(ForestEnemy);
            if(Mode == 3)
                SpawnEnemy(GraveyardEnemy);
            if(Mode == 4)
                SpawnEnemy(VoidEnemy);
        }
    }
    public void SpawnEnemy(GameObject[] arrayEnemy){    
        Vector3 randomPosition = RandomizeEnemyPositionWithPattern();

        //##random na masih belum persentase
        int randomPointer = Random.Range(0,arrayEnemy.Length);
        RespawnTime = 0.3f;
        GameObject spawnEnemy = Instantiate(arrayEnemy[randomPointer], randomPosition, transform.rotation);
        
    }
    public void SpawnBoss(GameObject[] arrayEnemy){    
        Vector3 randomPosition = RandomizeEnemyPositionWithPattern();

        //##random na masih belum persentase
        int randomPointer = Random.Range(0,arrayEnemy.Length);
        GameObject spawnEnemy = Instantiate(arrayEnemy[randomPointer], randomPosition, transform.rotation);
        
    }
    public void SpawnBossOrc(){    
        Vector3 randomPosition = RandomizeEnemyPositionWithPattern();

        //##random na masih belum persentase
        RespawnTime = 0.3f;
        Instantiate(BossOrcKing, randomPosition, transform.rotation);
        
    }

    public void SpawnBossLich(){    
        Vector3 randomPosition = RandomizeEnemyPositionWithPattern();

        //##random na masih belum persentase
        RespawnTime = 0.3f;
        Instantiate(BossLich, randomPosition, transform.rotation);
        
    }
    public void SpawnBossSlimeKing(){    
        Vector3 randomPosition = RandomizeEnemyPositionWithPattern();

        //##random na masih belum persentase
        RespawnTime = 0.3f;
        Instantiate(BossSlimeKing, randomPosition, transform.rotation);
        
    }

    private int random = 0;
    [SerializeField] private float rangeSpawnOutsideScreen = 0;
    [SerializeField] private float spawnTopDown = 0;
    [SerializeField] private float spawnLeftRight = 0;
    private Vector3 RandomizeEnemyPositionWithPattern(){
        Vector3 enemyPosition = new Vector3(0f,0f,0f);
        if(playerObject!= null)
            enemyPosition = playerObject.transform.position;
        enemyPosition.z = 0;

        float randomX = Random.Range(-14f,14f);
        float randomY = Random.Range(-7f,7f);

        switch(random){
            case 0:
                if(randomX > 0)
                    enemyPosition.x = rangeSpawnOutsideScreen + spawnTopDown + enemyPosition.x;
                else
                    enemyPosition.x = -rangeSpawnOutsideScreen - spawnTopDown + enemyPosition.x;
                enemyPosition.y = enemyPosition.y + randomY;
                break;
            case 1:
                enemyPosition.x = enemyPosition.x + randomX;
                if(randomY > 0)
                    enemyPosition.y = rangeSpawnOutsideScreen + spawnLeftRight + enemyPosition.y;
                else
                    enemyPosition.y = -rangeSpawnOutsideScreen - spawnLeftRight + enemyPosition.y; 
                break;
        }
        random++;
        if(random > 1){
            random = 0;
        }

        return enemyPosition;
    }
    [SerializeField] GameObject BossHPBar;
    [SerializeField] GameObject BossNameBar;
    public void turnOnBossBar(float Health, float MaxHealth, bool boss,string name){
        if(BossHPBar.activeSelf == false && boss)
        {
            BossHPBar.SetActive(true);
            BossHPBar.GetComponent<Slider>().value = Health/MaxHealth * 100;
            BossNameBar.GetComponent<TextMeshProUGUI>().text = name;
        }
    }
    public void turnOffBossBar(){
        BossHPBar.SetActive(false);
    }
    public void UpdateHealthBar(float Health, float MaxHealth, string name){
        if(BossHPBar.activeSelf == false) 
            BossHPBar.SetActive(true);
        BossNameBar.GetComponent<TextMeshProUGUI>().text = name;
        BossHPBar.GetComponent<Slider>().value = Health/MaxHealth * 100;
    }
}

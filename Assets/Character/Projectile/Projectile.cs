using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float Time_Left = 5;
    [SerializeField] private float speed = 5;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject explosionSkillEffect;
    private bool Skill = false;
    public void OnSkill(){
        Skill = true;
    }
    private Vector3 direction;
    private float Time_Now = 0;
    private float Attack_Projectile = 0;
    private int Type_Bullet = 0;
    private bool stop = false;
    private float homingAccuracy = 1f;
    private float homingReminder = 10f;
    private Vector3 LastPositionPlayer = new (0,0,0);
    private GameObject playerObject;

    Setting MenuSetting;

    // 1 normal bullet, 2 explosive bullet player, 3 homing necro, 4 homing lich, 5 homing negative vision
    private float Negative_Time = 0;

    public void setNegative_Time(float time){
        Negative_Time = time;
    }
    public float getNegative_Time(){
        return Negative_Time;
    }
    private string NegativeName = "";

    public void setNegativeName(string name){
        NegativeName = name;
    }
    public string getNegativeName(){
        return NegativeName;
    }

    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        MenuSetting = GameObject.FindGameObjectWithTag("SettingObject").GetComponent<Setting>();
    }

    // Update is called once per frame
    private void FixedUpdate() {
        Time_Now += Time.deltaTime;
        if(Time_Left < Time_Now)
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            DestroyObject();
        }

        homingReminder += Time.deltaTime;
        if(!stop)
        {
            if (Type_Bullet == 3)
            {
                if(homingReminder > homingAccuracy)
                {
                    LastPositionPlayer = playerObject.transform.position;
                    homingReminder = 0;
                }
                Vector3 aimDirection = ( LastPositionPlayer - gameObject.transform.position);
                float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                angle = angle * Mathf.Deg2Rad;
                aimDirection.x = Mathf.Cos(angle);
                aimDirection.y = Mathf.Sin(angle);

                gameObject.GetComponent<Rigidbody2D>().velocity = (aimDirection * speed * Time.deltaTime);

                Vector3 Direction = (aimDirection).normalized;
                float angleH = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.AngleAxis(angleH,Vector3.forward);
                transform.rotation = targetRotation;
            } 
            else if (Type_Bullet == 4)
            {
                Vector3 aimDirection = ( playerObject.transform.position - gameObject.transform.position);
                float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                angle = angle * Mathf.Deg2Rad;
                aimDirection.x = Mathf.Cos(angle);
                aimDirection.y = Mathf.Sin(angle);

                gameObject.GetComponent<Rigidbody2D>().velocity = (aimDirection * speed * Time.deltaTime);

                Vector3 Direction = (aimDirection).normalized;
                float angleH = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.AngleAxis(angleH,Vector3.forward);
                transform.rotation = targetRotation;
            }
            else if (Type_Bullet == 5)
            {
                Vector3 aimDirection = ( playerObject.transform.position - gameObject.transform.position);
                float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                angle = angle * Mathf.Deg2Rad;
                aimDirection.x = Mathf.Cos(angle);
                aimDirection.y = Mathf.Sin(angle);

                gameObject.GetComponent<Rigidbody2D>().velocity = (aimDirection * speed * Time.deltaTime);

                Vector3 Direction = (aimDirection).normalized;
                float angleH = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.AngleAxis(angleH,Vector3.forward);
                transform.rotation = targetRotation;
            }
            else 
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = (direction * speed * Time.deltaTime);
            }
        }
    }

    bool once = true;
    void OnTriggerEnter2D(Collider2D target)
    {
        //Destroy(gameObject,0);
        if(Type_Bullet == 1)
        {
            DestroyObject();
        }
        else if (Type_Bullet == 2)//## masih ada error
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            GameObject animation = gameObject;
            float data = gameObject.GetComponentInChildren<CircleCollider2D>().radius;
            float divide = 1;
            if(once)
            {
                once = false;
                if(!Skill)
                {
                    MenuSetting.playSFX(MenuSetting.SFX_Fireball_Impact,"");
                    animation = Instantiate(explosionEffect, gameObject.transform.position, new Quaternion(0,0,0,0));
                    divide = 2.5f;
                }
                else
                {
                    MenuSetting.playSFX(MenuSetting.SFX_Fireball_Impact_Skill,"");
                    animation = Instantiate(explosionSkillEffect, gameObject.transform.position, new Quaternion(0,0,0,0));
                    divide = 2.8f;
                }
            }
            Vector3 scale = new Vector3(data/divide,data/divide,0);
            animation.transform.localScale = scale;

            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.GetComponentInChildren<CircleCollider2D>().enabled = true;
            stop = true;
            Color testColor = Color.white;
            testColor.a = 0;
            gameObject.GetComponent<SpriteRenderer>().color = testColor;

            Invoke("DestroyObject", 0.05f);
        }
        else if (Type_Bullet == 3)
            DestroyObject();
        else if (Type_Bullet == 4)
            DestroyObject();
        else if (Type_Bullet == 5)
            DestroyObject();
    }
    private void DestroyObject(){
        Destroy(gameObject);
    }

    public void getDirection(Vector3 Direction){
        direction = Direction;
    }
    public void SetAtkDamageProjetile(float Damage){
        Attack_Projectile = Damage;
    }
    public float GetAtkDamageProjetile(){
        return Attack_Projectile;
    }
    public void SetTypeBullet(int Type){
        Type_Bullet = Type;
    }
    public int GetTypeBullet(){
        return Type_Bullet;
    }
    public void SetSpeed(float SpeedBullet){
        speed = SpeedBullet;
    }
    public void SetTime(float TimeDelete){
        Time_Left = TimeDelete;
    }
}

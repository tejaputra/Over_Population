using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    internal GameObject playerObject;
    internal string BossName;
    internal float Health = 0;
    internal float MaxHealth = 0;
    private float Attack = 0;
    private float Movement_Speed = 0;
    private float Point = 0;
    private float Exp = 0;
    [HideInInspector] public bool meleeAtk = false;
    internal float Scale_X = 0;
    
    void Awake(){
        if(GameObject.FindGameObjectWithTag("Player") != null)
            playerObject = GameObject.FindGameObjectWithTag("Player");
        Scale_X = transform.localScale.x;
    }
    bool Explosion = false;
    void OnTriggerEnter2D(Collider2D target)
    {
        
        //checkin for take range attack from player
        if(playerObject != null && target.tag == "Player_Projectile" && !GameObject.FindWithTag("Player_Explosion"))
        {
            float damageTaken = GameObject.FindWithTag("Player_Projectile").GetComponent<Projectile>().GetAtkDamageProjetile();
            TakeDamage(damageTaken);
            //Debug.Log("Kena damage range " + damageTaken);
        }
        if(!Explosion && playerObject != null &&target.name == "Explosion")
        {
            float damageTaken = GameObject.FindWithTag("Player_Projectile").GetComponent<Projectile>().GetAtkDamageProjetile();
            TakeDamage(damageTaken);
            Explosion = true;
            //Debug.Log("Kena damage magic " + damageTaken);
            Invoke("ExplosionCancel", 0.1f);
        }
        //checkin for take melee attack from player
        if(playerObject != null && target.tag == "Player_Melee") //sudah oke
        {
            float damageTaken = GameObject.FindWithTag("Player_Melee").GetComponent<AtkSwordHolder>().GetDamage();
            TakeDamage(damageTaken);
        }
        
    }

    private void ExplosionCancel()
    {
        Explosion = false;
    }


    internal void DamagePlayerMelee(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D collider in colliders)   
        {
            if (collider.GetComponent<PolygonCollider2D>() != null && PlayerDeathCheck() && meleeAtk)
            {
                Player_Script playerScript = GameObject.FindWithTag("Player").GetComponent<Player_Script>();
                playerScript.TakeDamage(Attack);
            }
        }
    }
    internal bool DamagePlayerBoss(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D collider in colliders)   
        {
            if ((collider.GetComponent<CapsuleCollider2D>() != null ||  collider.GetComponent<CircleCollider2D>() != null) && PlayerDeathCheck())
            {
                Player_Script playerScript = GameObject.FindWithTag("Player").GetComponent<Player_Script>();
                playerScript.TakeDamage(Attack);
                return false;
            }
        }
        return true;
    }

    void DamagePlayerMeleeWormy(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D collider in colliders)   
        {
            if (collider.GetComponent<PolygonCollider2D>() != null && PlayerDeathCheck() && meleeAtk)
            {
                Player_Script playerScript = GameObject.FindWithTag("Player").GetComponent<Player_Script>();
                playerScript.TakeDamage(Attack);
            }
        }
    }

    public void MovementEnemy(){
        if(playerObject != null)
        {
            if(playerObject.transform.position.x + 0.3f > transform.position.x)
            {
                if(gameObject.name.Contains("Slime"))
                {
                    transform.localScale = new Vector2(-Scale_X, transform.localScale.y);
                } 
                else 
                {
                    transform.localScale = new Vector2(Scale_X, transform.localScale.y);
                }
            } 
            else if (playerObject.transform.position.x - 0.3f < transform.position.x)
            {
                if(gameObject.name.Contains("Slime"))
                {
                    transform.localScale = new Vector2(Scale_X, transform.localScale.y);
                } 
                else 
                {
                    transform.localScale = new Vector2(-Scale_X, transform.localScale.y);
                }
            }

            Vector3 direction = playerObject.transform.position - gameObject.transform.position;
            direction.y -= 0.75f;
            gameObject.GetComponent<Rigidbody2D>().velocity = direction.normalized * Time.deltaTime * Movement_Speed*50;
        }
    }
    public void RotationEnemy(){
        if(playerObject.transform.position.x + 0.3f > transform.position.x)
        {
            if(gameObject.name.Contains("Slime"))
            {
                transform.localScale = new Vector2(-Scale_X, transform.localScale.y);
            } 
            else 
            {
                transform.localScale = new Vector2(Scale_X, transform.localScale.y);
            }
        } 
        else if (playerObject.transform.position.x - 0.3f < transform.position.x)
        {
            if(gameObject.name.Contains("Slime"))
            {
                transform.localScale = new Vector2(Scale_X, transform.localScale.y);
            } 
            else 
            {
                transform.localScale = new Vector2(-Scale_X, transform.localScale.y);
            }
        }
    }
    public bool PlayerDeathCheck(){
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            return true;
        } else {
            return false;
        }
    }
    public bool MeleeEnemyCheck(){
        if(meleeAtk){
            return true;
        } else {
            return false;
        }
    }

    private void MeleeEnemyOff(){
        meleeAtk = false;
    }

    public bool CheckAttackInsideMainCamera(float rangeAttack){
        if(playerObject != null)
        {
            Vector3 stageDimensionTopLeft = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
            Vector3 stageDimensionBotRight = stageDimensionTopLeft;
            stageDimensionBotRight.x = (playerObject.transform.position.x - stageDimensionTopLeft.x) + playerObject.transform.position.x;
            stageDimensionBotRight.y = (playerObject.transform.position.y - stageDimensionTopLeft.y) + playerObject.transform.position.y;

            stageDimensionTopLeft.x -= rangeAttack*1.7f;
            stageDimensionTopLeft.y -= rangeAttack;
            stageDimensionBotRight.x += rangeAttack*1.7f;
            stageDimensionBotRight.y += rangeAttack;

            if(stageDimensionTopLeft.x > transform.position.x && stageDimensionBotRight.x < transform.position.x &&
                stageDimensionTopLeft.y > transform.position.y && stageDimensionBotRight.y < transform.position.y)
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckAttackOutsideMainCamera(float rangeAttack){
        if(playerObject != null)
        {
            Vector3 stageDimensionTopLeft = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
            Vector3 stageDimensionBotRight = stageDimensionTopLeft;
            stageDimensionBotRight.x = (playerObject.transform.position.x - stageDimensionTopLeft.x) + playerObject.transform.position.x;
            stageDimensionBotRight.y = (playerObject.transform.position.y - stageDimensionTopLeft.y) + playerObject.transform.position.y;

            stageDimensionTopLeft.x -= rangeAttack*1.5f;
            stageDimensionTopLeft.y -= rangeAttack;
            stageDimensionBotRight.x += rangeAttack*1.5f;
            stageDimensionBotRight.y += rangeAttack;

            if(stageDimensionTopLeft.x < transform.position.x && stageDimensionBotRight.x > transform.position.x &&
                stageDimensionTopLeft.y < transform.position.y && stageDimensionBotRight.y > transform.position.y)
            {
                return true;
            }
        }
        return false;
    }
    private Vector3 Angle_Direction;
    public Quaternion AngleAttack(Transform Pivot,float offset){
        Quaternion targetRotation =  new Quaternion(0,0,0,0);
        if(playerObject != null)
            {
            Vector3 aimDirection = ( playerObject.transform.position - Pivot.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            Angle_Direction = aimDirection;
            targetRotation = Quaternion.AngleAxis(angle + offset,Vector3.forward);
            }
        return targetRotation;

    }
    private Vector3 Angle_Direction2;
    public Vector3 AngleDegreeAttack(Transform Pivot,float offset){
        Vector3 Angle_Direction2 =  new Vector3(0,0,0);
        if(playerObject != null)
        {
            Vector3 aimDirection = ( playerObject.transform.position - Pivot.position);
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            angle += offset;
            angle = angle * Mathf.Deg2Rad;
            aimDirection.x = Mathf.Cos(angle);
            aimDirection.y = Mathf.Sin(angle);
            //float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            Angle_Direction2 = aimDirection;
        }
        return Angle_Direction2;
    }
    internal void RangeAttack(GameObject Projectile, Transform Pivot, int Type, float DamageEnemies){
        GameObject Bullet = Instantiate(Projectile, Pivot.position,AngleAttack(Pivot, 0));
        Bullet.GetComponent<Projectile>().getDirection(Angle_Direction);
        Bullet.GetComponent<Projectile>().SetTypeBullet(Type);
        Bullet.GetComponent<Projectile>().SetAtkDamageProjetile(DamageEnemies);
    }
    internal void RangeAttack(GameObject Projectile, Transform Pivot, int Type, float DamageEnemies, float Speed, float timeDeath){
        GameObject Bullet = Instantiate(Projectile, Pivot.position,AngleAttack(Pivot, 0));
        Bullet.GetComponent<Projectile>().getDirection(Angle_Direction);
        Bullet.GetComponent<Projectile>().SetTypeBullet(Type);
        Bullet.GetComponent<Projectile>().SetAtkDamageProjetile(DamageEnemies);
        Bullet.GetComponent<Projectile>().SetSpeed(Speed);
        Bullet.GetComponent<Projectile>().SetTime(timeDeath);
    }
    internal void RangeAttack(GameObject Projectile, Transform Pivot, float offset, int Type, float DamageEnemies, float Speed, float timeDeath){
        Vector3 data = AngleDegreeAttack(Pivot, offset);
        Quaternion quaternion = Quaternion.Euler(data.x, data.y, data.z);
        GameObject Bullet = Instantiate(Projectile, Pivot.position, quaternion);
        Bullet.GetComponent<Projectile>().getDirection(Angle_Direction);
        Bullet.GetComponent<Projectile>().SetTypeBullet(Type);
        Bullet.GetComponent<Projectile>().SetAtkDamageProjetile(DamageEnemies);
        Bullet.GetComponent<Projectile>().SetSpeed(Speed);
        Bullet.GetComponent<Projectile>().SetTime(timeDeath);
    }
    internal void RangeAttackEffect(GameObject Projectile, Transform Pivot, int Type, float DamageEnemies, float Speed, float timeDeath, string negativeEffect, float negativeTime){
        GameObject Bullet = Instantiate(Projectile, Pivot.position,AngleAttack(Pivot, 0));
        Bullet.GetComponent<Projectile>().getDirection(Angle_Direction);
        Bullet.GetComponent<Projectile>().SetTypeBullet(Type);
        Bullet.GetComponent<Projectile>().SetAtkDamageProjetile(DamageEnemies);
        Bullet.GetComponent<Projectile>().SetSpeed(Speed);
        Bullet.GetComponent<Projectile>().SetTime(timeDeath);
        Bullet.GetComponent<Projectile>().setNegativeName(negativeEffect);
        Bullet.GetComponent<Projectile>().setNegative_Time(negativeTime);
    }
    internal void Summoning(List<GameObject> enemies, List<Vector3> pivot){
        int lengthEnemies = enemies.Count;
        int lengthPivot = pivot.Count;
        while(enemies.Count != 0 && pivot.Count != 0){
            int randomSelectionEnemies = Random.Range(0,lengthEnemies);
            int randomSelectionPivot = Random.Range(0,lengthPivot);
            GameObject summon = Instantiate(enemies[randomSelectionEnemies], pivot[randomSelectionPivot], new Quaternion(0,0,0,0));
            summon.GetComponent<Enemy>().Summoned = true;
            enemies.RemoveAt(randomSelectionEnemies);
            pivot.RemoveAt(randomSelectionPivot);
            lengthEnemies--;
            lengthPivot--;
        }
    }

    Color RemainderColor;
    private void TakeDamage(float Damage_Player){
        Health -= Damage_Player;
        if(boss){
            GameObject.FindGameObjectWithTag("Game_Information").GetComponent<SpawnController>().UpdateHealthBar(Health, MaxHealth, BossName);
        }

        //effect color
        if(gameObject.GetComponent<SpriteRenderer>().color != Color.red)
        {
            RemainderColor = gameObject.GetComponent<SpriteRenderer>().color;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        Invoke("backColor", 0.1f);
    }
    private void backColor(){
        gameObject.GetComponent<SpriteRenderer>().color = RemainderColor;
    }

    public bool CheckHealth(){
        if(Health <= 0){
            return true;
        } else {
            return false;
        }
    }
    [SerializeField] GameObject Death_Animation;
    public void DestroyObject(){
        if(Death_Animation != null)
        {
            GameObject _death_Animation = Instantiate(Death_Animation, gameObject.transform.position, gameObject.transform.rotation);
            Vector3 _data = _death_Animation.transform.localScale;
            if(Random.Range(1,2) == 1)
            {
                _data.x = -1;
                _data.x += Random.Range(-5,10) * 0.01f;
            }
            else
            {
                _data.x = 1;
                _data.x -= Random.Range(-5,10) * 0.01f;
            }
            _data.y -= Random.Range(-5,10) * 0.01f;

            _death_Animation.transform.localScale = _data;
        }

        SpawnController.CounterDeath += 1;
        playerObject.GetComponent<Player_Script>().setExp(Exp);
        playerObject.GetComponent<Player_Script>().setPoint(Point);
        if(boss)
        {
            SpawnController.BossCounterDeath += 1;
            GameObject.FindGameObjectWithTag("Game_Information").GetComponent<SpawnController>().turnOffBossBar();
        }
        Destroy(gameObject);
    }
    [SerializeField] internal bool Summoned = false;
    [SerializeField] internal float _multiply_Time_Void_Spawn = 0.5f;
    public void setParameter(float enemyHealth, float enemyAttack, float enemyMovement, float enemyPoint, float enemyExp, float SpawnTime){
        Health = enemyHealth;
        MaxHealth = Health;
        Attack = enemyAttack;
        Movement_Speed = enemyMovement;
        Point = enemyPoint;
        Exp = enemyExp;
        if(!Summoned)
        {
            if(Data_Player.mode != 4)
                SpawnController.RespawnTime += SpawnTime;
            else
            {
                SpawnController.RespawnTime += (SpawnTime * _multiply_Time_Void_Spawn);
            }
        }
    }
    public void setParameterBoss(float enemyHealth, float enemyAttack, float enemyMovement, float enemyPoint, float enemyExp, string name){
        Health = enemyHealth;
        MaxHealth = Health;
        Attack = enemyAttack;
        Movement_Speed = enemyMovement;
        Point = enemyPoint;
        Exp = enemyExp;
        BossName = name;
    }

    //=================================================Boss==================================================================
    [SerializeField] internal bool boss = false;

    public bool MovementEnemyToPosition(Vector2 Position){
        Vector2 direction = playerObject.transform.position - gameObject.transform.position;
        direction += Position;
        Vector2 PositionedMove = playerObject.transform.position;
        PositionedMove += Position;
        gameObject.GetComponent<Rigidbody2D>().MovePosition(gameObject.GetComponent<Rigidbody2D>().position + (direction.normalized * Time.deltaTime * Movement_Speed *0.1f));
        if((gameObject.GetComponent<Rigidbody2D>().position.x - 0.5f < PositionedMove.x  && PositionedMove.x < gameObject.GetComponent<Rigidbody2D>().position.x + 0.5f) 
            && (gameObject.GetComponent<Rigidbody2D>().position.y - 0.5f < PositionedMove.y && PositionedMove.y < gameObject.GetComponent<Rigidbody2D>().position.y + 0.5f))
            return false;
        else
            return true;
    }
    public void MovementEnemyToPosition(Vector2 Position, float speed){
        Vector2 direction = Position;
        transform.position = Vector3.MoveTowards(gameObject.transform.position, direction, speed);
        /*if((gameObject.GetComponent<Rigidbody2D>().position.x - 0.5f < PositionedMove.x  && PositionedMove.x < gameObject.GetComponent<Rigidbody2D>().position.x + 0.5f) 
            && (gameObject.GetComponent<Rigidbody2D>().position.y - 0.5f < PositionedMove.y && PositionedMove.y < gameObject.GetComponent<Rigidbody2D>().position.y + 0.5f))
            return false;
        else
            return true;*/
    }
    public void MovementEnemyTowardPosition(Vector3 aimDirection){
        gameObject.GetComponent<Rigidbody2D>().MovePosition(gameObject.transform.position + aimDirection.normalized * 0.4f);
    }
    public void HealingPercentage(float healingHP){
        if(Health > 0)
            Health += healingHP/100 * MaxHealth;
        GameObject.FindGameObjectWithTag("Game_Information").GetComponent<SpawnController>().UpdateHealthBar(Health, MaxHealth, BossName);
    }
}

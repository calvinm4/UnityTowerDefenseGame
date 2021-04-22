using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemies : MonoBehaviour
{
    public List<GameObject> enemiesInRange;
    private float lastShotTime;
    private MonsterData monsterData;
    private float range;
    public string type;

    // Start is called before the first frame update
    void Start()
    {
        enemiesInRange = new List<GameObject>();
        lastShotTime = Time.time;
        monsterData = gameObject.GetComponentInChildren<MonsterData>();
        range = monsterData.CurrentLevel.range;

    }

    // Update is called once per frame
    void Update()
    {
        range = monsterData.CurrentLevel.range;
        Collider2D[] targs = Physics2D.OverlapCircleAll (transform.position,range);
        enemiesInRange.Clear();
        for (int i = 0; i < targs.Length;i++){
          if (targs[i].gameObject.tag.Equals("Enemy"))
                  {
                enemiesInRange.Add(targs[i].gameObject);
                  EnemyDestructionDelegate del =
                      targs[i].gameObject.GetComponent<EnemyDestructionDelegate>();
                  del.enemyDelegate += OnEnemyDestroy;
                  }

        }
        GameObject target = null;
        if(type == "Slow"){

          float minSpeed = 0;
          foreach (GameObject enemy in enemiesInRange)
          {

            float temp = enemy.GetComponent<MoveEnemy>().speed;
            if (temp > minSpeed)
            {
                Debug.Log("fucl");
                target = enemy;
                minSpeed = temp;
            }
        }
      }
        else if(type == "Weaken"){
          float minWeaken = 10;
          foreach (GameObject enemy in enemiesInRange)
          {

            float temp = enemy.GetComponent<MoveEnemy>().weakenAmount;
            if (temp < minWeaken)
            {
                Debug.Log("fucl");
                target = enemy;
                minWeaken = temp;
            }
        }
        }
        else{
        float minimalEnemyDistance = float.MaxValue;
          foreach (GameObject enemy in enemiesInRange)
          {
            float distanceToGoal = enemy.GetComponent<MoveEnemy>().DistanceToGoal();
            if (distanceToGoal < minimalEnemyDistance)
            {
                target = enemy;
                minimalEnemyDistance = distanceToGoal;
            }
          }
        }
        // 2
        if (target != null)
        {
        if (Time.time - lastShotTime > monsterData.CurrentLevel.fireRate)
        {
            Shoot(target.GetComponent<Collider2D>());
            lastShotTime = Time.time;
            Vector3 direction = gameObject.transform.position - target.transform.position;
            gameObject.transform.rotation = Quaternion.AngleAxis(
                Mathf.Atan2 (direction.y, direction.x) * 180 / Mathf.PI +90,
                new Vector3 (0, 0, 1));
        }
        // 3

        }
    }

    void OnEnemyDestroy(GameObject enemy)
    {
    enemiesInRange.Remove (enemy);
    }

//     void OnTriggerEnter2D (Collider2D other)
//     {
// // 2
//         if (other.gameObject.tag.Equals("Enemy"))
//         {
//         enemiesInRange.Add(other.gameObject);
//         EnemyDestructionDelegate del =
//             other.gameObject.GetComponent<EnemyDestructionDelegate>();
//         del.enemyDelegate += OnEnemyDestroy;
//         }
//     }
//     // 3
//     void OnTriggerExit2D (Collider2D other)
//     {
//         if (other.gameObject.tag.Equals("Enemy"))
//         {
//         enemiesInRange.Remove(other.gameObject);
//         EnemyDestructionDelegate del =
//             other.gameObject.GetComponent<EnemyDestructionDelegate>();
//         del.enemyDelegate -= OnEnemyDestroy;
//         }
//     }


    void Shoot(Collider2D target)
    {
    GameObject bulletPrefab = monsterData.CurrentLevel.bullet;
    // 1
    Vector3 startPosition = gameObject.transform.position;
    Vector3 targetPosition = target.transform.position;
    startPosition.z = bulletPrefab.transform.position.z;
    targetPosition.z = bulletPrefab.transform.position.z;

    // 2
    GameObject newBullet = (GameObject)Instantiate (bulletPrefab);
    newBullet.transform.position = startPosition;
    BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>();
    bulletComp.target = target.gameObject;
    bulletComp.startPosition = startPosition;
    bulletComp.targetPosition = targetPosition;

    // 3
    Animator animator =
        monsterData.CurrentLevel.visualization.GetComponent<Animator>();
    animator.SetTrigger("fireShot");
    AudioSource audioSource = gameObject.GetComponent<AudioSource>();
    audioSource.PlayOneShot(audioSource.clip);
    }
}

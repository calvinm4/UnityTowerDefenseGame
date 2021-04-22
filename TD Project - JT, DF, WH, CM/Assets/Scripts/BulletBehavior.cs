using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed = 10;
    public int damage;
    public GameObject target;
    public Vector3 startPosition;
    public Vector3 targetPosition;
    public float slowAmount;
    private float distance;
    private float startTime;
    public float weakenAmount; // damage multiplier for hits on monster: 1.5 = 1.5 times normal damage
    public GameObject explosion;

    private GameManagerBehavior gameManager;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        distance = Vector2.Distance (startPosition, targetPosition);
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManagerBehavior>();
        Vector3 direction = gameObject.transform.position - targetPosition;
        gameObject.transform.rotation = Quaternion.AngleAxis(
            Mathf.Atan2 (direction.y, direction.x) * 180 / Mathf.PI +90,
            new Vector3 (0, 0, 1));
    }

    // Update is called once per frame
    void Update()
    {
        float timeInterval = Time.time - startTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);

        // 2
        if (gameObject.transform.position.Equals(targetPosition))
        {
        if (target != null)
        {
            if (explosion != null){
              Instantiate(explosion, transform.position, Quaternion.identity); // damage handling done in explosion.cs so we return here
              Destroy(gameObject);
              return;
            }
            target.GetComponent<MoveEnemy>().weakenAmount = weakenAmount;
            if (target.GetComponent<MoveEnemy>().weakenAmount > 1){
              target.transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>().color = new Color (0.50f,0.1f,0.50f);
            }
            // 3
            Transform healthBarTransform = target.transform.Find("HealthBar");
            HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();

            healthBar.currentHealth -= Mathf.Max(damage * target.GetComponent<MoveEnemy>().weakenAmount, 0);
            // 4
            if (healthBar.currentHealth <= 0)
            {
            Destroy(target);
            AudioSource audioSource = target.GetComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

            gameManager.Gold += target.GetComponent<MoveEnemy>().goldValue;
            }
            float tempSpeed = target.GetComponent<MoveEnemy>().speed - slowAmount;
            if (tempSpeed > 0.1){
              target.GetComponent<MoveEnemy>().speed = tempSpeed;
            }
            else{
              target.GetComponent<MoveEnemy>().speed = 0.1f;
            }
        }
        Destroy(gameObject);
        }
    }
}

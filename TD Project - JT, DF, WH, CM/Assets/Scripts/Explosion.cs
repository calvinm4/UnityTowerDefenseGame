using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    Rigidbody2D rigidbody;
    public int damage;
    private List<int> colls = new List<int>();
    private GameManagerBehavior gameManager;
    void OnCollisionEnter2D(Collision2D other){

      if (other.gameObject.GetComponent<MoveEnemy>()) {

        if (!colls.Contains(other.gameObject.GetInstanceID())){

          colls.Add(other.gameObject.GetInstanceID());
          Transform healthBarTransform = other.gameObject.transform.Find("HealthBar");
          HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();

          healthBar.currentHealth -= Mathf.Max(damage, 0);
        // 4
          if (healthBar.currentHealth <= 0)
          {
            Destroy(other.gameObject);
            AudioSource audioSource = other.gameObject.GetComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

            gameManager.Gold += 50;
        }
        }
      }

    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManagerBehavior>();
        rigidbody = GetComponent<Rigidbody2D>();
        Destroy(gameObject,.25f);

    }

    // Update is called once per frame
    void Update()
    {

    }
}

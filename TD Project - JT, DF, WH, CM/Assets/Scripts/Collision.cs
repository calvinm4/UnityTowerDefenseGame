using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.GetComponent<EnemyMovement>()) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Source for the code below: https://answers.unity.com/questions/1216124/help-with-auto-moving-script.html
    Rigidbody2D _rb;
    public static int movespeed = 1;
    public static int weakenAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Script to set the enemy's position to automatically move
        transform.position = transform.position + (Vector3.right * movespeed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{


    void Start()
    {



    }

    // Update is called once per frame
    // void move(){
    //   Vector3 des = GameObject.Find("Enemy").transform.position;
    //   while (GameObject.Find("Rocket").transform.position !=  des){
    //     GameObject.Find("Rocket").transform.position = Vector2.MoveTowards(GameObject.Find("Rocket").transform.position,des, 0.1f);
    //   }
    // }


    void Update()
    {
        float step = 2 * Time.deltaTime;

          GameObject.Find("Rocket").transform.position = Vector2.MoveTowards(GameObject.Find("Rocket").transform.position,GameObject.Find("Enemy").transform.position,step);


    }
}

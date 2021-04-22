using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TreetopCollider : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.GetComponent<MoveEnemy>()) {
            GetComponent<Tilemap>().color = new Color(1, 1, 1, 0.5f);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.GetComponent<MoveEnemy>()) {
            GetComponent<Tilemap>().color = new Color(1, 1, 1, 1f);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
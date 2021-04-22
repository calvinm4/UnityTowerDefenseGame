using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButtonManager : MonoBehaviour
{

    public GameObject left;
    public GameObject right;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetMouseButtonDown (0)) {
        Vector3 vec = new Vector3(75f,0,0);
        Vector3 vec2 = new Vector3(0,25f,0);
        if (Input.mousePosition.x < left.transform.position.x - 75 || Input.mousePosition.x >  right.transform.position.x + 75 ||Input.mousePosition.y > left.transform.position.y +25){
          gameObject.SetActive(false);
        }

    }
  }
}

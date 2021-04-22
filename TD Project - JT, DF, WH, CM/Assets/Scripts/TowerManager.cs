using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{
    // Start is called before the first frame update


    private GameManagerBehavior gameManager;
    public GameObject buttons;
    private static GameObject curr;
    public void ShowOptions(){
        Debug.Log("here");
        GameObject.Find("Canvas").transform.Find("TowerOptions").gameObject.SetActive(true);
        int temp = 0;
 
        for (int i = curr.GetComponent<MonsterData>().levels.IndexOf(curr.GetComponent<MonsterData>().CurrentLevel); i>-1; i--){
          temp += curr.GetComponent<MonsterData>().levels[i].cost;
        }
        GameObject.Find("Canvas").transform.Find("TowerOptions").transform.Find("Button - Sell Tower").GetComponentInChildren<Text>().text = "Sell for " + (temp/2).ToString();
        if (curr.GetComponent<MonsterData>().GetNextLevel() != null){
          GameObject.Find("Canvas").transform.Find("TowerOptions").transform.Find("Button - Upgrade").GetComponentInChildren<Text>().text = "Upgrade for " + curr.GetComponent<MonsterData>().GetNextLevel().cost.ToString();
        }
        else{
          GameObject.Find("Canvas").transform.Find("TowerOptions").transform.Find("Button - Upgrade").GetComponentInChildren<Text>().text = "Already Max Level!";
        }
    }
    public void Sell(){
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        int temp = 0;

        for (int i = curr.GetComponent<MonsterData>().levels.IndexOf(curr.GetComponent<MonsterData>().CurrentLevel); i>-1; i--){
          temp += curr.GetComponent<MonsterData>().levels[i].cost;
        }
        gameManager.Gold += temp/2;
        Destroy(curr);
        GameObject.Find("Canvas").transform.Find("TowerOptions").gameObject.SetActive(false);

    }
    public void Upgrade(){
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        if (curr.GetComponent<MonsterData>().GetNextLevel() != null){

          if (gameManager.Gold >= curr.GetComponent<MonsterData>().GetNextLevel().cost){
          gameManager.Gold -= curr.GetComponent<MonsterData>().GetNextLevel().cost;
          curr.GetComponent<MonsterData>().IncreaseLevel();
          GameObject.Find("Canvas").transform.Find("TowerOptions").gameObject.SetActive(false);
          Debug.Log(gameManager);

          curr = null;
          }
        }
    }
    void OnMouseUp(){
      curr = gameObject;

      ShowOptions();





    }
    void Start()
    {
      gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }
    void Awake(){

    }

    // Update is called once per frame
    void Update()
    {

    }
}

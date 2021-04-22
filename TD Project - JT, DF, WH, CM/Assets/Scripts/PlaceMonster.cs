using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMonster : MonoBehaviour
{

    static private bool sell = false;
    private static GameObject monsterPrefab;
    public GameObject monsterPrefabSlow;
    public GameObject monsterPrefabStandard;
    private GameObject monster;
    private GameManagerBehavior gameManager;
    public GameObject monsterPrefabExplosive;
    public GameObject monsterPrefabWeaken;
    public GameObject TowerTypes;
    public static bool isPaused;
    private static Vector3 pos;
    private bool hasTower;

    public void PlaceStandard(){

      gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
      int cost = monsterPrefabStandard.GetComponent<MonsterData>().levels[0].cost;
      if(gameManager.Gold >= cost){
        monster = (GameObject)Instantiate(monsterPrefabStandard, pos, Quaternion.identity);
        GameObject.Find("Canvas").transform.Find("Tower Buttons").gameObject.SetActive(false);
        gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;
        hasTower = true;
        
        Debug.Log(hasTower + "eqhjfbjwhebfhjwebfjhwbehj");
    }

    }
    public void PlaceSlow(){
      gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
      int cost = monsterPrefabSlow.GetComponent<MonsterData>().levels[0].cost;
      if(gameManager.Gold >= cost){
        monster = (GameObject)Instantiate(monsterPrefabSlow, pos, Quaternion.identity);
        GameObject.Find("Canvas").transform.Find("Tower Buttons").gameObject.SetActive(false);
        gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;
        hasTower = true;
      }


    }
    public void PlaceExplosive(){
      gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
      int cost = monsterPrefabExplosive.GetComponent<MonsterData>().levels[0].cost;
      if(gameManager.Gold >= cost){
        monster = (GameObject)Instantiate(monsterPrefabExplosive, pos, Quaternion.identity);
        GameObject.Find("Canvas").transform.Find("Tower Buttons").gameObject.SetActive(false);
        gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;
        hasTower = true;
      }
    }
    public void PlaceWeaken(){
      gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
      int cost = monsterPrefabWeaken.GetComponent<MonsterData>().levels[0].cost;
      if(gameManager.Gold >= cost){
        monster = (GameObject)Instantiate(monsterPrefabWeaken, pos, Quaternion.identity);
        GameObject.Find("Canvas").transform.Find("Tower Buttons").gameObject.SetActive(false);
        gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;
        hasTower = true;
      }
    }





    void TowerTypeOptions(){
        GameObject.Find("Canvas").transform.Find("Tower Buttons").gameObject.SetActive(true);

    }
    void OnMouseUp()
    {

      if (isPaused){
          return;
      }
      pos = transform.position;
      Debug.Log(hasTower);

      if (!hasTower){
        TowerTypeOptions();
        return;
      }
      else if (monster != null){
        //monster = monster;
        return;
      }
        if (CanSellMonster()){






        //   if (isPaused)
        //   {
        //       return;
        //   }
        //   int temp = 0;
        //   for (int i = monster.GetComponent<MonsterData>().levels.IndexOf(monster.GetComponent<MonsterData>().CurrentLevel); i>-1; i--){
        //     temp += monster.GetComponent<MonsterData>().levels[i].cost;
        //   }
        //   gameManager.Gold += temp/2;
        //   Destroy(monster);
        //
        // }
        //
        // else if (CanPlaceMonster())
        // {
        //     if (isPaused)
        //     {
        //         return;
        //     }
        //     else {
        //     monster = (GameObject)
        //         Instantiate(monsterPrefab, transform.position, Quaternion.identity);
        //
        //     AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        //     audioSource.PlayOneShot(audioSource.clip);
        //
        //     gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;
        //     }
        // }
        // else if (CanUpgradeMonster())
        // {
        //     if (isPaused)
        //     {
        //         return;
        //     }
        //     else {
        //     monster.GetComponent<MonsterData>().IncreaseLevel();
        //     AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        //     audioSource.PlayOneShot(audioSource.clip);
        //     gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;
        //     }
        //

  }}
    public void sellToggle(){

      if (sell){
        sell = false;
      }
      else{
        sell = true;
      }
    }

    private bool CanSellMonster(){

      return sell && monster != null;
    }

    private bool CanUpgradeMonster()
{
  if (monster != null)
  {
    MonsterData monsterData = monster.GetComponent<MonsterData>();
    MonsterLevel nextLevel = monsterData.GetNextLevel();
    if (nextLevel != null)
    {
      return gameManager.Gold >= nextLevel.cost;
    }
  }
  return false;
}
    // Start is called before the first frame update
    void Start()
    {
      gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
      monsterPrefab = monsterPrefabStandard;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

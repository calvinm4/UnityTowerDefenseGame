using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerBehavior : MonoBehaviour
{
    public Text waveLabel;
    public GameObject[] nextWaveLabels;
    public bool gameOver = false;
    private int wave;
    public bool isPaused;
    public int Wave
    {
        get
        {
            return wave;
        }
        set
        {
            wave = value;
            if (!gameOver)
            {
                for (int i = 0; i < nextWaveLabels.Length; i++)
                {
                    nextWaveLabels[i].GetComponent<Animator>().SetTrigger("nextWave");
                }
            }
            waveLabel.text = "WAVE: " + (wave + 1);
        }
    }


    public Text goldLabel;
    private int gold;
    public int Gold {
      get
      {
        return gold;
      }
      set
      {
        gold = value;
        goldLabel.GetComponent<Text>().text = "GOLD: " + gold;
      }
}
    // Start is called before the first frame update
    void Start()
    {
        Gold = 1000;
        Wave = 0;
        Health = 3;
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Open Menu
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                MenuController.instance.Show();
            }
            else
            {
                MenuController.instance.Hide();
            }
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void InfiniteGold()
    {
        Gold = 999999999;
    }

    public Text healthLabel;
    public GameObject[] healthIndicator;
    private int health;

public int Health
{
  get
  {
    return health;
  }
  set
  {
    // 1
    if (value < health)
    {
      Camera.main.GetComponent<CameraShake>().Shake();
    }
    // 2
    health = value;
    // healthLabel.text = "HEALTH: " + health;
    // 3
    if (health <= 0 && !gameOver)
    {
      gameOver = true;
      GameObject gameOverText = GameObject.FindGameObjectWithTag("GameOver");
      gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
    }
    // 4 
    for (int i = 0; i < healthIndicator.Length; i++)
    {
      if (i < Health)
      {
        healthIndicator[i].SetActive(true);
      }
      else
      {
        healthIndicator[i].SetActive(false);
      }
    }
  }
}
}

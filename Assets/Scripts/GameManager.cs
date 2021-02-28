using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum STATUS
{
    WOOD = 0,
    SLIVER,
    GOLD,
    NONE
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //public STATUS status;

    public GameObject finishPanel;
    public GameObject abandonPanel;
    public TextMeshProUGUI chestLabel;
    public TextMeshProUGUI timerLabel;
    public GameObject centerObject;
    public GameObject obstacleObject;
    public GameObject playerObject;
    public float radius;
    public int smallCircleNumbers = 10;
    public int middleCircleNumbers = 20;
    public int largeCircleNumbers = 30;

    private int timer = 0;
    private int playerSkill;
    private int puzzleLevel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        centerObject = GameObject.FindGameObjectWithTag("center");
    }

    void SpawnFirstObstacles()
    {
        var center = centerObject.transform.position;
        for (int i = 0; i < smallCircleNumbers; i++)
        {
            var pos = RandomCircle(center, radius);
            // make the object face the center
            var rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            
            var current = Instantiate(obstacleObject, pos, rot);
            current.transform.parent = centerObject.transform;
        }
    }

    void SpawnSecondObstacles()
    {
        var center = centerObject.transform.position;
        for (int i = 0; i < middleCircleNumbers; i++)
        {
            var pos = RandomCircle(center, radius * 2);
            var rot = Quaternion.FromToRotation(Vector3.forward, center - pos);

            var current = Instantiate(obstacleObject, pos, rot);
            current.transform.parent = centerObject.transform;
        }
    }

    void SpawnThirdObstacles()
    {
        var center = centerObject.transform.position;
        for (int i = 0; i < middleCircleNumbers; i++)
        {
            var pos = RandomCircle(center, radius * 3);
            var rot = Quaternion.FromToRotation(Vector3.forward, center - pos);

            var current = Instantiate(obstacleObject, pos, rot);
            current.transform.parent = centerObject.transform;
        }
    }

    private void Start()
    {
        playerSkill = PlayerPrefs.HasKey("skill") ? PlayerPrefs.GetInt("skill") : 50;
        finishPanel.SetActive(false);
        abandonPanel.SetActive(false);
        centerObject = GameObject.FindGameObjectWithTag("center");
        string name = PlayerPrefs.HasKey("status") ? PlayerPrefs.GetString("status") : "wood";
        if (name == "wood")
        {
            chestLabel.text = ("Wooden Chest: Easy Level");
            puzzleLevel = 150;
            timer = 90;
            SpawnFirstObstacles();
        }
        if (name == "sliver")
        {
            chestLabel.text = ("Sliver Chest: Moderate Level");
            puzzleLevel = 170;
            timer = 60;
            SpawnFirstObstacles();
            SpawnSecondObstacles();
        }
        if (name == "gold")
        {
            chestLabel.text = ("Golden Chest: Hard Level");
            puzzleLevel = 200;
            timer = 30;
            SpawnFirstObstacles();
            SpawnSecondObstacles();
            SpawnThirdObstacles();        
        }
        SpawnPlayerBlock();
        timerLabel.text = timer.ToString();
        StartCoroutine(WaitAndCount(1.0f));
    }

    public void FinishTheMinigame()
    {
        if (playerSkill <= 90)
            playerSkill += 10;
        PlayerPrefs.SetInt("skill", playerSkill);
        StartCoroutine(Wait(2.0f));
        finishPanel.SetActive(true);
    }

    private void Update()
    {
    }
    public void SpawnPlayerBlock()
    {
        Instantiate(playerObject, new Vector3(0.0f, -17.0f, 10.0f), Quaternion.identity);
    }

    Vector3 RandomCircle(Vector3 center, float radius) 
    { 
        var ang = Random.value * 360; 
        Vector3 pos; 
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad); 
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad); 
        pos.z = center.z; 
        return pos; 
    }

    IEnumerator WaitAndCount(float time)
    {
        yield return new WaitForSeconds(time);
        if (timer > 0)
            timer--;
        else
        {
            AbandoneTheMinigame();
        }
        timerLabel.text = timer.ToString();
        StartCoroutine(WaitAndCount(1.0f));
    }
    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("SampleScene");
    }
    
    void AbandoneTheMinigame()
    {
        StartCoroutine(Wait(2.0f));
        abandonPanel.SetActive(true);
    }

    public int CalculateObstacleSpeed()
    {
        return puzzleLevel - playerSkill;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestController : MonoBehaviour
{
    // Start is called before the first frame update

    public CharacterManager character;


    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "wood")
        {
            PlayerPrefs.SetInt("skill", character.playerSkill);
            PlayerPrefs.SetString("status", "wood");
            SceneManager.LoadScene("MinigameScene");
        }
        if (gameObject.tag == "sliver")
        {
            PlayerPrefs.SetInt("skill", character.playerSkill);
            PlayerPrefs.SetString("status", "sliver");
            SceneManager.LoadScene("MinigameScene");
        }
        if (gameObject.tag == "gold")
        {
            PlayerPrefs.SetInt("skill", character.playerSkill);
            PlayerPrefs.SetString("status", "gold");
            SceneManager.LoadScene("MinigameScene");
        }
    }
}

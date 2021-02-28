using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterManager : MonoBehaviour
{
    [Range(0, 100)] public int playerSkill = 50;
    public float speed = 25.0f;
    public float gravity = 40.0F;
    private Vector3 moveDirection = Vector3.zero;
    private float turner;
    private float looker;
    public float sensitivity = 5;
    private CharacterController controller;
    public TextMeshProUGUI playerSkillLabel;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        controller = GetComponent<CharacterController>();
        if (PlayerPrefs.HasKey("skill"))
        {
            playerSkillLabel.text = "Player Lockpick Skill:" + PlayerPrefs.GetInt("skill");
            playerSkill = PlayerPrefs.GetInt("skill");
        }
        else
        {
            playerSkillLabel.text = "Player Lockpick Skill:" + playerSkill;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"),-Input.GetAxis("Vertical"),0);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }

        turner = Input.GetAxis("Mouse X") * sensitivity;
        looker = Input.GetAxis("Mouse Y") * sensitivity;
        if (turner != 0)
        {
            transform.eulerAngles += new Vector3(0, turner, 0);
        }
        //if (looker != 0)
        //{
        //    transform.eulerAngles += new Vector3(looker, 0, 0);
        //}
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}

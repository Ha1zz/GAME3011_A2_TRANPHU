using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMinigameManager : MonoBehaviour
{
    public float speed = 25.0f;
    public float gravity = 40.0F;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private Transform originalPosition;
    private Rigidbody rigidbody;
    private bool check = false;

    // Start is called before the first frame update
    void Start()
    {
        check = true;
        rigidbody = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        originalPosition = transform;
        if (rigidbody.velocity.magnitude > 0)
        {
            controller.Move(new Vector3(0, 0, 0));
        }

    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void MoveBack()
    {
        transform.position = new Vector3(0.0f, -17.0f, 10.0f);
        controller.enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "obstacle")
        {
            controller.enabled = false;
            MoveBack();
        }
        if (other.tag == "center" && check == true)
        {
            check = false;
            GameManager.instance.FinishTheMinigame();
        }
    }
}

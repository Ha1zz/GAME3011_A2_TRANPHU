using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private float rotateSpeed = 200f;
    public GameObject centerObject;
    private float radius = 40f;
    private float angle;
    private Vector3 center;
    private float timeCounter;

    private Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        centerObject = GameObject.FindGameObjectWithTag("center");
        radius = Vector3.Distance(centerObject.transform.position, this.transform.position);
        //rigidBody.velocity = new Vector3(10.0f, 0.0f, 0);
        //ApplyCircularAcceleration(centerObject.gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        centerObject = GameObject.FindGameObjectWithTag("center");
        transform.RotateAround(centerObject.transform.position, new Vector3(0.0f, 0.0f, 1.0f), GameManager.instance.CalculateObstacleSpeed() * Time.deltaTime);
        //Vector3 toCenter = center - transform.position;
        //centerObject = GameObject.FindGameObjectWithTag("center");
        //ApplyCircularAcceleration(centerObject.gameObject.transform.position);

        //centerObject = GameObject.FindGameObjectWithTag("center");
        //center.z = centerObject.transform.position.z;

        //angle += rotateSpeed * Time.deltaTime;

        //var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
        //transform.position = center + new Vector3(offset.x, offset.y, centerObject.transform.position.z);

        //timeCounter += Time.deltaTime * rotateSpeed;
        //timeCounter += rotateSpeed;
        //float x = Mathf.Cos(timeCounter * radius);//* centerObject.transform.position.x;
        //float y = Mathf.Sin(timeCounter * radius); //* centerObject.transform.position.y;
        //float y = centerObject.transform.position.x + 1;
        //float x = centerObject.transform.position.y + 1;
        //var check = transform.InverseTransformPoint(centerObject.transform.position);
        //check.z = check
        //float z = check.z;//centerObject.transform.position.z;
        //float z = centerObject.transform.position.z + 1;

        //transform.localPosition = new Vector3(x, y, z);
    }


    void ApplyCircularAcceleration(Vector3 center)
    {
        Vector3 toCenter = center - transform.position;
        Debug.Log("ToCenter" + toCenter);
        Debug.Log("CHECK" + toCenter.normalized);
        Debug.Log("CHECKHEY" + rigidBody.velocity.sqrMagnitude);
        rigidBody.velocity += toCenter.normalized * rigidBody.velocity.sqrMagnitude / toCenter.magnitude;
        //Debug.Log("CHECK" + rigidBody.velocity);
    }
}

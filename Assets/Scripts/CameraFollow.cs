using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	private GameObject ball, cameraPoint;
	private Vector3 offSet;

    // Start is called before the first frame update
    void Start()
    {
        cameraPoint = GameObject.FindWithTag("CameraPoint");
        ball = GameObject.FindWithTag("PlayerBall");
        offSet = this.gameObject.transform.position - ball.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (ball.transform.position.y > cameraPoint.transform.position.y)
        this.transform.position = ball.transform.position + offSet;
    }
}

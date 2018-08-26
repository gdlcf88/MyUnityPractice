using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    //定义移动的速度
    public float MoveSpeed = 8f;

    void Start()
    {

    }
    void Update()
    {
        //如果按下W或上方向键
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            MoveForward();
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            MoveBack();
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Dump();
        }
    }

    void MoveForward()
    {
        this.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
    }
    void MoveBack()
    {
        this.transform.Translate(Vector3.back * MoveSpeed * Time.deltaTime);
    }
    void MoveLeft()
    {
        this.transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
    }
    void MoveRight()
    {
        this.transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
    }
    void Dump()
    {
        if (this.transform.position.y == 1.0)
        {
            this.transform.Translate(Vector3.up * MoveSpeed * Time.deltaTime * 20);
        }
    }
}

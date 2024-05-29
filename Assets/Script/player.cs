using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Transform monster;
    public Transform target;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        // Set vi tri
        // monster.position = new Vector3(1f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 3f)
        {
            if (target != null)
            {
                monster.position = Vector2.MoveTowards(monster.position, target.position, 0.01f);
            }
            time = 0f;
        }
        //monster.position = new Vector2 (0, 0);
            //Doi kich co
            monster.localScale = new Vector3(1f, 1f, 1f);
        //Kiểu di chuyển 1 có tọa độ đích
        monster.position = Vector2.MoveTowards(monster.position, target.position, 0.01f);
        //Kiểu di chuyển nhấn phím
        //if (Input.GetKey(KeyCode.W))
        //{
        //    monster.Translate(Vector3.up * Time.deltaTime * speed);
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    monster.Translate(Vector3.left * Time.deltaTime * speed);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    monster.Translate(Vector3.down * Time.deltaTime * speed);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    monster.Translate(Vector3.right * Time.deltaTime * speed);
        //}
        //GetKey => nhận lệnh theo phím bấm
        //GetKeyDown => nhận lệnh theo số lần bấm phím
    }
}

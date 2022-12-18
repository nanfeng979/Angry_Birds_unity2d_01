using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black_Bird : Bird_red
{
    // 获取猪和建筑物对象集合
    public List<Pig_green> EnemyObj = new List<Pig_green>();

    // 对象触碰到触发器时
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "enemy")
        {
            EnemyObj.Add(other.gameObject.GetComponent<Pig_green>());
        }
    }

    // 对象离开触发器时
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "enemy")
        {
            EnemyObj.Remove(other.gameObject.GetComponent<Pig_green>());
            other.gameObject.GetComponent<Pig_green>().Dead();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    // 单例
    public static GameObjectManager instance;

    // 红鸟数组
    public List<Bird_red> bird_Reds;
    // 绿猪数组
    public List<Pig_green> pig_Greens;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        // 初始化
        init();
    }

    // 初始化
    private void init() {
        // 遍历bird_Reds
        foreach(Bird_red bird_red in bird_Reds)
        {
            // 获取当前遍历出来的子物体的下标
            int index = bird_Reds.IndexOf(bird_red);
            if(index == 0)
            {
                // 启用弹簧和脚本组件
                bird_red.enabled = true;
                bird_red.sp.enabled = true;
            }
            else
            {
                // 禁用弹簧和脚本组件
                bird_red.enabled = false;
                bird_red.sp.enabled = false;
            }
        }
    }

    public void afterBirdFly()
    {
        // 检查pig_Greens与bird_Reds的数量
        if(pig_Greens.Exists(t => t == null))
        {
            // pig_Greens没有子对象时
            Debug.Log("赢了");
            return;
        }
        if(bird_Reds.Count >= pig_Greens.Count)
        {
            // bird_Reds比pig_Greens多时
            // 初始化
            init();
        } else {
            Debug.Log("输了");
        }
    }
}

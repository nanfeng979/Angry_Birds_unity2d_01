using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// panel23就改bg颜色，分23阶段
public class GameObjectManager : MonoBehaviour
{
    // 单例
    public static GameObjectManager instance;

    // 红鸟数组
    public List<Bird_red> bird_Reds;
    // 绿猪数组
    public List<Pig_green> pig_Greens;

    

    // bird_red一开始时所在的位置
    private Vector3 bird_start_pos;


    // 获取结算界面
    public GameObject win;
    public GameObject lose;
    // 星星数组界面
    public GameObject[] starts;


    private int guanqiaNum = 4;

    private void Awake() {
        instance = this;
        // 获得bird_red一开始时所在的位置
        bird_start_pos = bird_Reds[0].transform.position;
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
                // 改变下一个bird_red的位置，使得不会受到弹簧很大的拉力
                bird_red.transform.position = bird_start_pos;
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
        init();
        // 检查pig_Greens与bird_Reds的数量
        if(pig_Greens.Count <= 0)
        {
            // pig_Greens没有子对象时
            win.SetActive(true);
            showStarts();
            Debug.Log("赢了");
        }
        // if(bird_Reds.Count >= pig_Greens.Count)
        // {
        //     // bird_Reds比pig_Greens多时
        //     // 初始化
        //     init();
        // } 
        // else {
        if(pig_Greens.Count > 0 && bird_Reds.Count == 0) {
            lose.SetActive(true);
            Debug.Log("输了");
        }
    }

    // 显示星星个数函数
    public void showStarts() {
        // 执行协程函数showStartsByTime
        StartCoroutine("showStartsByTime");
    }

    // 协程函数
    IEnumerator showStartsByTime() {
        // 根据游戏逻辑显示星星个数，目前暂且全部显示
        for(int i = 0; i < 3; i++)
        {
            // 等待0.3f执行
            yield return new WaitForSeconds(0.3f);
            // 显示星星
            starts[i].SetActive(true);
        }
    }

    // 
    public void Replay()
    {
        SaveData();
        SceneManager.LoadScene(2);
    }

    public void Home()
    {
        SaveData();
        SceneManager.LoadScene(1);
    }

    // 播放音效
    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    public void SaveData() {
        // 默认获得三颗星
        PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), 3);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelect : MonoBehaviour
{
    public int starsCount = 0;
    public bool canSelect = false;

    public GameObject lock_;
    public GameObject stars;

    public GameObject panel;

    public GameObject map;

    private void Start() {
        // 计算所有关卡的星星数量
        int sum = 0;
        for(int i = 1; i <= 4; i++) {
            sum += PlayerPrefs.GetInt("levelNo" + i);
        }
        // 
        PlayerPrefs.SetInt("totalNum", sum);

        if(PlayerPrefs.GetInt("totalNum", 0) >= starsCount) {
            canSelect = true;
        }
        
        Debug.Log(PlayerPrefs.GetInt("totalNum"));

        if(canSelect) {
            lock_.SetActive(false);
            stars.SetActive(true);
        }
    }

    // 鼠标点击方法
    public void Select() {
        if(canSelect) {
            panel.SetActive(true);
            map.SetActive(false);
        }
    }
}

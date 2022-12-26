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
        if(PlayerPrefs.GetInt("totalNum", 0) >= starsCount) {
            canSelect = true;
        }
        // PlayerPrefs.SetInt("totalNum", 0);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelect : MonoBehaviour
{
    public int starsCount = 0;
    public bool canSelct = false;

    public GameObject lock_;
    public GameObject stars;


    private void Start() {
        if(PlayerPrefs.GetInt("totalNum", 0) >= starsCount) {
            canSelct = true;
        }

        if(canSelct) {
            lock_.SetActive(false);
            stars.SetActive(true);
        }
    }
}

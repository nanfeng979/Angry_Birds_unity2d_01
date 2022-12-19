using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Select : MonoBehaviour
{

    public bool canSelect = false;

    public Sprite levelBg;

    private Image image;

    private void Awake() {
        image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(transform.parent.GetChild(0).name == gameObject.name) {
            canSelect = true;
        }

        if(canSelect) {
            image.overrideSprite = levelBg;
            transform.Find("num").gameObject.SetActive(true);
        }
    }

    public void Selected() {
        if(canSelect) {
            PlayerPrefs.SetString("nowLevel", "level" + gameObject.name);
        }
    }

}

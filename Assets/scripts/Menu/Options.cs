using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Options : MonoBehaviour
{
    private GameObject musicSlider;
    private GameObject off;
    private GameObject on;
    private float volume;
    private float savedVolume;
    


    void Start()
    {
        musicSlider = GameObject.Find("Music");                         // sala ti amo <3
        off = GameObject.Find("OffButton");
        on = GameObject.Find("OnButton");
    }

    void Update()
    {
        volume = musicSlider.GetComponent<UnityEngine.UI.Slider>().value;
        if (volume == 0f)
        {
            off.SetActive(false);
            on.SetActive(true);
        }
        else
        {
            on.SetActive(false);
            off.SetActive(true);
        }
    }
    public void SaveVolume()
    {
        savedVolume = volume;
    }
    public void SetVolume()
    {
        musicSlider.GetComponent<UnityEngine.UI.Slider>().value = savedVolume;
    }


}

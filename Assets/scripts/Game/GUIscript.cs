using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIscript : MonoBehaviour
{
    private GameObject dummyHealtBar;
    private GameObject playerHealtBar;
    private GameObject dummy;
    private GameObject player;


    private float dummyHealt;
    private float playerHealt;

    // Start is called before the first frame update
    void Start()
    {
        dummyHealtBar = GameObject.Find("DummyHealtBar");
        dummy = GameObject.Find("Dummy");
        playerHealtBar = GameObject.Find("HealtBar");
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dummyHealt = dummy.GetComponent<DummyController>().currentHealt;
        dummyHealtBar.GetComponent<Slider>().value = dummyHealt;
        playerHealt = player.GetComponent<PlayerController>().currentHealt;
        playerHealtBar.GetComponent<Slider>().value = playerHealt;
    }
}

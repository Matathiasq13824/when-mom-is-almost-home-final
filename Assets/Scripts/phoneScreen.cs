using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class phoneScreen : MonoBehaviour
{
    private GameObject player;
    public GameObject phone;
    private bool inGameMenuActive = false;
    private int flag = 0;
    public GameObject textMeshPro;
    public System.DateTime startTime;
    AudioSource audioData;
    private int spritenum = 0;
    private int spriten;

    void Start()
    {
        player = GameObject.Find("OVRPlayerController");
        phone = GameObject.Find("phone1k");
        inGameMenuActive = false;
        textMeshPro = GameObject.Find("phone screen hologram");
        startTime = System.DateTime.UtcNow;
        spriten = 0;
    }

    void Update()
    {
        System.TimeSpan ts = System.DateTime.UtcNow - startTime;
        if(ts.Seconds == 30)
        {
            //sending audio notification to user and updating image on textmesh
            if (spritenum/72 < 6)
            {
                audioData = phone.GetComponent<AudioSource>();
                audioData.Play(0);
                spritenum += 1;
            }
        }


        if ((player.transform.position-phone.transform.position).sqrMagnitude<3*3)
        {
            // the player is within a radius of 3 units to this game object
            if (OVRInput.Get(OVRInput.Button.One) && !inGameMenuActive)
            {
                //display the phone screen hologram
                spriten = spritenum/72;
                textMeshPro.GetComponent<TextMeshPro>().text = "<sprite="+spriten.ToString()+">";
                inGameMenuActive = true;
                flag=1;
            }
            else
            {
                flag = 0;
            }
        }
        if(flag == 1)
        {
            //display the phone screen hologram
            spriten = spritenum/72;
            textMeshPro.GetComponent<TextMeshPro>().text = "<sprite="+spriten.ToString()+">";
        }
        if (inGameMenuActive && OVRInput.Get(OVRInput.Button.Two))
        {
            //remove the display of the phone screen
            inGameMenuActive = false;
            textMeshPro.GetComponent<TextMeshPro>().text = "";
            flag = 0;
        }
    }
}

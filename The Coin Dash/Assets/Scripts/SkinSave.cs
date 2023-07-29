using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSave : MonoBehaviour
{
    public Transform player;
    private int index;

    public void Start()
    {
        for (int i = 0; i < player.childCount; i++)
            player.GetChild(i).gameObject.SetActive(false);

        index = PlayerPrefs.GetInt("chosenSkin");
        player.GetChild(index).gameObject.SetActive(true);
    }
}

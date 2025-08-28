using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] private GameObject player;
    void HideEggAndShowPlayer()
    {
        AudioManager.instance.PLayCrackEggClip();
        gameObject.SetActive(false);
        player.SetActive(true);
    }
}

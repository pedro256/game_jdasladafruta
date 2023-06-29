using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventsController : MonoBehaviour
{
    PlayerController player;
    MotoController moto;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        moto = FindObjectOfType<MotoController>();

        player.interactedBike += moto.PlayerInteractionBike;
    }


}

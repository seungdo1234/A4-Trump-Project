using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Flipped : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Back;

    public void Flipped_Card()
    {
        Back.color = Color.grey;
    }
}

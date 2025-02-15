using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerStats")]
[System.Serializable]
public class PlayerStats : ScriptableObject
{
    [SerializeField, Tooltip("Movement speed of player.")]
    private float speed;
    [SerializeField, Tooltip("Jump speed of player.")]
    private float jumpSpeed;

    public float Speed
    {
        get { return speed; }
    }

    public float JumpSpeed
    {
        get { return jumpSpeed; }
    }
}

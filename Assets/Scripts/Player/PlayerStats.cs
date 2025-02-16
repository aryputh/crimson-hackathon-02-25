using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerStats")]
[System.Serializable]
public class PlayerStats : ScriptableObject
{
    [SerializeField, Tooltip("Movement speed of player.")]
    private float movementSpeed;
    [SerializeField, Tooltip("Jump speed of player.")]
    private float jumpSpeed;

    public float MovementSpeed
    {
        get { return movementSpeed; }
    }

    public float JumpSpeed
    {
        get { return jumpSpeed; }
    }
}

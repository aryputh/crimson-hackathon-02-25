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
    [SerializeField, Tooltip("Does this toggle gravity?")]
    private bool togglesGravity;

    public float MovementSpeed => movementSpeed;
    public float JumpSpeed => jumpSpeed;
    public bool TogglesGravity
    {
        get => togglesGravity;
        set => togglesGravity = value;
    }
}

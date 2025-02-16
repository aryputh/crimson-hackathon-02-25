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
    [SerializeField, Tooltip("A number representing how the platform alters gravity.")]
    private int gravityInfluence;

    public float MovementSpeed => movementSpeed;
    public float JumpSpeed => jumpSpeed;
    public int GravityInfluence => gravityInfluence;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoosterStats : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 150f;
    [SerializeField] private float _flyYPower = 70f;
    public float MovementSpeed => _movementSpeed;
    public float FlyYPower => _flyYPower;
}

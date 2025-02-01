using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    public IntReference HP;
    [SerializeField]
    public IntReference damage;
    [SerializeField]
    public FloatReference interactionDistance;
    [SerializeField]
    public FloatReference speed;
}

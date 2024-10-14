using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VolumeConfig", menuName = "ScriptableObjects/Audio/VolumeConfig", order = 1)]
public class VolumeConfig : ScriptableObject
{
    [Range(0f, 1f)]
    public float volume = 0.5f;
}

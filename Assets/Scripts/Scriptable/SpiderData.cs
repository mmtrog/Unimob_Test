using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SpiderData", menuName = "ScriptableObjects/SpiderData", order = 1)]
public class SpiderData : ScriptableObject
{
    public AnimatorOverrideController animator;

    public float speed;
}

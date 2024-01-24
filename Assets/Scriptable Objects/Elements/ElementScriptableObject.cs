using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "element", menuName = "DataElement", order = 100)]

public class ElementScriptableObject : ScriptableObject
{
    public Sprite spriteElement;

    public ElementsPlayer element;

    public AudioClip audio;

    public Color colorElement;
}

public enum ElementsPlayer
{
    Fire,
    Water,
    Thunder,
    Earth,
    Light,
    Nothing
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Color Data", menuName = "Card Color Data")]
public class CardColorData : ScriptableObject
{
    public Color color;
    public Color championBoxColor;
    public Color nameBoxColor;
    public Color descriptionBoxColor;
}
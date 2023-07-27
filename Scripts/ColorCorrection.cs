using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCorrection : MonoBehaviour
{
    [SerializeField] Color _player = new Color(220, 220, 0);
    [SerializeField] Color _obstacle = new Color(220, 220, 0);
    [SerializeField] Color _tube = new Color(220, 220, 220);
    [SerializeField] Color _death = new Color(220, 70, 70);
}

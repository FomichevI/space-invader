using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    public static Counter S;

    public int count { get { return _count; } }
    private int _count = 0;
    [SerializeField] private TextMeshProUGUI counterText;


    void Start()
    {
        if (S == null)
            S = this;
        counterText.text = _count.ToString();
    }

    public void CountUp(int value)
    {
        _count += value;
        counterText.text = _count.ToString();
    }
}

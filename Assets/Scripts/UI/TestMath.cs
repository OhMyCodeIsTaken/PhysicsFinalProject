using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMath : MonoBehaviour
{
    [SerializeField] private float _hyp;
    [SerializeField] private float _adj;
    [SerializeField] private float _opp;

    // Start is called before the first frame update
    void Start()
    {
        CalcAngle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CalcAngle()
    {
        double angle = Math.Acos(_adj / _hyp) * 180 / Math.PI;

        Debug.Log(angle);
    }
}

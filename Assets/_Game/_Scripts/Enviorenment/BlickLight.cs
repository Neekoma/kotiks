using UnityEngine;
using UnityEngine.Rendering.Universal;


public class BlickLight : MonoBehaviour
{
    [SerializeField]
    private Light2D _light;

    [SerializeField]
    private float _startT;

    [SerializeField]
    private float frequency;


    private void FixedUpdate()
    {
        _startT += Time.fixedDeltaTime;

        _light.intensity = Mathf.Abs(Mathf.Cos(_startT * frequency));
    }
}
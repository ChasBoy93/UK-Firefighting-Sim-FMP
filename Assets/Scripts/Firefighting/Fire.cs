using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] public float currentIntensity = 1.0f;
    private float startIntensity = 5.0f;

    public ParticleSystem firePS = null;

    void Start()
    {
        startIntensity = firePS.emission.rateOverTime.constant;
    }

    void Update()
    {
        ChangeIntensity();
    }

    void ChangeIntensity()
    {
        var emission = firePS.emission;
        emission.rateOverTime = currentIntensity * startIntensity;
    }
}

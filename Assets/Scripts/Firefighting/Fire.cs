using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float currentIntensity = 1.0f;
    float timeLastWatered = 0;
    public float regenDelay = 2.5f;
    public float regenRate = 0.1f;
    private float[] startIntensities = new float[0];

    public ParticleSystem[] fireParticleSystem = new ParticleSystem[0];

    private bool isLit = true;

    void Start()
    {
        startIntensities = new float[fireParticleSystem.Length];

        for(int i = 0; i < fireParticleSystem.Length; i++)
        {
            startIntensities[i] = fireParticleSystem[i].emission.rateOverTime.constant;
        }
    }


    void Update()
    {
        if(isLit && currentIntensity < 1.0f && Time.time - timeLastWatered >= regenDelay)
        {
            currentIntensity += regenRate * Time.deltaTime;
            ChangeIntensity();
        }
    }

    public bool TryExtinguish(float amount)
    {
        timeLastWatered = Time.time;
        currentIntensity -= amount;
        ChangeIntensity();
        if (currentIntensity <= 0)
        {
            isLit = false;
            return true;
        }


        return false; //Fire is still lit.
    }
    void ChangeIntensity()
    {
        for(int i = 0; i < fireParticleSystem.Length; i++)
        {
            var emission = fireParticleSystem[i].emission;
            emission.rateOverTime = currentIntensity * startIntensities[i];
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class LightFlickering : MonoBehaviour
{
    public new Light light;
    public float minIntensity = 0f;
    public float maxIntensity = 1f;
    [Range(1, 50)]
    public int smoothing = 5;

    Queue<float> smoothQueue;
    float lastSum = 0;


    public void Reset() {
        smoothQueue.Clear();
        lastSum = 0;
    }

    private void Start() {
        smoothQueue = new Queue<float>(smoothing);
    }

    private void Update() {
        while (smoothQueue.Count >= smoothing) {
            lastSum -= smoothQueue.Dequeue();
        }

        float newVal = Random.Range(minIntensity, maxIntensity);
        smoothQueue.Enqueue(newVal);
        lastSum += newVal;

        light.intensity = lastSum / (float)smoothQueue.Count;
    }
}

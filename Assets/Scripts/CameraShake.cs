using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float frequency = 0.5f;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        StartCoroutine(ShakeCamera(amplitude));
    }

    private IEnumerator ShakeCamera(float amplitude)
    {
        Vector3 originalPos = Vector3.zero;

        while (true)
        {
            float xOffset = Random.Range(-0.5f, 0.5f) * amplitude;
            float yOffset = Random.Range(-0.5f, 0.5f) * amplitude;

            transform.localPosition = new Vector3(xOffset, yOffset, originalPos.z);

            yield return new WaitForSeconds(frequency);
        }

        //transform.localPosition = originalPos;
    }
}

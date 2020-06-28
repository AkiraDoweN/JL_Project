using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject target;
    public float smoothing = 5.0f;
    private Vector3 offset;
    private Vector3 newCamPos;
    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    void Update()
    {
        newCamPos = target.transform.position + offset;

        transform.position = Vector3.Lerp(transform.position, newCamPos, smoothing * Time.deltaTime);
    }


    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(- 0.1f, 0.1f) * magnitude;
            float y = Random.Range(- 0.1f, 0.1f) * magnitude;

            transform.localPosition = new Vector3(newCamPos.x + x, originalPos.y, originalPos.z + y);

            elapsed += Time.deltaTime;

            yield return null;

        }
        transform.localPosition = originalPos;
    }


}

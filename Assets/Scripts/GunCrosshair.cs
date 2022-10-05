using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunCrosshair : MonoBehaviour
{
    [SerializeField] private RectTransform crosshair;

    [Header("Crosshair Properties")]
    public float minSize = 25f;
    public float idleSize = 50f;
    public float maxSize = 100f;
    public float currentSize = 50f;
    public float normalEnemyDistance = 25f;
    public float timeMultiplier = 3f;

    [Header("Other")]
    public Camera fpsCam;
    private Image[] crosshairImages;

    void Start()
    {
        crosshairImages = GetComponentsInChildren<Image>();
        // Debug.Log("crosshairImages: " + crosshairImages.Length);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit rHit;
        bool didHit = Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out rHit);
        if (didHit)
        {
            // Debug.Log("CrosshairOn: " + rHit.transform.name);
            // hit.rigidbody.AddForceAtPosition(transform.forward * impactForce, hit.point);
            // Debug.Log("Enemy: " + rHit.transform.CompareTag("Enemy"));
            bool isEnemy = rHit.transform.CompareTag("Enemy");
            // Debug.Log("Distance: " + rHit.distance);
            if (isEnemy)
            {
                foreach (var image in crosshairImages)
                {
                    image.color = Color.red;

                }

            }
            else
            {
                foreach (var image in crosshairImages)
                {
                    image.color = Color.green;

                }
            }
            if (rHit.distance < normalEnemyDistance)
            {
                currentSize = Mathf.Lerp(currentSize, minSize, Time.deltaTime * timeMultiplier);
            }
            else if (rHit.distance < normalEnemyDistance * 2)
            {
                currentSize = Mathf.Lerp(currentSize, idleSize, Time.deltaTime * timeMultiplier);
            }
            else
            {
                currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime * timeMultiplier);
            }
        }
        else
        {
            foreach (var image in crosshairImages)
            {
                image.color = Color.green;

            }
            currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime * 2f);
        }
        crosshair.sizeDelta = new Vector2(currentSize, currentSize);
    }

    void FixedUpdate()
    {
    }
}

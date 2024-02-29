using TMPro;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    #region Pressure Plate Variables
    [Header("Pressure Plate Variables")]
    [SerializeField] private LayerMask heavyEnough;

    [HideInInspector] public bool isTriggered;
    #endregion

    #region BoxCast Variables
    [Header("BoxCast Variables")]
    [SerializeField] private Vector3 boxSize;
    [SerializeField] private float castFrequency;
    [SerializeField] private float yOffset;

    private Vector3 center; 
    private float timeSinceLastCast;
    #endregion

    #region UI
    [Header("UI")]
    [SerializeField] private GameObject weightTemplate;
    [SerializeField] private string weightType;

    private WeightUI weightUI;
    #endregion

    [Header("Flame  Reference")]
    [SerializeField] private GameObject flame;

    private void Awake()
    {
        // Calculate the center position of the box with the desired offset along the y-axis
        center = transform.position + new Vector3(0f, yOffset, 0f);

        flame.SetActive(false);

        UpdateUI();
    }

    private void Update()
    {
        // Increment timeSinceLastCast
        timeSinceLastCast += Time.deltaTime;

        // Check if it's time to cast the box again
        if (timeSinceLastCast >= 1f / castFrequency)
        {
            // Reset timeSinceLastCast
            timeSinceLastCast = 0f;

            // Cast the box
            CastBox();
        }
    }

    private void CastBox()
    {
        // Collect layers of all colliders detected within the box
        Collider[] colliders = Physics.OverlapBox(center, boxSize / 2f, Quaternion.identity);

        // Integer to keep track of weight
        int weight = 0;

        // Display the layers of all detected colliders
        foreach (Collider collider in colliders)
        {
            if ((heavyEnough & (1 << collider.gameObject.layer)) != 0)
            {
                // Collider's layer is included in the heavyEnough layer mask
                weight++;
            }
        }

        if (weight > 0)
        {
            isTriggered = true;
            flame.SetActive(isTriggered);
        }
        else
        {
            isTriggered = false;
            flame.SetActive(isTriggered);
        }
    }

    private void UpdateUI()
    {
        // Instantiate the status text prefab and set it as a child of the pressure plate
        weightTemplate = Instantiate(weightTemplate, transform);
        weightTemplate.transform.localPosition = new Vector3(0, 0.625f, 0);

        weightUI = weightTemplate.GetComponent<WeightUI>(); 
        weightUI.weightText.text = weightType;
    }

    private void OnDrawGizmos()
    {
        // Draw wireframe box gizmo
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, boxSize);
    }
}

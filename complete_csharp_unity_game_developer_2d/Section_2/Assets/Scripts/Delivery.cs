using UnityEngine;

public class Delivery : MonoBehaviour
{

    [SerializeField] float packageDestroyDelay = 0.1f;
    [SerializeField] Color32 noPackageColor = new(255, 255, 255, 255);
    [SerializeField] Color32 hasPackageColor = new(255, 90, 255, 255);

    private bool hasPackage = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = noPackageColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Package") && !hasPackage)
        {
            hasPackage = true;
            spriteRenderer.color = hasPackageColor;
            Destroy(other.gameObject, packageDestroyDelay);
            Debug.Log("Package acquired.");
        }
        else if (other.CompareTag("Customer") && hasPackage)
        {
            hasPackage = false;
            spriteRenderer.color = noPackageColor;
            Destroy(other.gameObject, packageDestroyDelay);
            Debug.Log("Package delivered.");
        }
    }
}

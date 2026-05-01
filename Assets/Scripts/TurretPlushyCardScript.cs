using UnityEngine;

public class TurretPlushyCardScript : MonoBehaviour
{
    private Collider2D cardCollider;
    private Vector3 startDragPosition;
    public GameObject turretToSpawn;
    public int cost;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cardCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    private void OnMouseDown()
    {
        startDragPosition = transform.position;
        transform.position = GetMousePossitionInWorldSpace();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePossitionInWorldSpace();
    }

    private void OnMouseUp()
    {
        cardCollider.enabled = false;
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
        cardCollider.enabled = true;
        if (hitCollider != null && hitCollider.TryGetComponent(out TurretPossiblePositionScript turretPosition))
        {
            turretPosition.OnCardDrop(this);
        }
        transform.position = startDragPosition;
    }

    public Vector3 GetMousePossitionInWorldSpace()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;
        return worldPosition;
    }
}

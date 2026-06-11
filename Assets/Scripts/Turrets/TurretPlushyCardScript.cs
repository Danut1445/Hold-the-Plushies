using UnityEngine;

public class TurretPlushyCardScript : MonoBehaviour
{
    public GameObject turretToSpawn;
    public int cost;
    public int turretID;
    public TMPro.TMP_Text costText;

    private Collider2D cardCollider;
    private Vector3 startDragPosition;
    private GameObject UICard;
    private GameObject notUnlockedScreen;
    private bool unlocked;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cardCollider = gameObject.GetComponent<BoxCollider2D>();
        UICard = gameObject.transform.GetChild(0).gameObject;
        notUnlockedScreen = UICard.transform.GetChild(0).gameObject;
        costText.SetText(cost.ToString());
        if (turretID < PlayerStats.GetNumberWeapons())
        {
            notUnlockedScreen.SetActive(false);
            unlocked = true;
        } else
        {
            unlocked = false;
        }
    }

    private void OnMouseDown()
    {
        if (!unlocked)
        {
            return;
        }
        UICard.SetActive(false);
        startDragPosition = transform.position;
        transform.position = GetMousePossitionInWorldSpace();
    }

    private void OnMouseDrag()
    {
        if (!unlocked)
        {
            return;
        }
        transform.position = GetMousePossitionInWorldSpace();
    }

    private void OnMouseUp()
    {
        if (!unlocked)
        {
            return;
        }
        UICard.SetActive(true);
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

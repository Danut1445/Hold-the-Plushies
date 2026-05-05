using UnityEngine;
using UnityEngine.UI;

public class TurretLogicScript : MonoBehaviour
{
    public int upgradeCost;
    public int refund;
    public TMPro.TMP_Text upgradeCostText;
    public TMPro.TMP_Text refundText;
    private int level;
    private int maxLevel;
    private GameObject turretObject;
    private GameObject UITurret;
    private GameObject turretPossiblePosition;
    private GameObject logic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turretObject = gameObject.transform.GetChild(0).gameObject;
        UITurret = gameObject.transform.GetChild(1).gameObject;
        level = 1;
        maxLevel = 3; // Change to depend on the player logic

        UITurret.SetActive(false);
    }

    public void setCosts(int cost, GameObject position, GameObject logic)
    {
        upgradeCost = Mathf.RoundToInt(0.7f * cost);
        refund = Mathf.RoundToInt(0.5f * cost);
        upgradeCostText.SetText(upgradeCost.ToString());
        refundText.SetText(refund.ToString());
        turretPossiblePosition = position;
        this.logic = logic;
    }

    private void OnMouseDown()
    {
        bool showUI = UITurret.active;
        showUI = showUI ^ true;
        UITurret.SetActive(showUI);
    }

    public void upgrade()
    {
        if (logic.GetComponent<LevelLogicScript>().spendMoney(upgradeCost))
        {
            upgradeCost *= 2;
            refund *= 2;
            turretObject.GetComponent<TurretBasicScript>().bulletDamage += 15;
            turretObject.GetComponent<TurretBasicScript>().reloadTime -= 0.3f;
            level += 1;
            upgradeCostText.SetText(upgradeCost.ToString());
            refundText.SetText(refund.ToString());
            if (level == maxLevel)
            {
                UITurret.transform.GetChild(0).GetComponent<Button>().interactable = false;
                upgradeCostText.SetText("Maxed");
            }
        }
    }

    public void remove()
    {
        logic.GetComponent<LevelLogicScript>().gainMoney(refund);
        turretPossiblePosition.SetActive(true);
        Destroy(gameObject);
    }
}

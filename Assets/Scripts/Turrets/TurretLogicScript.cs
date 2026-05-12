using UnityEngine;
using UnityEngine.UI;

public class TurretLogicScript : MonoBehaviour
{
    public int upgradeAmmount1;
    public float upgradeAmmount2;
    public TMPro.TMP_Text upgradeCostText;
    public TMPro.TMP_Text refundText;

    private int level = 1;
    private int maxLevel;
    private int upgradeCost;
    private int refund;
    private GameObject turretObject;
    private GameObject UITurret;
    private GameObject turretPossiblePosition;
    private GameObject logic;
    private GameObject rangeDisplay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turretObject = gameObject.transform.GetChild(0).gameObject;
        UITurret = gameObject.transform.GetChild(1).gameObject;
        maxLevel = 3; // Change to depend on the player logic
        rangeDisplay = UITurret.transform.GetChild(4).gameObject;
        SetRange(turretObject.GetComponent<UpgradableTurretScript>().GetRange());

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
        bool showUI = UITurret.activeSelf;
        showUI = showUI ^ true;
        UITurret.SetActive(showUI);
    }

    public void upgrade()
    {
        if (logic.GetComponent<LevelLogicScript>().spendMoney(upgradeCost))
        {
            upgradeCost *= 2;
            refund *= 2;
            level += 1;
            turretObject.GetComponent<UpgradableTurretScript>().UpgradeTurret(upgradeAmmount1, upgradeAmmount2);
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
        turretObject.GetComponent<UpgradableTurretScript>().RemoveTurret();
        Destroy(gameObject);
    }

    public int GetLevel()
    {
        return level;
    }

    public GameObject GetTurretObject()
    {
        return turretObject;
    }

    public void SetRange(float range)
    {
        rangeDisplay.transform.localScale = new Vector3(2 * range, 2 * range, 1);
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Vector3 BuildPosition { get { return this.transform.position + offset; } }
    public Color hoverColor;
    public Vector3 offset;
    public Color notEnoughMoneyColor;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color defaultColor;
    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        defaultColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild) return;

        BuildTurret(buildManager.GetTurretToBuild());
        // buildManager.BuildTurretOn(this);
    }

    void BuildTurret(TurretBlueprint turretToBuild)
    {
        if (PlayerStats.Money < turretToBuild.cost) return;

        PlayerStats.Money -= turretToBuild.cost;

        GameObject turret = Instantiate(turretToBuild.prefab, BuildPosition, Quaternion.identity);
        this.turret = turret;
        turretBlueprint = turretToBuild;

        GameObject effect = Instantiate(buildManager.buildEffect, BuildPosition, Quaternion.identity);
        Destroy(effect, 5f);
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost) return;

        PlayerStats.Money -= turretBlueprint.upgradeCost;

        // Get rid of the old turret.
        Destroy(turret);

        // Build the upgraded one.
        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, BuildPosition, Quaternion.identity);
        turret = _turret;

        GameObject effect = Instantiate(buildManager.buildEffect, BuildPosition, Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;
    }

    public void SellTurret()
    {

        PlayerStats.Money += turretBlueprint.SellAmount;

        GameObject effect = Instantiate(buildManager.sellEffect, BuildPosition, Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(turret);
        turretBlueprint = null;
        isUpgraded = false;
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
 
        if (!buildManager.CanBuild) return;
        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    void OnMouseExit()
    {
        rend.material.color = defaultColor;
    }

}

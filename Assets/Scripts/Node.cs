using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Vector3 BuildPosition { get { return this.transform.position + offset; } }
    public Color hoverColor;
    public Vector3 offset;
    public Color notEnoughMoneyColor;

    [Header("Optional")]
    public GameObject turret;

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

        if (!buildManager.CanBuild) return;

        if (turret != null) return;

        buildManager.BuildTurretOn(this);
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

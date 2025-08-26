using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] List<float> _camY;
    [SerializeField] Camera _cam;
    [SerializeField] CoinsManager _cm;
    [SerializeField] PricesData _prices;
    public int currentModeCam;
    public static CameraManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }
    private void Start()
    {
        _cam = FindObjectOfType<Camera>();
    }
    public void UpgradeCam()
    {
        if (currentModeCam < _camY.Count-1 && _cm.coins >= _prices.upgradeVisionCost)
        {
            _cm.coins -= _prices.upgradeVisionCost;
            currentModeCam++;
            _cam.transform.localPosition = new Vector3(_cam.transform.position.x, _camY[currentModeCam], _cam.transform.position.z);
        }
        GameManager.Instance.HideAllPanels();
    }
}

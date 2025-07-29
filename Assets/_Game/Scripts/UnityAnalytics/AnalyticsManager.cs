using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance;
    private bool _isInitialized = false;

    public readonly string jsEventName = "jumpScareQuantity";
    public readonly string mcEventName = "monsterCatchQuantity";

    private void Awake()
    {
        if(Instance != null && Instance != this)
            Destroy(Instance );
        else
            Instance = this;
    }

    private async void Start()
    {
        await UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
        _isInitialized = true;
    }
    public void TriggerAnalyticsData(string eventName)
    {
        if (!_isInitialized) 
        { 
            Debug.LogError($"AnalyticsService is not initialized");
            return;
        }

        AnalyticsService.Instance.RecordEvent(eventName);
        AnalyticsService.Instance.Flush();

        Debug.Log($"{eventName} triggered");
    }
}

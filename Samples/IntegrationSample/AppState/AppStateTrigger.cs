namespace Samples.IntegrationSample.AppState
{
    public enum AppStateType
    {
        Initialize,
        LoadingData,
        WaitForRetry,
        AppReady,
    }
    
    public enum AppStateTrigger
    {
        AppInitializeComplete,
        LoadingDataComplete,
        LoadingDataFailed,
        RetryComplete
    }
}
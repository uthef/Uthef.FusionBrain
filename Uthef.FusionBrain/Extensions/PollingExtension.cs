using Uthef.FusionBrain.Types.ResponseModels;

namespace Uthef.FusionBrain.Extensions
{
    public static class PollingExtension
    {
        public readonly static TimeSpan DefaultInterval = TimeSpan.FromSeconds(1);

        public static async Task<GenerationStatus> PollAsync(this FusionBrainApi api, Guid uuid, 
            TimeSpan? customInterval = null, Action<GenerationStatus>? callback = null)
        {
            GenerationStatus status;
            bool inProcess;

            do
            {
                status = await api.CheckStatusAsync(uuid);
                await Task.Delay(customInterval ?? DefaultInterval);

                inProcess = !status.Completed && !status.Failed;

                if (inProcess) callback?.Invoke(status);
            }
            while (inProcess);

            return status;
        }
    }
}

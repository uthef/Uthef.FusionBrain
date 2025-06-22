using Uthef.FusionBrain.Serialization;

namespace Uthef.FusionBrain.Types.ResponseModels;

public class ServiceAvailability : Serializable<ServiceAvailability>
{
    public string Status { get; }

    public ServiceAvailability(string status)
    {
        Status = status;
    }
    
}
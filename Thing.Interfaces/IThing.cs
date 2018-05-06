using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using DevicesIoT;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Remoting.FabricTransport;
using Microsoft.ServiceFabric.Services.Remoting;

[assembly: FabricTransportActorRemotingProvider(RemotingListener = RemotingListener.V2Listener, RemotingClient = RemotingClient.V2Client)]
namespace Thing.Interfaces
{
    public interface IThing : IActor
    {
        Task ActivateMe(string region, int version);
        Task SendTelemetryAsync(ThingTelemetry telemetry);
    }

    [DataContract]
    class ThingState
    {
        [DataMember]
        public List<ThingTelemetry> _telemetry;
        [DataMember]
        public ThingInfo _deviceInfo;
        [DataMember]
        long _deviceGroupId;
    }

}

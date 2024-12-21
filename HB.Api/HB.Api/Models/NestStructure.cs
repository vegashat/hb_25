using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HB.Api.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Device
    {
        public string name { get; set; }
        public string type { get; set; }
        public string assignee { get; set; }
        public Traits traits { get; set; }
        public List<ParentRelation> parentRelations { get; set; }
    }

    public class MaxImageResolution
    {
        public int width { get; set; }
        public int height { get; set; }
    }

    public class MaxVideoResolution
    {
        public int width { get; set; }
        public int height { get; set; }
    }

    public class ParentRelation
    {
        public string parent { get; set; }
        public string displayName { get; set; }
    }

    public class NestStructure
    {
        public List<Device> devices { get; set; }
    }

    public class SdmDevicesTraitsCameraEventImage
    {
    }

    public class SdmDevicesTraitsCameraImage
    {
        public MaxImageResolution maxImageResolution { get; set; }
    }

    public class SdmDevicesTraitsCameraLiveStream
    {
        public MaxVideoResolution maxVideoResolution { get; set; }
        public List<string> videoCodecs { get; set; }
        public List<string> audioCodecs { get; set; }
        public List<string> supportedProtocols { get; set; }
    }

    public class SdmDevicesTraitsCameraMotion
    {
    }

    public class SdmDevicesTraitsCameraPerson
    {
    }

    public class SdmDevicesTraitsCameraSound
    {
    }

    public class SdmDevicesTraitsConnectivity
    {
        public string status { get; set; }
    }

    public class SdmDevicesTraitsFan
    {
        public string timerMode { get; set; }
    }

    public class SdmDevicesTraitsHumidity
    {
        public int ambientHumidityPercent { get; set; }
    }

    public class SdmDevicesTraitsInfo
    {
        public string customName { get; set; }
    }

    public class SdmDevicesTraitsSettings
    {
        public string temperatureScale { get; set; }
    }

    public class SdmDevicesTraitsTemperature
    {
        public double ambientTemperatureCelsius { get; set; }
    }

    public class SdmDevicesTraitsThermostatEco
    {
        public List<string> availableModes { get; set; }
        public string mode { get; set; }
        public double heatCelsius { get; set; }
        public double coolCelsius { get; set; }
    }

    public class SdmDevicesTraitsThermostatHvac
    {
        public string status { get; set; }
    }

    public class SdmDevicesTraitsThermostatMode
    {
        public string mode { get; set; }
        public List<string> availableModes { get; set; }
    }

    public class SdmDevicesTraitsThermostatTemperatureSetpoint
    {
        public double? coolCelsius { get; set; }
        public double? heatCelsius { get; set; }
    }

    public class Traits
    {
        [JsonProperty("sdm.devices.traits.Info")]
        public SdmDevicesTraitsInfo sdmdevicestraitsInfo { get; set; }

        [JsonProperty("sdm.devices.traits.Humidity")]
        public SdmDevicesTraitsHumidity sdmdevicestraitsHumidity { get; set; }

        [JsonProperty("sdm.devices.traits.Connectivity")]
        public SdmDevicesTraitsConnectivity sdmdevicestraitsConnectivity { get; set; }

        [JsonProperty("sdm.devices.traits.Fan")]
        public SdmDevicesTraitsFan sdmdevicestraitsFan { get; set; }

        [JsonProperty("sdm.devices.traits.ThermostatMode")]
        public SdmDevicesTraitsThermostatMode sdmdevicestraitsThermostatMode { get; set; }

        [JsonProperty("sdm.devices.traits.ThermostatEco")]
        public SdmDevicesTraitsThermostatEco sdmdevicestraitsThermostatEco { get; set; }

        [JsonProperty("sdm.devices.traits.ThermostatHvac")]
        public SdmDevicesTraitsThermostatHvac sdmdevicestraitsThermostatHvac { get; set; }

        [JsonProperty("sdm.devices.traits.Settings")]
        public SdmDevicesTraitsSettings sdmdevicestraitsSettings { get; set; }

        [JsonProperty("sdm.devices.traits.ThermostatTemperatureSetpoint")]
        public SdmDevicesTraitsThermostatTemperatureSetpoint sdmdevicestraitsThermostatTemperatureSetpoint { get; set; }

        [JsonProperty("sdm.devices.traits.Temperature")]
        public SdmDevicesTraitsTemperature sdmdevicestraitsTemperature { get; set; }

        [JsonProperty("sdm.devices.traits.CameraLiveStream")]
        public SdmDevicesTraitsCameraLiveStream sdmdevicestraitsCameraLiveStream { get; set; }

        [JsonProperty("sdm.devices.traits.CameraImage")]
        public SdmDevicesTraitsCameraImage sdmdevicestraitsCameraImage { get; set; }

        [JsonProperty("sdm.devices.traits.CameraPerson")]
        public SdmDevicesTraitsCameraPerson sdmdevicestraitsCameraPerson { get; set; }

        [JsonProperty("sdm.devices.traits.CameraSound")]
        public SdmDevicesTraitsCameraSound sdmdevicestraitsCameraSound { get; set; }

        [JsonProperty("sdm.devices.traits.CameraMotion")]
        public SdmDevicesTraitsCameraMotion sdmdevicestraitsCameraMotion { get; set; }

        [JsonProperty("sdm.devices.traits.CameraEventImage")]
        public SdmDevicesTraitsCameraEventImage sdmdevicestraitsCameraEventImage { get; set; }
    }


}
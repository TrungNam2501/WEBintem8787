using System.Net.NetworkInformation;

namespace IntemBB.Web.Services;

public class NetworkService
{
    private static readonly Dictionary<string, string> MachineIPs = new()
    {
        { "rdMay1", "198.1.8.16" },
        { "rdMay2", "198.1.8.15" },
        { "rdMay02", "198.1.8.17" },
        { "rdMay04", "198.1.8.18" }
    };

    private static readonly Dictionary<string, string> MachineNames = new()
    {
        { "rdMay1", "Máy -1" },
        { "rdMay2", "Máy -9" },
        { "rdMay02", "Máy -1 mới" },
        { "rdMay04", "Máy -9 mới" }
    };

    public (bool success, string message) CheckMachineConnection(string machineKey)
    {
        if (!MachineIPs.TryGetValue(machineKey, out var ip))
            return (false, "Máy không hợp lệ");

        var machineName = MachineNames.GetValueOrDefault(machineKey, machineKey);

        try
        {
            using var ping = new Ping();
            var reply = ping.Send(ip, 3000);
            if (reply.Status == IPStatus.Success)
                return (true, $"Kết nối thành công {machineName}");
            return (false, $"Máy tính {machineName} đang tắt");
        }
        catch (Exception ex)
        {
            return (false, $"Lỗi kết nối {machineName}: {ex.Message}");
        }
    }

    public bool PingMachine(string machineCode)
    {
        var ipMap = new Dictionary<string, string>
        {
            { "01", "198.1.8.21" }, { "02", "198.1.8.22" },
            { "03", "198.1.8.23" }, { "04", "198.1.8.24" },
            { "05", "198.1.8.35" }, { "06", "198.1.8.36" },
            { "07", "198.1.8.37" }, { "08", "198.1.8.38" }
        };

        if (!ipMap.TryGetValue(machineCode, out var ip))
            return false;

        try
        {
            using var ping = new Ping();
            var reply = ping.Send(ip, 3000);
            return reply.Status == IPStatus.Success;
        }
        catch
        {
            return false;
        }
    }
}

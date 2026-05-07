using System.Collections.Concurrent;
using System.Net.NetworkInformation;

namespace IntemBB.Web.Services;

public class NetworkService
{
    // Timeout ngắn hơn để tránh chặn request lâu khi máy đích offline / mạng LAN yếu.
    // 800ms đủ với LAN nội bộ (RTT thường < 50ms); nếu quá thì coi như mất kết nối.
    private const int PingTimeoutMs = 800;

    // Cache kết quả ping trong vài giây để các postback liên tiếp không phải ping lại.
    private static readonly TimeSpan CacheTtl = TimeSpan.FromSeconds(3);

    private readonly ConcurrentDictionary<string, (DateTime Expires, bool Success)> _cache = new();

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

    private static readonly Dictionary<string, string> MaybbIps = new()
    {
        { "01", "198.1.8.21" }, { "02", "198.1.8.22" },
        { "03", "198.1.8.23" }, { "04", "198.1.8.24" },
        { "05", "198.1.8.35" }, { "06", "198.1.8.36" },
        { "07", "198.1.8.37" }, { "08", "198.1.8.38" }
    };

    public async Task<(bool success, string message)> CheckMachineConnectionAsync(string machineKey)
    {
        if (!MachineIPs.TryGetValue(machineKey, out var ip))
            return (false, "Máy không hợp lệ");

        var machineName = MachineNames.GetValueOrDefault(machineKey, machineKey);
        var ok = await PingAsync(ip);
        return ok
            ? (true, $"Kết nối thành công {machineName}")
            : (false, $"Máy tính {machineName} đang tắt");
    }

    // Giữ lại API đồng bộ cho các call-site cũ. Bên trong vẫn dùng cache + PingAsync
    // nên không bị chặn 3s như implementation cũ.
    public (bool success, string message) CheckMachineConnection(string machineKey)
        => CheckMachineConnectionAsync(machineKey).GetAwaiter().GetResult();

    public Task<bool> PingMachineAsync(string machineCode)
    {
        if (!MaybbIps.TryGetValue(machineCode, out var ip))
            return Task.FromResult(false);
        return PingAsync(ip);
    }

    public bool PingMachine(string machineCode)
        => PingMachineAsync(machineCode).GetAwaiter().GetResult();

    private async Task<bool> PingAsync(string ip)
    {
        if (_cache.TryGetValue(ip, out var entry) && entry.Expires > DateTime.UtcNow)
            return entry.Success;

        bool ok = false;
        try
        {
            using var ping = new Ping();
            var reply = await ping.SendPingAsync(ip, PingTimeoutMs);
            ok = reply.Status == IPStatus.Success;
        }
        catch
        {
            ok = false;
        }

        _cache[ip] = (DateTime.UtcNow.Add(CacheTtl), ok);
        return ok;
    }
}

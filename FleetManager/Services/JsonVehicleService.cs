using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FleetManager.Models;

namespace FleetManager.Services;

public class JsonVehicleService : IVehicleService
{
    private readonly string _filePath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory, "Assets", "vehicles.json");

    public async Task<List<Vehicle>> LoadVehiclesAsync()
    {
        try
        {
            if (!File.Exists(_filePath))
                return new List<Vehicle>();

            string json = await File.ReadAllTextAsync(_filePath);

            var options = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() }
            };

            return JsonSerializer.Deserialize<List<Vehicle>>(json, options) ?? new List<Vehicle>();
        }
        catch (Exception)
        {
            return new List<Vehicle>();
        }
    }

    public async Task SaveVehiclesAsync(List<Vehicle> vehicles)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };

        string json = JsonSerializer.Serialize(vehicles, options);
        await File.WriteAllTextAsync(_filePath, json);
    }
}
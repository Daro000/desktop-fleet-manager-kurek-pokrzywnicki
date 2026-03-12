using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using FleetManager.Models;

namespace FleetManager.Services;

public class JsonVehicleService : IVehicleService
{
    private readonly string _filePath = "Assets/vehicles.json";

    public async Task<List<Vehicle>> LoadVehiclesAsync()
    {
        try
        {
            if (!File.Exists(_filePath))
                return new List<Vehicle>();

            string json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<Vehicle>>(json) ?? new List<Vehicle>();
        }
        catch (Exception)
        {
            
            return new List<Vehicle>();
        }
    }

    public async Task SaveVehiclesAsync(List<Vehicle> vehicles)
    {
        string json = JsonSerializer.Serialize(vehicles, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        await File.WriteAllTextAsync(_filePath, json);
    }
}
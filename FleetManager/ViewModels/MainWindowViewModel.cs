using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using FleetManager.Models;
using FleetManager.Services;
using ReactiveUI;

namespace FleetManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IVehicleService _vehicleService = new JsonVehicleService();
    
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    private const string FilePath = "Assets/vehicles.json";
    public ObservableCollection<VehicleItemViewModel> Vehicles { get; } = new();

    private VehicleItemViewModel? _selectedVehicle;
    public VehicleItemViewModel? SelectedVehicle
    {
        get => _selectedVehicle;
        set => this.RaiseAndSetIfChanged(ref _selectedVehicle, value);
    }

    public MainWindowViewModel()
    {
        LoadVehiclesAsync();
    }

    private async void LoadVehiclesAsync()
    {
        List<Vehicle> vehicles = await _vehicleService.LoadVehiclesAsync();

        foreach (Vehicle vehicle in vehicles)
        {
            Vehicles.Add(new VehicleItemViewModel(vehicle));
        }
    }
    
    
    private void SaveVehiclesToJson()
    {
        try
        {
            File.WriteAllText(FilePath, JsonSerializer
                .Serialize(Vehicles, JsonOptions ));
            Console.WriteLine("Json saved!");
        }catch (Exception exception) when (exception is
                                               IOException or
                                               UnauthorizedAccessException or
                                               JsonException)
        

        {
            Console.WriteLine($"Save File Error {exception.Message}");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Unexpected error (report this!): {exception.Message}");
        }
    } 
}
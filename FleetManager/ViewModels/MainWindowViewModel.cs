using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
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
    
    
    public ReactiveCommand<Unit, Unit> SaveVehicleCommand { get; }

    public MainWindowViewModel()
    {
        LoadVehiclesAsync();
        
        SaveVehicleCommand = ReactiveCommand.Create(SaveVehiclesToJson);
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
            var dataToSave = Vehicles.Select(vm => vm.Model).ToList();
            
            
            File.WriteAllText(FilePath, JsonSerializer
                .Serialize(dataToSave, JsonOptions ));
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
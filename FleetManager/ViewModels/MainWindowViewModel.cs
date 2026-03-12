using System.Collections.Generic;
using System.Collections.ObjectModel;
using FleetManager.Models;
using FleetManager.Services;
using ReactiveUI;

namespace FleetManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IVehicleService _vehicleService = new JsonVehicleService();

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
}
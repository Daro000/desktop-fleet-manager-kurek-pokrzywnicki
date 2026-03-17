using System.Data;
using System.Reactive;
using Avalonia.Media;
using FleetManager.Models;
using ReactiveUI;

namespace FleetManager.ViewModels;
 
public class VehicleItemViewModel : ViewModelBase
{
    private readonly Vehicle _vehicle;
    
    public Vehicle Model => _vehicle;
    
    public ReactiveCommand<Unit, Unit> VehicleInRoutCommand { get; }
    public ReactiveCommand<Unit, Unit> VehicleInServiceCommand { get; }
    public ReactiveCommand<Unit, Unit> VehicleInAvailableCommand { get; }

    public VehicleItemViewModel(Vehicle vehicle)
    {
        _vehicle = vehicle;
        VehicleInRoutCommand = ReactiveCommand.Create(() => { UpdateVechicleStatus(VehicleStatus.InRoute); });
        VehicleInServiceCommand = ReactiveCommand.Create(() => { UpdateVechicleStatus(VehicleStatus.Service); });
        VehicleInAvailableCommand = ReactiveCommand.Create(() => { UpdateVechicleStatus(VehicleStatus.Available); });


    }

    public string Id     => _vehicle.Id;
    public string Name   => _vehicle.Name;
    public string Status => _vehicle.Status.ToString();
    public double Fuel   => _vehicle.FuelPercentage;
 
    public string FuelDisplay => $"{_vehicle.FuelPercentage:F0}%";
    
    public IBrush FuelColor => _vehicle.FuelPercentage switch
    {
        >= 75 => Brushes.Green,
        >= 15 => Brushes.Gold,
        _     => Brushes.Red
    };
    
    public void UpdateVechicleStatus(VehicleStatus newStatus)
    {
        _vehicle.Status = newStatus;
        this.RaisePropertyChanged(nameof(Status));
    }
}

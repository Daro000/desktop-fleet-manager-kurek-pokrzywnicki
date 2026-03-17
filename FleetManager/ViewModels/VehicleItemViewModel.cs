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

    public ReactiveCommand<Unit, Unit> RefuelVehicleCommand { get; }
    private bool _isFuelButtonEnabled = true;
    public bool IsFuelButtonEnabled
    {
        get => _isFuelButtonEnabled;
        set => this.RaiseAndSetIfChanged(ref _isFuelButtonEnabled, value);
    }

    public VehicleItemViewModel(Vehicle vehicle)
    {
        _vehicle = vehicle;
        VehicleInRoutCommand = ReactiveCommand.Create(() => { UpdateVechicleStatus(VehicleStatus.InRoute); });
        VehicleInServiceCommand = ReactiveCommand.Create(() => { UpdateVechicleStatus(VehicleStatus.Service); });
        VehicleInAvailableCommand = ReactiveCommand.Create(() => { UpdateVechicleStatus(VehicleStatus.Available); });


        RefuelVehicleCommand = ReactiveCommand.Create(() => RefuelVehicle());
        
        IsFuelButtonEnabled = (vehicle.Status != VehicleStatus.InRoute);

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
        
        IsFuelButtonEnabled = (newStatus != VehicleStatus.InRoute);
        
        _vehicle.Status = newStatus;
        
        this.RaisePropertyChanged(nameof(Status));
        this.RaisePropertyChanged(nameof(IsFuelButtonEnabled));
        
    }

    public void RefuelVehicle()
    {
        _vehicle.FuelPercentage = 100;
        
        this.RaisePropertyChanged(nameof(Fuel));
        this.RaisePropertyChanged(nameof(FuelDisplay));
        this.RaisePropertyChanged(nameof(FuelColor));
    }
}

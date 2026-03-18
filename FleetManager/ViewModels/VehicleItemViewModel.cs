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
    
    private bool _isRouteButtonEnabled = true;
    public bool IsRouteButtonEnabled
    {
        get => _isRouteButtonEnabled;
        set => this.RaiseAndSetIfChanged(ref _isRouteButtonEnabled, value);
    }
    
    private bool _isServiceButtonEnabled  = true;

    public bool IsServiceButtonEnabled
    {
        get => _isServiceButtonEnabled;
        set => this.RaiseAndSetIfChanged(ref _isServiceButtonEnabled, value);
    }
    
    
    private bool _isAvaiableButtonEnabled  = true;

    public bool IsAvaiableButtonEnabled
    {
        get => _isAvaiableButtonEnabled;
        set => this.RaiseAndSetIfChanged(ref _isAvaiableButtonEnabled, value);
    }

    public VehicleItemViewModel(Vehicle vehicle)
    {
        _vehicle = vehicle;
        VehicleInRoutCommand = ReactiveCommand.Create(() => { UpdateVechicleStatus(VehicleStatus.InRoute); });
        VehicleInServiceCommand = ReactiveCommand.Create(() => { UpdateVechicleStatus(VehicleStatus.Service); });
        VehicleInAvailableCommand = ReactiveCommand.Create(() => { UpdateVechicleStatus(VehicleStatus.Available); });


        RefuelVehicleCommand = ReactiveCommand.Create(() => RefuelVehicle());
        
        IsFuelButtonEnabled = (vehicle.Status != VehicleStatus.InRoute);
        IsRouteButtonEnabled = (vehicle.Status != VehicleStatus.InRoute);
        IsAvaiableButtonEnabled = (vehicle.Status != VehicleStatus.Available);
        IsServiceButtonEnabled = (vehicle.Status != VehicleStatus.Service);
        IsRouteButtonEnabled = (vehicle.FuelPercentage > 15);

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
        if  (newStatus == VehicleStatus.InRoute)
        {
            _vehicle.FuelPercentage -= 15; 
            IsRouteButtonEnabled = false;
            IsAvaiableButtonEnabled = true;
            IsServiceButtonEnabled = true;
            
        }
        else if (newStatus == VehicleStatus.Available)
        {
            IsRouteButtonEnabled = (_vehicle.FuelPercentage > 15);
            IsAvaiableButtonEnabled = false;
            IsServiceButtonEnabled = true;
        }
        else if (newStatus == VehicleStatus.Service)
        {
            IsRouteButtonEnabled = (_vehicle.FuelPercentage > 15);
            IsAvaiableButtonEnabled = true;
            IsServiceButtonEnabled = false;
        }
        
        
        _vehicle.Status = newStatus;
        
        this.RaisePropertyChanged(nameof(Status));
        this.RaisePropertyChanged(nameof(IsFuelButtonEnabled));
        this.RaisePropertyChanged(nameof(IsRouteButtonEnabled));
        this.RaisePropertyChanged(nameof(FuelDisplay));
        this.RaisePropertyChanged(nameof(FuelColor));
        this.RaisePropertyChanged(nameof(Fuel));
    }

    public void RefuelVehicle()
    {
        _vehicle.FuelPercentage = 100;
        IsRouteButtonEnabled = (_vehicle.FuelPercentage >15);
        
        this.RaisePropertyChanged(nameof(Fuel));
        this.RaisePropertyChanged(nameof(FuelDisplay));
        this.RaisePropertyChanged(nameof(FuelColor));
    }
}

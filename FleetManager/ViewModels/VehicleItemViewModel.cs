using Avalonia.Media;
using FleetManager.Models;
 
namespace FleetManager.ViewModels;
 
public class VehicleItemViewModel : ViewModelBase
{
    private readonly Vehicle _vehicle;
 
    public VehicleItemViewModel(Vehicle vehicle)
    {
        _vehicle = vehicle;
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
}
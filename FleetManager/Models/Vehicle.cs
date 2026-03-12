using ReactiveUI;

namespace FleetManager.Models;

public enum VehicleStatus
{
    Available,
    InRoute,
    Service
}

public class Vehicle : ReactiveObject
{
    private string _id = string.Empty;
    private string _name = string.Empty;
    private string _type = string.Empty;
    private double _fuelPercentage;
    private VehicleStatus _status;

    public string Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public string Type
    {
        get => _type;
        set => this.RaiseAndSetIfChanged(ref _type, value);
    }

    public double FuelPercentage
    {
        get => _fuelPercentage;
        set => this.RaiseAndSetIfChanged(ref _fuelPercentage, value);
    }

    public VehicleStatus Status
    {
        get => _status;
        set => this.RaiseAndSetIfChanged(ref _status, value);
    }
}
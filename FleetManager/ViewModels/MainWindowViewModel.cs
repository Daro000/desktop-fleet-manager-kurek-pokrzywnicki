using System.Collections.ObjectModel;
using FleetManager.Models;
using ReactiveUI;

namespace FleetManager.ViewModels;
 
public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<VehicleItemViewModel> Vehicles { get; } = new();
 
    private VehicleItemViewModel? _selectedVehicle;
    public VehicleItemViewModel? SelectedVehicle
    {
        get => _selectedVehicle;
        set => this.RaiseAndSetIfChanged(ref _selectedVehicle, value);
    }
 
    public MainWindowViewModel()
    {
        // Tymczasowe dane testowe 
        
        Vehicles.Add(new VehicleItemViewModel(new Vehicle { Id = "RV-001", Name = "Scania R500",     FuelPercentage = 92, Status = VehicleStatus.Available }));
        Vehicles.Add(new VehicleItemViewModel(new Vehicle { Id = "RV-002", Name = "MAN TGX 18.440", FuelPercentage = 45, Status = VehicleStatus.InRoute   }));
        Vehicles.Add(new VehicleItemViewModel(new Vehicle { Id = "RV-003", Name = "Volvo FH16",      FuelPercentage = 10, Status = VehicleStatus.Service   }));
        Vehicles.Add(new VehicleItemViewModel(new Vehicle { Id = "RV-004", Name = "Mercedes Actros", FuelPercentage = 68, Status = VehicleStatus.Available }));
    }
}
namespace Podstawy_widoków.ViewModels;

public class VehicleItemViewModel
{
    public IEnumerable<VehicleRecordViewModel> VehicleRecords { get; set; }

    public VehicleItemViewModel(IEnumerable<VehicleRecordViewModel> vehicleRecords)
    {
        VehicleRecords = vehicleRecords;
    }
}
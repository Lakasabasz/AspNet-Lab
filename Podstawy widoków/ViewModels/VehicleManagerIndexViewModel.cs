namespace Podstawy_widoków.ViewModels;

public class VehicleManagerIndexViewModel
{
    public (bool, string)? Status { get; set; }
    public IEnumerable<VehicleRecordViewModel> VehicleRecords { get; set; }

    public VehicleManagerIndexViewModel((bool, string)? status, IEnumerable<VehicleRecordViewModel> vehicleRecords)
    {
        Status = status;
        VehicleRecords = vehicleRecords;
    }
}
namespace CarRentalService.Types
{
    public enum RegistrationResult
    {
        Success,
        DeliveryDetailsNotFound,
        InvalidMeterReader,
        InvalidDate,
        CouldNotRegisterDelivery,
        CouldNotRegisterReturn
    }
}
using Domain.Enums;

namespace Infrastructure.Extensions
{
    public static class ConverterExtensions
    {
        public static DoctorStatuses FromStringToDoctorStatusesEnum(this string enumValStr)
        {
            DoctorStatuses enumVal;
            var parseResult = Enum.TryParse<DoctorStatuses>(enumValStr, out enumVal);
            if (!parseResult)
                throw new InvalidOperationException($"doctor's status value is invalid");
            return enumVal;
        }
    }
}

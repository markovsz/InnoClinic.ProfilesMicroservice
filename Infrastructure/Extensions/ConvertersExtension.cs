using Domain.Enums;

namespace Infrastructure.Extensions
{
    public static class ConvertersExtension
    {
        /*        
        public static T FromStringToEnum<T>(this string enumValStr) where T : struct, IConvertible
        {
            T enumVal;
            var parseResult = Enum.TryParse<T>(enumValStr, out enumVal);
            if (!parseResult)
                throw new InvalidOperationException($"{nameof(DoctorStatuses)}'s value is invalid");
            return enumVal;
        }
        */

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

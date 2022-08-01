using System;

namespace PersonRegistrationApp.BussinessLogic
{
    public static class IsGuidCompatibleExtension
    {
        public static bool IsGuidCompatible(this string guid)
        {
            if(guid is not null)
              {
                var newGuid = Guid.TryParse(guid, out Guid result);
                if (newGuid)
                {
                    return true;
                }
                return false;
              }
            return false;
        }
    }
}

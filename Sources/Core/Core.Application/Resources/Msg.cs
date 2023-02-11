using System.Diagnostics.CodeAnalysis;

namespace Core.Application.Resources
{
    [ExcludeFromCodeCoverage]
    public static class Msg
    {
        // COD0001
        public static string INTERNAL_SERVER_ERROR_COD => "COD0001";
        public static string INTERNAL_SERVER_ERROR_TXT => "Internal server error.";

        // COD0002
        public static string X0_IS_REQUIRED_COD => "COD0002";
        public static string X0_IS_REQUIRED_TXT => "{0} is required.";

        // COD0003
        public static string DATA_BASE_SERVER_ERROR_COD => "COD0003";
        public static string DATA_BASE_SERVER_ERROR_TXT => "Database server error.";

        // COD0004
        public static string DATA_OF_X0_X1_NOT_FOUND_COD => "COD0004";
        public static string DATA_OF_X0_X1_NOT_FOUND_TXT => "Data of {0} {1} not found.";

        // COD0005
        public static string IDENTIFIER_X0_IS_INVALID_COD => "COD0005";
        public static string IDENTIFIER_X0_IS_INVALID_TXT => "Identifier {0} is invalid.";

        // COD0006
        public static string FAILED_TO_UPDATE_X0_COD => "COD0006";
        public static string FAILED_TO_UPDATE_X0_TXT => "Failed to update {0}.";

        // COD0007
        public static string RESPONSE_FAILED_MESSAGE_COD => "COD0007";
        public static string RESPONSE_FAILED_MESSAGE_TXT => "Failed to update {0}.";

        // COD0008
        public static string RESPONSE_SUCCEEDED_MESSAGE_COD => "COD0008";
        public static string RESPONSE_SUCCEEDED_MESSAGE_TXT => "Request processed.";

        // COD0009
        public static string OBJECT_X0_IS_NULL_COD => "COD0009";
        public static string OBJECT_X0_IS_NULL_TXT => "Object {0} is null.";

        // COD0010
        public static string FAILED_TO_REMOVE_X0_COD => "COD0010";
        public static string FAILED_TO_REMOVE_X0_TXT => "Failed to remove {0}.";
    }
}
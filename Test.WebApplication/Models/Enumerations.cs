using System.Runtime.Serialization;

namespace Test.WebApplication.Models
{
    public class Enumerations
    {
    }
    public enum RequestDataType
    {
        /// This Type is used for endpoint which accepts primitive data type, recommeded for GET, PUT, DELETE. Also can be used with POST and PUT
        [EnumMember(Value = "QueryString")]
        QueryString,
        /// This Type is used for endpoint which accepts complex data type, recommended for POST. Also Can be used with GET DELETE
        [EnumMember(Value = "NonQueryString")]
        NonQueryString,
    }
}

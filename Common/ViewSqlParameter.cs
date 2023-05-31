using System.Data;

namespace Common
{
    public class ViewSqlParameter
    {
        public string Name{ get; set; }
        public string Value { get; set; }

        public ViewSqlParameter(string name, string value)
        {
            Name = name;
            Value = value;
        }
        public ViewSqlParameter()
        {
        }
    }
}
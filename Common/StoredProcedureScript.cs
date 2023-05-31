namespace Common
{
    public class StoredProcedureScript
    {
        public List<ViewSqlParameter> SqlParameters { get; set; } = null!;
        public string Script { get; set; } = null!;
    }
    
    public class ExecutionScript
    {
        public string Script { get; set; }
        public string Result { get; set; }

        public bool NeedParse { get; set; } = true;
        public List<ViewSqlParameter> InputVariables { get; set; } = new();
        public List<ViewSqlParameter> OutputVariables { get; set; } = new();
    }
}

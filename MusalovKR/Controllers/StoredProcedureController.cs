using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace MusalovKR.Controllers
{
    public class StoredProcedureController : Controller
    {
        private readonly AppDbContext _context;

        public StoredProcedureController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(ExecutionScript model)
        {
            if (string.IsNullOrEmpty(model.Script)) return View(model);

            if (model.InputVariables.Count == 0 | model.OutputVariables.Count == 0)  // парсим названия переменных 
            {
                string[] parts = model.Script.Split(',');
                List<string> variables = new();

                foreach (string part in parts)
                {
                    variables.Add(part[part.IndexOf("@")..part.Length]);
                }

                foreach (var s in variables)
                {
                    if (s.ToLower().Contains("output"))
                        model.OutputVariables.Add(new(s.Split(' ')[0], null!));
                    else
                        model.InputVariables.Add(new(s, null!));
                }

                var newModel = new ExecutionScript
                {
                    Script = model.Script,
                    NeedParse = model.NeedParse,
                    InputVariables = model.InputVariables,
                    OutputVariables = model.OutputVariables
                };

                return View(newModel);
            }
            else
            {
                model.Result = Execution(model);
                return View(model);
            }
        }

        private string Execution(ExecutionScript query)
        {
            var parameters = new List<SqlParameter>();

            for (int i = 0; i < query.InputVariables.Count; i++)
            {
                parameters.Add(new()
                {
                    ParameterName = query.InputVariables[i].Name,
                    Value = query.InputVariables[i].Value,
                    Direction = query.InputVariables[i].Value is null ? ParameterDirection.Output : ParameterDirection.Input,
                    Size = int.MaxValue
                });
            }
            for (int i = 0; i < query.OutputVariables.Count; i++)
            {
                parameters.Add(new()
                {
                    ParameterName = query.OutputVariables[i].Name,
                    Value = query.OutputVariables[i].Value,
                    Direction = query.OutputVariables[i].Value is null ? ParameterDirection.Output : ParameterDirection.Input,
                    Size = int.MaxValue
                });
            }

            _context.Database.ExecuteSqlRaw(query.Script, parameters);
            return parameters.LastOrDefault().Value.ToString();
        }
    }
}

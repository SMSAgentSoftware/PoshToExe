using System;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Collections.ObjectModel;
using System.Management.Automation;

namespace PoshToExe
{
    class Program
    {
        static void Main(string[] args)
        {
            // Make sure your PowerShell script is non-interactive. 
            // Use Write-Output instead of Write-Host if you need to return output for a console app.
            // Don't use automatic variables that may not exist in the environment, like $PSScriptRoot (try (Get-Location).Path).

            // Set a default value for the 'Count' parameter if one wasn't passed on the cmdline
            string a;
            if (args.Length == 0)
            {
                a = "10";
            }
            else
            {
                a = args[0];                
            }

            string psScript = Properties.Resources.PoshScript;
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();
            PowerShell ps = PowerShell.Create();
            ps.Runspace = runspace;
            ps.AddScript("Set-ExecutionPolicy -Scope Process Unrestricted");
            ps.AddScript(psScript);
            ps.AddParameter("Count", a);
            Collection<PSObject> results = ps.Invoke();
            runspace.Close();

            StringBuilder stringBuilder = new StringBuilder();
            foreach (PSObject obj in results)
            {
                stringBuilder.AppendLine(obj.ToString());
            }
            Console.WriteLine(stringBuilder.ToString());
        }
    }
}
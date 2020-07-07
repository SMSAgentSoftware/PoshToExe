using System.Management.Automation.Runspaces;
using System.Threading;

namespace PoshToExe
{
    class Program
    {
        static void Main()
        {
            // Credit: https://github.com/gtworek/PSBits/blob/master/Misc/No-PowerShell.cs
            
            // Make sure your PowerShell script is non-interactive. 
            // Don't use automatic variables that may not exist in the environment, like $PSScriptRoot (try (Get-Location).Path).
            
            string psScript = Properties.Resources.PoshScript;            
            Runspace runspace = RunspaceFactory.CreateRunspace();
            // Single-threaded apartmentstate is required when using WPF elements
            runspace.ApartmentState = ApartmentState.STA;
            runspace.Open();
            Pipeline pipeline = runspace.CreatePipeline();
            pipeline.Commands.AddScript("Set-ExecutionPolicy -Scope Process Unrestricted");
            pipeline.Commands.AddScript(psScript);
            // Alternatively call an external script
            // pipeline.Commands.AddScript("c:\\temp\\myscript.ps1");
            pipeline.Invoke();
            runspace.Close();
        }
    }
}
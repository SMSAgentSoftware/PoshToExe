Param($Count)
$Results = Get-CimInstance -ClassName Win32_ReliabilityRecords -Filter "SourceName='Microsoft-Windows-WindowsUpdateClient'" | 
    Where {$_.Message -Match "Installation Successful:"} |
    Select -First $Count | 
    Select ProductName,TimeGenerated
Write-Output "Update  InstallDate"
Write-Output "------  -----------"
foreach ($item in $results)
{
    Write-Output "$($Item.ProductName), $($Item.TimeGenerated)"
}
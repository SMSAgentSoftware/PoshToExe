# Sign exe with certificate from local store
# Requires Windows 10 SDK or SignTool.exe somewhere on local machine
$Path = "<Path>"
$Exe = "MyScript.exe"
$SignToolPath = "C:\Program Files (x86)\Windows Kits\10\App Certification Kit"
$CertDnsName = "SMSAgent.blog"
$CertStore = "CurrentUser\My"
$Thumbprint = Get-ChildItem -Path Cert:\$CertStore -DnsName $CertDnsName | Select -ExpandProperty Thumbprint
Start-Process -FilePath "$SignToolPath\signtool.exe" -ArgumentList "sign /fd SHA256 /td SHA256 /tr http://timestamp.digicert.com /sha1 $Thumbprint ""$Path\$Exe"""
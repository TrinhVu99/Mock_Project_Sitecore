$src="D:\Project\SitecoreMIG\A.Source\mig93\src"
$destination="D:\\Temps\Uni"
Get-Item -Path ($src+"/Foundation/*/serialization") |%{
   Write-Host "Start copy" $_.FullName 
   Copy-Item -Path $_.FullName -Destination ($destination+"\Foundation\"+$_.Parent.Name+"\serialization") -Force -Recurse 
}
Get-Item -Path ($src+"/Feature/*/serialization") |%{
   Write-Host "Start copy" $_.FullName 
   Copy-Item -Path $_.FullName -Destination ($destination+"\Feature\"+$_.Parent.Name+"\serialization") -Force -Recurse
}
Get-Item -Path ($src+"/Project/*/serialization") |%{
   Write-Host "Start copy" $_.FullName 
   Copy-Item -Path $_.FullName -Destination ($destination+"\Project\"+$_.Parent.Name+"\serialization") -Force -Recurse
}
OpenCover.Console.exe -register:user -target:dotnet.exe -targetargs:"test src\ConfigCat.Client.Tests\ConfigCat.Client.Tests.csproj -c Release" -output:.\coverage.xml -filter:"+[*]ConfigCat.Client.* -[ConfigCatClientTests]*" -oldstyle -returntargetcode

pause

rem https://www.appveyor.com/blog/2017/03/17/codecov/

%USERPROFILE%\.nuget\packages\opencover\4.6.519\tools\OpenCover.Console.exe -target:dotnet.exe -targetargs:"test tests\XPath2.Extensions.Tests\XPath2.Extensions.Tests.csproj --no-build" -filter:"+[XPath2]* -[*Tests*]*" -output:coverage.xml -register:user -oldStyle -searchdirs:".\tests\XPath2.Extensions.Tests\bin\Debug\net452"

%USERPROFILE%\.nuget\packages\ReportGenerator\2.5.6\tools\ReportGenerator.exe -reports:"coverage.xml" -targetdir:"report"

start report\index.htm
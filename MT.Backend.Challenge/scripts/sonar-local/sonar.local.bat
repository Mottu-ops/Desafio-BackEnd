cls
Echo Iniciando o sonar 
dotnet sonarscanner begin /k:"[trocar-por-sonar-project-name]" /d:sonar.host.url="http://localhost:9000" /d:sonar.language="cs" /d:sonar.exclusions="**/*Development.json,**/bin/**/*,**/obj/**/*"  /d:sonar.token="[trocar-por-sonar-token]" /d:sonar.cs.vscoveragexml.reportsPaths=coverage.xml

dotnet restore

dotnet build --no-incremental
dotnet-coverage collect "dotnet test" -f xml -o "coverage.xml"

dotnet sonarscanner end /d:sonar.token="[trocar-por-sonar-token]"
Echo Finalizando o sonar, aperte qq tecla para continuar

pause


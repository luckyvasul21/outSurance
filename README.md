Attached 2 projects:

OS: Windows

python: pytest, playwright
	TestFile: ..\OutsurancePython\tests\test_loginpage.py
Dotnet: xunit, playwright
	TestFile: ..\outSurance\playwright-xunit\PrimeService.Tests\login_test.cs



Python pre-requisites
	install python (used version: 3.10.0)
	check in cli (by using command <python --version>)
	in project Folder (OutsurancePython)
		CLI commands:
			python -m pip install --user virtualenv (Create virtual env to run tests)
			python -m venv env
			.\env\Scripts\activate
			where python
Python Steps:
1. Unzip OutsurancePython folder
2. cd OutsurancePython
3. pip install -r requirements.txt ( install all libraries required for project like playwright, pytest)
3. playwright install (will install browsers drivers)
4. python -m pytest tests --browser-channel chrome --headed --slowmo 1000 (CLI)
     <pytests tests> => will read the project for tests
     <--browser-channel chrome> specific browser to run tests
     <--headed> to run tests in head mode
     <--slowmo 2000> to execute actions with 2 seconds apart
	 
DotNet pre-requisites:
	intall dotnet (used .NETFramework,Version=v4.8)
	check in PowerShell (by using command <dotnet --version>)
DotNet Steps:
1. unzip outSurance folder
2. cd playwright-xunit (solution folder)
3. open sln in Visual Studio
4. Build Solution or  <dotnet build>
3. Step 3 will install packages required for project from project filr (...\outSurance\playwright-xunit\PrimeService.Tests\Outsource.Tests.csproj)
4. pwsh bin/Debug/netX/playwright.ps1 install (Install required browsers by replacing netX with the actual output folder name, e.g. net4.8 for my .net version)
5. <dotnet test> (CLI powershell from sln folder)
	this will look for all tests in sln, those are marked with Xunit tags like [Fact], [Theory]

	 
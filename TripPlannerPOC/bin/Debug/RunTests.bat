
ECHO Running tests via SpecRun .....

%~dp0\..\..\..\packages\SpecRun.Runner.1.6.2\tools\SpecRun.exe run Default.srprofile /basefolder:%~dp0. /outputfolder:%~dp0. /report:MyReport.html

START chrome.exe --new-window "file://%~dp0MyReport.html"
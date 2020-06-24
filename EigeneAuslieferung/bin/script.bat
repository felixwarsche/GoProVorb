@echo off

rem ----------------------------------------------------------------------------
rem     Abschlusspruefung Sommer2020 - Entwicklung eines Sofwaresystems
rem ----------------------------------------------------------------------------
rem 
rem     Name           : Felix Warschewski
rem     File           : skript.bat
rem  
rem --- Kurzbeschreibung -------------------------------------------------------
rem 
rem     Führt Programm mit allen Testdateien in Ordner Tests aus.

rem ----------------------------------------------------------------------------

echo Verarbeite Testbeispiele...
FOR %%I IN (Tests\*.txt) DO ZufallsklassenAnwendung.exe %%I
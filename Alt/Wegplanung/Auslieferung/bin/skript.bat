@echo off

rem ----------------------------------------------------------------------------
rem     Abschlusspruefung Sommer2012 - Entwicklung eines Sofwaresystems
rem ----------------------------------------------------------------------------
rem 
rem     Name           : Alexander Willkomm
rem     File           : skript.bat
rem  
rem --- Kurzbeschreibung -------------------------------------------------------
rem 
rem     Führt Programm mit allen Testdateien in Ordner Testfaelle aus.
rem 
rem --- Logbuch der Aenderungen -------------------------------------------------
rem  
rem     Datum            Taetigkeit
rem     ------------------------------------------------------------------------
rem     
rem     23.05.2012       (awi) erstellt
rem  
rem ----------------------------------------------------------------------------

echo Verarbeite Testbeispiele...
FOR %%I IN (Testfaelle\*.txt) DO Wegplanung.exe %%I
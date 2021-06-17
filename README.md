# WaitForService

In essence, this program starts a service (if it is not already), waits until it is in the running state, then starts a program.

My need for this came from a startup program I had which would launch before my PostgreSQL database was ready for connections.  

At first run, a dialog box allows selection of a service on the local computer, explorer lookup of a program, and startup visibility (normal, maximized, minimized, or hidden).  There are also options to run at user logon and save the configuration to the current user's registry hive.

Reconfiguration can be forced at any time by holding down the Shift key during start up.

The service selection has an option to hide Microsoft services from the dropdown list (like msconfig), but it is not 100% accurate.  Suggestions welcome.


# WaitForService

In essence, this program starts and/or waits until a service is in the running state, then starts a program.

My need for this came from a startup program I had which would launch before my PostgreSQL database was ready for connections.  

At first run, a dialog box allows selection of a service on the local computer, explorer lookup of a program, and startup visibility (maximized, minimized, etc.).  

There are also options to run at logon and save the configuration to the registry.

Reconfiguration can be forced by holding down the Shift key during program start up.

### Notes:

The service selection has an option to hide Microsoft services from the dropdown list (like msconfig), but it is not 100% accurate.  Suggestions welcome.



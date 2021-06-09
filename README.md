# WaitForService

In essence, this program starts a service (if it is not already), waits until it is in the running state, then starts a program.

My need for this came from a startup program I had which would launch before my PostgreSQL database was ready for connections.  

A configuration file in xml format specifies the service, program, and start type (normal, minimized, maximized or hidden).  If left unconfigured or partially configured, a dialog allows selection of a service on the local computer, explorer lookup of a program, and start type, with an option to save the configuration to the xml file.

The service selection has an option to hide Microsoft services from the dropdown list (like msconfig), but it is not 100% accurate.  Suggestions welcome.


# WaitForService

In essence, this program  waits until a service is in the running state, then starts a program.

My need for this came from a startup program I had which would launch before my PostgreSQL database was ready for connections.  

It can start a service if run as administrator but this does not work when configured to run at logon.

During install, a dialog box allows selection of a service on the local computer, explorer lookup of a program, and startup visibility (maximized, minimized, etc.).  Option to lock the workstation after successful program start.  Enhances security when auto logging on the workstation.

New function replaces run at logon function to allow choosing the user, instead of running for all users.

It can be reconfigured by running Configure.exe or by holding down the shift key while WaitForService is starting.

### Notes:

The service selection has an option to hide Microsoft services from the dropdown list (like msconfig), but it is not 100% accurate.  Suggestions welcome.



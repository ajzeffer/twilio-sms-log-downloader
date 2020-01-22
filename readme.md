# Twilio Log Exporter 

There isn't currently any paging we just try and get x records in one call. 

# Args 
You can pass in a start date by calling `dotnet run 2020-01-01` .. if nothing passed in will default to DateTime.Now.AddDays(-1)
- Just looks for first arg not named // kinda hacky 

# Mac users 
you may need to call the dotnet run command with sudo as we are writing to the filesystem and dotnet doesn't have access to write to the fs with out running in an elevated shell
# Project structure
> NB! Direcotories *Core*, *Presentation* and *Infrastructure* are only displayed in Solution Explorer (Visual Studio)

The project contains the following three main architectural layers:
- Core: Includes Application and Domain sub-layers
	- Application: Defines business logic and its types. *Notes.Application* refers to *Notes.Domain*
	- Domain: Defines enterprise logic and its types
	
	> The difference between enterprise logic and buisness logic is enterprise logic can be used in **different** application
	> whereas buisness logic can only be used in **this** application

- Presentation: Defines Web API. It's assumed that this layer also refers to the Core layer, although this is not explicitly stated in *.csproj* file
- Infrastructure: Defines everything that will be intended to communicate with the database. *Notes.Persistence* refers to *Notes.Application*